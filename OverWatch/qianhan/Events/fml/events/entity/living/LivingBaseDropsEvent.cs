using InfinityMemoriesEngine.OverWatch.qianhan.Entities;
using InfinityMemoriesEngine.OverWatch.qianhan.Entities.items;
using InfinityMemoriesEngine.OverWatch.qianhan.Events.fml.common.eventhandler;
using InfinityMemoriesEngine.OverWatch.qianhan.Util;

namespace InfinityMemoriesEngine.OverWatch.qianhan.Events.fml.events.entity.living
{
    //这是一个事件类，用于处理生物掉落物品的事件
    [Cancelable]
    public class LivingBaseDropsEvent : LivingEvent
    {
        private DamageSource source;
        private List<EntityItem> drops;
        private int lootingLevel;
        private bool recentlyHit;

        public ArrayList<Entity> Drops { get; }
        public int LootionglLevel { get; }

        public LivingBaseDropsEvent(EntityLivingBase entity, DamageSource source, List<EntityItem> drops, int lootingLevel, bool recentlyHit) : base(entity)
        {

            this.source = source;
            this.drops = drops;
            this.lootingLevel = lootingLevel;
            this.recentlyHit = recentlyHit;
        }

        public LivingBaseDropsEvent(EntityLivingBase entity, DamageSource source, ArrayList<Entity> drops1, int lootionglLevel, bool recentlyHit) : base(entity)
        {
            this.source = source;
            Drops = drops1;
            LootionglLevel = lootionglLevel;
            this.recentlyHit = recentlyHit;
        }

        public DamageSource getSource()
        {
            return source;
        }
        public List<EntityItem> getDrops()
        {
            return drops;
        }
        public int getLootingLevel()
        {
            return lootingLevel;
        }
        public bool isRecentlyHit()
        {
            return recentlyHit;
        }

    }
}
