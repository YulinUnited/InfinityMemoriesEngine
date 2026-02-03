using InfinityMemoriesEngine.OverWatch.qianhan.Worlds.loads;

namespace InfinityMemoriesEngine.OverWatch.qianhan.InfinityMemoriesEngine.MainManager
{
    internal static class MainWorldManager
    {
        private static WorldLoader worldLoader;
        public static void Init()
        {
            worldLoader = new WorldLoader();
        }
        public static void Update()
        {
            if (!WorldLoader.Yes)
            {
                WorldLoader.OldLoadWorlds();
            }
        }
    }
}
