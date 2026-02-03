using InfinityMemoriesEngine.OverWatch.qianhan.Entities.xuechengai.game.nbt;
using InfinityMemoriesEngine.OverWatch.qianhan.Util;

namespace InfinityMemoriesEngine.OverWatch.qianhan.Entities
{
    public class MultiPartEntityPart : Entity
    {
        public IEntityMultiPart parent;
        public string partName;

        public MultiPartEntityPart(IEntityMultiPart parent, String partName, float width, float height) : base(parent.getWorld())
        {
            this.setSize(width, height);
            this.parent = parent;
            this.partName = partName;
        }
        //占位符
        public void setSize(float width, float height)
        {
            throw new NotImplementedException();
        }

        protected void entityInit()
        {
        }

        protected void readEntityFromNBT(NBTTagCompound compound)
        {
        }

        protected void writeEntityToNBT(NBTTagCompound compound)
        {
        }

        public bool canBeCollidedWith()
        {
            return true;
        }

        public bool attackEntityFrom(DamageSource source, float amount)
        {
            return this.isEntityInvulnerable(source) ? false : this.parent.attackEntityFromPart(this, source, amount);
        }

        public bool isEntityEqual(Entity entityIn)
        {
            return this == entityIn || this.parent == entityIn;
        }
    }
}
