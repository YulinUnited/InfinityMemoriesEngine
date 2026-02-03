using InfinityMemoriesEngine.OverWatch.qianhan.Events.fml.common.eventhandler;

namespace InfinityMemoriesEngine.OverWatch.qianhan.annotations
{
    /// <summary>
    /// 用于标记一个注解是文档化的。
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    [Retention(RetentionPolicy.RUNTIME)]
    [Target(ElementType.ANNOTATION_TYPE)]
    public class Documented : Attribute
    {
        public Documented() { }
    }
}
