using InfinityMemoriesEngine.OverWatch.qianhan.annotations.meta;

namespace InfinityMemoriesEngine.OverWatch.qianhan.annotations
{
    /// <summary>
    /// Nonnull 注解用于标记一个类、方法、字段或参数为非空。
    /// </summary>
    [Documented]
    [TypeQualifier]
    [Retention(RetentionPolicy.RUNTIME)]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public class NonnullAttribute : Attribute
    {
        public When when;

        public When WhenValue { get; }
        /// <summary>
        /// NonnullAttribute 构造函数，默认情况下标记为 ALWAYS。
        /// </summary>
        /// <param name="when"></param>
        public NonnullAttribute(When when = When.ALWAYS)
        {
            WhenValue = when;
        }
        /// <summary>
        /// 当值为 null 时，返回 When.NEVER，否则返回 When.ALWAYS。
        /// </summary>
        public class Checker : ITypeQualifierValidator<NonnullAttribute>
        {
            public When ForConstantValue(NonnullAttribute qualifierArgument, object value)
            {
                return value == null ? When.NEVER : When.ALWAYS;
            }
        }
    }

    // 定义接口，类似于 Java 中的 TypeQualifierValidator<T>
    /// <summary>
    /// ITypeQualifierValidator<T> 接口用于验证类型限定符。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ITypeQualifierValidator<T>
    {
        When ForConstantValue(T qualifierArgument, object value);
    }
}
