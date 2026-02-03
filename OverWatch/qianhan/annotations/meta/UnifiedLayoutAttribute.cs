namespace InfinityMemoriesEngine.OverWatch.qianhan.annotations.meta
{
    [AttributeUsage(AttributeTargets.Struct | AttributeTargets.Field | AttributeTargets.Enum)]
    public class UnifiedLayoutAttribute : Attribute
    {
        public int Pack { get; }
        public Type? UnderlyingType { get; }

        public UnifiedLayoutAttribute(int pack, Type? underlyingType = null)
        {
            Pack = pack;
            UnderlyingType = underlyingType;
        }
    }
}
