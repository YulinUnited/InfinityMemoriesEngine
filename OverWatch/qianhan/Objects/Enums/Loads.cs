namespace InfinityMemoriesEngine.OverWatch.qianhan.Objects.Enums
{
    /// <summary>
    /// 要么打破一切，要么回归于无，强载入，让类、方法、属性、字段、事件等在程序运行也可以加载，你需要直接传入强制性的MainObject里面的MainTick
    /// 只有这样才能插入到主循环中，才能被加载，避免掉可能的已知和未知的错误；
    /// <exception>在预估中，可能抛出：空间栈异常等
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false)]

    public class Loads : Attribute
    {
        public OverLoad overLoad;
        public float Tick => (float)MainObject.MainTick;
        public Loads(OverLoad Load, int tick)
        {
            //MainObject.MainTick = tick;
            this.overLoad = Load;
        }
    }
    /// <summary>
    /// OverLoad枚举，用于标记类、方法、属性、字段、事件等的加载状态
    /// </summary>
    [Flags]
    public enum OverLoad
    {
        None = 0,
        /// <summary>
        /// 类加载
        /// </summary>
        Class = 1,
        /// <summary>
        /// 方法加载
        /// </summary>
        MetHod = 1 << 1,
        /// <summary>
        /// 属性加载
        /// </summary>
        Property = 1 << 2,
        /// <summary>
        /// 字段加载
        /// </summary>
        Field = 1 << 3,
        /// <summary>
        /// 事件加载
        /// </summary>
        Event = 1 << 4,
        /// <summary>
        /// 更新包加载，通常用于更新包的加载状态
        /// </summary>
        UpdatePackage = 1 << 5,
        /// <summary>
        /// 全部加载，包含类、方法、属性、字段、事件和更新包
        /// </summary>
        All = Class | MetHod | Property | Field | Event | UpdatePackage
    }
}
