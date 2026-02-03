namespace InfinityMemoriesEngine.OverWatch.qianhan.Enums
{
    /// <summary>
    /// 兼容 Unity 的 MonoBehaviour 生命周期事件的属性类。
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class MonoBehaviourAttribute : Attribute
    {
        public MonoBehaviourPhase Phase { get; private set; }
        public MonoBehaviourAttribute(MonoBehaviourPhase phase)
        {
            Phase = phase;
        }
    }
    public enum MonoBehaviourPhase
    {
        Enable = 1, //1<<0
        Disable = 2, //1<<1
        OnApplicationQuit = 4, //1<<2
        OnDestroy = 8, //1<<3
        OnApplicationPause = 16, //1<<4
        OnApplicationFocus = 32, //1<<5
        OnBecameVisible = 64, //1<<6
        OnBecameInvisible = 128,
        OnCollisionEnter = 256, //1<<7
        OnCollisionExit = 512, //1<<8
        OnCollisionStay = 1024, //1<<9
        OnEnable = 2048, //1<<10
        OnDisable = 4096,
    }
}
