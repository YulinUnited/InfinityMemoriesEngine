using System.Reflection;
using InfinityMemoriesEngine.OverWatch.qianhan.Start;

namespace InfinityMemoriesEngine.OverWatch.qianhan.MainCollection
{
    public static class CoustomComponentExtensions
    {
        public static ChinesePhase ChinesePhase;

        [ComponentAll]
        public static T? GetCustomComponentAttribute<T>(this MethodInfo method) where T : Attribute
        {
            var attrs = method.GetCustomAttributes(typeof(T), true);
            return attrs.FirstOrDefault() as T;
        }
        public static bool HasCustomComponentAttribute<T>(this MethodInfo method) where T : Attribute
        {
            return method.GetCustomComponentAttribute<T>() != null;
        }
        public static T? GetCustomComponentAttribute<T>(this Type type) where T : Attribute
        {
            var attrs = type.GetCustomAttributes(typeof(T), true);
            return attrs.FirstOrDefault() as T;
        }

        public static bool HasCustomComponentAttribute<T>(this Type type) where T : Attribute
        {
            return type.GetCustomComponentAttribute<T>() != null;
        }
    }
    [AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = true)]
    public class ComponentAll : Attribute
    {
        
    }
}
