using InfinityMemoriesEngine.OverWatch.qianhan.annotations.meta;

namespace InfinityMemoriesEngine.OverWatch.qianhan.annotations
{
    /// <summary>
    /// 用于标记静态成员的访问方式。
    /// </summary>
    [AttributeUsage(AttributeTargets.All)]
    [VisibleToOtherModules]
    public class StaticAccessorAttribute : Attribute, BindingsAttribute
    {
        public string Name { get; set; }

        public StaticAccessorType Type { get; set; }
        /// <summary>
        /// 默认构造函数。
        /// </summary>
        public StaticAccessorAttribute()
        {
        }
        /// <summary>
        /// 构造函数，指定静态成员的名称。
        /// </summary>
        /// <param name="name"></param>
        [VisibleToOtherModules]
        internal StaticAccessorAttribute(string name)
        {
            Name = name;
        }
        /// <summary>
        /// 构造函数，指定静态成员的访问类型。
        /// </summary>
        /// <param name="type"></param>
        public StaticAccessorAttribute(StaticAccessorType type)
        {
            Type = type;
        }
        /// <summary>
        /// 构造函数，指定静态成员的名称和访问类型。
        /// </summary>
        /// <param name="name"></param>
        /// <param name="type"></param>
        public StaticAccessorAttribute(string name, StaticAccessorType type)
        {
            Name = name;
            Type = type;
        }
    }
}
