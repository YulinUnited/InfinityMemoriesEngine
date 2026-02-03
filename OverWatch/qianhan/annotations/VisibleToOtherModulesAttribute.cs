namespace InfinityMemoriesEngine.OverWatch.qianhan.annotations
{
    /// <summary>
    /// 标记此类或成员对其他模块可见的属性。
    /// </summary>
    [AttributeUsage(AttributeTargets.All, Inherited = false)]
    [VisibleToOtherModules]
    public class VisibleToOtherModulesAttribute : Attribute
    {
        public VisibleToOtherModulesAttribute() { }
        public VisibleToOtherModulesAttribute(params string[] modules) { }
    }
}
