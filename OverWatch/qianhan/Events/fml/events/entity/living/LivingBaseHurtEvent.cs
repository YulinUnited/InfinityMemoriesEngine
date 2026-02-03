using InfinityMemoriesEngine.OverWatch.qianhan.Entities;
using InfinityMemoriesEngine.OverWatch.qianhan.Events.fml.common.eventhandler;
using InfinityMemoriesEngine.OverWatch.qianhan.Util;

namespace InfinityMemoriesEngine.OverWatch.qianhan.Events.fml.events.entity.living
{
    //这是一个生物体受伤事件
    [Cancelable]
    public class LivingBaseHurtEvent : LivingEvent
    {
        private DamageSource damageSource;
        private float damageAmount;
        public LivingBaseHurtEvent(EntityLivingBase entityLiving, DamageSource source, float amount) : base(entityLiving)
        {
            this.damageSource = source;
            this.damageAmount = amount;
        }
        public DamageSource getSource()
        {
            return damageSource;
        }
        public float getAmount()
        {
            return damageAmount;
        }
        public void setAmount(float amount)
        {
            this.damageAmount = amount;
        }
    }
}
