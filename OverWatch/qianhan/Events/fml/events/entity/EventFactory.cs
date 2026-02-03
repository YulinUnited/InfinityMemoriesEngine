using InfinityMemoriesEngine.OverWatch.qianhan.Entities;
using InfinityMemoriesEngine.OverWatch.qianhan.Events.fml.common;
using InfinityMemoriesEngine.OverWatch.qianhan.Events.fml.events.entity.living;

namespace InfinityMemoriesEngine.OverWatch.qianhan.Events.fml.events.entity
{
    public static class EventFactory
    {
        public static float onLivingHeal(EntityLivingBase livingBase, float amount)
        {
            LivingBaseHealEvent @event = new LivingBaseHealEvent(livingBase, amount);
            return ForgeIronteted.EVENT_BUS.post(@event) ? 0 : @event.getHealAmount();
        }
    }
}
