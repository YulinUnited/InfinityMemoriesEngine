namespace InfinityMemoriesEngine.OverWatch.qianhan.Enums
{
    /// <summary>
    /// 对象移除、释放、调度标记位。用于 LifecycleManager、RemovalSystem 等执行流程控制。
    /// Destroy 作为兼容用词出现，不推荐主动使用，仅为兼容外部引擎。
    /// </summary>
    [Flags]
    public enum RemovalFlags
    {
        None = 0,

        // 标准行为
        /// <summary>
        /// remove 标记位，表示对象被主动移除或标记为不再使用，引擎会每20000时刻查找标记为remove的方法或类等，将其清理掉且不再调度。
        /// </summary>
        remove = 1 << 0,                  // 主动移除
        /// <summary>
        /// 将被标记的目标对象标记为停用状态，生命周期中暂不使用，但仍保留在内存中，通常可以作为一个占位符或延迟处理的对象。
        /// </summary>
        Deactivate = 1 << 1,              // 标记为停用，生命周期中暂不使用
        /// <summary>
        /// 将被标记的目标对象标记为释放状态，通常在引用计数为零时触发，表示资源可以被回收或释放，但不建议在主逻辑中使用，因为这会造成引擎主动停止对该对象的调度。
        /// </summary>
        Release = 1 << 2,                 // 用于资源释放，引用计数为零时触发
        /// <summary>
        /// 将被标记的目标对象标记为退休状态，表示生命周期已终止但不立即释放，通常用于需要保留状态或数据的对象。
        /// </summary>
        Retire = 1 << 3,                  // 永久不再调用，视为生命周期结束但不立即释放
        /// <summary>
        /// Death同等于 Retire，表示该对象生命周期彻底终止，通常用于游戏对象或实体的死亡状态，为了让Retire更具语义化，Death作为Retire的别名。
        /// </summary>
        Death = Retire,                     // 表示该对象生命周期彻底终止，通常用于游戏对象或实体的死亡状态，为了让Retire更具语义化，Death作为Retire的别名
        /// <summary>
        /// 同名，仅作返回标记，表示该对象或方法仅用于返回值，不进行其他操作。通常用于函数调用或方法返回时的标记，通常引擎在调度时，会执行完逻辑后立即返回，不进行其他处理。
        /// </summary>
        Return = 1 << 12,                     // 仅返回，不进行任何其他操作

        // 调用行为
        /// <summary>
        /// 仅手动触发，外部不调度。通常用于需要手动控制调用时机的情况，如事件处理或特定逻辑触发。
        /// </summary>
        ManualCall = 1 << 4,              // 仅手动触发，外部不调度
        /// <summary>
        /// 通常用于需要跳过当前调用的情况，如事件取消或特定逻辑不再需要执行时。引擎会在调度时跳过该调用，不执行相关逻辑,请注意，如果是事件类，全局事件被修改为true，那么所有事件将永久不再触发。
        /// </summary>
        SkipCall = 1 << 5,                // 永久不再触发（如事件取消）
        /// <summary>
        /// 用于取消正在调度的调用，如 Coroutine 或 Timer 等。通常在需要中断当前调度流程时使用，引擎会在下一次调度时检查并取消相关调用，请注意，如果是事件类，全局事件被修改为true，那么所有事件将永久不再触发，所以全局事件的取消需要谨慎使用。
        /// </summary>
        CancelPending = 1 << 6,           // 取消正在调度的调用（如Coroutine/Timer）

        // 引擎兼容层 - 保留但不推荐
        /// <summary>
        /// 以彻底弃用，Destroy 仅用于兼容 Unity/Unreal 等外部引擎调用，不建议在主逻辑中使用，标记后引擎会再下一次调度时检查并处理这些被标记为 Destroy 的对象
        /// 并将其标记为暂时不使用状态，通常无法被调度或访问，但会被引擎在一定时刻时间内自动回收或处理。
        /// </summary>
        [Obsolete("Destroy 仅用于兼容 Unity/Unreal 等外部引擎调用，不建议在主逻辑中使用")]
        Destroy = 1 << 7,                 // 暂不使用对象（外部系统中表示“销毁”）
        /// <summary>
        /// 以彻底弃用，DestroyAll 仅用于兼容需求，不推荐使用，同Destroy。
        /// </summary>
        [Obsolete("DestroyAll 仅用于兼容需求，不推荐使用")]
        DestroyAll = 1 << 8,              // 全体销毁（兼容标记）

        // 控制行为（后处理判断）
        /// <summary>
        /// 用于保护对象，避免被清除系统自动回收。通常用于需要保留状态或数据的对象，但不希望其被自动回收，请注意，如果被标记为isRemove=true时，会被引擎直接回收而不是被保护。
        /// </summary>
        Protected = 1 << 9,               // 保护对象，避免被清除系统自动回收
        /// <summary>
        /// 当对象被锁定时，不能更改 RemovalFlags。通常用于需要确保对象状态不变的情况，如重要游戏对象或核心系统组件，但必须要注意的是，如果被标记为isRemove=true时，会被引擎直接回收而不是被锁定。
        /// </summary>
        Locked = 1 << 10,                 // 锁定状态，不能更改 RemovalFlags
        /// <summary>
        /// 将被标记的目标对象回收至对象池，不调用析构函数。通常用于需要复用对象的情况，如游戏对象或实体的复用，避免频繁创建和销毁带来的性能开销，千万不要将需要完整回收回对象池的对象标记为 isRemove=true，因为这会导致引擎直接从指针上回收对象而不是回收至对象池，也不建议与其他 RemovalFlags 组合使用。
        /// </summary>
        ReturnToPool = 1 << 11,           // 回收至对象池，不调用析构

        // 分组控制（组合标志，不用于单独赋值）
        /// <summary>
        /// 用于标记对象的生命周期已结束，通常在对象不再需要时使用。引擎会在下一次调度时检查并处理这些标记为 RuntimeRemoved 的对象。
        /// </summary>
        RuntimeRemoved = remove | Deactivate | Retire | Death,
        /// <summary>
        /// 用于标记对象的生命周期已结束，通常在对象不再需要时使用。引擎会在下一次调度时检查并处理这些标记为 GCRelated 的对象。
        /// </summary>
        GCRelated = remove | Release | Destroy,
        /// <summary>
        /// 用于标记对象不被清除系统自动回收，通常用于需要保留状态或数据的对象。引擎会在下一次调度时检查并处理这些标记为 Protected 的对象。
        /// </summary>
        Immutable = Protected | Locked,
        /// <summary>
        /// 将被标记的目标对象标记为所有 RemovalFlags 的组合，表示该对象在所有方面都被移除或释放。通常用于需要彻底清除对象的情况，但请注意，这会导致对象无法再被使用或访问。
        /// </summary>
        All = ~0
    }

    /// <summary>
    /// 绑定方法或对象生命周期控制的 Removal 标记属性
    /// </summary>
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
    public class RemovalAttribute : Attribute
    {
        public RemovalFlags Flags { get; }

        public RemovalAttribute(RemovalFlags flags)
        {
            Flags = flags;
        }
    }
}
