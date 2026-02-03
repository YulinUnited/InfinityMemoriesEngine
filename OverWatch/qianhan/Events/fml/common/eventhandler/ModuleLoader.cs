using System.Reflection;

namespace InfinityMemoriesEngine.OverWatch.qianhan.Events.fml.common.eventhandler
{
    public static class ModuleLoader
    {
        public static void LoadAndSubscribe(string dllPath)
        {
            var asm = Assembly.LoadFrom(dllPath);

            // 是否声明了订阅
            var asmAttr = asm.GetCustomAttribute<SubscribeAttribute>();
            if (asmAttr != null)
            {
                Console.WriteLine($"发现模块: {asmAttr.Tag ?? asm.FullName}");
                SubscriptionManager.Initialize(asm);
            }
            else
            {
                Console.WriteLine("该DLL未声明订阅，跳过");
            }
        }
    }
}
