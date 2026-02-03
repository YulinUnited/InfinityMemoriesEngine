using InfinityMemoriesEngine.OverWatch.qianhan.Entities;
using InfinityMemoriesEngine.OverWatch.qianhan.Events.fml.common.eventhandler;
using InfinityMemoriesEngine.OverWatch.qianhan.PotionEffects;

namespace InfinityMemoriesEngine.OverWatch.qianhan.Events.fml.events.entity.living
{
    [Cancelable]
    public class LivingUpdatePotionEffectEvent : LivingEvent
    {
        private PotionEffect effect;
        public LivingUpdatePotionEffectEvent(EntityLivingBase entity, PotionEffect effect) : base(entity)
        {
            this.effect = effect;
        }
        public PotionEffect getEffect()
        {
            return effect;
        }
    }
}
