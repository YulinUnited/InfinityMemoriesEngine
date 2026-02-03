using System.Numerics;
using System.Runtime.InteropServices;
using InfinityMemoriesEngine.OverWatch.qianhan.Entities.ai;
using InfinityMemoriesEngine.OverWatch.qianhan.Entities.items;
using InfinityMemoriesEngine.OverWatch.qianhan.GarbageCollection;
using InfinityMemoriesEngine.OverWatch.qianhan.Items;
using InfinityMemoriesEngine.OverWatch.qianhan.Metas;
using InfinityMemoriesEngine.OverWatch.qianhan.Numbers;
using InfinityMemoriesEngine.OverWatch.qianhan.Start;
using InfinityMemoriesEngine.OverWatch.qianhan.Util;
using InfinityMemoriesEngine.OverWatch.qianhan.Util.Base;
using InfinityMemoriesEngine.OverWatch.qianhan.Util.math;
using InfinityMemoriesEngine.OverWatch.qianhan.Worlds;
using OverWatch.QianHan.Log.network;
using static InfinityMemoriesEngine.OverWatch.qianhan.Numbers.Mathf;
using Debug = InfinityMemoriesEngine.OverWatch.qianhan.Log.lang.Debug;
using Vector3 = InfinityMemoriesEngine.OverWatch.qianhan.Numbers.Mathf.Vector3;

namespace InfinityMemoriesEngine.OverWatch.qianhan.Entities
{
    /// <summary>
    /// Entity重写版，用于表示在世界中的实体类型，不继承MainObject，避免与其他模块冲突。
    /// 不建议直接继承此类，建议使用ModuleBase或其他模块化基类。
    /// </summary>
    public unsafe class Entity
    {
        public float speed;
        private Set<string> tags;

        public Item item = new Item();

        public World world;

        public double posX;

        public double posY;

        public double posZ;

        public bool isDead;

        internal bool forceDead;

        public bool isKey;

        public bool isClearDebuff;

        public bool isRemoved;

        public bool isActive;

        protected bool isAlive;

        internal bool isEntity;

        public bool isUpdate;

        public bool isAi;

        internal bool invulnerable;

        public bool isFallingObject;

        public bool isInRemovingProcess;

        public bool isRecycle;

        protected float MaxHealth;

        protected float MinHealth;

        protected float currentHealth;

        private Entity* entityptr;

        public float width;

        public float height;

        public float prevRotationYaw;

        public float prevRotationPitch;

        public int chunkCoordX;

        private AxisAlignedBB boundingBox;

        protected bool firstUpdate;

        public DataManager dataManagers;// = new DataManager();

        public DataParameter<float> MAXHEALTH = new DataParameter<float>("Max_Health");

        public DataParameter<float> CURRENTHEALTH = new DataParameter<float>("Current_Health");

        public float currentDamage;

        public DamageSource source = new DamageSource();
        public float RotationYaw { set; get; }
        public float RotationPitch { set; get; }
        public EntityAITasks tasks = new EntityAITasks();

        private int max;

        private int min;

        public string Name;

        public long id;

        internal int currentVlaue;

        private List<Entity> riddenByEntities;//new List<Entity>();

        private Entity ridingEntity;

        private List<ItemEntity> entityItem;//new List<ItemEntity>();

        public ItemEntity ItemEntity;//new ItemEntity();
        private int entityId;

        public IntPtr sizes { get; private set; }

        public int Size { get; private set; }
        public bool IsAddedToWorld { get; private set; }
        public Vector3 Position;
        internal double lastTickPosX;
        internal double lastTickPosY;
        internal double lastTickPosZ;
        internal float rotationYaw;
        internal float rotationPitch;
        internal bool addedToChunk;
        internal int ticksExisted;
        internal bool updateBlocked;
        internal int chunkCoordY;
        internal int chunkCoordZ;

