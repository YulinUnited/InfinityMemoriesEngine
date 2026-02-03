namespace InfinityMemoriesEngine.OverWatch.qianhan.annotations.meta
{
    /// <summary>
    /// 用于标记静态访问器的类型。
    /// </summary>
    [VisibleToOtherModules]
    public enum StaticAccessorType
    {
        /// <summary>
        /// 点
        /// </summary>
        Dot,
        /// <summary>
        /// 箭头
        /// </summary>
        Arrow,
        /// <summary>
        /// 双冒号
        /// </summary>
        DoubleColon,
        /// <summary>
        /// 带默认返回值的箭头访问器
        /// </summary>
        ArrowWithDefaultReturnIfNull
    }
}
