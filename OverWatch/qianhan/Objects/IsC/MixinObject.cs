using System.Runtime.InteropServices;
using InfiniteMemoriesEngine.OverWatch.qianhan.Bytes;
using InfinityMemoriesEngine.OverWatch.qianhan.Util;
using Boolean = InfinityMemoriesEngine.OverWatch.qianhan.Util.Boolean;

namespace InfinityMemoriesEngine.OverWatch.qianhan.Objects.IsC
{
    [StructLayout(LayoutKind.Sequential, Pack = 8, Size = 40)]
    public struct MixinObject
    {
        public int64_t mixin_id;
        public Boolean isRemove;
        Boolean isAlive;
        nuint mixin_data;
        size_t mixin_data_size;
        Boolean isMixinObject;

        public static MixinObject From(Objects.MixinObject obj)
        {
            return new MixinObject
            {
                mixin_id = obj.mixin_id,
                isRemove = obj.isRemove,
                isAlive = obj.isAlive,
                mixin_data = obj.mixin_data,
                isMixinObject = obj.isMixinObject
            };
        }
    }

    public unsafe static class MixinObjects
    {

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern MixinObject* MixinObject_Create(int64_t mixin_id, Boolean isRemove, nuint mixin_data, size_t mixin_data_size);

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MixinObject_Destroy(MixinObject* obj);

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MixinObject_Malloc(MixinObject** @object);

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int64_t getMixinObjectID(MixinObject* obj);

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MixinObject_Free(MixinObject* obj);
    }
}
