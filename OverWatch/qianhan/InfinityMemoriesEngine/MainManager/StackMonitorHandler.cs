using InfinityMemoriesEngine.OverWatch.qianhan.LightCarriers;
using InfinityMemoriesEngine.OverWatch.qianhan.Log.lang;
using InfinityMemoriesEngine.OverWatch.qianhan.StackMonitors;

namespace InfinityMemoriesEngine.OverWatch.qianhan.InfinityMemoriesEngine.MainManager
{
    internal static class StackMonitorHandler
    {
        public static void Init()
        {
            StackMonitor.CSM_Start();
            StackMonitor.CSM_EnableAutoRegister(1, new nuint(1024 * 1024 * 10), 1000);
            StackMonitor.CSM_SetOverflowHandler(OnStackOverflow);
        }

        public static void OnStackOverflow(uint pid,uint tid,nuint used,nuint threshold)
        {
            Debug.LogWarning($"[警告] 进程 {pid} 线程 {tid} 栈溢出: {used}/{threshold} bytes");
        }
    }
}
