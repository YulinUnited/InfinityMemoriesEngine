using System.Runtime.InteropServices;
using InfiniteMemoriesEngine.OverWatch.qianhan.Bytes;
using InfinityMemoriesEngine.OverWatch.qianhan.Util;
using Boolean = InfinityMemoriesEngine.OverWatch.qianhan.Util.Boolean;

namespace InfinityMemoriesEngine.OverWatch.qianhan.NBTData
{
    public enum NBT_TYPE : int
    {
        NBT_BYTE = 1,
        NBT_INT = 3,
        NBT_FLOAT = 5,
        NBT_STRING = 8,
        NBT_LIST = 9,
        NBT_COMPOUND = 10

    }

    public static unsafe class NBT
    {
        public delegate uint64_t NBT_HANDLE();

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern NBT_HANDLE NBT_OpenWriterW(wchar_t* path, uint32_t flags);
        
        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern Boolean NBT_WriteByte(NBT_HANDLE h, char* name, uint8_t value);
        
        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern Boolean NBT_WriteInt(NBT_HANDLE h, char* name, int32_t value);

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern Boolean  NBT_WriteFloat(NBT_HANDLE h, char* name, float value);

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern Boolean NBT_WriteString(NBT_HANDLE h, char* name, char* value);


        // 写入容器（开始/结束）
        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern Boolean NBT_StartCompound(NBT_HANDLE h, char* name);

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern Boolean NBT_EndCompound(NBT_HANDLE h);

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern Boolean NBT_StartList(NBT_HANDLE h, char* name, NBT_TYPE elemType);

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        static extern Boolean NBT_EndList(NBT_HANDLE h);


        // 关闭写入器（会回填根元素计数）
        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern Boolean NBT_CloseWriter(NBT_HANDLE h);
    }
}
