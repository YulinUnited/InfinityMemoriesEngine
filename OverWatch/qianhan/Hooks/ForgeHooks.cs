using InfinityMemoriesEngine.OverWatch.qianhan.Entities;
using InfinityMemoriesEngine.OverWatch.qianhan.Entities.PlayerEntity;
using InfinityMemoriesEngine.OverWatch.qianhan.Events.fml.common;
using InfinityMemoriesEngine.OverWatch.qianhan.Events.fml.events.entity.living;
using InfinityMemoriesEngine.OverWatch.qianhan.Events.util;
using InfinityMemoriesEngine.OverWatch.qianhan.PotionEffects;
using InfinityMemoriesEngine.OverWatch.qianhan.Util;

namespace InfinityMemoriesEngine.OverWatch.qianhan.Hooks
{
    /// <summary>
    /// 钩子类，用于处理生物体相关的事件
    /// </summary>
    public class ForgeHooks
    {
        static Entity e;

        /// <summary>
        /// 当生物体设置攻击目标时触发的事件
        /// </summary>
        /// <param name="entityLiving"></param>
        /// <param name="entity"></param>
        public static void onLivingSetAttackTarget(EntityLivingBase entityLiving, EntityLivingBase entity)
        {
            ForgeIronteted.EVENT_BUS.post(new LivingSetAttackTargetEvent(entityLiving, entity));
        }
        /// <summary>
        /// 当生物体更新时触发的事件
        /// </summary>
        /// <param name="livingBase"></param>
        /// <returns></returns>
        public static bool onLivingUpdate(EntityLivingBase livingBase)
        {
            return ForgeIronteted.EVENT_BUS.post(new LivingUpdateEvent(livingBase));
        }
        public static bool onInputKey(KeyCode keyCode, bool isCancelable, Entity entity, Entity vimcn)
        {
            return ForgeIronteted.EVENT_BUS.post(new InputEvent(keyCode, isCancelable, entity, vimcn));
        }
        /// <summary>
        /// 当生物体跳跃时触发的事件
        /// </summary>
        /// <param name="livingBase"></param>
        /// <returns></returns>
        public static bool onLivingJump(EntityLivingBase livingBase)
        {
            return ForgeIronteted.EVENT_BUS.post(new LivingJumpEvent(livingBase));
        }
        /// <summary>
        /// 当生物体受到攻击时触发的事件
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="source"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool onLivingAttack(EntityLivingBase entity, DamageSource source, float value)
        {
            return entity is EntityPlayer || !ForgeIronteted.EVENT_BUS.post(new LivingBaseAttackEvent(entity, source, value));
        }
        /// <summary>
        /// 当玩家攻击生物体时触发的事件
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="src"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public static bool onPlayerAttack(EntityLivingBase entity, DamageSource src, float amount)
        {
            return !ForgeIronteted.EVENT_BUS.post(new LivingBaseAttackEvent(entity, src, amount));
        }
        /// <summary>
        /// 当生物体受到伤害时触发的事件
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="source"></param>
        /// <param name="a"></param>
        /// <returns></returns>
        public static float onLivingHurt(EntityLivingBase entity, DamageSource source, float a)
        {
            LivingBaseHurtEvent @event = new LivingBaseHurtEvent(entity, source, a);
            return (ForgeIronteted.EVENT_BUS.post(@event)) ? 0 : @event.getAmount();
        }
        /// <summary>
        /// 当生物体死亡时触发的事件
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool onLivingDeath(EntityLivingBase entity, DamageSource source)
        {
            return ForgeIronteted.EVENT_BUS.post(new LivingBaseDeathEvent(entity, source));
        }
        /// <summary>
        /// 当生物体受到伤害时触发的事件
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="source"></param>
        /// <param name="ount"></param>
        /// <returns></returns>
        public static float onLivingDamage(EntityLivingBase entity, DamageSource source, float ount)
        {
            LivingBaseDamageEvent @event = new LivingBaseDamageEvent(entity, source, ount);
            return (ForgeIronteted.EVENT_BUS.post(@event)) ? 0 : @event.getAmount();
        }
        /// <summary>
        /// 当生物体掉落物品时触发的事件
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="source"></param>
        /// <param name="drops"></param>
        /// <param name="lootionglLevel"></param>
        /// <param name="recentlyHit"></param>
        /// <returns></returns>
        public static bool onLivingDrops(EntityLivingBase entity, DamageSource source, ArrayList<Entity> drops, int lootionglLevel, bool recentlyHit)
        {
            return ForgeIronteted.EVENT_BUS.post(new LivingBaseDropsEvent(entity, source, drops, lootionglLevel, recentlyHit));
        }
        /// <summary>
        /// 当生物体掉落物品时触发的事件
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="distance"></param>
        /// <param name="damageMultiplier"></param>
        public static void onLivingFall(EntityLivingBase entity, float distance, float damageMultiplier)
        {
            ForgeIronteted.EVENT_BUS.post(new LivingBaseFallEvent(entity, distance, damageMultiplier));
        }
        /// <summary>
        /// 当生物体治疗时触发的事件
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public static bool onLivingHeal(EntityLivingBase entity, float amount)
        {
            return ForgeIronteted.EVENT_BUS.post(new LivingBaseHealEvent(entity, amount));
        }
        /// <summary>
        /// 当生物体使用药水效果时触发的事件
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="effect"></param>
        /// <returns></returns>
        public static bool onLivingUpdatePotionEffect(EntityLivingBase entity, PotionEffect effect)
        {
            return ForgeIronteted.EVENT_BUS.post(new LivingUpdatePotionEffectEvent(entity, effect));
        }

    }
}
