using InfinityMemoriesEngine.OverWatch.qianhan.MainCollection;
using InfinityMemoriesEngine.OverWatch.qianhan.Start;
using OverWatch.qianhan.Util;

namespace InfinityMemoriesEngine.OverWatch.qianhan.InfinityMemoriesEngine.MainManager
{
    /// <summary>
    /// 统合生命周期的统一调度
    /// </summary>
    internal static class MainLifecycleManager
    {
        /// <summary>
        /// 加载生命周期方法调用器
        /// </summary>
        public static void Init()
        {
            var context = new DefaultLifecycleContextProvider();
            LifecycleInvoker.SetContextProvider(context);
        }
        /// <summary>
        /// 将生命周期方法调用器与引擎的生命周期阶段关联
        /// </summary>
        /// <param name="engine">目标</param>
        /// <param name="phase">生命周期</param>
        public static void InvokeAll(object engine, ChinesePhase phase)
        {
            LifecycleInvoker.InvokeByPhase(engine, phase);
        }
        /// <summary>
        /// 可不断调度的反射方案
        /// </summary>
        public static void Invoke()
        {
            isAiTick(); isCurrentTick(); isEvents(); isFixedUpdate();
            isOnFixedUpdate(); isOnUpdate(); isOnLateUpdate(); isOnRender(); isOnGUI();
            isOnPhysics(); isOnNetWorkSync(); isUpdate(); isLateUpdate(); isTick();
            isMinTick();
        }
        /// <summary>
        /// 只反射一次的，不建议每时刻调度
        /// </summary>
        public static void TheOneInvoke()
        {
            isAwake(); isOnAwake(); isUnAwake(); isStart(); isOnStart(); isUnStart();
            isUnFixedUpdate(); isUnLateUpdate(); isUnUpdate(); isRemoveAll();
            isRemoveAwake(); isRemoveStart(); isRemoveFixedUpdate(); isRemoveLateUpdate(); isRemoveUpdate();
        }
        private static void isAwake()
        {
            LifecycleDispatcher.Awake();
        }
        private static void isOnAwake()
        {
            LifecycleDispatcher.onAwake();
        }
        private static void isUnAwake()
        {
            LifecycleDispatcher.unAwake();
        }
        private static void isStart()
        {
            LifecycleDispatcher.Start();
        }
        private static void isOnStart()
        {
            LifecycleDispatcher.onStart();
        }
        private static void isUnStart()
        {
            LifecycleDispatcher.unStart();
        }
        private static void isUpdate()
        {
            LifecycleDispatcher.Update();
        }
        private static void isOnUpdate()
        {
            LifecycleDispatcher.onUpdate();
        }
        private static void isUnUpdate()
        {
            LifecycleDispatcher.unUpdate();
        }
        private static void isLateUpdate()
        {
            LifecycleDispatcher.LateUpdate();
        }
        private static void isOnLateUpdate()
        {
            LifecycleDispatcher.onLateUpdate();
        }
        private static void isUnLateUpdate()
        {
            LifecycleDispatcher.unLateUpdate();
        }
        private static void isFixedUpdate()
        {
            LifecycleDispatcher.FixedUpdate();
        }
        private static void isOnFixedUpdate()
        {
            LifecycleDispatcher.onFixedUpdate();
        }
        private static void isUnFixedUpdate()
        {
            LifecycleDispatcher.unFixedUpdate();
        }
        private static void isRemoveAwake()
        {
            LifecycleDispatcher.removeAwake();
        }
        private static void isRemoveStart()
        {
            LifecycleDispatcher.removeStart();
        }
        private static void isRemoveUpdate()
        {
            LifecycleDispatcher.removeUpdate();
        }
        private static void isRemoveLateUpdate()
        {
            LifecycleDispatcher.removeLateUpdate();
        }
        private static void isRemoveFixedUpdate()
        {
            LifecycleDispatcher.removeFixedUpdate();
        }
        private static void isOnRender()
        {
            LifecycleDispatcher.onRender();
        }
        private static void isOnGUI()
        {
            LifecycleDispatcher.onGUI();
        }
        private static void isOnPhysics()
        {
            LifecycleDispatcher.onPhysics();
        }
        private static void isOnNetWorkSync()
        {
            LifecycleDispatcher.onNetWorkSync();
        }
        private static void isAiTick()
        {
            LifecycleDispatcher.AiTick();
        }
        private static void isTick()
        {
            LifecycleDispatcher.Tick();
        }
        private static void isMinTick()
        {
            LifecycleDispatcher.MinTick();
        }
        private static void isCurrentTick()
        {
            LifecycleDispatcher.CurrentTick();
        }
        private static void isRemoveAll()
        {
            LifecycleDispatcher.removeAll();
        }
        private static void isEvents()
        {
            LifecycleDispatcher.Events();
        }
    }
}
