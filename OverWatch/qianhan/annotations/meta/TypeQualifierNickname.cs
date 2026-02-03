using InfinityMemoriesEngine.OverWatch.qianhan.Events.fml.common.eventhandler;

namespace InfinityMemoriesEngine.OverWatch.qianhan.annotations.meta
{
    /// <summary>
    /// 用于标识一个类型限定符的别名。
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    [Documented]
    [Target(ElementType.ANNOTATION_TYPE)]
    public class TypeQualifierNickname : Attribute
    {
    }
}
