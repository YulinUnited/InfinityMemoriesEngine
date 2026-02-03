using System.Runtime.InteropServices;
using InfinityMemoriesEngine.OverWatch.qianhan.InfinityMemoriesEngine.MainManager;

namespace InfinityMemoriesEngine.OverWatch.qianhan.Objects.MemoryManager
{
    /// <summary>
    /// 非托管内存管理器，用于处理非托管内存的分配和释放。
    /// 请注意：托管内存请使用C#的垃圾回收机制，即要么手动触发GC，要么让C#自动处理，但为了不必要的麻烦，建议由GC自动管理（或者使用此类来处理非托管内存）。
    /// </summary>
    public unsafe class NewMemoryManager : IMemoryManager
    {
        /// <summary>
        /// 标记是否需要移除当前对象。
        /// </summary>
        public bool IsRemove { get; set; }
        private IntPtr Intptr;
        private bool IsCPtrFree = false;

        /// <summary>
        /// 清理非托管内存。
        /// </summary>
        public void Release()
        {
            if (IsRemove)
            {
                if (Intptr != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(Intptr);
                    Intptr = IntPtr.Zero;//虽然多余
                }
                if (IsCPtrFree)
                {
                    //如果需要从C层面释放内存，启用它
                    MainMemoryManager.removeObject(Intptr.ToPointer());
                }
            }
        }
        /// <summary>
        /// 是否设置启用C层面释放内存。
        /// </summary>
        /// <param name="bool">是否需要启动</param>
        public void SetIsCPtrFree(bool @bool)
        {
            IsCPtrFree = @bool;
        }
        /// <summary>
        /// 清理全部的非托管内存，如果指针不为零和启用了由C层面释放内存，则会释放掉所有的非托管内存。
        /// </summary>
        public void ReleaseAll()
        {
            if (!IsRemove && !IsCPtrFree) return;
            if (Intptr != IntPtr.Zero && IsRemove)
            {
                Marshal.FreeHGlobal(Intptr);
                Intptr = IntPtr.Zero;//将指针悬空
            }
            if (IsRemove && IsCPtrFree)
            {
                if (Intptr != IntPtr.Zero)
                {
                    removeObject(Intptr.ToPointer());
                }
                else
                {
                    MainMemoryManager.removeAllObjects();
                }
            }
        }
        /// <summary>
        /// 非C向移除对象。
        /// </summary>
        /// <param name="ptr"></param>
        /// <returns>如果成功释放则返回true,否则返回/默认返回false</returns>
        public unsafe bool removeObject(void* ptr)
        {
            if (ptr == null) return false;
            if (ptr != null)
            {
                Intptr = (IntPtr)ptr;
                Marshal.FreeHGlobal(Intptr);
                Intptr = IntPtr.Zero;
                return true;
            }
            return false;
        }
        /// <summary>
        /// 设置当前的非托管内存指针。
        /// </summary>
        /// <param name="ptr"></param>
        public void SetIntPtr(IntPtr ptr)
        {
            Intptr = ptr;
        }
        /// <summary>
        /// 获取当前的非托管内存指针。
        /// </summary>
        /// <returns></returns>
        public IntPtr GetIntPtr()
        {
            return Intptr;
        }
        /// <summary>
        /// 分配一块非托管内存空间。
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        public static IntPtr Mallocs(int size)
        {
            return Marshal.AllocHGlobal(size);
        }
        /// <summary>
        /// 释放掉非托管内存。
        /// </summary>
        /// <param name="ptr">内存对象</param>
        public static void Frees(IntPtr ptr)
        {
            if (ptr != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(ptr);
                ptr = IntPtr.Zero; //指针悬空
            }
        }
        /// <summary>
        /// 向C占据一块内存空间。(Aa是为了避免驼峰时出现无法理解的情况)
        /// </summary>
        /// <param name="string"></param>
        /// <param name="size"></param>
        /// <returns>返回来自MainMemoryManager的createObject，即分配一块带有命名可识度的内存空间</returns>
        public static IntPtr OccupyAaBlockOfMemorySpaceToC(string @string, int size)
        {
            IntPtr namePtr = Marshal.StringToHGlobalAnsi(@string);
            try
            {
                sbyte* typeEntity = (sbyte*)namePtr.ToPointer();
                void* p = MainMemoryManager.createObject(typeEntity, (UIntPtr)size);
                return (nint)p;
            }
            finally
            {
                Marshal.FreeHGlobal(namePtr);
            }
        }
    }
}
