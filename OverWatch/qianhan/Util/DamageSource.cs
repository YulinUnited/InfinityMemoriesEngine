using InfinityMemoriesEngine.OverWatch.qianhan.Entities;

namespace InfinityMemoriesEngine.OverWatch.qianhan.Util
{
    /// <summary>
    /// 伤害来源类
    /// </summary>
    public class DamageSource
    {
        /// <summary>
        /// 代表铁砧造成的伤害，你可以理解为从高处坠落的物体碰撞到玩家或者其他生物体造成的伤害的具体来源
        /// </summary>
        public static DamageSource ANVIL = new DamageSource("anvil");
        /// <summary>
        /// 通常用于两个物体对于生物体的挤压造成的伤害，这种伤害无视护甲和防御减免可以直接计算生命值
        /// 这是不是有点...像是被挤死的感觉？你知道的，挤死人的伤害来源
        /// 或者说，是不是有点...强人所男了？哦对，这就是强人所男的伤害来源
        /// </summary>
        public static DamageSource CRAMMING = (new DamageSource("cramming")).setDamageBypassesArmor();
        /// <summary>
        /// 溺毙伤害，通常由氧气归零时对于生命值的扣减，比如：在水中憋气时间过长，或者被淹没在水中或者其他液体中，包括在真空中等
        /// </summary>
        public static DamageSource DROWN = (new DamageSource("drown")).setDamageBypassesArmor();
        /// <summary>
        /// 从高出掉落时造成的伤害，通常是从高处掉落到地面或者其他物体上造成的伤害
        /// 告诉一下那个姓林的妹妹，你要是从高处掉下来，记得先把头放在手上上，保护好你的头部，别让它受伤了，哦天哪，她摔的可真惨
        /// </summary>
        public static DamageSource FALL = (new DamageSource("fall")).setDamageBypassesArmor();
        /// <summary>
        /// 通常由下落的方块造成的伤害，比如：从高处掉落的方块或者其他物体造成的伤害，所以，高空抛物伤人，砸的没商量；
        /// </summary>
        public static DamageSource FALLING_BLOCK = new DamageSource("fallingBlock");
        /// <summary>
        /// 艺术就是爆炸！所以，这个就是来自于艺术造成的伤害，你知道的，爆炸！
        /// </summary>
        public static DamageSource FIREWORKS = (new DamageSource("fireworks")).setExplosion();
        /// <summary>
        /// 注意交通安全，即使是飞的也一样，如果飞行的时候撞到墙壁上了，那么就会造成伤害
        /// 北京第三区交通委提醒您：道路千万条，安全第一条，行车..飞行不规范，亲人两行泪~~
        /// </summary>
        public static DamageSource FLY_INTO_WALL = (new DamageSource("flyIntoWall")).setDamageBypassesArmor();
        /// <summary>
        /// 这一个就是全体的伤害来源，通常用于一些特殊的情况，比如：被击杀时的伤害来源
        /// </summary>
        public static DamageSource GENERAL = new DamageSource("general");
        /// <summary>
        /// 作为通用伤害来源，嗯，这就是一个通用的伤害来源，你还指望我去介绍它？别妄想了
        /// </summary>
        public static DamageSource GENERIC = (new DamageSource("generic")).setDamageBypassesArmor();
        /// <summary>
        /// 红色是热烈，炽热的爱，炽热的心，炽热的情，炽热的火焰，炽热的温度，炽热的伤害来源
        /// </summary>
        public static DamageSource HOT_FLOOR = (new DamageSource("hotFloor")).setFireDamage();

