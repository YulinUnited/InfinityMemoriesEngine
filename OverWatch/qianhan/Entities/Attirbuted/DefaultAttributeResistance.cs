namespace InfinityMemoriesEngine.OverWatch.qianhan.Entities.Attirbuted
{
    /// <summary>
    /// 默认属性抗性,不具备克制属性的不会有克制倍率
    /// 请注意，如果没有实现IAttributeResistanceResolver时，引擎默认调用此逻辑
    /// </summary>
    public class DefaultAttributeResistance : IAttributeResistanceResolver
    {
        /// <summary>
        /// 获取同特殊属性之间的克制关系，默认返回-1表示克制倍率为空
        /// </summary>
        /// <param name="special"></param>
        /// <param name="attributes"></param>
        /// <returns></returns>
        public float getResistanceMultiplier(SpecialAttributes special, SpecialAttributes attributes)
        {
            return -1.0F;
        }
        /// <summary>
        /// 默认返回-1表示克制倍率为空,正义、审判克制倍率是被写死的
        /// </summary>
        /// <param name="attributes"></param>
        /// <param name="element"></param>
        /// <returns></returns>
        public float getResistanceMultiplier(SpecialAttributes attributes, ElementAttribute element)
        {
            if (attributes == SpecialAttributes.Trial && element == ElementAttribute.All)
            {
                return 1.0F;
            }
            if (attributes == SpecialAttributes.Justice && element == ElementAttribute.All)
            {
                return 1.0F;
            }
            if (attributes == SpecialAttributes.Chaos && element == ElementAttribute.All)
            {
                return 0.5F;
            }

            return -1.0F;
        }
        /// <summary>
        /// 默认返回-1表示克制倍率为空
        /// </summary>
        /// <param name="special"></param>
        /// <param name="attributes"></param>
        /// <returns></returns>
        public float getResistanceMultipliers(ElementAttribute special, ElementAttribute attributes)
        {
            return -1;
        }

        /// <summary>
        /// 除了审判和正义双属性是写死的外，其他特殊同属性一概默认为-1
        /// </summary>
        /// <param name="attributes"></param>
        /// <param name="special"></param>
        /// <param name="value"></param>
        public void setResistanceMultiplier(SpecialAttributes attributes, SpecialAttributes special, float value)
        {
            if (attributes == SpecialAttributes.Trial && special == SpecialAttributes.Trial || attributes == SpecialAttributes.Justice && special == SpecialAttributes.Justice)
            {
                value = 4.0F;
            }
        }
        /// <summary>
        /// 默认返回-1表示克制倍率为空
        /// </summary>
        /// <param name="element"></param>
        /// <param name="attribute"></param>
        /// <param name="value"></param>
        public void setResistanceMultiplier(ElementAttribute element, ElementAttribute attribute, float value)
        {

        }
        /// <summary>
        /// 默认返回-1表示克制倍率为空
        /// </summary>
        /// <param name="special"></param>
        /// <param name="attribute"></param>
        /// <param name="value"></param>
        public void setResistanceMultiplier(SpecialAttributes special, ElementAttribute attribute, float value)
        {

        }
    }
}
