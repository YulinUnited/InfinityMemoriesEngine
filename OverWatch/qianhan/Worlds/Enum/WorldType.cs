namespace InfinityMemoriesEngine.OverWatch.qianhan.Worlds.Enum
{
    public enum WorldType
    {
        Default = 0,
        SpecialWorld = 1,
        CustomWorld = 2,
    }
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class WorldLoaderAttribute : Attribute
    {
        public WorldType WorldType { get; }

        public WorldLoaderAttribute(WorldType worldType)
        {
            WorldType = worldType;
        }
    }
}
