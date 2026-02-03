namespace InfinityMemoriesEngine.OverWatch.qianhan.Events.fml.common.eventhandler
{
    public enum AnnotationEvent
    {
        Note = 0,//无事发生
        None = 1 << 1,//慢事件，默认为1
        Internal = 1 << 2,//内部保留事件，默认为2不向外派发
        HighPriority = 1 << 3,//最高优先级事件，默认为3
        High = 1 << 4,//高优先级事件，默认为4
        LowPriority = 1 << 5,//低优先级事件，默认为5
        OnceOnly = 1 << 6,//仅执行一次事件，默认为6
        SystemCritical = 1 << 7,//系统关键事件不支持被Cancelable修饰，不包括Remove这个全局事件，默认为7
        DebugOnly = 1 << 8,//仅用于调试事件，默认为8
        Stop = 1 << 9,//停止事件，默认为9
        Cancel = 1 << 10,//取消事件，默认为10
        Unsafe = 1 << 11,//不安全事件，默认为11
        Safe = 1 << 12,//安全事件，默认为12
        Global = 1 << 13,//全局事件，默认为13
        GlobalMark = 1 << 14,//全局标记事件，默认为14
        GlobalCancel = 1 << 15,//全局取消事件，默认为15
        GlobalRemove = 1 << 16,//全局移除事件，默认为16
        GlobalSet = 1 << 17,//全局设置事件，默认为17
        GlobalSetMark = 1 << 18,//全局设置标记事件，默认为18
        GlobalSetCancel = 1 << 19,//全局设置取消事件，默认为19
        GlobalSetRemove = 1 << 20,//全局设置移除事件，默认为20


        UI = 1 << 21,//UI事件，默认为25
        UISet = 1 << 22,//UI设置事件，默认为26
        UISetMark = 1 << 23,//UI设置标记事件，默认为27
        UISetCancel = 1 << 24,//UI设置取消事件，默认为28
        UISetRemove = 1 << 25,//UI设置移除事件，默认为29
        UIStop = 1 << 26//UI停止事件，默认为38
    }
}
