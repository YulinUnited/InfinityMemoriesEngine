namespace InfinityMemoriesEngine.OverWatch.qianhan.Start
{
    /// <summary>
    /// 枚举定义执行优先级，事件优先级最高
    /// </summary>
    public enum ChinesePhase
    {
        //事件优先级最高
        /// <summary>
        /// 事件
        /// </summary>
        Events = 1,//1<<0
        //用户的优先级同等于Events
        /// <summary>
        /// 用户
        /// </summary>
        User = Events,//同Events
        //初期初始化，优先级仅次于Events
        /// <summary>
        /// 唤醒
        /// </summary>
        Awake = 2,//1<<1
        //初始加载，优先级处于高阶层
        /// <summary>
        /// 开始
        /// </summary>
        Start = 4,//1<<2
        //更新，优先级处于较高阶层
        /// <summary>
        /// 循环更新
        /// </summary>
        Update = 8,//1<<3
        //更新后，优先级处于较低阶层
        /// <summary>
        /// 等待更新/稍后更新
        /// </summary>
        LateUpdate = 16,//1<<4
        //物理更新，优先级处于较低阶层
        /// <summary>
        /// 固定更新
        /// </summary>
        FixedUpdate = 32,//1<<5
        //当渲染启动时，位于高阶层，此为渲染层
        /// <summary>
        /// 当渲染开始时
        /// </summary>
        onRender = 64,//1<<6
        //当UI启动时，位于高阶层，此为UI层
        /// <summary>
        /// 当UI开始时
        /// </summary>
        onGUI = 128,//1<<7
        //当物理启动时，位于高阶层，此为物理层
        /// <summary>
        /// 当物理开始时
        /// </summary>
        onPhysics = 256,//1<<8
        //当网络启动时，位于高阶层，此为网络层
        /// <summary>
        /// 当网络开始时
        /// </summary>
        onNetWorkSync = 512,//1<<9
        //当AI被启动时，位于高阶层，此为AI层
        /// <summary>
        /// 当AI时刻开始时
        /// </summary>
        AiTick = 1024,//1<<10
        //Tick时刻，位于高阶层，此为Tick层
        /// <summary>
        /// 时刻开始
        /// </summary>
        Tick = 2048,//1<<11
        //当Tick时刻被启动时，位于中等，此为Tick层
        /// <summary>
        /// 最小时刻开始
        /// </summary>
        MinTick = 4096,//1<<12
        //当Tick时刻被启动时，位于低等，此为Tick层
        /// <summary>
        /// 当前时刻开始
        /// </summary>
        CurrentTick = 8192,//1<<13
        //当Awake启动时，此可以被用于Awake的回调
        /// <summary>
        /// 当唤醒开始时
        /// </summary>
        onAwake = 16384,//1<<14
        //当Start启动时，此可以被用于Start的回调
        /// <summary>
        /// 当开始时
        /// </summary>
        onStart = 32768,//1<<15
        //当Update启动时，此可以被用于Update的回调
        /// <summary>
        /// 当更新开始时
        /// </summary>
        onUpdate = 65536,//1<<16
        //当LateUpdate启动时，此可以被用于LateUpdate的回调
        /// <summary>
        /// 当稍后更新/延迟更新开始时
        /// </summary>
        onLateUpdate = 131072,//1<<17
        //当FixedUpdate启动时，此可以被用于FixedUpdate的回调
        /// <summary>
        /// 当固定更新开始时
        /// </summary>
        onFixedUpdate = 262144,//1<<18
        //当Awake结束时，此可以被用于Awake的回调
        /// <summary>
        /// 当唤醒结束时
        /// </summary>
        unAwake = 524288,//1<<19
        //当Start结束时，此可以被用于Start的回调
        /// <summary>
        /// 当开始结束时
        /// </summary>
        unStart = 1048576,//1<<20
        //当Update结束时，此可以被用于Update的回调
        /// <summary>
        /// 当更新结束时
        /// </summary>
        unUpdate = 2097152,//1<<21
        //当LateUpdate结束时，此可以被用于LateUpdate的回调
        /// <summary>
        /// 当稍后更新/延迟更新结束时
        /// </summary>
        unLateUpdate = 4194304,//1<<22
        //当FixedUpdate结束时，此可以被用于FixedUpdate的回调
        /// <summary>
        /// 当固定更新结束时
        /// </summary>
        unFixedUpdate = 8388608,//1<<23
        //当Awake被移除时，可以用于生命周期的回溯或者回调
        /// <summary>
        /// 当唤醒被移除时
        /// </summary>
        removeAwake = 16777216,//1<<24
        //当Start被移除时，可以用于生命周期的回溯或者回调
        /// <summary>
        /// 当开始被移除时
        /// </summary>
        removeStart = 33554432,//1<<25
        //当Update被移除时，可以用于生命周期的回溯或者回调
        /// <summary>
        /// 当更新被移除时
        /// </summary>
        removeUpdate = 67108864,//1<<26
        //当LateUpdate被移除时，可以用于生命周期的回溯或者回调
        /// <summary>
        /// 当稍后更新/延迟更新被移除时
        /// </summary>
        removeLateUpdate = 134217728,//1<<27
        //当FixedUpdate被移除时，可以用于生命周期的回溯或者回调
        /// <summary>
        /// 当固定更新被移除时
        /// </summary>
        removeFixedUpdate = 268435456,//1<<28
        //回调所有
        /// <summary>
        /// 回调所有，不分先后顺序，执行所有生命周期，已被禁用
        /// </summary>
        All = ~0//-1:所有为均为1
    }
    /// <summary>
    /// 用于调度生命周期的元数据结构，通过属性和枚举组合使用，可以显式调度生命周期
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class ChineseAttribute : Attribute
    {
        /// <summary>
        /// 生命周期枚举
        /// </summary>
        public ChinesePhase Phase;
        /// <summary>
        /// 生命周期属性
        /// </summary>
        /// <param name="phase"></param>
        public ChineseAttribute(ChinesePhase phase) => this.Phase = phase;
        internal ChineseAttribute() { }
    }
}
