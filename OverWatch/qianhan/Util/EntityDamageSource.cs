using InfinityMemoriesEngine.OverWatch.qianhan.Entities;
using InfinityMemoriesEngine.OverWatch.qianhan.Entities.PlayerEntity;
using static InfinityMemoriesEngine.OverWatch.qianhan.Numbers.Mathf;
using Nullable = InfinityMemoriesEngine.OverWatch.qianhan.annotations.Nullable;

namespace InfinityMemoriesEngine.OverWatch.qianhan.Util
{
    public class EntityDamageSource : DamageSource
    {
        [Nullable]
        protected Entity trueSource;
        private bool isThornsDamage;
        public EntityDamageSource(string damageType, [Nullable] Entity entity) : base(damageType)
        {
            this.trueSource = entity;
        }
        public EntityDamageSource setIsThornsDamage()
        {
            this.isThornsDamage = true;
            return this;
        }
        public bool getIsThornsDamage()
        {
            return this.isThornsDamage && this.trueSource != null;
        }
        [Nullable]
        public override Entity getTrueSource()
        {
            return this.trueSource;
        }
        public void setTrueSource(Entity entity)
        {
            if (entity != null)
            {
                trueSource = entity;
            }
        }

        public bool isDifficultyScaled()
        {
            return this.trueSource != null && this.trueSource is EntityLivingBase && !(this.trueSource is EntityPlayer);
        }
        [Nullable]
        public Vector3 getDamageLocation()
        {
            return new Vector3((float)this.trueSource.posX, (float)this.trueSource.posY, (float)this.trueSource.posZ);
        }
    }
}
