using InfinityMemoriesEngine.OverWatch.qianhan.Events.fml.common.eventhandler;

namespace InfinityMemoriesEngine.OverWatch.qianhan.annotations.meta
{
    /// <summary>
    /// TypeQualifier注解用于标识一个类型或方法适用于特定的类型。
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    [Documented]
    [Target(ElementType.ANNOTATION_TYPE)]
    [Retention(RetentionPolicy.RUNTIME)]
    public class TypeQualifier : Attribute
    {
        public Type ApplicableTo { get; }
        public TypeQualifier(Type applicableTo = null)
        {
            ApplicableTo = applicableTo ?? typeof(object);
        }
    }
}
