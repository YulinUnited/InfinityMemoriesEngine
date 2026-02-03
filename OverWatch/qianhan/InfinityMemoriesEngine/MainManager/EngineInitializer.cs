using InfiniteMemoriesEngine.OverWatch.qianhan.Bytes;
using InfinityMemoriesEngine.OverWatch.qianhan.GarbageCollection;
using InfinityMemoriesEngine.OverWatch.qianhan.LightCarriers;
using InfinityMemoriesEngine.OverWatch.qianhan.MainCollection.Colliders;

namespace InfinityMemoriesEngine.OverWatch.qianhan.InfinityMemoriesEngine.MainManager
{
    internal static class EngineInitializer
    {
        public static void InitAll()
        {
            AIManager.Init();
            MainWorldManager.Init();
            MainLifecycleManager.Init();
            MixinGC.MixinGC_Init((size_t)8192 );//分配8GB的混合垃圾回收堆
            LightCarrier.LC_Init((double)12, callback: new NULL());
        }
    }
}
