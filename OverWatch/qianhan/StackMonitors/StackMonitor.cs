using System.Runtime.InteropServices;
using InfinityMemoriesEngine.OverWatch.qianhan.Util;

namespace InfinityMemoriesEngine.OverWatch.qianhan.StackMonitors
{
    /// <summary>
    /// 栈监控专属类型，它不属于任何特定的线程或进程。
    /// </summary>
    public unsafe static class StackMonitor
    {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void CSM_OverflowHandler(uint pid, uint tid, nuint usedByte, nuint thresholdBytes);

        /// <summary>
        /// 启动栈空间监控。
        /// </summary>
        /// <returns></returns>
        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int CSM_Start();
        /// <summary>
        /// 停止栈空间监控。
        /// </summary>

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern void CSM_Stop();
        /// <summary>
        /// 对指定的线程进行栈空间监控注册。
        /// </summary>
        /// <param name="pid"></param>
        /// <param name="tid"></param>
        /// <param name="thresholdBytes"></param>
        /// <returns></returns>

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int CSM_RegisterThread(uint pid, uint tid, nuint thresholdBytes);
        /// <summary>
        /// 撤销对指定线程的栈空间监控注册。
        /// </summary>
        /// <param name="tid"></param>
        /// <returns></returns>

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int CSM_UnregisterThread(uint tid);
        /// <summary>
        /// 设置栈空间溢出处理程序。
        /// </summary>
        /// <param name="handler"></param>

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern void CSM_SetOverflowHandler(CSM_OverflowHandler handler);
        /// <summary>
        /// 启动自动注册功能。
        /// </summary>
        /// <param name="enable"></param>
        /// <param name="defaultThresholdBytes"></param>
        /// <param name="enumIntervalMs"></param>

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern void CSM_EnableAutoRegister(int enable, nuint defaultThresholdBytes, uint enumIntervalMs);

        /// <summary>
        /// 设置栈空间监控的轮询间隔时间。
        /// </summary>
        /// <param name="pollMs"></param>
        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern void CSM_SetPollIntervalMs(uint pollMs);
    }
}