        /// <summary>
        /// 实体结构体，用于与非托管代码交互。
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct EntityStruct
        {
            public int id;
            public float x, y, z;
        }
        /// <summary>
        /// 设置实体名称
        /// </summary>
        /// <param name="name">要设置的名称</param>
        [MetaCompileAttirbute("修改名称", "设置名称", "将名称设置为", "名称修改为")]
        public void setEntityName(string name)
        {
            Name = name;
        }
        /// <summary>
        /// 获取实体名称
        /// </summary>
        /// <returns>返回名称</returns>
        [MetaCompileAttirbute("获取实体名称", "获取实体名字", "获取目标名称", "获取目标名字")]
        public string getEntityName()
        {
            return Name;
        }
        /// <summary>
        /// 设置实体位置和朝向
        /// </summary>
        /// <param name="x">目标坐标X</param>
        /// <param name="y">目标坐标Y</param>
        /// <param name="z">目标坐标Z</param>
        /// <param name="yaw"></param>
        /// <param name="pitch"></param>
        public void setLocationAndAngles(double x, double y, double z, float yaw, float pitch)
        {
            this.posX = x;
            this.posY = y;
            this.posZ = z;
            this.setRotationYaw(yaw);
            this.setRotationPitch(pitch);
            this.setPosition(this.posX, this.posY, this.posZ);
        }
        /// <summary>
        /// 设置实体位置
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public void setPosition(double x, double y, double z)
        {
            this.Position = new Vector3((float)x, (float)y, (float)z);
        }
        public Entity() { }
        public Entity(World world)
        {
            this.world = world;
            this.isDead = false;
            this.isAlive = true;
            this.isEntity = true;
            this.isActive = true;
            this.isUpdate = true;
            this.isAi = true;
            this.invulnerable = false;
            this.isFallingObject = false;
            this.isInRemovingProcess = false;
            this.isRecycle = false;
            this.riddenByEntities = new List<Entity>();
            this.entityItem = new List<ItemEntity>();
            this.Position = new Vector3(0, 0, 0);
            this.Size = 0;
            this.forceDead = false;
        }

        /// <summary>
        /// 当实体被激活时
        /// </summary>
        [Chinese(ChinesePhase.onAwake)]
        public virtual void onEntityAwake()
        {

        }
        /// <summary>
        /// 当实体初始化时
        /// </summary>
        [Chinese(ChinesePhase.onStart)]
        public virtual void onEntityStart()
        {
            dataManagers = new DataManager();
            MAXHEALTH = new DataParameter<float>("Max_Health");
            CURRENTHEALTH = new DataParameter<float>("Current_Health");
        }
        /// <summary>
        /// 当实体更新时
        /// </summary>
        [Chinese(ChinesePhase.onUpdate)]
        public virtual void onEntityUpdate()
        {
            if (isKey)
            {
                MaxHealth = dataManagers.get<float>(MAXHEALTH);
                currentHealth = dataManagers.get<float>(CURRENTHEALTH);
                min++;
                //启动while循环保证只执行一次
                while (min < max)
                {
                    dataManagers.get<float>(MAXHEALTH);
                    dataManagers.get<float>(CURRENTHEALTH);
                    isKey = false;
                    break;
                }
            }
        }

        public nuint GetEntityIntPtr()
        {
            return (nuint)entityptr;
        }

        public boolean MallocEntityMemory()
        {
            nuint uint64 = GetEntityIntPtr();
            if (uint64 == null)
            {
                return false;
            }
            MixinGC.MixinGC_Allocate(uint64);
            return true;
        }

        /// <summary>
        /// 获取骑乘的实体列表
        /// </summary>
        /// <returns>返回实体的骑乘列表</returns>
        public virtual List<Entity> getPassengers()
        {
            return riddenByEntities;
        }


