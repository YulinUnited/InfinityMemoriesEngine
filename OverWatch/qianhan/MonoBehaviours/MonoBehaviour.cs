using InfinityMemoriesEngine.OverWatch.qianhan.Enums;
using InfinityMemoriesEngine.OverWatch.qianhan.Objects.Behaviours;
using InfinityMemoriesEngine.OverWatch.qianhan.Start;

namespace InfinityMemoriesEngine.OverWatch.qianhan.MonoBehaviours
{
    /// <summary>
    /// 本类用于项目迁移兼容，**不是 Unity 的 MonoBehaviour**，也不符合 Unity 的设计规范与运行机制。
    /// </summary>
    /// <remarks>
    /// <para>
    /// 本类继承自 <see cref="Behaviour"/>，仅用于提供与 Unity 相似的继承结构，便于已有项目的平滑迁移。
    /// </para>
    /// <para>
    /// 所有生命周期方法（如 <c>Start</c>、<c>Update</c> 等）**不会自动调用**，
    /// 必须通过自定义的 <see cref="ChineseAttribute"/> 结合 <see cref="ChinesePhase"/> 枚举进行声明与注册。
    /// 编译器将支持枚举自动补全，方便开发者手动标注需要调度的方法。
    /// </para>
    /// <para>
    /// 对于需要保留 Unity 原始调用风格的迁移代码，可启用“字段反射调度器”机制，
    /// 以在运行时通过反射查找并执行带有生命周期名称的方法（例如 <c>Update()</c>）。
    /// 该机制需由外部调度器主动调用并传入当前实例，不在默认行为范围内。
    /// </para>
    /// </remarks>

