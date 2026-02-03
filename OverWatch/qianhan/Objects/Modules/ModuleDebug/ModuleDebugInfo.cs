using System.Diagnostics;
using System.Text;
using InfinityMemoriesEngine.OverWatch.qianhan.Events;

namespace InfinityMemoriesEngine.OverWatch.qianhan.Objects.Modules.ModuleDebug
{
    public static class ModuleDebugInfo
    {
        private static Event @event = new Event();
        public static bool EnableDebugInfo = true;
        private static readonly Dictionary<Type, string> RegistrationStack = new Dictionary<Type, string>();
        private static readonly Dictionary<Type, string> AccessStack = new Dictionary<Type, string>();

        public static void Register<T>()
        {
            //如果全局事件是true而不是false则无效
            if (!EnableDebugInfo && @event.isGlobalMark) return;
            var trace = new StackTrace(2, true);
            RegistrationStack[typeof(T)] = trace.ToString();
        }

        public static void Access<T>()
        {
            //如果全局事件是true而不是false则无效
            if (!EnableDebugInfo && @event.isGlobalMark) return;
            var trace = new StackTrace(2, true);
            AccessStack[typeof(T)] = trace.ToString();
        }
        /// <summary>
        /// 模块调试化信息
        /// </summary>
        /// <returns>当前模块调试信息</returns>
        /// <exception cref="NotImplementedException">如果全局事件是true时</exception>
        public static string DumpAllInfo()
        {
            if (!@event.isGlobalMark)
            {
                var sb = new StringBuilder();
                sb.AppendLine("=== [模块调试信息] ===");

                foreach (var kv in RegistrationStack)
                {
                    sb.AppendLine($"[注册] {kv.Key.Name}:\n{kv.Value}");
                }

                foreach (var kv in AccessStack)
                {
                    sb.AppendLine($"[访问] {kv.Key.Name}:\n{kv.Value}");
                }

                return sb.ToString();
            }
            throw new NotImplementedException($"[InfiniteMemoriesEngine]:全局事件为{@event.isGlobalMark}，请检查全局事件是否是false，请注意，如果是true则无效！！！");
        }
        public static void Clear()
        {
            if (!@event.isGlobalMark)
            {
                RegistrationStack.Clear();
                AccessStack.Clear();
            }
            return;
        }
    }
}
