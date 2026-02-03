using InfinityMemoriesEngine.OverWatch.qianhan.Events;
using InfinityMemoriesEngine.OverWatch.qianhan.Events.fml.common.eventhandler;

namespace InfinityMemoriesEngine.OverWatch.qianhan.priority
{
    public class EventPriority:IEventListener
    {
        public enum EventPrioritys
        {
            HIGHEST,
            HIGH,
            Normal,
            LOW,
            LOWEST
        }

        public void invoke(Event events)
        {
        }

        public void invoke(EventArgs var1)
        {
            throw new NotImplementedException();
        }
    }
}
