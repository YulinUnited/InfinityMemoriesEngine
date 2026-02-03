using System.Reflection;

namespace InfinityMemoriesEngine.OverWatch.qianhan.Events.fml.common.eventhandler
{
    /// <summary>
    /// 注解类，仿java的注解
    /// 为什么不搞点事呢，这多有乐子啊？
    /// 咳咳，为什么要搞出这玩意？因为...因为我想要一个注解的功能，这多欢乐啊？
    /// </summary>
    public static class Annotation
    {
        static Event @event { get; set; } = new Event();
        public static bool IsCancelable() => @event.isCanceledEvent();
        public static bool IsGlobalMark() => @event.isGlobalMarEvent();

        private static readonly Dictionary<MethodInfo, bool> cancelableCache = new();

        public static bool isCancelable(MethodInfo method)
        {
            return method.DeclaringType?.GetCustomAttributes(typeof(Cancelable), true).Length > 0
               || method.GetCustomAttributes(typeof(Cancelable), true).Length > 0;
        }
        public static bool isGlobalMark(MethodInfo method)
        {
            return method.DeclaringType?.GetCustomAttributes(typeof(Remove), true).Length > 0
               || method.GetCustomAttributes(typeof(Remove), true).Length > 0;
        }
        public static bool IsInheritedAttribute<T>(this MethodInfo method) where T : Attribute
        {
            return method.DeclaringType?.IsDefined(typeof(T), true) == true ||
                   method.IsDefined(typeof(T), true);
        }


        public static bool IsCancelable(MethodInfo method)
        {
            if (!cancelableCache.TryGetValue(method, out var cached))
            {
                cached = HasAttribute<Cancelable>(method);
                cancelableCache[method] = cached;
            }
            return cached;
        }
        public static bool HasAttribute<T>(MethodInfo method) where T : Attribute
        {
            return method.DeclaringType?.IsDefined(typeof(T), true) == true ||
                   method.IsDefined(typeof(T), true);
        }
    }
}
