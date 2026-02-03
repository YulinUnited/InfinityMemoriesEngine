using System.Diagnostics;
using Debug = InfinityMemoriesEngine.OverWatch.qianhan.Log.lang.Debug;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;
using InfiniteMemoriesEngine.OverWatch.qianhan.Bytes;
using InfinityMemoriesEngine.OverWatch.qianhan.GarbageCollection;

namespace InfinityMemoriesEngine.OverWatch.qianhan.Objects
{
    /// <summary>
    /// MainObject类，基础类，除了World、Item和Entity以及部分基础类外或多或少都需要继承它
    /// </summary>
    public unsafe class MainObject : object
    {
        //默认0字节
        internal int AlloctedSizeKB = 0;
        //上限11.765625MB
        internal const int MaxAllocSizeKB = 12048;
        /// <summary>
        /// 对象ID
        /// </summary>

        public long objectId;
        /// <summary>
        /// 对象nuint指针
        /// </summary>

        public nuint_8 nativePtr;
        /// <summary>
        /// 对象名称
        /// </summary>

        public string? objectName;

        /// <summary>
        /// 主时刻
        /// </summary>
        public static double MainTick { get; } = 0;

        internal static double MainMaxTick { private set; get; } = 50000;
        /// <summary>
        /// 当前的主时刻Tick
        /// </summary>

        public static long MainCurrentTick { internal set; get; } = 0;
        /// <summary>
        /// 类型
        /// </summary>
        protected static Type? type;

        internal object? obj;
        /// <summary>
        /// 目标是否处于可见
        /// </summary>
        public bool Active { set; get; }
        /// <summary>
        /// 目标是否存活
        /// </summary>
        public bool isAlive { set; get; }
        /// <summary>
        /// 目标是否是一个对象
        /// </summary>
        public bool isObject { get; set; }
        /// <summary>
        /// 目标是否确定为移除
        /// </summary>
        public bool isRemove { get; set; }

        /// <summary>
        /// 设置当前的最大Tick值，默认值为50000，范围为0-5000之间，超过范围则设置为1000
        /// </summary>
        /// <param name="tick">最大时刻</param>
        public void setMaxTick(double tick)
        {
            MainMaxTick = (tick > 5000 || tick <= 0) ? 1000 : tick;
        }
        /// <summary>
        /// 获取当前的最大Tick值
        /// </summary>
        /// <returns></returns>
        public double getMaxTick()
        {
            return MainMaxTick;
        }

        /// <summary>
        /// 这是通过引用系统自带的kerne132.dll来释放内存的
        /// </summary>
        /// <param name="hProcess"></param>
        /// <param name="dwMinimumWorkingSetSize"></param>
        /// <param name="dwMaximumWorkingSetSize"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll")]
        public static extern bool SetProcessWorkingSetSize(IntPtr hProcess, int dwMinimumWorkingSetSize, int dwMaximumWorkingSetSize);

        /// <summary>
        /// 如果上面的移除无效的情况下，就使用这个方法,警告，这属于极端方式，可能会导致线程全局暂停或其他问题，请谨慎使用
        /// </summary>
        public static void ForceGCCollect(bool @bool)
        {
            if (@bool)
            {
                try
                {
                    GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced, blocking: true, compacting: true);
                    GC.WaitForPendingFinalizers();
                    GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced, blocking: true, compacting: true);

                    using (Process p = Process.GetCurrentProcess())
                    {
                        SetProcessWorkingSetSize(p.Handle, -1, -1);
                    }
                }
                catch (Exception ex)
                {
                    Debug.LogError("Aggressive memory trim failed: " + ex.Message);
                }
            }
        }
        /// <summary>
        /// 移除对象，释放内存，以C#指针模式，相对于温和的释放方式，这个方法会直接释放内存
        /// </summary>
        public virtual unsafe void remove()
        {
            if (isRemove)
            {
                return;
            }
            isRemove = true;

            nuint ptrvalue = nativePtr;
            if (ptrvalue == nuint.Zero)
            {
                return;
            }
            nativePtr = nuint.Zero;
            void* ptr = (void*)ptrvalue;
            MixinGCControl.free(ptr);
        }
        /// <summary>
        /// 利用MixinGC进行内存分配
        /// </summary>
        /// <param name="ptr">分配的对象大小</param>
        public virtual void MallocMemory(size_t ptr)
        {
            MixinGCControl.malloc(ptr);
        }

        /// <summary>
        /// 这只是一个简写方法，返回一个空的类类型列表
        /// </summary>
        /// <returns></returns>
        public static List<object> getClassTypes()
        {
            return new List<object>();
        }
        /// <summary>
        /// 获取类类型列表，并对其中的MainObject类型进行内存移除操作
        /// </summary>
        public static void getClassType()
        {
            List<Object> classTypes = new List<Object>();

            foreach (var type in classTypes)
            {
                if (type is MainObject mainObject)
                {
                    mainObject.remove();
                }
            }

        }

    }
    /// <summary>
    /// MemoryOpsEnhancer类，内存操作增强类，提供了更高效的内存清理方法
    /// </summary>
    public class MemoryOpsEnhancer : MainObject
    {
        /// <summary>
        /// 使用AVX2指令集来清除内存，提供更高效的内存清理方法
        /// </summary>
        /// <param name="intptr">nint指针</param>
        /// <param name="size">大小</param>
        internal static unsafe void removeWithBoost(IntPtr intptr, int size)
        {
            if (Avx2.IsSupported)
            {
                AvxZeroMemory(intptr, size);
                Marshal.FreeHGlobal(intptr);
            }
        }
        /// <summary>
        /// 通过AVX2指令集来清除内存，使用256位向量操作来提高性能
        /// </summary>
        /// <param name="ptr"></param>
        /// <param name="size"></param>
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        private static unsafe void AvxZeroMemory(IntPtr ptr, int size)
        {
            var zero = Vector256<byte>.Zero;
            byte* p = (byte*)ptr;

            int blockSize = 32; // 每次写 256-bit（32 字节）

            int i = 0;
            for (; i <= size - blockSize; i += blockSize)
            {
                Avx2.Store(p + i, zero);
            }

            // 剩下不足 32 的部分，用普通循环补零
            for (; i < size; i++)
            {
                *(p + i) = 0;
            }
        }
    }
}
