using static InfinityMemoriesEngine.OverWatch.qianhan.priority.EventPriority;

namespace InfinityMemoriesEngine.OverWatch.qianhan.Events.fml.common.eventhandler
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class EventListenerAttribute : Attribute
    {
        public Type EventType { get; }
        public EventPrioritys Priority { get; }
        public bool ReceiveCancelled { get; }

        public EventListenerAttribute(Type eventType,
                                      EventPrioritys priority = EventPrioritys.Normal,
                                      bool receiveCancelled = false)
        {
            EventType = eventType;
            Priority = priority;
            ReceiveCancelled = receiveCancelled;
        }
    }
}
