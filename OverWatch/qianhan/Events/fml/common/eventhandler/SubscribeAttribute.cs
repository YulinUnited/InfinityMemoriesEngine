namespace InfinityMemoriesEngine.OverWatch.qianhan.Events.fml.common.eventhandler
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Module | AttributeTargets.Method, AllowMultiple = false)]
    public class SubscribeAttribute : Attribute
    {
        public string Tag { get; }
        public string Phase { get; }
        public bool IsSubscribe { get; set; }
        /// <summary>
        /// 默认支持不写tab
        /// </summary>
        /// <param name="Tab"></param>
        public SubscribeAttribute(string Tab = null)
        {
            Tag = Tab;
        }
        /// <summary>
        /// 需要传入phase和isSubscrbe
        /// </summary>
        /// <param name="phase"></param>
        /// <param name="isSubscribe"></param>
        public SubscribeAttribute(string phase, bool isSubscribe)
        {
            this.Phase = phase;
            IsSubscribe = isSubscribe;
        }
    }
}
