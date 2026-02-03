using InfinityMemoriesEngine.OverWatch.qianhan.Entities;

namespace InfinityMemoriesEngine.OverWatch.qianhan.Events.fml.events.entity.living
{
    /// <summary>
    /// 实体攻击目标设置事件
    /// </summary>
    public class LivingSetAttackTargetEvent : LivingEvent
    {
        private EntityLivingBase target;
        public LivingSetAttackTargetEvent(EntityLivingBase entityLiving, EntityLivingBase target) : base(entityLiving)
        {
            this.target = target;
        }
        public EntityLivingBase getTarget() { return target; }
    }
}
