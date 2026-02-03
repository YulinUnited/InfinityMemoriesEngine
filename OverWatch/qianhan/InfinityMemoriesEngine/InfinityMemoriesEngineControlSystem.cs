
using InfinityMemoriesEngine.OverWatch.qianhan.Events;
using InfinityMemoriesEngine.OverWatch.qianhan.Log.lang;

namespace InfinityMemoriesEngine.OverWatch.qianhan.InfinityMemoriesEngine
{
    public static class InfinityMemoriesEngineControlSystem
    {
        private static Event @event = new Event();

        public static void main()
        {
            if(!@event.getGlobalMarkEvent())
            {
                Debug.LogError($"{@event.getGlobalMarkEvent()}为true，无法运行，请通过“全局事件”按钮将全局事件标记为false");
                return;
            }
            else
            {
                Debug.Log("Infinity Memories Engine Control System 已启动，欢迎使用无限回忆引擎！");
                
            }
        }
    }
}
