using System.Reflection;
using InfinityMemoriesEngine.OverWatch.qianhan.annotations;
using InfinityMemoriesEngine.OverWatch.qianhan.Events.fml.common.eventhandler;
using InfinityMemoriesEngine.OverWatch.qianhan.Log.lang;

namespace InfinityMemoriesEngine.OverWatch.qianhan.Events.fml
{
    /// <summary>
    /// 事件总线用于储存或者发布事件
    /// </summary>
    public class EventBus : Event, IEventExceptionHandler
    {
        private bool shutdown;
        private Dictionary<int, IEventListener[]> listener = new Dictionary<int, IEventListener[]>();
        private static int maxID = 0;
        private int busID = maxID++;

        private IEventExceptionHandler eventExceptionHandler;
        private static readonly object locks = new object();
        private static readonly Dictionary<Type, List<Delegate>> eventListeners = new Dictionary<Type, List<Delegate>>();
        //private static Dictionary<Type, List<Action<object>>> eventListeners = new Dictionary<Type, List<Action<object>>>();
        public void DebugAnnotationMap()
        {
            if (eventListeners.Count == 0)
            {
                Debug.Log("没有注册任何事件");
            }
            else
            {
                foreach (var eventListener in eventListeners)
                {
                    Debug.Log($"事件类型: {eventListener.Key.Name}, 监听器数量: {eventListener.Value.Count}");
                }
            }
        }

        public static void Subscribe<T>(Action<T> listener) where T : class
        {
            Type eventType = typeof(T);
            lock (locks)
            {
                if (!eventListeners.ContainsKey(eventType))
                {
                    eventListeners[eventType] = new List<Delegate>();
                }
                eventListeners[eventType].Add(listener);
            }
        }

        public static void unSubscribe<T>(Action<T> listener) where T : class
        {
            Type eventType = typeof(T);
            lock (locks)
            {
                if (eventListeners.ContainsKey(eventType))
                {
                    eventListeners[eventType].Remove(listener);
                }
            }
        }

        public static void Publish<T>(T eventInstance) where T : class
        {
            Type type = typeof(T);
            lock (locks)
            {
                if (eventListeners.TryGetValue(type, out var listeners))
                {
                    foreach (var listener in listeners.ToArray())
                    {
                        (listener as Action<T>)?.Invoke(eventInstance);
                    }
                }
            }
        }
        public bool post(Event eventInstance)
        {
            if (shutdown) return false;

            if (!listener.TryGetValue(busID, out IEventListener[]? eventListeners))
            {
                return false;
            }

            int index = 0;

            try
            {
                for (; index < eventListeners.Length; index++)
                {
                    eventListeners[index].invoke(eventInstance);
                }
            }
            catch (Exception ex)
            {
                eventExceptionHandler.handleException(this, eventInstance, eventListeners, index, ex);
                throw;
            }

            return eventInstance.isCancelable() && eventInstance.onCanceled() && eventInstance.isGlobalMarks() && eventInstance.onGlobalMarks();
        }


        //自动注册订阅事件
        public static void AutoSubscribe(object target)
        {
            var methods = target.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            foreach (var method in methods)
            {
                if (method.GetCustomAttribute<SubscribeAttribute>() != null) // 检查有没有 `[Subscribe]`
                {
                    var parameters = method.GetParameters();
                    if (parameters.Length == 1) // 确保方法只有一个参数
                    {
                        Type eventType = parameters[0].ParameterType;
                        var actionType = typeof(Action<>).MakeGenericType(eventType);
                        var actionDelegate = Delegate.CreateDelegate(actionType, target, method);

                        typeof(EventBus).GetMethod("Subscribe")?
                            .MakeGenericMethod(eventType)
                            .Invoke(null, new object[] { actionDelegate });
                        //这个不是Unity的提醒，而是由我仿照Unity的源码创建的，纯属是好玩才这么做的
                        Debug.Log($"自动订阅事件: {eventType.Name} -> {method.Name}");
                    }
                }
            }
        }

