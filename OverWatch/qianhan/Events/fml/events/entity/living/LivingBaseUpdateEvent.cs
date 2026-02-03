using InfinityMemoriesEngine.OverWatch.qianhan.Entities;
using InfinityMemoriesEngine.OverWatch.qianhan.Events.fml.common.eventhandler;

namespace InfinityMemoriesEngine.OverWatch.qianhan.Events.fml.events.entity.living
{
    [Cancelable]
    public class LivingBaseUpdateEvent : EntityEvent
    {
        public LivingBaseUpdateEvent(Entity entity) : base(entity)
        {
        }
    }
}
