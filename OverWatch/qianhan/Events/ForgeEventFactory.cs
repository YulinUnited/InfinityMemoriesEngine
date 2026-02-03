using InfinityMemoriesEngine.OverWatch.qianhan.Entities;
using InfinityMemoriesEngine.OverWatch.qianhan.Events.fml.common;
using InfinityMemoriesEngine.OverWatch.qianhan.Events.fml.events.entity;

namespace InfinityMemoriesEngine.OverWatch.qianhan.Events
{
    public class ForgeEventFactory
    {
        public static bool canEntityUpdate(Entity entityIn)
        {
            CanUpdate @event = new CanUpdate(entityIn);
            ForgeIronteted.EVENT_BUS.post(@event);
            return @event.getCanUpdate();
        }
    }
}