    public class MonoBehaviour : Behaviour
    {
#if false
        /// <summary>
        /// 错误的调度方式示例：
        /// </summary>
        public override void Update()
        {
            // 这种方式会导致死循环，因为 MonoBehaviour 的 Update 方法会被反射调度器调用,但在这里又调用了 onUpdate()
            onUpdate();
        }
        public override void onUpdate()
        {
            // 死循环，因为 onUpdate() 又会调用 unUpdate()
            unUpdate();
        }
        public override void unUpdate()
        {
            // 死循环，因为 unUpdate() 又会调用 removeUpdate()
            removeUpdate();
        }
        public override removeUpdate()
        {
            // 死循环，因为 removeUpdate() 又会调用 onUpdate()
            onAwake();
        }
        ///<summary>
        ///上述是错误的示例代码，实际使用中请避免这种方式，无论是调度自身也好，还是调度其他方法也好，都应该避免这种死循环的情况，最好的解决办法使用限制避免这种需要死循环的情况。
        ///</summary>
#endif
        public MonoBehaviour(int intpar) : base(intpar)
        {
            instanceID = intpar;
        }
        /// <summary>
        /// 初始化方法，类似于 Unity 的 <c>Start()</c> 方法。
        /// </summary>
        [Chinese(ChinesePhase.Start)]
        public virtual void Start()
        {
            // 迁移项目时可重写此方法以实现初始化逻辑
        }
        /// <summary>
        /// 唤醒方法，类似于 Unity 的 <c>Awake()</c> 方法。
        /// </summary>
        [Chinese(ChinesePhase.Awake)]
        public virtual void Awake()
        {
            // 迁移项目时可重写此方法以实现唤醒逻辑
        }
        /// <summary>
        /// 当 MonoBehaviour 被启用时调用，类似于 Unity 的 <c>OnEnable()</c> 方法。
        /// </summary>
        [MonoBehaviour(MonoBehaviourPhase.OnEnable)]
        public virtual void OnEnable()
        {
            // 迁移项目时可重写此方法以实现启用逻辑
        }
        /// <summary>
        /// 当 MonoBehaviour 被禁用时调用，类似于 Unity 的 <c>OnDisable()</c> 方法。
        /// </summary>
        [MonoBehaviour(MonoBehaviourPhase.OnDisable)]
        public virtual void OnDisable()
        {
            // 迁移项目时可重写此方法以实现禁用逻辑
        }
        /// <summary>
        /// 销毁方法，类似于 Unity 的 <c>OnDestroy()</c> 方法。
        /// </summary>
        [MonoBehaviour(MonoBehaviourPhase.OnDestroy)]
        public virtual void OnDestroy()
        {
            // 迁移项目时可重写此方法以实现销毁逻辑
        }
        /// <summary>
        /// 每时刻调用，区别于Unity的Update，在这里是以Tick也就是时刻为单位进行调度，类似于 Unity 的 <c>Update()</c> 方法。
        /// </summary>
        [Chinese(ChinesePhase.Update)]
        public virtual void Update()
        {
            // 迁移项目时可重写此方法以实现更新逻辑
        }
        /// <summary>
        /// 固定时刻调用，类似于 Unity 的 <c>FixedUpdate()</c> 方法。
        /// </summary>
        [Chinese(ChinesePhase.FixedUpdate)]
        public virtual void FixedUpdate()
        {
            // 迁移项目时可重写此方法以实现固定更新逻辑
        }
        /// <summary>
        /// 在Update调度后调用，类似于 Unity 的 <c>LateUpdate()</c> 方法。
        /// </summary>
        [Chinese(ChinesePhase.LateUpdate)]
        public virtual void LateUpdate()
        {
            // 迁移项目时可重写此方法以实现延迟更新逻辑
        }
        /// <summary>
        /// 当UI渲染时调用，类似于 Unity 的 <c>OnGUI()</c> 方法。
        /// </summary>
        [Chinese(ChinesePhase.onGUI)]
        public virtual void OnGUI()
        {
            // 迁移项目时可重写此方法以实现 GUI 绘制逻辑
        }
        /// <summary>
        /// 当 对象 被销毁时调用，类似于 Unity 的 <c>Destroy()</c> 方法。
        /// </summary>
        [Removal(RemovalFlags.remove)]
        public virtual void Destroy()
        {
            // 迁移项目时可重写此方法以实现销毁逻辑
            // 注意：此方法不会自动调用，需要手动触发或通过外部调度器调用
        }
        /// <summary>
        /// 当应用程序退出时调用，类似于 Unity 的 <c>OnApplicationQuit()</c> 方法。
        /// </summary>
        [MonoBehaviour(MonoBehaviourPhase.OnApplicationQuit)]
        public virtual void OnApplicationQuit()
        {
            // 迁移项目时可重写此方法以实现应用退出逻辑
        }
        /// <summary>
        /// 当应用程序暂停时调用，类似于 Unity 的 <c>OnApplicationPause()</c> 方法。
        /// </summary>
        /// <param name="pauseStatus"></param>
        [MonoBehaviour(MonoBehaviourPhase.OnApplicationPause)]
        public virtual void OnApplicationPause(bool pauseStatus)
        {
            // 迁移项目时可重写此方法以实现应用暂停逻辑
        }
        /// <summary>
        /// 当应用程序聚焦状态改变时调用，类似于 Unity 的 <c>OnApplicationFocus()</c> 方法。
        /// </summary>
        /// <param name="focusStatus"></param>
        [MonoBehaviour(MonoBehaviourPhase.OnApplicationFocus)]
        public virtual void OnApplicationFocus(bool focusStatus)
        {
            // 迁移项目时可重写此方法以实现应用聚焦逻辑
        }
        /// <summary>
        /// 当对象变为可见时调用，类似于 Unity 的 <c>OnBecameVisible()</c> 方法。
        /// </summary>
        [MonoBehaviour(MonoBehaviourPhase.OnBecameVisible)]
        public virtual void OnBecameVisible()
        {
            // 迁移项目时可重写此方法以实现对象变为可见逻辑
        }
        /// <summary>
        /// 当对象变为不可见时调用，类似于 Unity 的 <c>OnBecameInvisible()</c> 方法。
        /// </summary>
        [MonoBehaviour(MonoBehaviourPhase.OnBecameInvisible)]
        public virtual void OnBecameInvisible()
        {
            // 迁移项目时可重写此方法以实现对象变为不可见逻辑
        }
        /// <summary>
        /// 当发生碰撞时调用，类似于 Unity 的 <c>OnCollisionEnter()</c> 方法。
        /// </summary>
        /// <param name="collision"></param>
        [MonoBehaviour(MonoBehaviourPhase.OnCollisionEnter)]
        public virtual void OnCollisionEnter(Collision collision)
        {
            // 迁移项目时可重写此方法以实现碰撞进入逻辑
        }
        /// <summary>
        /// 当碰撞退出时调用，类似于 Unity 的 <c>OnCollisionExit()</c> 方法。
        /// </summary>
        /// <param name="collision"></param>
        [MonoBehaviour(MonoBehaviourPhase.OnCollisionExit)]
        public virtual void OnCollisionExit(Collision collision)
        {
            // 迁移项目时可重写此方法以实现碰撞退出逻辑
        }
        /// <summary>
        /// 当碰撞持续时调用，类似于 Unity 的 <c>OnCollisionStay()</c> 方法。
        /// </summary>
        /// <param name="collision"></param>
        [MonoBehaviour(MonoBehaviourPhase.OnCollisionStay)]
        public virtual void OnCollisionStay(Collision collision)
        {
            // 迁移项目时可重写此方法以实现碰撞持续逻辑
        }
        /// <summary>
        /// 当Awake被调用时调用，此方法属于本引擎的原有生命周期调度，警告：请不要在里面使用类似于Awake->onAwake() 的方法调用方式，会引起死循环。
        /// </summary>
        [Chinese(ChinesePhase.onAwake)]
        public virtual void onAwake()
        {
            //迁移项目时可重写此方法以实现当唤醒时逻辑
        }
        /// <summary>
        /// 当Start被调用时调用，此方法属于本引擎的原有生命周期调度，警告：请不要在里面使用类似于Start->onStart() 的方法调用方式，会引起死循环。
        /// </summary>
        [Chinese(ChinesePhase.onStart)]
        public virtual void onStart()
        {
            //迁移项目时可重写此方法以实现当启动时逻辑
        }
        /// <summary>
        /// 当Update被调用时调用，此方法属于本引擎的原有生命周期调度，警告：请不要在里面使用类似于Update->onUpdate() 的方法调用方式，会引起死循环。
        /// </summary>
        [Chinese(ChinesePhase.onUpdate)]
        public virtual void onUpdate()
        {
            // 迁移项目时可重写此方法以实现当更新时逻辑
        }
        /// <summary>
        /// 当LateUpdate被调用时调用，此方法属于本引擎的原有生命周期调度，警告：请不要在里面使用类似于LateUpdate->onLateUpdate() 的方法调用方式，会引起死循环。
        /// </summary>
        [Chinese(ChinesePhase.onLateUpdate)]
        public virtual void onLateUpdate()
        {
            // 迁移项目时可重写此方法以实现当延迟更新时逻辑
        }
        /// <summary>
        /// 当FixedUpdate被调用时调用，此方法属于本引擎的原有生命周期调度，警告：请不要在里面使用类似于FixedUpdate->onFixedUpdate() 的方法调用方式，会引起死循环。
        /// </summary>
        [Chinese(ChinesePhase.onFixedUpdate)]
        public virtual void onFixedUpdate()
        {
            // 迁移项目时可重写此方法以实现当固定更新时逻辑
        }
        /// <summary>
        /// 当结束唤醒时调用，此方法属于本引擎的原有生命周期调度，警告：请不要在里面使用类似于unAwake->Awake->onAwake->unAwake/removeAwake->unAwake 的方法调用方式，会引起死循环。
        /// </summary>
        [Chinese(ChinesePhase.unAwake)]
        public virtual void unAwake()
        {
            // 迁移项目时可重写此方法以实现唤醒结束后逻辑
        }
        /// <summary>
        /// 当结束启动时调用，此方法属于本引擎的原有生命周期调度，警告：请不要在里面使用类似于unStart->Start->onStart->unStart/removeStart->unStart 的方法调用方式，会引起死循环。
        /// </summary>
        [Chinese(ChinesePhase.unStart)]
        public virtual void unStart()
        {
            // 迁移项目时可重写此方法以实现启动结束后逻辑
        }
        /// <summary>
        /// 当结束更新时调用，此方法属于本引擎的原有生命周期调度，警告：请不要在里面使用类似于unUpdate->Update->onUpdate->unUpdate/removeUpdate->unUpdate 的方法调用方式，会引起死循环。
        /// </summary>
        [Chinese(ChinesePhase.unUpdate)]
        public virtual void unUpdate()
        {
            // 迁移项目时可重写此方法以实现更新结束后逻辑
        }
        /// <summary>
        /// 当结束延迟更新时调用，此方法属于本引擎的原有生命周期调度，警告：请不要在里面使用类似于unLateUpdate->LateUpdate->onLateUpdate->unLateUpdate/removeLateUpdate->unLateUpdate 的方法调用方式，会引起死循环。
        /// </summary>
        [Chinese(ChinesePhase.unLateUpdate)]
        public virtual void unLateUpdate()
        {
            // 迁移项目时可重写此方法以实现延迟更新结束后逻辑
        }
        /// <summary>
        /// 当结束固定更新时调用，此方法属于本引擎的原有生命周期调度，警告：请不要在里面使用类似于unFixedUpdate->FixedUpdate->onFixedUpdate->unFixedUpdate/removeFixedUpdate->unFixedUpdate 的方法调用方式，会引起死循环。
        /// </summary>
        [Chinese(ChinesePhase.unFixedUpdate)]
        public virtual void unFixedUpdate()
        {
            // 迁移项目时可重写此方法以实现固定更新结束后逻辑
        }
        /// <summary>
        /// 当移除唤醒时调用，此方法属于本引擎的原有生命周期调度，警告：请不要在里面使用类似于removeAwake->Awake->onAwake->unAwake/removeAwake->unAwake 的方法调用方式，会引起死循环。
        /// </summary>
        [Chinese(ChinesePhase.removeAwake)]
        public virtual void removeAwake()
        {
            // 迁移项目时可重写此方法以实现唤醒被移除后的逻辑
        }
        /// <summary>
        /// 当移除启动时调用，此方法属于本引擎的原有生命周期调度，警告：请不要在里面使用类似于removeStart->Start->onStart->unStart/removeStart->unStart 的方法调用方式，会引起死循环。
        /// </summary>
        [Chinese(ChinesePhase.removeStart)]
        public virtual void removeStart()
        {
            // 迁移项目时可重写此方法以实现启动被移除后的逻辑
        }
        /// <summary>
        /// 当移除更新时调用，此方法属于本引擎的原有生命周期调度，警告：请不要在里面使用类似于removeUpdate->Update->onUpdate->unUpdate/removeUpdate->unUpdate 的方法调用方式，会引起死循环。
        /// </summary>
        [Chinese(ChinesePhase.removeUpdate)]
        public virtual void removeUpdate()
        {
            // 迁移项目时可重写此方法以实现更新被移除后的逻辑
        }
        /// <summary>
        /// 当移除延迟更新时调用，此方法属于本引擎的原有生命周期调度，警告：请不要在里面使用类似于removeLateUpdate->LateUpdate->onLateUpdate->unLateUpdate/removeLateUpdate->unLateUpdate 的方法调用方式，会引起死循环。
        /// </summary>
        [Chinese(ChinesePhase.removeLateUpdate)]
        public virtual void removeLateUpdate()
        {
            // 迁移项目时可重写此方法以实现延迟更新被移除后的逻辑
        }
        /// <summary>
        /// 当移除固定更新时调用，此方法属于本引擎的原有生命周期调度，警告：请不要在里面使用类似于removeFixedUpdate->FixedUpdate->onFixedUpdate->unFixedUpdate/removeFixedUpdate->unFixedUpdate 的方法调用方式，会引起死循环。
        /// </summary>
        [Chinese(ChinesePhase.removeFixedUpdate)]
        public virtual void removeFixedUpdate()
        {
            // 迁移项目时可重写此方法以实现固定更新被移除后的逻辑
        }
    }
}
