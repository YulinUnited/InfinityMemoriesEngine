using System.Runtime.InteropServices;
using InfiniteMemoriesEngine.OverWatch.qianhan.Bytes;
using InfinityMemoriesEngine.OverWatch.qianhan.Entities.IsC;
using InfinityMemoriesEngine.OverWatch.qianhan.Log.lang;
using InfinityMemoriesEngine.OverWatch.qianhan.Objects.IsC;
using InfinityMemoriesEngine.OverWatch.qianhan.Util;
using static InfinityMemoriesEngine.OverWatch.qianhan.GarbageCollection.MixinGC;

namespace InfinityMemoriesEngine.OverWatch.qianhan.GarbageCollection
{

    [StructLayout(LayoutKind.Sequential, Pack = 8, Size = 40)]
    public struct MixinGCStats
    {
        public size_t total_allocated_bytes;//目标堆中分配的总字节数
        public size_t freed_bytes;//目标是否满足上次GC释放的字节数
        public uint64_t gc_count;//目标的GC运行总次数
        public double last_gc_seconds;//是否满足上次GC运行的持续时间戳
        public size_t live_object_count;//目标堆中存活对象的数量
    }

    /// <summary>
    /// 轻量弹性化GC系统，用于管理托管堆内存的分配和回收
    /// <para>
    /// 请注意，使用本系统需要开启unsafe不安全代码支持
    /// </para>
    /// MixinGC是通过业务判断是否回收，您需要在必要时解除业务对象的引用，内置紧急GC，当紧急GC启动时会无条件回收所有的分配对象，这会造成空引用异常，请务必注意。
    /// </summary>

    public unsafe static class MixinGC
    {

        /* --------------------- Delegates --------------------- */
        /// <summary>
        /// MixinGC回收策略分配
        /// </summary>
        /// <param name="stats"></param>
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate void MixinGC_OnCollect(ref MixinGCStats* stats);

        /// <summary>
        /// MixinGC分配标志枚举
        /// </summary>
        public enum MixinAllocFlags : int
        {
            MIXIN_ALLOC_NONE = 0,
            MIXIN_ALLOC_ZERO = 1 << 0,
            MIXIN_ALLOC_NO_GC = 1 << 1,
            MIXIN_ALLOC_GC_TRACKED = 1 << 2,
            MIXIN_ALLOC_FIXED = 1 << 3,
            MIXIN_ALLOC_INTERNAL = 1 << 4,
        }
        /// <summary>
        /// MixinGC回收策略枚举
        /// </summary>
        public enum GCStrategy:int
        {
            GC_STRATEGY_AGGRESSIVE,//积极回收
            GC_STRATEGY_BALANCED,//平衡回收
            GC_STRATEGY_CONSERVATIVE,//保守回收
            GC_STRATEGY_DEFERRED,//延迟回收
            GC_STRATEGY_CUSTOM,//自定义回收
            GC_STRATEGY_IMMEDIATELY//立即回收
        }

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern nint MixinGC_AllocateEx(size_t size, MixinAllocFlags flags);
        
        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MixinGC_FreeEx(nuint ptr);
        /* API surface */
        //启动和关闭GC系统

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MixinGC_Init(size_t initial_handle_capacity);

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MixinGC_Shutdown();

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        //内存分配和释放
        public static extern void* MixinGC_Allocate(nuint size);

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MixinGC_Free(nuint payload);

        //GC句柄管理
        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern GCHandle MixinGC_CreateHandle(void* payload);

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern void* MixinGC_HandleToPtr(GCHandle h);

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MixinGC_DestroyHandle(GCHandle h);
        //分配根对象

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MixinGC_RegisterRoot(GCHandle h);

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MixinGC_UnregisterRoot(GCHandle h);
        // GC控制和统计

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MixinGC_CollectNow();

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int MixinGC_StartPeriodicCollector(uint interval_seconds);

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MixinGC_StopPeriodicCollector();

        //将预估可回收内存大小返回给调用者
        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern size_t MixinGC_PredictReclaimable();

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern MixinGCStats MixinGC_GetStats();

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MixinGC_SetOnCollectCallback(MixinGC_OnCollect cb);
        //获取和设置周期性收集器的时间间隔
        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Get_GC_PeriodicCollector();

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern void Set_GC_PeriodicCollector(uint interval_seconds);

