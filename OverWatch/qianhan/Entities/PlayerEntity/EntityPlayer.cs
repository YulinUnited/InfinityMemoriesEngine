using System.Security.Cryptography.Xml;
using InfinityMemoriesEngine.OverWatch.qianhan.Entities.xuechengai.game;
using InfinityMemoriesEngine.OverWatch.qianhan.Items;
using static InfinityMemoriesEngine.OverWatch.qianhan.Numbers.Mathf;

namespace InfinityMemoriesEngine.OverWatch.qianhan.Entities.PlayerEntity
{
    public class EntityPlayer : EntityLivingBase
    {
        // 玩家属性
        public int UID = 0;
        public float attackDamage;
        public float criticalChance;
        public float criticalDamage;
        public float defense;
        public float moveSpeed;
        public float skillDamage;
        public bool TheOne;
        public float WaalkSpeed = 5f;
        public float RunSpeed = 10.0F;
        public float CruchSpeed = 2.0F;
        public float JumpForce = 5.0F;
        //private Rigidbody rb;
        public bool isGrounded = false;
        // 玩家指令等级
        public int CommandLevel;
        public Vector3 moveDirection;
        public float verticalVelocity; // 垂直方向速度
                                       // 玩家持有的物品
        public ItemStack[] inventory = new ItemStack[10]; // 假设玩家有10个格子
        public ItemStack? heldItem;
        public EntityLivingBase target;
        //HealthBarManagers healthBar;
        public Transform groundCheck;
        public Vector3 position;
        protected double MoveX;
        protected double MoveY;
        protected double MoveZ;
        public EntityPlayer()
        {
            heldItem = new ItemStack();
            target = this;
            groundCheck = this.GroundCheck;
            posX = this.MoveX;
            posY = this.MoveY;
            posZ = this.MoveZ;
            this.position = new Vector3((float)posX, (float)posY, (float)posZ);
            this.moveDirection = new Vector3(0, 0, 0);
            this.verticalVelocity = 0;
            this.isGrounded = false;
            this.isDead = false;
            this.MaxDamage = 64;
            this.MaxHealth = 800;
            this.currentHealth = MaxHealth;
            this.setMaxHealth(MaxHealth);
            this.setMaxDamage(MaxDamage);
        }

        public int MaxDamage { get; }
        public Transform GroundCheck { get; }
        public ItemStack getHeldItem(EnumHand hand)
        {
            if (hand == EnumHand.MAIN_HAND)
            {

            }
            return heldItem;
        }
    }
}