        public void handleException(EventBus bus, Event @event, IEventListener[] listeners, int index, Throwable throwable)
        {
            for (int e = 0; e < listeners.Length; e++)
            {
                var listener = listeners[e];
                try
                {
                    listener.invoke(@event);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("EventBus", "Exception caught during firing event: " + @event + " to listener " + listener, ex);
                }
            }
        }

        public void handleException(EventBus eventBus, Event eventInstance, IEventListener[] eventListeners, int index, Exception ex)
        {
            for (int e = 0; e < eventListeners.Length; e++)
            {
                var listener = eventListeners[e];
                try
                {
                    listener.invoke(eventInstance);
                }
                catch (Exception ex1)
                {
                    System.Diagnostics.Debug.WriteLine("EventBus", "Exception caught during firing event: " + eventInstance + " to listener " + listener, ex1);
                }
            }
        }

        public void Register<T>(Action<T> listener)
        {
            if (listener == null)
                throw new ArgumentNullException(nameof(listener));

            var type = typeof(T);

            lock (locks)
            {
                if (!eventListeners.TryGetValue(type, out var listeners))
                {
                    listeners = new List<Delegate>();
                    eventListeners[type] = listeners;
                }

                // 避免重复添加
                if (!listeners.Contains(listener))
                {
                    listeners.Add(listener);
                }
            }
            if (listener == null)
            {
                throw new ArgumentNullException(nameof(listener));
            }
            lock (locks)
            {
                if (!eventListeners.TryGetValue(type, out var listeners))
                {
                    listeners = new List<Delegate>();
                    eventListeners[type] = listeners;
                }

                // 避免重复添加
                if (!listeners.Contains(listener))
                {
                    listeners.Add(listener);
                }
            }
        }

        public void Dispatch<T>(T evt)
        {
            if (evt == null)
                throw new ArgumentNullException(nameof(evt));

            var type = typeof(T);

            List<Delegate> listenersCopy;

            lock (locks)
            {
                if (!eventListeners.TryGetValue(type, out var listeners) || listeners.Count == 0)
                    return;

                listenersCopy = new List<Delegate>(listeners); // 拷贝避免迭代时修改
            }

            foreach (var listener in listenersCopy)
            {
                try
                {
                    ((Action<T>)listener)?.Invoke(evt);
                }
                catch (Exception ex)
                {
                    eventExceptionHandler?.handleException(ex); // 可自定义处理逻辑
                }
            }
        }
        internal override bool isCancelable() => true;
        /// <summary>
        /// 当事件被取消时调用
        /// </summary>
        /// <exception cref="InvalidOperationException">如果类没有被修饰为可取消则抛出异常</exception>
        public void Cancel()
        {
            var attr = GetType().GetCustomAttribute<Cancelable>();
            if (attr == null)
            {
                throw new InvalidOperationException("当前类没有被声明为可以取消，请检查是否使用了[Cancelable]声明为可以被取消.");
            }
            isCancelable();
        }
        public void handleException(Exception ex)
        {
            eventExceptionHandler.handleException(ex);
        }

