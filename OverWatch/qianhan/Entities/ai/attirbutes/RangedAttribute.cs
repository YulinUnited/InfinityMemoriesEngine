using InfinityMemoriesEngine.OverWatch.qianhan.Entities.xuechengai.game;
using Nullable = InfinityMemoriesEngine.OverWatch.qianhan.annotations.Nullable;

namespace InfinityMemoriesEngine.OverWatch.qianhan.Entities.ai.attirbutes
{
    public class RangedAttribute : BaseAttribute
    {
        private double minimumValue;
        public double maximumValue;
        private string description;

        public RangedAttribute([Nullable] IAttribute parentIn, String unlocalizedNameIn, double defaultValue, double minimumValueIn, double maximumValueIn) : base(parentIn, unlocalizedNameIn, defaultValue)
        {
            this.minimumValue = minimumValueIn;
            this.maximumValue = maximumValueIn;

            if (minimumValueIn > maximumValueIn)
            {
                throw new ArgumentException("Minimum value cannot be bigger than maximum value!");
            }
            else if (defaultValue < minimumValueIn)
            {
                throw new ArgumentException("Default value cannot be lower than minimum value!");
            }
            else if (defaultValue > maximumValueIn)
            {
                throw new ArgumentException("Default value cannot be bigger than maximum value!");
            }
        }

        public RangedAttribute setDescription(String descriptionIn)
        {
            this.description = descriptionIn;
            return this;
        }

        public String getDescription()
        {
            return this.description;
        }

        public override double clampValue(double value)
        {
            value = MathHelper.clamp(value, this.minimumValue, this.maximumValue);
            return value;
        }
    }
}
