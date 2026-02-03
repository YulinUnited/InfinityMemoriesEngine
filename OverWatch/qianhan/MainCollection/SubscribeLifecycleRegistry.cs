namespace InfinityMemoriesEngine.OverWatch.qianhan.MainCollection
{
    /// <summary>
    /// 订阅生命周期注册表，请不要使用Subscribe
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class SubscribeLifecycleRegistry : Attribute
    {
        public LifetcycleRegisterys Registery;
        public SubscribeLifecycleRegistry(LifetcycleRegisterys registery)
        {
            Registery = registery;
        }
    }
    /// <summary>
    /// 订阅生命周期枚举类
    /// </summary>
    [Flags]
    public enum LifetcycleRegisterys
    {
        /// <summary>
        /// 订阅Awake
        /// </summary>
        SubscribeAwake = 1 << 0,
        /// <summary>
        /// 订阅Start
        /// </summary>
        SubscribeStart = 1 << 1,
        /// <summary>
        /// 订阅Update
        /// </summary>
        SubscribeUpdate = 1 << 2,
        /// <summary>
        /// 订阅LateUpdate
        /// </summary>
        SubscribeLateUpdate = 1 << 3,
        /// <summary>
        /// 订阅FixedUpdate
        /// </summary>
        SubscribeFixedUpdate = 1 << 4,
        /// <summary>
        /// 移除订阅
        /// </summary>
        removeSubscribe = 1 << 5,
        /// <summary>
        /// 移除订阅Awake
        /// </summary>
        removeSubscribeAwake = 1 << 6,
        /// <summary>
        /// 移除订阅Start
        /// </summary>
        removeSubscribeStart = 1 << 7,
        /// <summary>
        /// 移除订阅Update
        /// </summary>
        removeSubscribeUpdate = 1 << 8,
        /// <summary>
        /// 移除订阅LateUpdate
        /// </summary>
        removeSubscribeLateUpdate = 1 << 9,
        /// <summary>
        /// 移除订阅FixedUpdate
        /// </summary>
        removeSubscribeFixedUpdate = 1 << 10,
    }
}