        /* Elastic GC tuning APIs */
        //设置堆内存限制和弹性阈值
        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MixinGC_SetHeapLimit(size_t bytes); /* total heap cap for pressure calc */

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MixinGC_SetElasticThreshold(double ratio); /* 0..1 */

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MixinGC_CollectIncremental(int steps);


        /* Business hook helpers:
           Default implementations provided in .c (safe stubs).
           If your payload layout differs, replace these functions in your build.
        */
        //是否是实体和主对象的检查和提取函数
        /// <summary>
        /// 是否是实体对象，请注意，void64为64位系统自带的指针类型，非托管指针，您需要开启unsafe模式才能使用；
        /// <para>
        /// void*在C#中表示一个不安全的指针类型，允许直接操作内存地址，在64位系统中，void*通常对应于大小为8，在32位系统中对应于大小为4；
        /// </para>
        /// </summary>
        /// <param name="payload">指针对象</param>
        /// <returns></returns>
        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int is_entity_object(nuint payload); /* returns nonzero if payload is an Entity */

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern Entity extract_entity_value(nuint payload); /* returns copy of entity state */

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern MainObject extract_mainobject_value(nuint payload); /* returns copy of mainobject state */

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern MixinObject extract_MixinObject_value(nuint payload);
    }

    /// <summary>
    /// 控制MixinGC回收，需要搭配回收策略
    /// </summary>
    public static unsafe class MixinGCControl
    {
        private static GCStrategy GC;

        /// <summary>
        /// 立刻让MixinGC回收托管堆内存
        /// </summary>
        public static void Collect()
        {
            if (getGCStrategy() == GCStrategy.GC_STRATEGY_IMMEDIATELY)
            {
                MixinGC.MixinGC_CollectNow();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public static void setElasticThreshold(double value)
        {
            if (value > 1.0)
            {
                Debug.Log($"{value}大于1.0，请在限定区域");
                return;
            }
            else
            {
                MixinGC.MixinGC_SetElasticThreshold(value);
            }
        }
        /// <summary>
        /// 分配一块MixinGC的托管内存
        /// </summary>
        /// <param name="cb">分配大小</param>
        public static void mallic(nuint cb)
        {
            if (cb < 0)
            {
                Debug.Log($"{cb}为0，请重新分配");
                return;
            }
            else MixinGC.MixinGC_Allocate(cb);
        }
        /// <summary>
        /// 设置GC分配策略
        /// </summary>
        /// <param name="strategy"></param>
        public static void setGCStrategy(GCStrategy strategy)
        {
            strategy = GC;
        }
        /// <summary>
        /// 获取GC分配策略
        /// </summary>
        /// <returns></returns>
        public static GCStrategy getGCStrategy()
        {
            return GC;
        }
        /// <summary>
        /// 增量回收
        /// </summary>
        /// <param name="steps">回收增量执行步数</param>
        public static void Collect(int steps)
        {
            if (steps == 0)
            {
                return;
            }
            else
            {
                if (getGCStrategy() == GCStrategy.GC_STRATEGY_BALANCED)
                {
                    MixinGC.MixinGC_CollectIncremental(steps);
                }
            }
        }
        /// <summary>
        /// 获取当前可能被GC回收的总字节数
        /// </summary>
        /// <returns>返回MixinGC的可回收字节数量</returns>
        public static UIntPtr getPredictReclaimable()
        {
            return MixinGC.MixinGC_PredictReclaimable();
        }
        /// <summary>
        /// 分配指定大小的MixinGC托管内存块
        /// </summary>
        /// <param name="size">Uint64参数</param>
        /// <returns>MixinGC分配的内存大小</returns>
        public static unsafe void* malloc(nuint size)
        {
            return MixinGC.MixinGC_Allocate(size);
        }
        /// <summary>
        /// 释放MixinGC托管内存块
        /// </summary>
        /// <param name="ptr">分配的内存</param>
        public static unsafe void free(void* ptr)
        {
            MixinGC_Free((nuint)ptr);
        }
        /// <summary>
        /// 停用MixinGC系统，实际上手动调用MixinGC_Shutdown函数已经可用做到，本函数已经弃用
        /// </summary>
        [Obsolete("本函数已经弃用，但是否会删除尚不可知")]
        public static void Shutdown()
        {
            MixinGC_Shutdown();
        }
    }
}
