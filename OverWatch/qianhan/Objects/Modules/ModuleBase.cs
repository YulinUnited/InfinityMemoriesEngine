namespace InfinityMemoriesEngine.OverWatch.qianhan.Objects.Modules
{
    public abstract class ModuleBase
    {
        /// <summary>
        /// 目标对象，可谓Entity/SystemHost甚至全局单例
        /// </summary>
        public object? Host { internal set; get; }
        /// <summary>
        /// 模块名称，可用于调试或日志
        /// </summary>
        public virtual string ModuleName => GetType().Name;
        /// <summary>
        /// 启用？还是不启用？默认启用
        /// </summary>
        public virtual bool Enabled { set; get; } = true;
        /// <summary>
        /// 当模块被挂在时触发
        /// </summary>
        public virtual void OnAttach() { }
        /// <summary>
        /// 当模块被移除时触发逻辑
        /// </summary>
        public virtual void OnDetach() { }
        /// <summary>
        /// 每时刻/帧调度，手动决定时机
        /// </summary>
        public virtual void OnUpdate() { }
    }
    /// <summary>
    /// 灵活接口，接口式模块
    /// </summary>
    public interface IModuleController
    {
        void Attach();      // OnAttach
        void Detach();      // OnDetach
        void Tick();        // OnUpdate
        void Disable();     // 替代 removeModules
    }
}
