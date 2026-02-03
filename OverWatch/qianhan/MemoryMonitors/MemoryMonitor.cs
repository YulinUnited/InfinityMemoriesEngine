using System.Runtime.InteropServices;
using InfiniteMemoriesEngine.OverWatch.qianhan.Bytes;
using InfinityMemoriesEngine.OverWatch.qianhan.Util;

namespace InfinityMemoriesEngine.OverWatch.qianhan.MemoryMonitors
{

    [StructLayout(LayoutKind.Sequential, Pack = 8, Size = 64)]
    public struct MemoryStats{
        public size_t totalAllocations;       // 分配次数
        public size_t totalFrees;             // 释放次数
        public size_t currentAllocations;     // 当前存活分配数
        public size_t peakAllocations;        // 最大同时存活分配数
        public size_t totalAllocatedBytes;    // 分配过的总字节数
        public size_t totalFreedBytes;        // 已释放总字节数
        public size_t currentAllocatedBytes;  // 当前存活字节数
        public size_t peakAllocatedBytes;     // 峰值字节数
    }

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate void MM_ITER_CB(nuint ptr, size_t size, double lastAccessTime, int collectable, nuint user);

    public unsafe static class MemoryMonitor
    {
        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern nuint mm_malloc(size_t size);
        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern void mm_free(nuint ptr);
        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern nuint mm_malloc_aligned(size_t size, size_t alignment);
        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern MemoryStats mm_getStats();
        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern void mm_resetStats(int resetPeaks);
        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern void mm_printStats();
        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern MemoryStats mm_get_Stats();
        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern void mm_dumpLeaks();
        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern void mm_setLargeAllocThreshold(size_t bytes);
        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern size_t mm_gc(double max_age_seconds, size_t min_size_to_free);
        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern void mm_iterateAllocations(MM_ITER_CB cb, nuint user);
        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int mm_touch(nuint ptr);
        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int mm_unmark_collectable(nuint ptr);
        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int mm_mark_collectable(nuint ptr);
        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern void mm_free_safe(nuint** pptr);
        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern void mm_force_release_all();
    }
}