        /// <summary>
        /// 判断是否有实体正在骑乘此实体
        /// </summary>
        /// <returns>返回当前实体是否被骑乘</returns>
        public virtual bool isBeingRidden()
        {
            return !this.getPassengers().Count.Equals(0);
        }
        /// <summary>
        /// 将骑乘的实体移除
        /// </summary>
        public virtual void removePassengers()
        {
            for (int i = this.riddenByEntities.Count - 1; i >= 0; --i)
            {
                (this.riddenByEntities.GetRange(i, 1)[0]).disomuntRidingEntity();
            }
        }
        /// <summary>
        /// 移除所有骑乘实体
        /// </summary>
        public virtual void disomuntRidingEntity()
        {
            if (this.isBeingRidden())
            {
                this.riddenByEntities.Clear();
            }
        }
        /// <summary>
        /// 判断实体是否正在骑乘其他实体
        /// </summary>
        /// <returns>返回当前实体是否骑乘其他实体</returns>
        public virtual bool isRiding()
        {
            return this.getEntity() != null;
        }
        /// <summary>
        /// 获取实体类型，用于判断实体是否为特定类型。
        /// </summary>
        /// <returns>返回当前实体实例</returns>
        public virtual Entity getEntity()
        {
            if (!isEntity && this == null) return null;
            isEntity = true;
            return this;
        }
        /// <summary>
        /// 获取骑乘实体
        /// </summary>
        /// <returns>返回骑乘的实体类型</returns>
        public virtual Entity getRidingEntity()
        {
            return this.ridingEntity;
        }
        /// <summary>
        /// 设置骑乘实体
        /// </summary>
        /// <param name="entity">需要传入对应的实体类型</param>
        public virtual void setRidingEntity(Entity entity)
        {
            if (entity != null)
            {
                this.ridingEntity = entity;
            }
        }
        /// <summary>
        /// 设置实体死亡时是否掉落物品
        /// </summary>
        /// <param name="dropItems">是否允许掉落</param>
        public virtual void setDropItemsWhenDead(bool dropItems)
        {
            if (this.getEntityItem() != null)
            {
                if (dropItems)
                {
                    world.SpawnEntity(ItemEntity);
                }
            }
            this.ItemEntity = null;
        }
        /// <summary>
        /// 获取实体物品类型
        /// </summary>
        /// <returns>如果不为null则返回当前物品类型，否则返回null</returns>
        public virtual Item? getEntityItem()
        {
            if (item != null)
            {
                return item;
            }
            return null;
        }
        /// <summary>
        /// 伤害实体方法，通常用于处理实体受到伤害的逻辑。
        /// </summary>
        /// <param name="target">传入伤害的实体类型</param>
        /// <param name="source">传入伤害来源</param>
        /// <param name="value">传入收到的伤害值</param>
        public virtual void attackEntity(Entity target, DamageSource source, float value)
        {
            if (this.getHealth() == 0)
            {
                setDeath();
            }
        }
        /// <summary>
        /// 设置目标为死亡状态（强制死亡类型，无法被免死事件阻挡）
        /// </summary>
        public void setDeath()
        {
            this.invulnerable = false;
            this.isDead = true;
            this.forceDead = true;
            this.isRecycle = true;
        }
        /// <summary>
        /// 设置目标为死亡状态（弱死亡状态）
        /// </summary>
        public virtual void setDead()
        {
            this.isDead = true;
        }
        /// <summary>
        /// 当实体死亡时调用
        /// </summary>
        public virtual void onEntityDeath()
        {
            setDeath();
        }
        /// <summary>
        /// 当实体复活时调用
        /// </summary>
        public virtual void onEntityLiving()
        {
            this.isAlive = true;
            this.isEntity = true;
            this.isActive = true;
            this.isUpdate = true;
            this.isAi = true;
            this.invulnerable = false;
            this.isFallingObject = false;
            this.isInRemovingProcess = false;
            this.isRecycle = false;
            this.isDead = false;
            this.forceDead = false;
        }
        /// <summary>
        /// 当实体被杀死时
        /// </summary>
        public virtual void onKillEntity()
        {
            setDead();
        }
        /// <summary>
        /// 更好的生命恢复逻辑，使用此方法可以直接设置生命值
        /// </summary>
        /// <param name="value">传入回复的生命值数值，不可大于最大生命值，也不可低于0</param>
        /// <returns>返回当前实体的当前生命值</returns>
        public virtual float Health(float value)
        {
            if (value > getMaxHealth())
            {
                Debug.LogException($"{value}大于当前实体最大生命值上限，无法设置");
                throw new Exception();
            }
            if (value < 0)
            {
                Log.lang.Debug.LogException($"{value}小于0，无法设置当前实体生命值为负数");
                throw new Exception("当前实体生命值不能小于0");
            }
            setHealth(value);
            return getHealth();
        }
        /// <summary>
        /// 当实体死亡时调用
        /// </summary>
        /// <param name="source">需要传入伤害来源</param>
        public virtual void onDeath(DamageSource source)
        {

        }
        /// <summary>
        /// 生命值恢复逻辑
        /// </summary>
        /// <param name="amount">需要恢复的生命值</param>
        public virtual void RegenHealth(float amount)
        {
            if (currentHealth <= 0)
            {
                return;
            }
            else
            {
                currentHealth = Super.Min(currentHealth + amount, MaxHealth);
                dataManagers.set<float>(CURRENTHEALTH, currentHealth);
            }
        }
        /// <summary>
        /// 获取生命值
        /// </summary>
        /// <returns>返回当前实体的生命值</returns>
        public float getHealth()
        {
            if (dataManagers == null)
            {
                onEntityStart();
                return 0;
            }
            //return dataManager.get<float>(CURRENTHEALTH);
            return currentHealth;
        }
        /// <summary>
        /// 设置最大生命值
        /// </summary>
        /// <param name="value">传入需要设置的最大生命值数值</param>
        /// <exception cref="ArgumentException">如果数值为nan，会g强制死亡</exception>
        public void setMaxHealth(float value)
        {
            if (float.IsNaN(value))
            {
                setDeath();
                throw new ArgumentException($"当前目标最大生命值为NaN,以强制击杀{this}");
            }
            if (value <= 0) { throw new ArgumentException("最大生命值必须大于0"); }
            dataManagers.set<float>(MAXHEALTH, value);
            if (this.getHealth() > value) setHealth(value);
            else
            {
                setHealth(this.getHealth());
            }
        }
        /// <summary>
        /// 设置生命值
        /// </summary>
        /// <param name="value">需要设置的生命值</param>
        /// <exception cref="ArgumentException"></exception>
        public void setHealth(float value)
        {
            if (float.IsNaN((float)value))
            {
                setDeath();
                throw new ArgumentException($"当前目标生命值为NaN,无法设置，以强制击杀{this}");
            }
            if (dataManagers == null)
            {
                onEntityStart();
                return;
            }
            float clampedValue = Super.Clamped(value, 0, this.getMaxHealth());
            dataManagers.set<float>(CURRENTHEALTH, clampedValue);
            currentHealth = clampedValue;
        }