        /*/

        private readonly EventDelegateSystem delegateSystem = new EventDelegateSystem();
        private readonly EventFMLSystem fmlSystem = new EventFMLSystem();
        private readonly EventAutoScanSystem scanSystem = new EventAutoScanSystem();

        private bool shutdown;
        private IEventExceptionHandler handler;

        internal override bool isCancelable() => true;

        // --------- 对外统一 API（入口层） ----------

        public void Subscribe<T>(Action<T> listener) where T : class =>
            delegateSystem.Subscribe(listener);

        public void unSubscribe<T>(Action<T> listener) where T : class =>
            delegateSystem.unSubscribe(listener);

        public void Publish<T>(T evt) where T : class =>
            delegateSystem.Publish(evt);

        public bool post(Event evt) =>
            fmlSystem.post(evt);

        public void AutoSubscribe(object obj) =>
            scanSystem.AutoSubscribe(obj, delegateSystem);

        // 统一异常处理入口
        public void handleException(Exception ex) =>
            handler?.handleException(ex);

        public void handleException(EventBus bus, Event @event, IEventListener[] listeners, int index, Throwable throwable)
        {

            try
            {
                System.Diagnostics.Debug.WriteLine($"[EventBus] Throwable caught during event dispatch: {@event?.GetType().Name} -> {throwable}");
            }
            catch { }

            for (int i = index + 1; i < listeners.Length; i++)
            {
                try
                {
                    listeners[i].invoke(@event);
                }
                catch (Exception ex)
                {
                    try
                    {
                        System.Diagnostics.Debug.WriteLine($"[EventBus] Exception while invoking listener[{i}] for {@event?.GetType().Name}: {ex}");
                    }
                    catch { }
                }
            }
        }

        public void handleException(EventBus eventBus, Event eventInstance, IEventListener[] eventListeners, int index, Exception ex)
        {

            try
            {
                System.Diagnostics.Debug.WriteLine($"[EventBus] Exception caught dispatching {eventInstance?.GetType().Name}: {ex}");
            }
            catch { }

            for (int i = index + 1; i < eventListeners.Length; i++)
            {
                try
                {
                    eventListeners[i].invoke(eventInstance);
                }
                catch (Exception innerEx)
                {
                    try
                    {
                        System.Diagnostics.Debug.WriteLine($"[EventBus] Exception while invoking listener[{i}] for {eventInstance?.GetType().Name}: {innerEx}");
                    }
                    catch { }
                }
            }
            
        }

        /// <summary>
        /// Publish/Subscribe 委托体系（独立模块）
        /// </summary>
        public class EventDelegateSystem
    {
        private readonly object locks = new object();
        private readonly Dictionary<Type, List<Delegate>> listeners =
            new Dictionary<Type, List<Delegate>>();

        public void Subscribe<T>(Action<T> listener) where T : class
        {
            lock (locks)
            {
                var t = typeof(T);
                if (!listeners.ContainsKey(t))
                    listeners[t] = new List<Delegate>();

                listeners[t].Add(listener);
            }
        }

        public void unSubscribe<T>(Action<T> listener) where T : class
        {
            lock (locks)
            {
                var t = typeof(T);
                if (listeners.ContainsKey(t))
                    listeners[t].Remove(listener);
            }
        }

        public void Publish<T>(T evt) where T : class
        {
            List<Delegate>? copy;

            lock (locks)
            {
                var t = typeof(T);
                if (!listeners.TryGetValue(t, out var list))
                    return;

                copy = new List<Delegate>(list);
            }

            foreach (var d in copy)
                (d as Action<T>)?.Invoke(evt);
        }
    }

    /// <summary>
    /// Forge/FML 风格 IEventListener 调度体系
    /// </summary>
    public class EventFMLSystem
    {
        private static int maxID = 0;
        private readonly int busID = maxID++;

        private readonly Dictionary<int, IEventListener[]> map =
            new Dictionary<int, IEventListener[]>();

        public bool post(Event evt)
        {
            if (!map.TryGetValue(busID, out var arr))
                return false;

            int index = 0;

            try
            {
                for (; index < arr.Length; index++)
                    arr[index].invoke(evt);
            }
            catch
            {
                throw;
            }

            return evt.isCancelable() && evt.onCanceled() &&
                   evt.isGlobalMarks() && evt.onGlobalMarks();
        }
    }

    /// <summary>
    /// 扫描注解并自动注册到 DelegateSystem 中
    /// </summary>
    public class EventAutoScanSystem
    {
        public void AutoSubscribe(object target, EventDelegateSystem delegateSystem)
        {
            var methods = target.GetType()
                .GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            foreach (var m in methods)
            {
                if (m.GetCustomAttribute<SubscribeAttribute>() == null)
                    continue;

                var ps = m.GetParameters();
                if (ps.Length != 1) continue;

                var evtType = ps[0].ParameterType;

                var delType = typeof(Action<>).MakeGenericType(evtType);
                var del = Delegate.CreateDelegate(delType, target, m);

                typeof(EventDelegateSystem)
                    .GetMethod(nameof(EventDelegateSystem.Subscribe))
                    .MakeGenericMethod(evtType)
                    .Invoke(delegateSystem, new object[] { del });

                Debug.Log($"自动订阅事件: {evtType.Name} -> {m.Name}");
            }
        }
        */
    }
}