using InfinityMemoriesEngine.OverWatch.qianhan.Entities.ai.attirbutes;
using InfinityMemoriesEngine.OverWatch.qianhan.Entities.Attirbuted;
using InfinityMemoriesEngine.OverWatch.qianhan.Entities.items;
using InfinityMemoriesEngine.OverWatch.qianhan.Entities.xuechengai.game;
using InfinityMemoriesEngine.OverWatch.qianhan.Events;
using InfinityMemoriesEngine.OverWatch.qianhan.Events.fml;
using InfinityMemoriesEngine.OverWatch.qianhan.Events.fml.events.entity;
using InfinityMemoriesEngine.OverWatch.qianhan.Events.fml.events.entity.living;
using InfinityMemoriesEngine.OverWatch.qianhan.Hooks;
using InfinityMemoriesEngine.OverWatch.qianhan.Items;
using InfinityMemoriesEngine.OverWatch.qianhan.Log.lang;
using InfinityMemoriesEngine.OverWatch.qianhan.Numbers;
using InfinityMemoriesEngine.OverWatch.qianhan.Start;
using InfinityMemoriesEngine.OverWatch.qianhan.Util;
using OverWatch.QianHan.Log.network;

namespace InfinityMemoriesEngine.OverWatch.qianhan.Entities
{
    public unsafe class EntityLivingBase : Entity
    {
        protected LivingEvent livingEvent;
        Entity entity;
        protected Event Events;
        protected bool dead;
        protected bool isDodge;
        protected bool isSkill;
        public PrimaryAttribute primary;
        public ElementAttribute element;
        public SpecialAttributes special;
        public int attackSpeed;
        public int physicalStrenght;
        public int energy;
        public int deathTime;
        public new int ticksExisted;
        public AbstractAttributeMap attributeMap;
        public int Defense { get; set; }
        public int Armores { get; set; }
        public int ReadEvasion { get; set; }
        public int Dodge { get; set; }
        private float damage;
        private float MaxDamage;
        private EntityLivingBase revengeTarget;
        private int revengTimer;
        private EntityLivingBase lastAttackedEntity;
        private int lastAttackedEntityTime;
        private int idleTime;
        public static readonly DataParameter<float> MAXDAMAGE = new DataParameter<float>("Max_Damage");
        public static readonly DataParameter<float> CURRENTDAMAGE = new DataParameter<float>("Current_Damage");
        public EntityLivingBase() : base()
        {
            Events = new Event();
            //this.entity = this;
        }
        [Chinese(ChinesePhase.onStart)]
        public override void onEntityStart()
        {
            base.onEntityStart();
            this.source = new DamageSource();
            entity = this;
            entity.isDead = false;
            entity.Name = "EntityLivingBase";
            MaxDamage = dataManagers.get<float>(MAXDAMAGE);
            damage = dataManagers.get<float>(CURRENTDAMAGE);
            

        }

        public override void onEntityAwake()
        {
            livingEvent = new LivingEvent(this);
        }

        /// <summary>
        /// 执行kill命令时
        /// </summary>
        public override void onKillCommands()
        {
            base.onKillCommands();
            this.attackEntityForm(DamageSource.OUT_OF_WORLD, float.MaxValue);
        }

        public IAttributeInstance getEntityAttribute(IAttribute attribute)
        {
            return this.getAttirbute().getAttirbuteInstance(attribute);
        }

        public AbstractAttributeMap getAttirbute()
        {
            if (this.attributeMap == null)
            {
                this.attributeMap = new AttirbuteMap();
            }
            return this.attributeMap;
        }

