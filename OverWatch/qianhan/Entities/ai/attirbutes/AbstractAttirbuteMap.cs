using InfinityMemoriesEngine.OverWatch.qianhan.Util.Base;

namespace InfinityMemoriesEngine.OverWatch.qianhan.Entities.ai.attirbutes
{
    public class AbstractAttributeMap
    {
        protected Dictionary<IAttribute, IAttributeInstance> attributes = new Dictionary<IAttribute, IAttributeInstance>();
        public IAttributeInstance getAttirbuteInstance(IAttribute attribute)
        {
            return this.attributes.Get(attribute);
        }
    }
    public class AttributeInstance : IAttributeInstance
    {
        private readonly IAttribute attribute;
        private double baseValue;
        public AttributeInstance(IAttribute attribute)
        {
            this.attribute = attribute;
            this.baseValue = attribute.getDefaultValue();
        }
        public IAttribute getAttribute()
        {
            return attribute;
        }

        public double getAttributeValue()
        {
            return attribute.clampValue(baseValue);
        }

        public double getBaseValue()
        {
            return baseValue;
        }
        public void setBaseValue(double baseValue)
        {
            this.baseValue = baseValue;
        }
    }
    public class AttirbuteMap : AbstractAttributeMap
    {
        public AttirbuteMap()
        {
        }
        private readonly Dictionary<string, IAttributeInstance> instances = new();

        public IAttributeInstance get(IAttribute attr)
        {
            return instances.TryGetValue(attr.Name, out var inst)
                ? inst
                : null;
        }

        public IAttributeInstance register(IAttribute attr)
        {
            if (!instances.TryGetValue(attr.Name, out var inst))
            {
                inst = new AttributeInstance(attr);
                instances[attr.Name] = inst;
            }
            return inst;
        }
    }
}
