namespace InfinityMemoriesEngine.OverWatch.qianhan.Events.fml.relauncher
{
    public static class Sides
    {
        public static bool isServer() { return getSide() != Side.CLIENT; }
        public static bool isClient() { return getSide() == Side.CLIENT; }
        public enum Side
        {
            CLIENT,
            SERVER,
            CHIENT
        }
        public static Side getSide()
        {
            return Side.CLIENT;
        }
    }
}