        /// <summary>
        /// 判断当前实体是否为子类实体
        /// </summary>
        /// <returns></returns>
        public virtual bool isChild()
        {
            return false;
        }
        /// <summary>
        /// 用于判断当前实体是否可以掉落物品
        /// </summary>
        /// <returns>返回是否可以掉落</returns>
        protected virtual bool canDropLoot()
        {
            return !this.isChild();
        }
        /// <summary>
        /// 判断是否是玩家实体
        /// </summary>
        /// <returns>返回是否为玩家</returns>
        protected virtual bool isPlayer()
        {
            return false;
        }
        /// <summary>
        /// 获取当前实体的复仇目标
        /// </summary>
        /// <returns>返回当前实体的复仇对象</returns>
        public virtual EntityLivingBase getRevengeTarget()
        {
            return this.revengeTarget;
        }
        /// <summary>
        /// 当实体死亡时调用
        /// </summary>
        protected virtual void onDeathUpdate()
        {
            ++this.deathTime;
            if (this.deathTime == 20)
            {
                this.setDead();
            }
        }
        /// <summary>
        /// 设置复仇目标
        /// </summary>
        /// <param name="livingBase"></param>
        public virtual void setRevengeTarget(EntityLivingBase livingBase)
        {
            this.revengeTarget = livingBase;
            this.revengTimer = this.ticksExisted;
        }
        /// <summary>
        /// 获取当前实体的最后攻击对象
        /// </summary>
        /// <returns>返回最后的攻击对象</returns>
        public virtual EntityLivingBase getLastAttackedEntity()
        {
            return this.lastAttackedEntity;
        }
        /// <summary>
        /// 获取当前实体的最后攻击时间
        /// </summary>
        /// <returns></returns>
        public virtual int getLastAttackedEntityTime()
        {
            return this.lastAttackedEntityTime;
        }
        /// <summary>
        /// 获取实体的空闲时间
        /// </summary>
        /// <returns></returns>
        public virtual int getIdleTime()
        {
            return this.idleTime;
        }
        /// <summary>
        /// 对实体造成伤害，底层调用attackEnityForm方法
        /// </summary>
        /// <param name="livingBase">主体对象</param>
        /// <param name="source">伤害来源</param>
        /// <param name="damage">数值参数</param>
        public override void attackEntity(Entity livingBase, DamageSource source, float damage)
        {
            base.attackEntity(livingBase, source, damage);
            livingBase = (EntityLivingBase)livingBase;
            if (livingBase.isDead)
            {
                return;
            }
            livingBase.attackEntityForm(DamageSource.GENERIC, damage);
        }
        public override float Health(float healAmount)
        {
            if (float.IsNaN((float)healAmount))
            {
                this.setDeath();
                Debug.LogWarning($"{this}的治疗值为IsNaN，无法治疗,且以强制击杀");
                return 0.0F;
            }
            else
            {
                healAmount = EventFactory.onLivingHeal(this, healAmount);
                if (healAmount <= 0) return 0.0F;
                float f = this.getHealth();
                if (f > 0.0F)
                {
                    this.setHealth(f + healAmount);
                }
            }
            base.Health(healAmount);
            return healAmount;
        }
        /// <summary>
        /// 设置最大攻击伤害
        /// </summary>
        /// <param name="value"></param>
        public virtual void setMaxDamage(float value)
        {
            if (currentDamage <= 0)
            {
                //Debug.Log("最大伤害值不能小于等于0");
                return;
            }
            if (dataManagers == null) return;
            if (dataManagers != null)
            {
                currentDamage = MaxDamage;
                dataManagers.set<float>(MAXDAMAGE, MaxDamage);
            }
        }

