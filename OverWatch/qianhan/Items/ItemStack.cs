using InfinityMemoriesEngine.OverWatch.qianhan.Entities;
using InfinityMemoriesEngine.OverWatch.qianhan.Entities.PlayerEntity;

namespace InfinityMemoriesEngine.OverWatch.qianhan.Items
{
    public class ItemStack : Item
    {
        public int count;
        public int maxCount;
        public new string name;
        public int itemDamage;
        public static ItemStack EMPTY { get; internal set; }

        public ItemStack(string name, int count)
        {
            this.name = name;
            this.count = count;
            this.maxCount = 64;
        }
        public ItemStack(string name, int count, int maxCount)
        {
            this.name = name;
            this.count = count;
            this.maxCount = maxCount;
        }

        public ItemStack()
        {
        }

        public void setItemName(string name)
        {
            this.name = name;
        }
        public string getItemName()
        {
            return this.name;
        }
        public override void onItemUpdate()
        {
        }
        public virtual void hitEntity(EntityLivingBase entity, EntityPlayer player)
        {
            bool fag = base.hitEntity(this, entity, player);
        }
    }
}
