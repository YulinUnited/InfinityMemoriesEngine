using InfinityMemoriesEngine.OverWatch.qianhan.annotations.meta;

namespace InfinityMemoriesEngine.OverWatch.qianhan.annotations
{
    /// <summary>
    /// 仿Java的注解，表示该元素可以为null。
    /// </summary>
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
    [TypeQualifierNickname]
    [Retention(RetentionPolicy.RUNTIME)]
    [Documented]
    [Nonnull(when = When.UNKNOWN)]

    public class Nullable : Attribute
    {
    }
}
