using InfinityMemoriesEngine.OverWatch.qianhan.Entities;
using InfinityMemoriesEngine.OverWatch.qianhan.Events.fml.common.eventhandler;
using InfinityMemoriesEngine.OverWatch.qianhan.Util;

namespace InfinityMemoriesEngine.OverWatch.qianhan.Events.fml.events.entity.living
{
    /// <summary>
    /// 生物对象事件，用于生物死亡时生效可以被阻拦使死亡不生效
    /// </summary>
    [Cancelable]
    public class LivingBaseDeathEvent : LivingEvent
    {
        public DamageSource source;
        public LivingBaseDeathEvent(EntityLivingBase entity, DamageSource Source) : base(entity)
        {
            this.source = Source;
            this.isCanceled = false;
        }
        //区分外部库的getSource方法
        public DamageSource GetSource()
        {
            return source;
        }
    }
}
