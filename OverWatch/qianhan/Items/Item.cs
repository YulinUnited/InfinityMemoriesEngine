using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InfinityMemoriesEngine.OverWatch.qianhan.Entities;
using InfinityMemoriesEngine.OverWatch.qianhan.Entities.items;
using InfinityMemoriesEngine.OverWatch.qianhan.Entities.PlayerEntity;
using InfinityMemoriesEngine.OverWatch.qianhan.Entities.xuechengai.game;
using InfinityMemoriesEngine.OverWatch.qianhan.Items.Enums;
using InfinityMemoriesEngine.OverWatch.qianhan.Worlds;

namespace InfinityMemoriesEngine.OverWatch.qianhan.Items
{
    public class Item
    {
        protected int maxStackSize = 64;
        //private item containerItem = new item();

        private int Damage = 0;
        private int maxDamage = 100;
        protected bool hasSubtypes;
        private string unlocalizedName;
        protected bool canRepair = true;
        public string Name { get; protected set; }
        private int minDamage = 0;
        public Item()
        {
            maxStackSize = 64;
            //containerItem = null;
            Damage = maxDamage;
        }

        public Item setMaxStackSize(int @int)
        {
            this.maxStackSize = @int;
            return this;
        }

        public void setItemName(string name)
        {
            Name = name;
        }

        public string getItemName()
        {
            return Name;
        }

        public virtual ActionResult<ItemStack> onItemRightClick(World world, EntityPlayer player, EnumHand @enum)
        {
            return new ActionResult<ItemStack>(EnumActionResult.PASS, player.getHeldItem(@enum));
        }
        public virtual void onItemUpdate()
        {
        }
        public virtual float getDestroySpeed(ItemStack stack)
        {
            return 1.0f;
        }
        public virtual ItemStack onItemUseFinish(ItemStack stack, World worldIn, EntityLivingBase entityLiving)
        {
            return stack;
        }
        public virtual int getMetadata(int damage)
        {
            return 0;
        }
        public virtual int getMaxDamage()
        {
            return maxDamage;
        }

        public Item setMaxDamage(int maxDamageIn)
        {
            this.maxDamage = maxDamageIn;
            return this;
        }
        public bool isDamageable()
        {
            return this.maxDamage > 0 && (!this.hasSubtypes || this.maxStackSize == 1);
        }
        public virtual void onUpdate(ItemStack stack, World world, Entity entitYiN, int itemSlot, bool isSelected)
        { }
        public virtual void onCreated(ItemStack stack, World world, EntityPlayer playerIn)
        {

        }
        public virtual void onDestroy() { }
        public virtual EnumAction getItemUseAction(ItemStack stack)
        {
            return EnumAction.NONE;
        }
        public virtual int getMaxItemUseDuration(ItemStack stack)
        {
            return 0;
        }
        public virtual void onPlayerStoppedUsing(ItemStack stack, World worldIn, EntityLivingBase entityLiving, int timeLeft)
        {
        }
        public virtual int getItemEnchantability()
        {
            return 0;
        }
        public virtual bool canItemEditBlocks()
        {
            return false;
        }
        public virtual bool getIsRepairable(ItemStack toRepair, ItemStack repair)
        {
            return false;
        }
        public bool onDroppedByPlayer(ItemStack item, EntityPlayer player)
        {
            return true;
        }
        public string getHighlightTip(ItemStack item, String displayName)
        {
            return displayName;
        }
        public virtual bool isRepairable()
        {
            return canRepair && isDamageable();
        }
        public Item setNoRepair()
        {
            canRepair = false;
            return this;
        }
        public virtual float getXpRepairRatio(ItemStack stack)
        {
            return 2f;
        }
        public virtual void onUsingTick(ItemStack stack, EntityLivingBase player, int count)
        {
        }
        public virtual bool onLeftClickEntity(ItemStack stack, EntityPlayer player, Entity entity)
        {
            return false;
        }
        public virtual bool onEntityItemUpdate(EntityItem entityItem)
        {
            return false;
        }
        public virtual void onArmorTick(World world, EntityPlayer player, ItemStack itemStack) { }
        public virtual bool isBookEnchantable(ItemStack stack, ItemStack book)
        {
            return true;
        }
        public virtual int getDamage(ItemStack stack)
        {
            return stack.itemDamage;
        }
        public virtual void setDamage(ItemStack stack, int damage)
        {
            stack.itemDamage = damage;
            if (stack.itemDamage < 0)
            {
                stack.itemDamage = 0;
            }
        }
        public virtual string getUnlocalizedName()
        {
            return "item." + unlocalizedName;
        }
        public virtual int getMaxDamage(ItemStack stack)
        {
            return getMaxDamage();
        }
        public virtual bool hitEntity(ItemStack stack, EntityLivingBase target, EntityLivingBase attacker)
        {
            return false;
        }
        public int getMinDamage()
        {
            return minDamage;
        }
        public void setMinDamage(int minDamage)
        {
            this.minDamage = minDamage;
        }
    }
}
