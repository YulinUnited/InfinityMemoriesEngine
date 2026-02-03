using System.Runtime.InteropServices;
using InfiniteMemoriesEngine.OverWatch.qianhan.Bytes;
using InfinityMemoriesEngine.OverWatch.qianhan.Util;
using Boolean = InfinityMemoriesEngine.OverWatch.qianhan.Util.Boolean;

namespace InfinityMemoriesEngine.OverWatch.qianhan.Objects.IsC
{
    [StructLayout(LayoutKind.Sequential, Pack = 8, Size = 24)]
    public struct MainObject
    {
        public Boolean isAlive;//主对象/继承的对象是否处于活跃状态
        public Boolean isRemove;//主对象/继承的对象是否被标记为移除
        public Boolean isObject;//标记该对象是否为主对象/继承的对象
        /* more fields if needed */
        public int64_t main_object_id;
        public unsafe MainObject* next;//结构体自身指针，用于链表等数据结构

        public static MainObject From(Objects.MainObject obj)
        {
            return new MainObject
            {
                isAlive = obj.isAlive,
                isRemove = obj.isRemove,
                isObject = true,
                main_object_id = obj.objectId,
                next = null
            };
        }
    }

    public unsafe static class MainObjects
    {

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern MainObject* MainObject_Create(Boolean isAlive, Boolean isRemove);

        // 销毁 MainObject（延迟回收）

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MainObject_Destroy(MainObject* obj);

        // 获取单例对象

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern MainObject* getMainObject_Malloc();

        // 获取对象 ID

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int64_t getMainObjectID();

        // 手动分配 ID（慎用）

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MainObject_ID_Malloc(int64_t main_object_id);

        // ---------------------- MixinGC 回收时调用 ----------------------

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MainObject_GC_Recycle(MainObject* obj);

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MainObject_Malloc(MainObject* main_object);
    }
}
