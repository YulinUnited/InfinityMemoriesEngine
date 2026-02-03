using InfinityMemoriesEngine.OverWatch.qianhan.Events.fml.common.eventhandler;

namespace InfinityMemoriesEngine.OverWatch.qianhan.annotations
{
    /// <summary>
    /// RetentionAttribute 用于指定注解的保留策略。
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    [Retention(RetentionPolicy.RUNTIME)]
    [Target(ElementType.ANNOTATION_TYPE)]
    [Documented]
    public class Retention : Attribute
    {
        public RetentionPolicy Policy { get; }
        public Retention(RetentionPolicy policy)
        {
            Policy = policy;
        }
    }
}