        //伤害来源或者伤害源
        /// <summary>
        /// 致命的伤害来源
        /// </summary>
        public static DamageSource DEADLY = (new DamageSource("deadly")).setDeadlyDamageisArmor();
        /// <summary>
        /// 火的热情，火的温度，火的伤害来源
        /// </summary>
        public static DamageSource IN_FIRE = (new DamageSource("inFire")).setFireDamage();
        /// <summary>
        /// 总有些神人会把自己砌进墙里，或者把墙砌进自己里，或者把墙砌进别人里，或者把别人砌进墙里，或者把墙砌进墙里，或者把墙砌进天上去
        /// 这就是，砌墙造成的伤害来源或在墙内的伤害来源，到底有哪个神仙会把自己砌入墙里面呢？我也不知道
        /// </summary>
        public static DamageSource IN_WALL = (new DamageSource("inWall")).setDamageBypassesArmor();
        /// <summary>
        /// 来自炽热的岩浆的伤害来源，通常用于被岩浆烧到或者被岩浆淹没在里面造成的伤害，你知道的，炽热的岩浆代表滚烫的热水，所以，多喝热水
        /// </summary>
        public static DamageSource LAVA = (new DamageSource("lava")).setFireDamage();
        /// <summary>
        /// 感受雷鸣般的轰鸣声，感受雷电般的速度，感受雷电般的伤害来源
        /// </summary>
        public static DamageSource LIGHTNING_BOLT = new DamageSource("lightningBolt");
        /// <summary>
        /// 要记住，是药三分毒，那么这个毒是什么嘞？当然是魔法伤害啦
        /// </summary>
        public static DamageSource MAGIC = (new DamageSource("magic")).setDamageBypassesArmor().setDamageIsMagicDamage();
        /// <summary>
        /// 当火焰燃烧时的伤害来源，通常用于被火焰烧到或者被火焰淹没在里面造成的伤害
        /// </summary>
        public static DamageSource ON_FIRE = (new DamageSource("onFire")).setDamageBypassesArmor().setFireDamage();
        /// <summary>
        /// 如果说什么是最绝望的死法，那么它可以算是一个，迎接真神：掉出世界的边界，没错，这就是掉出世界的边界造成的伤害来源
        /// </summary>
        public static DamageSource OUT_OF_WORLD = (new DamageSource("outOfWorld")).setDamageBypassesArmor();
        /// <summary>
        /// 至于这个嘛，你知道的，看谁不爽就发个火球，打死后那么这个火球属于什么？当然是技能，所以这个就是来自于技能的伤害来源
        /// </summary>
        public static DamageSource SKILL = (new DamageSource("skill").setExplosion().setDifficultyScaled());
        /// <summary>
        /// 呀！你已经逐渐凋亡了，没错，这就是凋零伤害来源
        /// </summary>
        public static DamageSource WITHER = (new DamageSource("wither")).setDamageBypassesArmor();
        public bool attackDamage;//是否是攻击伤害
        public string damageType;//伤害类型，以this连用表示当前伤害类型
        private Entity entity;
        //private bool isDamageAllowedInCreativeMode;//是否在操作模式受伤，这个引用自minecraft;
        private bool damageIsAbsolute;//伤害是否是绝对值
        private bool difficultyScaled;//是否随难度变幻伤害
        private bool explosion;//是否为爆炸伤害
        private bool fireDamage;//是否是火焰伤害
        private float hungerDamage = 0.1F;//饥饿伤害值
        private bool IsUnblockable;//是否不可拦截
        private bool magicDamage;//是否为法术伤害
        private bool projectile;//是否是弹射物伤害
        private bool breakThrough;//击破
        private bool superSmash;//超击破
        public bool DeadlyDamage;//致命伤害
        public DamageSource(string v)
        {
            this.damageType = v;
        }
        public DamageSource() { }
        /// <summary>
        /// 判断是否是致命伤害
        /// </summary>
        /// <returns></returns>
        public bool isDeadlyDamage()
        {
            return DeadlyDamage;
        }
        /// <summary>
        /// 判断是否为超击破
        /// </summary>
        /// <returns></returns>
        public bool isSuperSmash()
        {
            return this.superSmash;
        }
        /// <summary>
        /// 判断是否是击破
        /// </summary>
        /// <returns></returns>
        public bool isBreakThrough()
        {
            return this.breakThrough;
        }
        /// <summary>
        /// 设置伤害为超击破伤害
        /// </summary>
        /// <returns></returns>
        public DamageSource setSuperSmashDamage()
        {
            this.superSmash = true;
            return this;
        }
        /// <summary>
        /// 设置伤害为击破伤害
        /// </summary>
        /// <returns></returns>
        public DamageSource setBreakThroughDamage()
        {
            this.breakThrough = true;
            return this;
        }
        /// <summary>
        /// 设置伤害无视护甲
        /// </summary>
        /// <returns></returns>
        public DamageSource setDamageBypassesArmor()
        {
            this.IsUnblockable = true;
            return this;
        }
        /// <summary>
        /// 获取伤害类型
        /// </summary>
        /// <returns></returns>
        public string getDamageType()
        {
            return this.damageType;
        }
        /// <summary>
        /// 判断伤害是否为绝对伤害
        /// </summary>
        /// <returns></returns>
        public bool isDamageAbsolute()
        {
            return this.damageIsAbsolute;
        }
        /// <summary>
        /// 设置致命伤害无视护甲
        /// </summary>
        /// <returns></returns>
        public DamageSource setDeadlyDamageisArmor()
        {
            this.DeadlyDamage = true;
            this.damageIsAbsolute = true;
            return this;
        }
        /// <summary>
        /// 设置伤害为致命的
        /// </summary>
        /// <returns></returns>
        public DamageSource setDamageisDeadlyDamage()
        {
            this.IsUnblockable = true;
            this.damageIsAbsolute = true;
            this.DeadlyDamage = true;
            return this;
        }
        /// <summary>
        /// 获取饥饿伤害
        /// </summary>
        /// <returns></returns>
        public float getHungerDamage()
        {
            return this.hungerDamage;
        }
        /// <summary>
        /// 判断伤害是否为火焰伤害
        /// </summary>
        /// <returns></returns>
        public bool isFireDamage()
        {
            return this.fireDamage;
        }
        /// <summary>
        /// 判断伤害是否为弹射物伤害
        /// </summary>
        /// <returns></returns>
        public bool isProjectile()
        {
            return this.projectile;
        }
        /// <summary>
        /// 判断伤害是否为不可防御
        /// </summary>
        /// <returns></returns>
        public bool isUnblockable()
        {
            return this.IsUnblockable; ;
        }
        /// <summary>
        /// 设置伤害为绝对的
        /// </summary>
        /// <returns></returns>
        public DamageSource setDamageIsAbsolute()
        {
            this.damageIsAbsolute = true;
            this.hungerDamage = 0.0F;
            return this;
        }
        /// <summary>
        /// 设置为魔法伤害
        /// </summary>
        /// <returns></returns>
        public DamageSource setDamageIsMagicDamage()
        {
            this.magicDamage = true;
            entity.currentDamage = 300.0F;
            return this;
        }
        /// <summary>
        /// 设置伤害随难度改变
        /// </summary>
        /// <returns></returns>
        public DamageSource setDifficultyScaled()
        {
            this.difficultyScaled = true;
            return this;
        }
        /// <summary>
        /// 设置为爆炸伤害
        /// </summary>
        /// <returns></returns>
        public DamageSource setExplosion()
        {
            this.explosion = true;
            return this;
        }
        /// <summary>
        /// 设置为火焰伤害
        /// </summary>
        /// <returns></returns>
        public DamageSource setFireDamage()
        {
            this.fireDamage = true;
            return this;
        }

        /// <summary>
        /// 设置为虚空伤害
        /// </summary>
        /// <returns></returns>

        public DamageSource setVoidDamage()
        {
            this.difficultyScaled = false;
            entity.currentDamage = float.MaxValue;
            return this;
        }

        public virtual Entity getTrueSource()
        {
            return entity;
        }
    }
}