        /// <summary>
        /// 移动实体
        /// </summary>
        /// <param name="x">移动倍率</param>
        public static void move(double x)
        {
            Vector3 result = Vector3.Zero();
            if (Move.TheLeft)
            {
                result.x -= x;
            }
            if(Move.TheRight)
            {
                result.x += x;
            }
            if(Move.TheUp)
            {
                result.z += x;
            }
            if(Move.TheDown)
            {
                result.z -= x;
            }
        }

        /// <summary>
        /// 获取最大生命值上限
        /// </summary>
        /// <returns>返回当前实体最大生命上限</returns>
        /// <exception cref="ArgumentException"></exception>
        public float getMaxHealth()
        {
            if (float.IsNaN(MaxHealth))
            {
                setDeath();
                throw new ArgumentException($"当前目标生命值为NaN,以及强制击杀{this}");
            }
            if (dataManagers != null)
            {
                //Debug.Log("dataManager不为null，已经获取到实体的最大生命值");
                //MaxHealth = dataManager.get<float>(MAXHEALTH);
                if (MaxHealth < 0)
                {
                    throw new ArgumentException("最大生命值为0，无法获取已经为零或者小于零的生命值");
                }
                MaxHealth = dataManagers.get<float>(MAXHEALTH);
                return MaxHealth;
            }
            return MaxHealth;
        }