        /// <summary>
        /// 获取攻击伤害
        /// </summary>
        /// <returns>返回伤害数值</returns>
        public virtual float getDamage()
        {
            if (currentDamage <= 0)
            {
                return 0.0F;
            }
            if (dataManagers == null)
            {
                return 0;
            }
            currentDamage = dataManagers.get<float>(CURRENTDAMAGE);
            return currentDamage;
        }
        /// <summary>
        /// 攻击实体方法，处理实体受到的伤害
        /// </summary>
        /// <param name="source">需要伤害来源</param>
        /// <param name="value">需要伤害值</param>
        /// <returns>返回是否造成伤害</returns>
        /*public override bool attackEntityForm(DamageSource source, float value)
        {
            if (!ForgeHooks.onLivingAttack(this, source, value)) return false;
            EntityLivingBase livingBase = this;
            if (livingBase == null) return false;
            if(!Events.getGlobalMarkEvent())
            {
                if(!livingBase.invulnerable||!livingBase.isDead)
                {
                    float finalDamage = ApplyDamageReduction(source, value);
                    if(source.DeadlyDamage)
                    {
                        finalDamage = livingBase.getMaxHealth() * 0.65F;
                        source.setDeadlyDamageisArmor();
                    }
                    float currentHealth = livingBase.getHealth();
                    float newHealth = MathF.Max(currentHealth - finalDamage, 0.0F);
                    if(float.IsNaN(newHealth))
                    {
                        livingBase.setDeath();
                        livingBase.world.removeEntity(this);
                        Debug.LogWarning($"{this}的生命值为IsNaN，以诛杀");
                    }
                    livingBase.setHealth(newHealth);
                    if(newHealth<=0)
                    {
                        livingBase.setDeath();
                        this.onDeath(source);
                        return true;
                    }
                }
                return true;
            }
            return false;
        }*/
        public override bool attackEntityForm(DamageSource source, float value)
        {
            //通过父类的getEntity()方法获取当前实体，并检查实体是否不为null（this!=null是冗沉的，但选择保留，避免getEntity是null）
            if (getEntity() != null && this != null)
            {
                //如果攻击事件未被取消或全局事件不为假时，则攻击无效
                if ((!ForgeHooks.onLivingAttack(this, source, value)) && Events.isGlobalMarEvent()) return false;
                //如果实体处于无敌状态或实体已死亡时
                if (this.isEntityInvulnerable(source) && this.getHealth() <= 0 && !this.isDead)
                {
                    return false;
                }
                if (!Events.getGlobalMarkEvent())
                {
                    EntityLivingBase livingBase = this;
                    float finalDamage = ApplyDamageReduction(source, value);
                    if (source.DeadlyDamage)
                    {
                        finalDamage = livingBase.getMaxHealth() * 0.65F;
                        source.setDeadlyDamageisArmor();
                    }
                    float currentHealth = livingBase.getHealth();
                    float newHealth = Mathf.Max(currentHealth - finalDamage, 0.0F);
                    if (float.IsNaN(newHealth))
                    {
                        livingBase.setDeath();
                        livingBase.world.removeEntity(this);
                        Debug.LogWarning($"{this}的生命值为IsNaN，以诛杀");
                    }
                    livingBase.setHealth(newHealth);
                    if (newHealth <= 0)
                    {
                        livingBase.setDeath();
                        this.onDeath(source);
                        return true;
                    }
                }
                return true;
            }
            return false;
        }

