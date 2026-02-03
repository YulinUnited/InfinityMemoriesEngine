using System.Runtime.InteropServices;
using InfinityMemoriesEngine.OverWatch.qianhan.Util;

namespace InfinityMemoriesEngine.OverWatch.qianhan.LightCarriers
{

    /// <summary>
    /// 光携线程系统，依赖于double精度计时发布线程，请在合适的时机发布线程，并在程序退出前停止线程。
    /// </summary>
    public static class LightCarrier
    {
        /// <summary>
        /// Tick回调函数签名
        /// </summary>
        /// <param name="tickSec"></param>
        /// <param name="frameIndex"></param>
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void TickCallback(double tickSec, ulong frameIndex);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void TickCallbacks(double tickSec, ulong frameIndex);


        /*
	 * LightCarrier - 高精度可中断调度线程（重写版）
	 *
	 * 导出接口：
	 *   typedef void (*TickCallback)(double tickIntervalSec);
	 *   int  StartLightCarrierThread(TickCallback cb, double tickIntervalSec);
	 *   void StopLightCarrierThread(void);
	 *   void UpdateMainTick(double tickIntervalSec);
	 *
	 * 注意：
	 * - tickIntervalSec 表示每次回调的目标间隔（秒，例如 0.0166667 表示 60Hz）
	 * - 回调在到达预定时间点时被调用（或当 stop 事件唤醒中断）
	 * - 回调在一个保护框架中调用（线程内捕捉异常），避免抛出未处理异常导致线程崩溃
	 */

		/// <summary>
		/// 启动光携线程系统
		/// </summary>
		/// <param name="cb">Tick回调签名</param>
		/// <param name="tickIntervalSec">回调时间间隔/秒</param>
		/// <returns>是否启动光携线程系统，1启动成功，0启动失败</returns>
        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int StartLightCarrierThread(TickCallback cb, double tickIntervalSec);

		[DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
		public static extern void StopLightCarrierThread();

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
		public static extern void UpdateMainTick(double tickIntervalSec);


		/// <summary>
		/// 非堵塞版光携线程系统
		/// </summary>
		/// <param name="mainTick"></param>
		/// <param name="callback"></param>

		[DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
		public static extern void LC_Init(double mainTick, TickCallbacks callback);

		[DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
		public static extern void LC_Stop();

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
		public static extern void LC_Update(double deltaTime);

		[DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
		public static extern void LC_SetMainTick(double newTick);

    }
}
