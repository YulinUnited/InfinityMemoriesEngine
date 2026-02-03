namespace InfinityMemoriesEngine.OverWatch.qianhan.Metas
{
    /// <summary>
    /// 元编译特性，用于标记需要在编译时进行特殊处理的方法。
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public sealed class MetaCompileAttirbute : Attribute
    {
        /// <summary>关键词集合（匹配命令时按任一关键词触发）</summary>
        public string[] KeyWords { get; }

        /// <summary>优先级（数值越大优先匹配）</summary>
        public int Priority { get; }
        /// <summary>
        /// MetaCompileAttirbute函数
        /// </summary>
        /// <param name="keywords">匹配关键字</param>

        public MetaCompileAttirbute(params string[] keywords)
            : this(0, keywords)
        {
        }
        /// <summary>
        /// MetaCompileAttirbute函数
        /// </summary>
        /// <param name="priority">数组</param>
        /// <param name="keywords">关键字</param>
        public MetaCompileAttirbute(int priority, params string[] keywords)
        {
            Priority = priority;
            KeyWords = keywords ?? Array.Empty<string>();
        }

        internal MetaCompileAttirbute()
        {
            Priority = 0;
            KeyWords = Array.Empty<string>();
        }
    }
}
