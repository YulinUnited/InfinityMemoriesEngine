namespace InfinityMemoriesEngine.OverWatch.qianhan.Items.Enums
{
    /// <summary>
    /// 物品枚举
    /// </summary>
    [Flags]
    public enum ItemEnums
    {
        /// <summary>
        /// 物品类别
        /// </summary>
        ItemType = 1 << 1,
        /// <summary>
        /// 物品稀有度
        /// </summary>
        ItemRarity = 1 << 2,
        /// <summary>
        /// 物品分类
        /// </summary>
        ItemCategory = 1 << 3,
        /// <summary>
        /// 物品子分类
        /// </summary>
        ItemSubCategory = 1 << 4,
        /// <summary>
        /// 物品属性
        /// </summary>
        ItemQuality = 1 << 5,
        /// <summary>
        /// 物品材料
        /// </summary>
        ItemMaterial1 = 1 << 6,
        /// <summary>
        /// 物品效果
        /// </summary>
        ItemEffect = 1 << 7,
        /// <summary>
        /// 物品属性
        /// </summary>
        ItemAttribute = 1 << 8,
        /// <summary>
        /// 物品等级
        /// </summary>
        ItemLevel = 1 << 9,
        /// <summary>
        /// 物品耐久度
        /// </summary>
        ItemDurability = 1 << 10,
        /// <summary>
        /// 物品重量
        /// </summary>
        ItemWeight = 1 << 11,
        /// <summary>
        /// 物品ID
        /// </summary>
        ItemValue = 1 << 12,
        /// <summary>
        /// 物品描述
        /// </summary>
        ItemDescription = 1 << 13,
        /// <summary>
        /// 物品图标
        /// </summary>
        ItemIcon = 1 << 14,
        /// <summary>
        /// 物品名称
        /// </summary>
        ItemName = 1 << 15
    }
    /// <summary>
    /// 物品类型
    /// </summary>
    [Flags]
    public enum ItemTypes
    {
        /// <summary>
        /// 武器
        /// </summary>
        Weapon = 1,
        /// <summary>
        /// 防具
        /// </summary>
        Armor = 2,
        /// <summary>
        /// 饰品
        /// </summary>
        Accessory = 3,
        /// <summary>
        /// 材料
        /// </summary>
        Material = 4,
        /// <summary>
        /// 药水
        /// </summary>
        Potion = 5,
    }
    /// <summary>
    /// 十八般兵器以及...未知
    /// </summary>
    [Flags]
    public enum WeaponMaterial
    {
        /// <summary>
        /// 无
        /// </summary>
        None = 0,
        /// <summary>
        /// 刀
        /// </summary>
        Knife = 1,
        /// <summary>
        /// 枪
        /// </summary>
        Gun = 2,
        /// <summary>
        /// 长枪
        /// </summary>
        Spear = 3,
        /// <summary>
        /// 剑
        /// </summary>
        Sword = 4,
        /// <summary>
        /// 弓
        /// </summary>
        Bow = 5,
        /// <summary>
        /// 戟
        /// </summary>
        Halberd = 6,
        /// <summary>
        /// 钩
        /// </summary>
        Hook = 7,
        /// <summary>
        /// 钺
        /// </summary>
        Yue = 8,
        /// <summary>
        /// 斧
        /// </summary>
        Axe = 9,
        /// <summary>
        /// 叉
        /// </summary>
        Fork = 10,
        /// <summary>
        /// 镋
        /// </summary>
        Tang = 11,
        /// <summary>
        /// 棒
        /// </summary>
        Rod = 12,
        /// <summary>
        /// 槊
        /// </summary>
        GroupName = 13,
        /// <summary>
        /// 棍
        /// </summary>
        Stick = 14,
        /// <summary>
        /// 鞭
        /// </summary>
        Whip = 15,
        /// <summary>
        /// 锏
        /// </summary>
        Trump = 16,
        /// <summary>
        /// 锤
        /// </summary>
        Hammer = 17,
        /// <summary>
        /// 爪
        /// </summary>
        Claw = 18,
        /// <summary>
        /// 拐
        /// </summary>
        Crutch = 19,
        /// <summary>
        /// 流星锤
        /// </summary>
        Flail = 20,
        Unkonwn = 21,
    }
    /// <summary>
    /// 武器的类型
    /// </summary>
    [Flags]
    public enum WeaponType
    {
        /// <summary>
        /// 近战武器
        /// </summary>
        Melee = 1,
        /// <summary>
        /// 远程武器
        /// </summary>
        Ranged = 2,
        /// <summary>
        /// 法术武器
        /// </summary>
        Spell = 3,
    }
    /// <summary>
    /// 武器的品级
    /// </summary>
    [Flags]
    public enum WeaponRarity
    {
        /// <summary>
        /// 无
        /// </summary>
        None = 0,
        /// <summary>
        /// 普通
        /// </summary>
        Common = 1,
        /// <summary>
        /// 一般
        /// </summary>
        Fair = 2,
        /// <summary>
        /// 较好
        /// </summary>
        Fairly = 3,
        /// <summary>
        /// 很好
        /// </summary>
        VeryGood = 4,
        /// <summary>
        /// 好
        /// </summary>
        Good = 5,
        /// <summary>
        /// 优秀
        /// </summary>
        Excellent = 6,
        /// <summary>
        /// 稀有
        /// </summary>
        Rare = 7,
        /// <summary>
        /// 稀少
        /// </summary>
        Few = 8,
        /// <summary>
        /// 优质
        /// </summary>
        Superior = 9,
        /// <summary>
        /// 精品
        /// </summary>
        Boutique = 10,
        /// <summary>
        /// 史诗
        /// </summary>
        Epic = 11,
        /// <summary>
        /// 传奇
        /// </summary>
        Legend = 12,
        /// <summary>
        /// 超越
        /// </summary>
        Supereme = 13,
        /// <summary>
        /// 极限
        /// </summary>
        Extermely = 14,
        /// <summary>
        /// 强大
        /// </summary>
        CapableAndVigorous = 15,
        /// <summary>
        /// 神器
        /// </summary>
        Artifact = 21,
        /// <summary>
        /// 超神器
        /// </summary>
        SuperArtifact = 22,
        /// <summary>
        /// 古神器
        /// </summary>
        AncientArtifact = 23,
        /// <summary>
        /// 远古神器
        /// </summary>
        AncientTimesArtifact = 24,
        /// <summary>
        /// 创世神器
        /// </summary>
        TheCreationArtifact = 25,
        /// <summary>
        /// 无上神器
        /// </summary>
        PeerlessArtifact = 26,
        /// <summary>
        /// 宇宙神器
        /// </summary>
        CosmicArtifact = 27,
        /// <summary>
        /// 寰宇级神器
        /// </summary>
        UniversalLevelArtifact = 28,
        /// <summary>
        /// 寰宇神器
        /// </summary>
        UniversalArtifact = 29,
        /// <summary>
        /// 寰宇级超神器
        /// </summary>
        UniversalLevelSuperArtifact = 30,
        /// <summary>
        /// 寰宇级古神器
        /// </summary>
        UniversalLevelAncientArtifact = 31,
        /// <summary>
        /// 至高，至上，至强，至极，审判级
        /// </summary>
        Trial = 32,
    }
    /// <summary>
    /// 声明工具枚举的属性
    /// </summary>
    [AttributeUsage(AttributeTargets.All, Inherited = false)]
    public class ToolEnumAttribute : Attribute
    {
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public readonly ItemEnums? enums;
        public readonly ItemTypes? types;
        public readonly WeaponMaterial? material;
        public readonly WeaponType? weaponType;
        public readonly WeaponRarity? weaponRarity;
        /// <summary>
        /// 获取物品类型
        /// </summary>
        /// <param name="itemType"></param>
        /// <returns></returns>
        public static string GetItemType(ItemTypes itemType)
        {
            return itemType switch
            {
                ItemTypes.Weapon => "武器",
                ItemTypes.Armor => "防具",
                ItemTypes.Accessory => "饰品",
                ItemTypes.Material => "材料",
                ItemTypes.Potion => "药水",
                _ => "未知物品类型"
            };
        }
        /// <summary>
        /// 如果只是武器请用它
        /// </summary>
        /// <param name="weaponMaterial"></param>
        /// <param name="weaponType"></param>
        /// <param name="weaponRarity"></param>
        public ToolEnumAttribute(WeaponMaterial weaponMaterial, WeaponType weaponType, WeaponRarity weaponRarity)
        {
            // 构造函数
            this.material = weaponMaterial;
            this.weaponType = weaponType;
            this.weaponRarity = weaponRarity;
        }
        /// <summary>
        /// 如果需要精确到极致，请使用它
        /// </summary>
        /// <param name="itemEnums"></param>
        /// <param name="itemTypes"></param>
        /// <param name="weaponMaterial"></param>
        /// <param name="weaponType"></param>
        /// <param name="weaponRarity"></param>
        public ToolEnumAttribute(ItemEnums itemEnums, ItemTypes itemTypes, WeaponMaterial weaponMaterial
            , WeaponType weaponType, WeaponRarity weaponRarity)
        {
            // 构造函数
            this.enums = itemEnums;
            this.types = itemTypes;
            this.material = weaponMaterial;
            this.weaponType = weaponType;
            this.weaponRarity = weaponRarity;
        }
        /// <summary>
        /// 如果只是物品，请使用它
        /// </summary>
        /// <param name="itemEnums"></param>
        /// <param name="itemTypes"></param>
        public ToolEnumAttribute(ItemEnums itemEnums, ItemTypes itemTypes)
        {
            this.enums = itemEnums;
            this.types = itemTypes;
        }
    }
}
