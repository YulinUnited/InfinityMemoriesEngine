using System.Reflection;
using InfinityMemoriesEngine.OverWatch.qianhan.Log.lang;
using InfinityMemoriesEngine.OverWatch.qianhan.Numbers;

namespace InfinityMemoriesEngine.OverWatch.qianhan.Events.fml.common.eventhandler
{
    public static class SubscriptionManager
    {
        private static readonly Dictionary<string, List<Delegate>> Subscribers = new Dictionary<string, List<Delegate>>();
        public static void Initialize(params Assembly[] assemblies)
        {
            var targets = assemblies.SelectMany(a => a.GetTypes());
            foreach (var t in targets)
            {
                var isUnsubscribe = t.GetCustomAttribute<unSubscribeAttribute>() != null;
                var isSubscribe = t.GetCustomAttribute<SubscribeAttribute>() != null;
                if (isSubscribe && isUnsubscribe)
                    throw new Exception($"警告： {t.FullName}只能存在一个订阅或取消订阅标记，请处理！");

                if (isSubscribe) RegisterClass(t);
                if (isUnsubscribe) UnregisterClass(t);

                foreach (var method in t.GetMethods(BindingFlags.Public | BindingFlags.NonPublic |
                                                       BindingFlags.Instance | BindingFlags.Static))
                {
                    var subAttr = method.GetCustomAttribute<SubscribeAttribute>();
                    var unsubAttr = method.GetCustomAttribute<unSubscribeAttribute>();

                    if (subAttr != null && unsubAttr != null)
                    {
                        Debug.LogWarning($"方法 {method.Name} 和 {t.FullName} 同时声明了订阅和取消订阅标记,引擎将调用{setUnSubscribeOrSubscribe}自动更正");
                        setUnSubscribeOrSubscribe();
                    }

                    if (subAttr != null) RegisterMethod(method, subAttr.Tag);
                    if (unsubAttr != null) UnregisterMethod(method, unsubAttr.Tag);
                }
            }
        }
        private static void RegisterClass(Type type)
        {
            var tag = type.GetCustomAttribute<SubscribeAttribute>()?.Tag ?? type.FullName;
            var method = type.GetMethod("Invoke", BindingFlags.Static | BindingFlags.Public);
            if (method != null)
                Register(tag, method, null);
        }
        /// <summary>
        /// 暂做占位符
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="method"></param>
        /// <param name="value"></param>
        /// <exception cref="NotImplementedException"></exception>
        private static void Register(string? tag, MethodInfo method, object value)
        {
            if (!Subscribers.TryGetValue(tag, out var list))
                list = Subscribers[tag] = new List<Delegate>();

            var del = Delegate.CreateDelegate(typeof(Action), value, method);
            list.Add(del);
        }

        private static void UnregisterClass(Type type)
        {
            var tag = type.GetCustomAttribute<unSubscribeAttribute>()?.Tag ?? type.FullName;
            var method = type.GetMethod("Invoke", BindingFlags.Static | BindingFlags.Public);
            if (method != null)
                Unregister(tag, method, null);
        }
        /// <summary>
        /// 暂时作为占位符
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="method"></param>
        /// <param name="value"></param>
        /// <exception cref="NotImplementedException"></exception>
        private static void Unregister(string? tag, MethodInfo method, object value)
        {
            if (!Subscribers.TryGetValue(tag, out var list))
                list = Subscribers[tag] = new List<Delegate>();

            var del = Delegate.CreateDelegate(typeof(Action), value, method);
            list.Clear();
        }
        /// <summary>
        /// 设置取消订阅还是订阅
        /// </summary>
        /// <param name="assemblies"></param>
        public static void setUnSubscribeOrSubscribe(params Assembly[] assemblies)
        {
            //初期例便合集
            foreach (var assembly in assemblies)
            {
                //例便整个合集确保是否符合条件
                foreach (var type in assembly.GetTypes())
                {
                    var hasSubscribe = type.GetCustomAttribute<SubscribeAttribute>() != null;
                    var hasUnsubscribe = type.GetCustomAttribute<unSubscribeAttribute>() != null;
                    //如果订阅和取消订阅不同时存在则返回，什么都不做
                    if (!hasSubscribe || !hasUnsubscribe) return;
                    // 冲突：类级别
                    if (hasSubscribe && hasUnsubscribe)
                    {
                        Debug.LogWarning($"类 {type.FullName} 同时存在 [Subscribe] 与 [unSubscribe]，已自动忽略取消订阅标记。");
                        float rand = Super.Clamps(new Random().NextSingle() * 0, 1);
                        Super.Random one = (Super.Random)rand;
                        if (one == Super.Random.one)
                        {
                            UnregisterClass(type);
                        }
                        if (one == Super.Random.value)
                        {
                            RegisterClass(type);
                        }
                        continue;
                    }
                    //例便订阅的方法确保不同时存在取消订阅和订阅
                    foreach (var method in type.GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
                    {
                        var mSubscribe = method.GetCustomAttribute<SubscribeAttribute>() != null;
                        var mUnsubscribe = method.GetCustomAttribute<unSubscribeAttribute>() != null;

                        if (mSubscribe && mUnsubscribe)
                        {
                            Debug.LogWarning($"方法 {method.Name} 在类 {type.FullName} 同时存在 [Subscribe] 与 [unSubscribe]，将保留订阅。");

                            UnregisterMethod(method, method.Name); // 先移除
                            RegisterMethod(method, method.Name);   // 再注册
                        }
                    }
                }
            }
        }


        private static void RegisterMethod(MethodInfo method, string tag)
        {
            tag ??= method.Name;
            object target = null;

            if (!method.IsStatic)
                target = Activator.CreateInstance(method.DeclaringType);

            var del = Delegate.CreateDelegate(typeof(Action), target, method);
            if (!Subscribers.TryGetValue(tag, out var list))
                Subscribers[tag] = list = new();
            list.Add(del);
        }

        private static void UnregisterMethod(MethodInfo method, string tag)
        {
            tag ??= method.Name;
            if (!Subscribers.TryGetValue(tag, out var list))
                return;

            list.RemoveAll(d => d.Method == method);
        }

        public static void Invoke(string tag)
        {
            if (Subscribers.TryGetValue(tag, out var list))
            {
                foreach (var del in list)
                {
                    try { del.DynamicInvoke(); }
                    catch (Exception e) { Console.WriteLine($"Error invoking: {e}"); }
                }
            }
        }
    }
}
