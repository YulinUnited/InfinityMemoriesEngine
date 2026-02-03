using System.Runtime.InteropServices;
using InfinityMemoriesEngine.OverWatch.qianhan.Util;

namespace InfinityMemoriesEngine.OverWatch.qianhan.BinaryHexadecimalFileMapper
{
    /// <summary>
    /// 二进制文件映射器，用于处理二进制文件的读写操作。
    /// 如果您需要使用此类且您对二进制文件的格式不熟悉，请先阅读相关文档，或寻求AI协助。
    /// 本类被使用于LifecyclePool和YulinToBinaryConverter等类中，用于处理方法指纹和实例的二进制存储。
    /// 本类为静态类，提供了外部C函数，不需要实例化即可使用。
    /// </summary>
    public static class NativeMethods
    {

        public const int YLP_MAX_NAME = 1024;
        public const int YLP_MAX_SIG = 4096;
        public const int YLP_MAX_ASM = 2048;
        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        public static extern IntPtr LP_OpenWriterW(string path, uint flags);
        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool LP_WriteMethod(IntPtr handle, uint fingerprintHash, byte[] declType, ushort declLen, byte[] methodName, ushort nameLen, byte[] signature, ushort sigLen);
        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool LP_WriteInstance(IntPtr handle, uint typeHash, byte[] typeName, ushort typeLen, byte[] assembly, ushort asmLen);
        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool LP_CloseWriter(IntPtr handle, uint methodCount, uint instanceCount);
        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern uint LP_Fnvla32(byte[] bytes, uint len);

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        public static extern IntPtr LP_OpenReaderW(string path);

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool LP_QueryCounts(IntPtr handle, out uint methodCount, out uint instanceCount);

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool LP_NextMethod(
            IntPtr handle,
            out uint fingerprintHash,
            [Out] byte[] declBuf, ushort declCap, out ushort outDeclLen,
            [Out] byte[] nameBuf, ushort nameCap, out ushort outNameLen,
            [Out] byte[] sigBuf, ushort sigCap, out ushort outSigLen);

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool LP_NextInstance(
            IntPtr handle,
            out uint typeHash,
            [Out] byte[] typeBuf, ushort typeCap, out ushort outTypeLen,
            [Out] byte[] asmBuf, ushort asmCap, out ushort outAsmLen);

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern void LP_CloseReader(IntPtr handle);

        //[DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        //public static extern uint LP_Fnv1a32([MarshalAs(UnmanagedType.LPArray)] byte[] bytes, uint len);

    }
}
