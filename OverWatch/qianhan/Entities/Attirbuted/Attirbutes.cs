namespace InfinityMemoriesEngine.OverWatch.qianhan.Entities.Attirbuted
{
    /// <summary>
    /// 主要属性，通常为角色的主属性，通常为力量、敏捷、耐力、智力等，目前存在的仅为基本属性；
    /// </summary>
    [Flags]
    public enum PrimaryAttribute
    {
        /// <summary>
        /// 没有属性
        /// </summary>
        NoAttributes = 0,
        /// <summary>
        /// 力量
        /// </summary>
        Strength = 1,
        /// <summary>
        /// 敏捷
        /// </summary>
        Agility = 2,
        /// <summary>
        /// 耐力
        /// </summary>
        Intelligence = 4,
        /// <summary>
        /// 智力
        /// </summary>
        Wisdom = 8,
    }
    /// <summary>
    /// 特殊属性，通常具备某种特殊效果的属性，它们普遍对相克和被克有额外的条件或无条件；
    /// </summary>
    [Flags]
    public enum SpecialAttributes
    {
        /// <summary>
        /// 没有属性
        /// </summary>
        Null = 0,
        /// <summary>
        /// 审判属性
        /// </summary>
        Trial = 1,
        /// <summary>
        /// 正义属性
        /// </summary>
        Justice = 2,
        /// <summary>
        /// 邪属性
        /// </summary>
        Evil = 4,
        /// <summary>
        /// 神圣/圣洁属性
        /// </summary>
        Holiness = 8,
        /// <summary>
        /// 混沌属性
        /// </summary>
        Chaos = 16,
    }
    /// <summary>
    /// 元素属性，通常为附加属性或者装备属性等，也可以拓展为角色主属性；
    /// </summary>
    [Flags]
    public enum ElementAttribute
    {
        /// <summary>
        /// 没有属性
        /// </summary>
        None = 0,
        /// <summary>
        /// 金属性
        /// </summary>
        Golden = 1 << 0,
        /// <summary>
        /// 木属性
        /// </summary>
        Wood = 1 << 1,
        /// <summary>
        /// 水属性
        /// </summary>
        Water = 1 << 2,
        /// <summary>
        /// 火属性
        /// </summary>
        Fire = 1 << 3,
        /// <summary>
        /// 土属性
        /// </summary>
        Earth = 1 << 4,
        /// <summary>
        /// 光属性
        /// </summary>
        Light = 1 << 5,
        /// <summary>
        /// 法属性
        /// </summary>
        Method = 1 << 6,
        /// <summary>
        /// 电属性
        /// </summary>
        Electricity = 1 << 7,
        /// <summary>
        /// 冰属性
        /// </summary>
        Ice = 1 << 8,
        /// <summary>
        /// 风属性
        /// </summary>
        Wind = 1 << 9,
        /// <summary>
        /// 雷属性
        /// </summary>
        Lightning = 1 << 10,
        /// <summary>
        /// 烈焰属性
        /// </summary>
        Inferno = 1 << 11,
        /// <summary>
        /// 飓风属性
        /// </summary>
        Typhoon = 1 << 12,
        /// <summary>
        /// 海啸
        /// </summary>
        Tsunmai = 1 << 13,
        /// <summary>
        /// 决峭属性
        /// </summary>
        JueQiao = 1 << 14,
        /// <summary>
        /// 暗属性
        /// </summary>
        Dark = 1 << 15,
        /// <summary>
        /// 特殊内部标记，避免与位运算冲突
        /// </summary>
        Alls = (1 << 16) - 1,
        /// <summary>
        /// 所有元素属性
        /// </summary>
        All = Golden | Wood | Water | Fire | Earth | Light | Method | Electricity | Ice | Wind | Lightning | Inferno | Typhoon | Tsunmai | JueQiao | Dark,
    }
}