        public virtual double getTrueHealth() => 0.0D;

        public virtual double getTrueMaxHealth() => 0.0D;

        public virtual void MarkVelocityChanged()
        {
            // 标记实体速度已改变，通常用于物理引擎或其他逻辑处理
            this.isUpdate = true;
        }
        /// <summary>
        /// 攻击实体
        /// </summary>
        /// <param name="source"></param>
        /// <param name="value"></param>
        /// <returns>返回攻击成功还是i攻击失败默认攻击失败</returns>
        public virtual bool attackEntityForm(DamageSource source, float value)
        {
            if (this.isEntityInvulnerable(source))
            {
                return false; // 如果实体无敌，则攻击失败
            }
            if (this.getHealth() <= 0)
            {
                return false; // 如果实体已经死亡，则攻击失败
            }
            this.MarkVelocityChanged(); // 标记实体速度已改变
            return false; // 默认攻击失败
        }

        public virtual bool attackEntityForm(float value, DamageSource source)
        {
            if (this.isEntityInvulnerable(source))
            {
                return false; // 如果实体无敌，则攻击失败
            }
            if (this.getHealth() <= 0)
            {
                return false; // 如果实体已经死亡，则攻击失败
            }
            this.MarkVelocityChanged(); // 标记实体速度已改变
            return false; // 默认攻击失败
        }

