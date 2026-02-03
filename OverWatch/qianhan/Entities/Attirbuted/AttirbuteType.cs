namespace InfinityMemoriesEngine.OverWatch.qianhan.Entities.Attirbuted
{
    public class AttributeType
    {
        public PrimaryAttribute Primary;
        public SpecialAttributes Attributes;
        public ElementAttribute Element;
        private Type Type;
        internal static readonly Type[] ValidTypes = new Type[]
        {
            typeof(PrimaryAttribute),
            typeof(SpecialAttributes),
            typeof(ElementAttribute)
        };
        public AttributeType(Type type)
        {
            if (!ValidTypes.Contains(type))
            {
                throw new ArgumentException("属性必须从主属性、特殊属性或元素属性中选取");
            }
            Type = type;
        }
        public void AttributeTypes(SpecialAttributes attributes, ElementAttribute element)
        {
            if (attributes == SpecialAttributes.Null)
            {
                throw new ArgumentException("属性不能为Null");
            }
            if (element == ElementAttribute.None)
            {
                throw new ArgumentException("属性不能为Null");
            }
            Attributes = attributes;
            Element = element;
        }
    }
}