        public override bool attackEntityForm(float value, DamageSource source)
        {
            //通过父类的getEntity()方法获取当前实体，并检查实体是否不为null（this!=null是冗沉的，但选择保留，避免getEntity是null）
            if (getEntity() != null && this != null)
            {
                //如果攻击事件未被取消或全局事件不为假时，则攻击无效
                if ((!ForgeHooks.onLivingAttack(this, source, value)) && Events.isGlobalMarEvent()) return false;
                //如果实体处于无敌状态或实体已死亡时
                if (this.isEntityInvulnerable(source) && this.getHealth() <= 0 && !this.isDead)
                {
                    return false;
                }
                if (!Events.getGlobalMarkEvent())
                {
                    EntityLivingBase livingBase = this;
                    float finalDamage = ApplyDamageReduction(source, value);
                    if (source.DeadlyDamage)
                    {
                        finalDamage = livingBase.getMaxHealth() * 0.65F;
                        source.setDeadlyDamageisArmor();
                    }
                    float currentHealth = livingBase.getHealth();
                    float newHealth = Mathf.Max(currentHealth - finalDamage, 0.0F);
                    if (float.IsNaN(newHealth))
                    {
                        livingBase.setDeath();
                        livingBase.world.removeEntity(this);
                        Debug.LogWarning($"{this}的生命值为IsNaN，以诛杀");
                    }
                    livingBase.setHealth(newHealth);
                    if (newHealth <= 0)
                    {
                        livingBase.setDeath();
                        this.onDeath(source);
                        return true;
                    }
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// 用于应用伤害减免逻辑，目前只是一个占位符
        /// </summary>
        /// <param name="source"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public virtual float ApplyDamageReduction(DamageSource source, float value)
        {
            if (Armores != 0 && source.isDamageAbsolute() && source.isUnblockable())
            {
                float damage = this.getDamage() - Armores;
                damage = value;
                return damage;
            }
            if (Defense != 0 && source.isUnblockable() && source.isDamageAbsolute())
            {
                float damage = this.getDamage() * (1 - Super.Clamped(Defense / 100.0F, 0, 0.8F));
                damage = value;
                return damage;
            }
            return value;
        }
        /// <summary>
        /// 当死亡时调用，处理死亡逻辑，也允许直接调用
        /// </summary>
        /// <param name="source"></param>
        public override void onDeath(DamageSource source)
        {
            if (!ForgeHooks.onLivingDeath(this, source)) return;
            //如果生物未死亡
            if (!this.isDead)
            {
                //获取伤害来源实体
                Entity entity = source.getTrueSource();
                //获取当前攻击的实体对象
                EntityLivingBase entityLivingBase = this.getAttackingEntity();
                //如果当前生物不为空或生物不处于无敌状态
                if (entityLivingBase != null || !this.isEntityAlive())
                {
                    //设置生物为死亡
                    this.dead = true;
                    //创建生物死亡事件
                    LivingBaseDeathEvent deathEvent = new LivingBaseDeathEvent(this, source);
                    //发布生物死亡事件
                    EventBus.Publish(deathEvent);
                    //获取全局事件变量
                    if (deathEvent.getGlobalMarkEvent())
                    {
                        //如果全局事件不是true
                        if (!deathEvent.getGlobalMarkEvent())
                        {
                            //返回不执行
                            return;
                        }
                    }
                    //如果实体已经被确定为强制死亡
                    if (this.forceDead)
                    {
                        this.setDeath();
                    }
                    //如果不可见是false时
                    if (!isActive)
                    {
                        //将是否不可见设置为true
                        this.isActive = true;
                    }
                    this.isRecycle = true;
                    // 如果该生物有掉落物时
                    this.spawnItemEntity(entity);
                }
            }
            // 如果无法标记为死亡则调用setDeath()方法强制标记为死亡状态
            this.setDeath();
        }
        /// <summary>
        /// 更新闪避逻辑
        /// </summary>
        private void UpdateDodge()
        {
            // 基础闪避逻辑，如果闪避值大于一定阈值，则设置为闪避成功
            float effectiveDodge = (float)Super.Clamped(Dodge, 0, 30);
            //不使用Unity的Random.Range方法而是我自定义的方法
            isDodge = effectiveDodge > Super.Clamps(0, 100);
        }
        //真实闪避
        /// <summary>
        /// 真实闪避逻辑，计算是否闪避成功
        /// </summary>
        public virtual void OunDodge()
        {
            //如果是必中
            if (this.isSkill)
            {
                this.setDamage(0.0F);//设置伤害值为0
            }
            float effectedDodge = (float)Super.Clamped(Dodge, 0, 50);
            //不使用Unity的Random.Range方法而是我自定义的方法
            isDodge = effectedDodge > Super.Clamps(1, 50);
        }
        //基础减免逻辑
        /// <summary>
        /// 基础伤害减免逻辑，应用盔甲和防御值
        /// </summary>
        /// <param name="source">伤害来源</param>
        /// <param name="value">伤害值</param>
        /// <returns>返回减免的伤害值</returns>
        public virtual float ApplyDamagedReduction(DamageSource source, float value)
        {
            float currentDamaged = value;
            UpdateDodge();
            if (isDodge && !isSkill)
            {
                return 0;
            }
            Armors(currentDamaged, source);
            Defens(currentDamaged, source);
            return Mathf.Max(currentDamaged, 0);
        }
        //盔甲逻辑，这只是个半成品
        public virtual void Armors(float va1, DamageSource source)
        {
            if (source == null) return;
            if (source.isDamageAbsolute() || source.isUnblockable())
            {
                return;
            }
            if (Armores > 0)
            {
                damage = Armores < 1 ? damage * 0.99F : Mathf.Max(damage - Armores, 0);
            }
        }
        //防御逻辑，只是一个半成品
        public virtual void Defens(float va2, DamageSource damageSource)
        {
            if (damageSource == null) return;
            if (damageSource.isDamageAbsolute() || damageSource.isUnblockable())
            {
                return;
            }
            if (Defense > 0)
            {
                damage *= (1 - Super.Clamped(Defense / 100.0F, 0, 0.8F));
            }
        }
        public virtual void spawnItemEntity(Entity entity)
        {
            Entity entity1 = (ItemEntity)entity;
            if (entity1 != null)
            {
                if (entity1.currentVlaue == 0)
                {
                    entity1.setDeath();
                }
                else
                {
                    //Debug.LogError("你确定实体物品是实体的子类吗？");
                }
            }
        }
        /// <summary>
        /// 枚举类型，表示随机值的范围
        /// </summary>
        public enum Left
        {
            None = 0,
            One = 1,
            Two = 2,
            Three = 3,
            Fourth = 4,
            Five = 5,
            Six = 6,
            Seven = 7,
            Eigh = 8,
            Nine = 9,
            Ten = 10,
        }
        /// <summary>
        /// 获取防御值
        /// </summary>
        public virtual void getDefense()
        {
            if (Defense > 0)
            {
                // 生成一个0到2之间的随机数并限制在[0, 2]的范围内
                int randomLeftValue = (int)Super.Clamps((float)new System.Random().NextDouble() * 3, 2); // 生成 0 到 2 的随机值
                Left randomLeft = (Left)randomLeftValue;

                if (randomLeft == Left.One)
                {
                    Armors(Armores, source);  // 执行 Armors 操作
                }
                else if (randomLeft == Left.Two)
                {
                    Defens(Defense, source);  // 执行 Defens 操作
                }
            }
        }
        public virtual EntityLivingBase getAttackingEntity()
        {
            return (EntityLivingBase)this;
        }
        public virtual void setDamage(float value)
        {
            float clamped = Super.Clamped(value, 0, MaxDamage);
            this.dataManagers.set<float>(CURRENTDAMAGE, clamped);
            this.damage = clamped;
        }
        public virtual float getMaxDamage()
        {
            MaxDamage = dataManagers.get<float>(MAXDAMAGE);
            return MaxDamage;
        }
        public virtual void TakeDamage(float value, float amount)
        {
            //如果实体未被标记为无敌或者实体并不处于无敌状态时
            if (!this.getEntityAlive() || !entity.isDead)
            {
                // 处理受到的伤害
                float newHealth = Mathf.Max(entity.getHealth() - amount, 0);
                entity.setHealth(newHealth);
                if (newHealth <= 0)
                {
                    this.onDeath(source);
                }
            }
            if (entity.isEntityAlive())
            {
                return;
            }
        }
        public virtual ItemStack getItemStackFromSlot(EntityEquipmentSlot slotIn)
        {
            return ItemStack.EMPTY;
        }
    }
}
