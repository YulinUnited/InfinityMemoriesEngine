namespace InfinityMemoriesEngine.OverWatch.qianhan.Entities.Attirbuted
{
    /// <summary>
    /// 属性抗性解析器接口
    /// </summary>
    public interface IAttributeResistanceResolver
    {
        /// <summary>
        /// 同特殊属性之间的克制倍率
        /// </summary>
        /// <param name="attacker"></param>
        /// <param name="defender"></param>
        /// <returns></returns>
        float getResistanceMultiplier(SpecialAttributes special, SpecialAttributes attributes);
        /// <summary>
        /// 特殊属性与元素之间的克制关系
        /// </summary>
        /// <param name="attributes"></param>
        /// <param name="element"></param>
        /// <returns></returns>
        float getResistanceMultiplier(SpecialAttributes attributes, ElementAttribute element);
        /// <summary>
        /// 同属性之间的克制关系
        /// </summary>
        /// <param name="special"></param>
        /// <param name="attributes"></param>
        /// <returns></returns>
        float getResistanceMultipliers(ElementAttribute special, ElementAttribute attributes);
        /// <summary>
        /// 设置同特殊属性的克制倍率，非强制性
        /// </summary>
        /// <param name="attacker"></param>
        /// <returns></returns>
        void setResistanceMultiplier(SpecialAttributes attributes, SpecialAttributes special, float value);
        /// <summary>
        /// 设置同元素的克制倍率，非强制性
        /// </summary>
        /// <param name="element"></param>
        /// <param name="attribute"></param>
        /// <param name="value"></param>
        void setResistanceMultiplier(ElementAttribute element, ElementAttribute attribute, float value);
        /// <summary>
        /// 设置特殊属性与元素之间的克制倍率，非强制性
        /// </summary>
        /// <param name="special"></param>
        /// <param name="attribute"></param>
        /// <param name="value"></param>
        void setResistanceMultiplier(SpecialAttributes special, ElementAttribute attribute, float value);
    }
}
