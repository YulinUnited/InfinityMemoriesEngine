using InfinityMemoriesEngine.OverWatch.qianhan.Events;

namespace InfinityMemoriesEngine.OverWatch.qianhan.Objects.Modules.ModuleAccess.ModuleObject
{
    /// <summary>
    /// 事件处理器模块系统，用于管理事件处理器模块的添加、获取和移除
    /// </summary>
    public static class EventModuleSystem
    {
        private static readonly Dictionary<Type, Dictionary<Type, object>> EventHandlers = new();
        /// <summary>
        /// 添加事件处理器模块
        /// </summary>
        /// <typeparam name="TEvent"></typeparam>
        /// <typeparam name="THandler"></typeparam>
        /// <param name="module"></param>
        public static void AttachEventHandler<TEvent, THandler>(object module) where TEvent : Event
        {
            var eventType = typeof(TEvent);
            if (!EventHandlers.TryGetValue(eventType, out var modules))
            {
                modules = new Dictionary<Type, object>();
                EventHandlers[eventType] = modules;
            }
            modules[module.GetType()] = module;
        }
        /// <summary>
        /// 获取事件处理器模块
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="eventObj"></param>
        /// <returns></returns>
        public static T getEventModule<T>(Event eventObj) where T : class
        {
            var eventType = eventObj.GetType();
            if (EventHandlers.TryGetValue(eventType, out var modules) && modules.TryGetValue(typeof(T), out var module))
            {
                return (T)module;
            }
            return null;
        }
        /// <summary>
        /// 移除事件处理器
        /// </summary>
        /// <typeparam name="TEvent"></typeparam>
        /// <typeparam name="THandler"></typeparam>
        /// <param name="module"></param>
        public static void DetachEventHandler<TEvent, THandler>(object module) where TEvent : Event
        {
            var eventType = typeof(TEvent);
            if (EventHandlers.TryGetValue(eventType, out var modules))
            {
                modules.Remove(module.GetType());
                if (modules.Count == 0)
                {
                    EventHandlers.Remove(eventType);
                }
            }
        }
        /// <summary>
        /// 判断对象是否为事件类型
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsEvent(this object obj)
        {
            return obj is Event;
        }
        /// <summary>
        /// 清理所有事件处理器
        /// </summary>
        public static void ClearEventHandlers()
        {
            EventHandlers.Clear();
        }
    }
}
