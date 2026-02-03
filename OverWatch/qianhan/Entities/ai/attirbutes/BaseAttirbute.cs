namespace InfinityMemoriesEngine.OverWatch.qianhan.Entities.ai.attirbutes
{
    public abstract class BaseAttribute : IAttribute
    {
        private IAttribute parent;
        private string unlocalizedName;
        private double defaultValue;
        private bool shouldWatch;

        public string Name => unlocalizedName;

        protected BaseAttribute(IAttribute parent, string name, double shouldWatch)
        {
            this.parent = parent;
            this.unlocalizedName = name;
            this.defaultValue = shouldWatch;
            if (unlocalizedName == null)
            {
                throw new ArgumentNullException("Name cannot be null");
            }
        }

        public virtual double clampValue(double value)
        {
            return value;
        }

        public double getDefaultValue()
        {
            return this.defaultValue;
        }

        public string getName()
        {
            return this.unlocalizedName;
        }

        public IAttribute getParent()
        {
            return this.parent;
        }

        public bool getShouldWatch()
        {
            return this.shouldWatch;
        }

        public BaseAttribute setShouldWatch(bool watch)
        {
            this.shouldWatch = watch;
            return this;
        }
        public int hashCode()
        {
            return this.unlocalizedName.GetHashCode();
        }
        public bool equals(object obj)
        {
            return obj is IAttribute attribute && attribute.getName().Equals(this.unlocalizedName);
        }
    }
}
