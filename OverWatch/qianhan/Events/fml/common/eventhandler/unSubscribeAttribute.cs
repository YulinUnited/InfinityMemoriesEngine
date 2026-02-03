namespace InfinityMemoriesEngine.OverWatch.qianhan.Events.fml.common.eventhandler
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Module | AttributeTargets.Method, AllowMultiple = false)]
    public class unSubscribeAttribute : Attribute
    {
        public string Tag { get; }
        public unSubscribeAttribute(string tag = null)
        {
            Tag = tag;
        }
    }
}
