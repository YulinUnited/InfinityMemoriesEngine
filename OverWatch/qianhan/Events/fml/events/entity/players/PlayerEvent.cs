using InfinityMemoriesEngine.OverWatch.qianhan.Entities;
using InfinityMemoriesEngine.OverWatch.qianhan.Entities.PlayerEntity;
using InfinityMemoriesEngine.OverWatch.qianhan.Events.fml.events.entity.living;

namespace InfinityMemoriesEngine.OverWatch.qianhan.Events.fml.events.entity.players
{
    public class PlayerEvent:LivingEvent
    {
        private EntityPlayer player;

        public PlayerEvent(EntityPlayer entityPlayer):base(entityPlayer)
        {
            player = entityPlayer;
        }

        public EntityPlayer getEntityPlayer()
        {
            return player;
        }

        public class HarvestCheck:PlayerEvent
        {
            private bool canHarvest;
            public HarvestCheck(EntityPlayer player, bool canHarvest) : base(player)
            {
                this.canHarvest = canHarvest;
            }
            public bool canHarvestBlock()
            {
                return canHarvest;
            }
            public void setCanHarvest(bool canHarvest)
            {
                this.canHarvest = canHarvest;
            }
        }

        public class BreakSpeed : PlayerEvent
        {
            private float OriginalSpeed;
            private float newSpeed = 0.0F;
            public BreakSpeed(EntityPlayer player, float original) : base(player)
            {
                this.OriginalSpeed = original;
                this.setNewSpeed(original);
            }
            public float getOriginalSpeed()
            {
                return this.OriginalSpeed;
            }
            public float getNewSpeed()
            {
                return newSpeed;
            }
            public void setNewSpeed(float newSpeed)
            {
                this.newSpeed = newSpeed;
            }
        }
        public class NameFormat : PlayerEvent
        {
            private string name;
            private string displayname;
            public NameFormat(EntityPlayer player, string username) : base(player)
            {
                this.name = username;
                this.setDisplayName(username);
            }
            public string getUsername()
            {
                return name;
            }
            public string getDisplayName()
            {
                return displayname;
            }
            public void setDisplayName(string displayName)
            {
                this.displayname = displayName;
            }
        }
        public class Clone : PlayerEvent
        {
            private EntityPlayer OldPlayer;
            private bool WasDeath;
            public Clone(EntityPlayer player, EntityPlayer oldPlayer, bool wasDeath) : base(player)
            {
                this.OldPlayer = oldPlayer;
                this.WasDeath = wasDeath;
            }
            public EntityPlayer getOriginal()
            {
                return OldPlayer;
            }
            public bool isWasDeath()
            {
                return WasDeath;
            }
        }
        public class StartTracking : PlayerEvent
        {
            private Entity Entity;
            public StartTracking(EntityPlayer player, Entity entity) : base(player)
            {
                this.Entity = entity;
            }
            public Entity getTarget()
            {
                return Entity;
            }
        }
    }
}