        /// <summary>
        /// 获取实体所在的世界
        /// </summary>
        /// <returns>返回当前实体所在的世界/场景类型</returns>
        public virtual World getEntityWorld()
        {
            return this.world;
        }
        /// <summary>
        /// 当执行Kill命令时调用
        /// </summary>
        public virtual void onKillCommands()
        {
            this.setDead();
        }
        /// <summary>
        /// 获取实体是否活着
        /// </summary>
        /// <returns>返回当前实体的存活状态</returns>
        public virtual bool getEntityAlive()
        {
            return this.isAlive;
        }
        /// <summary>
        /// 判断实体是否还活着
        /// </summary>
        /// <returns>返回当前实体是否处于存活状态</returns>
        public virtual bool isEntityAlive()
        {
            return !this.isAlive;
        }
        /// <summary>
        /// 设置实体是否处于存活状态
        /// </summary>
        /// <param name="v">通过传入的变量决定当前实体的存活状态，true或false</param>
        public virtual void setEntityAlive(bool v)
        {
            this.isAlive = v;
        }
        /// <summary>
        /// 判断实体是否处于无敌状态，无敌状态无法免疫setDeath的攻击
        /// </summary>
        /// <returns>返回当前实体的无敌状态</returns>
        public virtual bool isEntityInvulnerable()
        {
            return !this.invulnerable;
        }
        /// <summary>
        /// 获取实体的无敌状态
        /// </summary>
        /// <returns>返回当前实体的无敌状态</returns>
        public virtual bool getEntityInvulnerable()
        {
            return this.invulnerable;
        }
        /// <summary>
        /// 设置实体是否处于无敌状态
        /// </summary>
        /// <param name="v">通过传入的变量决定是否处于无敌状态</param>
        public virtual void setEntityInvulnerable(bool v)
        {
            this.invulnerable = v;
        }
        /// <summary>
        /// 获取命令发送者实体，通常用于执行命令时获取实体本身。
        /// </summary>
        /// <returns>返回命令发送者实体</returns>
        public virtual Entity getCommandSenderEntity()
        {
            return this;
        }
        /// <summary>
        /// 获取实体的ID，通常用于唯一标识实体。
        /// </summary>
        /// <returns>返回实体ID</returns>
        public virtual int getEntityId()
        {
            return this.entityId;
        }
        /// <summary>
        /// 设置实体的ID，通常用于唯一标识实体。
        /// </summary>
        /// <param name="id"></param>
        public virtual void setEntityId(int id)
        {
            this.entityId = id;
        }
        /// <summary>
        /// 获取实体的标签，通常用于显示或日志记录。
        /// </summary>
        /// <returns>返回标签</returns>
        public Set<string> getTags()
        {
            return this.tags;
        }
        /// <summary>
        /// 设置实体的标签，通常用于分类或标识实体。
        /// </summary>
        /// <param name="tags"></param>
        public void setTags(Set<string> tags)
        {
            this.tags = tags;
        }
        /// <summary>
        /// 添加标签到实体，通常用于分类或标识实体。
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        public bool AddTag(string tag)
        {
            if (this.tags.size() >= 1024)
            {
                return false;
            }
            this.tags.Add(tag);
            return true;
        }
        /// <summary>
        /// 移除实体的标签，通常用于清理或更新实体的状态。
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        public bool removeTag(string tag)
        {
            return this.tags.remove(tag);
        }
        /// <summary>
        /// 当实体不在世界中时调用，通常用于处理实体被移除或销毁的逻辑。
        /// </summary>
        public virtual void OutOfWorld()
        {
            this.setDead();
        }
        public virtual float EntityFlySound(float value) { return 0.0F; }
        public virtual bool MakeFlySound() { return false; }
        public virtual bool isEntityInvulnerable(DamageSource source)
        {
            return this.invulnerable && source != DamageSource.OUT_OF_WORLD;
        }
        /// <summary>
        /// 判断实体是否已经添加到世界中，通常用于检查实体是否处于活动状态。
        /// </summary>
        /// <returns></returns>
        public bool isAddedToWorld()
        {
            return this.IsAddedToWorld;
        }
        /// <summary>
        /// 当实体加入世界时，通常用于初始化实体的状态或属性。
        /// </summary>
        public virtual void onAddedToWorld()
        {
            this.IsAddedToWorld = true;
        }
        /// <summary>
        /// 当实体从世界中移除时，通常用于清理实体的状态或属性。
        /// </summary>
        public virtual void onRemovedFormWorld()
        {
            this.IsAddedToWorld = false;
        }
        public void moveTo(float x, float y, float z, float speed = 0.1f)
        {
            Vector3 dir = new Vector3(x, y, z) - Position;
            float dist = dir.Length();
            if (dist > 0.001f)
            {
                dir /= dist;
                Position += dir * speed;
            }
        }
        public float getDistanceSq(Entity target)
        {
            Vector3 delta = Position - target.Position;
            return delta.LengthSquared();
        }

        public float getDistanceSq(float x, float y, float z)
        {
            Vector3 delta = Position - new Vector3(x, y, z);
            return delta.LengthSquared();
        }

        public void setRotationYaw(float yaw)
        {
            RotationYaw = yaw % 360f;
            if (RotationYaw < 0) RotationYaw += 360f;
        }

        // 设置垂直朝向
        public void setRotationPitch(float pitch)
        {
            RotationPitch = Mathf.Max(-90f, Mathf.Min(90f, pitch));
        }

        // 让实体看向某个点（计算 yaw/pitch）
        public void lookAt(float x, float y, float z)
        {
            Vector3 delta = new Vector3(x, y, z) - Position;
            float dx = (float)delta.x;
            float dy = (float)delta.y;
            float dz = (float)delta.z;

            float distXZ = Mathf.Sqrt(dx * dx + dz * dz);
            setRotationYaw((float)(Mathf.Mathf_Atan2(dz, dx) * 180f / MathConstants.PI - 90f));
            setRotationPitch((float)(-Mathf.Mathf_Atan2(dy, distXZ) * 180f / MathConstants.PI));
        }
        /// <summary>
        /// 更新骑乘实体的位置和状态，请复写它以实现自定义逻辑。
        /// </summary>
        public virtual void updateRidden()
        {
            if (ridingEntity == null)
            {
                return;
            }
            else if (riddenByEntities.IsEmpty()) return;
        }
       
    }
}
