using System.Runtime.InteropServices;
using InfinityMemoriesEngine.OverWatch.qianhan.Entite.xuechengai.game.common.util;
using InfinityMemoriesEngine.OverWatch.qianhan.Events;
using InfinityMemoriesEngine.OverWatch.qianhan.LightCarriers;
using static InfinityMemoriesEngine.OverWatch.qianhan.Numbers.Mathf;
using Color = InfinityMemoriesEngine.OverWatch.qianhan.Numbers.Mathf.Color;
using Debug = InfinityMemoriesEngine.OverWatch.qianhan.Log.lang.Debug;
using Point = InfinityMemoriesEngine.OverWatch.qianhan.Numbers.Mathf.Point;
using Size = InfinityMemoriesEngine.OverWatch.qianhan.Numbers.Mathf.Size;

namespace InfinityMemoriesEngine.OverWatch.qianhan.Util
{
    /// <summary>
    /// Boolean结构体，表示一个字节的布尔值
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 1, Pack = 1)]
    public struct Boolean
    {
        /// <summary>
        /// 结构体的值，0表示false，非0表示true
        /// </summary>
        [FieldOffset(0)]
        public byte Value;

        /// <summary>
        /// 如果是1则为true
        /// </summary>
        public const byte True = 1;
        /// <summary>
        /// 如果是0则为false
        /// </summary>
        public const byte False = 0;

        /// <summary>
        /// 将 bool 转换为 Boolean
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator Boolean(bool value) => new() { Value = value ? (byte)1 : (byte)0 };

        /// <summary>
        /// 转换方式，非0为true，0为false
        /// </summary>
        /// <param name="boolean"></param>
        public static implicit operator bool(Boolean boolean) => boolean.Value != 0;
    }

    /// <summary>
    /// boolean结构体，表示一个字节的布尔值
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 1, Pack = 1)]
    public struct boolean
    {
        /// <summary>
        /// 结构体的值，0表示false，非0表示true
        /// </summary>
        [FieldOffset(0)]
        public byte Value;

        /// <summary>
        /// 保留字one，表示true
        /// </summary>
        public const byte one = 1;
        /// <summary>
        /// 保留字zero，表示false
        /// </summary>
        public const byte zero = 0;

        /// <summary>
        /// 将 bool 转换为 boolean
        /// </summary>
        /// <param name="value"></param>

        public static implicit operator boolean(bool value) => new() { Value = value ? (byte)1 : (byte)0 };

        /// <summary>
        /// 将 boolean 转换为 bool
        /// </summary>
        /// <param name="boolean"></param>
        public static implicit operator bool(boolean boolean) => boolean.Value != 0;
    }
}
namespace InfiniteMemoriesEngine.OverWatch.qianhan.Bytes
{

    /// <summary>
    /// 仿C语言的size_t类型，只适用于MixinGC，请注意，size_t为64位系统下的指针类别，若在32位系统请使用size32_t
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Size = 8, Pack = 8)]
    public readonly struct size_t
    {
        /// <summary>
        /// 大小类型
        /// </summary>
        public readonly nuint size;
        /// <summary>
        /// 构造函数转换为size_t类型
        /// </summary>
        /// <param name="ptr"></param>
        public size_t(nuint ptr)
        {
            size = ptr;
        }
        /// <summary>
        /// 隐式转换为size_t类型
        /// </summary>
        /// <param name="value">size_t类型</param>
        public static implicit operator nuint(size_t value) => value.size;
        /// <summary>
        /// 隐式转换为nuint类型
        /// </summary>
        /// <param name="value">nuint类型指针</param>
        public static implicit operator size_t(nuint value) => new size_t(value);
        public unsafe static implicit operator void*(size_t sizet) => (void*)sizet.size;
        public unsafe static implicit operator size_t(void* value) => new size_t((nuint)value);
    }

    [StructLayout(LayoutKind.Sequential, Size = 8, Pack = 8)]
    public struct ptrdiff_t
    {
        public long ptrdiff;
        public ptrdiff_t(long value)
        {
            ptrdiff = value;
        }

        public static implicit operator long(ptrdiff_t value) => value.ptrdiff;
        public static implicit operator ptrdiff_t(long value) => new ptrdiff_t(value);
    }
    [StructLayout(LayoutKind.Sequential, Size = 8, Pack = 8)]
    public struct intptr_t
    {
        public long intptr;
        public intptr_t(long value)
        {
            intptr = value;
        }

        public static implicit operator long(intptr_t value) => value.intptr;
        public static implicit operator intptr_t(long value) => new intptr_t(value);
    }
    /// <summary>
    /// 仿C语言的size_t类别型，只适用于win32位系统，请注意，size_t为64位系统下的指针类别，若在32位系统请使用size32_t
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Size = 4, Pack = 4)]
    public struct size32_t
    {
        public uint size32;
        public size32_t(uint value)
        {
            size32 = value;
        }
        public static implicit operator uint(size32_t value) => value.size32;
        public static implicit operator size32_t(uint value) => new size32_t(value);
        /// <summary>
        /// 未固定大小的void*指针进行size32_t转换，在这里void*根据系统位数进行转换
        /// </summary>
        /// <param name="size32">转换的指针</param>
        public unsafe static implicit operator void*(size32_t size32) => (void*)size32.size32;
        public unsafe static implicit operator size32_t(void* value) => new size32_t((uint)value);
        /// <summary>
        /// 对win32位系统的void*指针进行size32_t转换，在这里void32_t被固定为4字节
        /// </summary>
        /// <param name="void32">转换的指针</param>
        public unsafe static implicit operator size32_t(void32_t void32) => new size32_t((uint)void32.void32);
        public unsafe static implicit operator void32_t(size32_t size32) => new void32_t((void*)size32.size32);
    }

    /// <summary>
    /// 模拟C语言的int8_t类型
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 1, Pack = 1)]
    public struct int8_t
    {
        /// <summary>
        /// 对应的sbyte类型
        /// </summary>
        [FieldOffset(0)]
        public sbyte char1_t;

        public int8_t(sbyte value)
        {
            char1_t = value;
        }

        public static implicit operator sbyte(int8_t value) => value.char1_t;
        public static implicit operator int8_t(sbyte value) => new int8_t(value);
    }

    [StructLayout(LayoutKind.Sequential, Size = 2, Pack = 2)]
    public struct wchar_t
    {
        public ushort wchar;

        public wchar_t(ushort value)
        {
            wchar = value;
        }

        public static implicit operator ushort(wchar_t value) => value.wchar;
        public static implicit operator wchar_t(ushort value) => new wchar_t(value);

        public unsafe static implicit operator void*(wchar_t wcharT) => (void*)wcharT.wchar;
        public unsafe static implicit operator wchar_t(void* value) => new wchar_t((ushort)value);

        public unsafe static implicit operator void32_t(wchar_t wcharT) => (void32_t)(void*)wcharT.wchar;
        public unsafe static implicit operator wchar_t(void32_t value) => new wchar_t((ushort)(void*)value);

        public unsafe static implicit operator void64_t(wchar_t wchar)=> (void64_t)(void*)wchar.wchar;
        public unsafe static implicit operator wchar_t(void64_t value)=> new wchar_t((ushort)(void*)value);
    }

    /// <summary>
    /// 模拟C语言的int16_t类型
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 2,Pack = 2)]
    public struct int16_t
    {
        /// <summary>
        /// 对应的short类型
        /// </summary>
        [FieldOffset(0)]
        public short short2_t;

        public int16_t(short value)
        {
            short2_t = value;
        }

        public static implicit operator short(int16_t value) => value.short2_t;
        public static implicit operator int16_t(short value) => new int16_t(value);
    }
    /// <summary>
    /// 模拟C语言的int32_t类型
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 4,Pack = 4)]
    public struct int32_t
    {
        /// <summary>
        /// 对应的int类型
        /// </summary>
        [FieldOffset(0)]
        public int int4_t;

        public int32_t(int value)
        {
            int4_t = value;
        }

        public static implicit operator int(int32_t value) => value.int4_t;
        public static implicit operator int32_t(int value) => new int32_t(value);
    }
    /// <summary>
    /// 仿C语言的int64_t类型
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 8,Pack =8)]
    public struct int64_t
    {
        /// <summary>
        /// 单long类型
        /// </summary>
        [FieldOffset(0)]
        public long long8_t;

        public int64_t(long value)
        {
            long8_t = value;
        }

        /// <summary>
        /// 隐式转换为size_t类型
        /// </summary>
        /// <param name="value">size_t类型</param>
        public static implicit operator long(int64_t value) => value.long8_t;
        /// <summary>
        /// 隐式转换为nuint类型
        /// </summary>
        /// <param name="value">nuint类型指针</param>
        public static implicit operator int64_t(long value) => new int64_t(value);
    }
    /// <summary>
    /// 模拟C语言的uint8_t类型
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 1,Pack = 1)]
    public struct uint8_t
    {
        /// <summary>
        /// 对应的byte类型
        /// </summary>
        [FieldOffset(0)]
        public byte uchar1_t;

        public uint8_t(byte value)
        {
            uchar1_t = value;
        }

        public static implicit operator byte(uint8_t value) => value.uchar1_t;
        public static implicit operator uint8_t(byte value) => new uint8_t(value);
    }
    /// <summary>
    /// 仿C语言的uint16_t类型
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 2, Pack = 2)]
    public struct uint16_t
    {
        /// <summary>
        /// 对应的short类型
        /// </summary>
        [FieldOffset(0)]
        public ushort ushort2_t;

        public uint16_t(ushort value)
        {
            ushort2_t = value;
        }

        public static implicit operator ushort(uint16_t value) => value.ushort2_t;
        public static implicit operator uint16_t(ushort value) => new uint16_t(value);
        public unsafe static implicit operator void*(uint16_t uint16)=> (void*)uint16.ushort2_t;
        public unsafe static implicit operator uint16_t(void* value)=> new uint16_t((ushort)value);
    }
    /// <summary>
    /// 仿C语言的uint32_t类型
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 4, Pack = 4)]
    public struct uint32_t
    {
        /// <summary>
        /// 对应的uint类型
        /// </summary>
        [FieldOffset(0)]
        public uint uint4_t;

        public uint32_t(uint value)
        {
            uint4_t = value;
        }

        public static implicit operator uint(uint32_t value) => value.uint4_t;
        public static implicit operator uint32_t(uint value) => new uint32_t(value);
        public unsafe static implicit operator void*(uint32_t uint32)=> (void*)uint32.uint4_t;
        public unsafe static implicit operator uint32_t(void* value)=> new uint32_t((uint)value);
    }
    /// <summary>
    /// 仿C语言的uint64_t类型
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 8, Pack = 8)]
    public struct uint64_t
    {
        /// <summary>
        /// 对应的ulong类型
        /// </summary>
        [FieldOffset(0)]
        public ulong ulong8_t;

        public uint64_t(ulong value)
        {
            ulong8_t = value;
        }

        public static implicit operator ulong(uint64_t value) => value.ulong8_t;
        public static implicit operator uint64_t(ulong value) => new uint64_t(value);
        public unsafe static implicit operator void*(uint64_t uint64)=> (void*)uint64.ulong8_t;
        public unsafe static implicit operator uint64_t(void* value)=> new uint64_t((ulong)value);
    }
    /// <summary>
    /// 仿C语言的int_least8_t类型
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 1, Pack = 1)]
    public struct int_least8_t
    {
        /// <summary>
        /// 对应的sbyte类型
        /// </summary>
        [FieldOffset(0)]
        public sbyte int_least8;

        public int_least8_t(sbyte value)
        {
            int_least8 = value;
        }

        public static implicit operator sbyte(int_least8_t value) => value.int_least8;
        public static implicit operator int_least8_t(sbyte value) => new int_least8_t(value);
    }
    /// <summary>
    /// 仿C语言的int_least16_t类型
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 2, Pack = 2)]
    public struct int_least16_t
    {
        /// <summary>
        /// 对应的short类型
        /// </summary>
        [FieldOffset(0)]
        public short int_least16;

        public int_least16_t(short value)
        {
            int_least16 = value;
        }

        public static implicit operator short(int_least16_t value) => value.int_least16;
        public static implicit operator int_least16_t(short value) => new int_least16_t(value);
    }
    /// <summary>
    /// 仿C语言的int_least32_t类型
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 4, Pack =4)]
    public struct int_least32_t
    {
        /// <summary>
        /// 对应的int类型
        /// </summary>
        [FieldOffset(0)]
        public int int_least32;

        public int_least32_t(int value)
        {
            int_least32 = value;
        }

        public static implicit operator int(int_least32_t value) => value.int_least32;
        public static implicit operator int_least32_t(int value) => new int_least32_t(value);
    }
    /// <summary>
    /// 仿C语言的int_least64_t类型
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 8, Pack =8)]
    public struct int_least64_t
    {
        /// <summary>
        /// 对应的long类型
        /// </summary>
        [FieldOffset(0)]
        public long int_least64;

        public int_least64_t(long value)
        {
            int_least64 = value;
        }

        public static implicit operator long(int_least64_t value) => value.int_least64;
        public static implicit operator int_least64_t(long value) => new int_least64_t(value);
    }
    /// <summary>
    /// 仿C语言的uint_least8_t类型
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 1, Pack = 1)]
    public struct uint_least8_t
    {
        /// <summary>
        /// 对应的byte类型
        /// </summary>
        [FieldOffset(0)]
        public byte uint_least8;

        public uint_least8_t(byte value)
        {
            uint_least8 = value;
        }

        public static implicit operator byte(uint_least8_t value) => value.uint_least8;
        public static implicit operator uint_least8_t(byte value) => new uint_least8_t(value);
    }
    /// <summary>
    /// 仿C语言的uint_least16_t类型
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 2, Pack = 2)]
    public struct uint_least16_t
    {
        /// <summary>
        /// 对应的ushort类型
        /// </summary>
        [FieldOffset(0)]
        public ushort uint_least16;

        public uint_least16_t(ushort value)
        {
            uint_least16 = value;
        }

        public static implicit operator ushort(uint_least16_t value) => value.uint_least16;
        public static implicit operator uint_least16_t(ushort value) => new uint_least16_t(value);
        public unsafe static implicit operator void*(uint_least16_t uintleast16)=> (void*)uintleast16.uint_least16;
        public unsafe static implicit operator uint_least16_t(void* value)=> new uint_least16_t((ushort)value);
    }
    /// <summary>
    /// 仿C语言的uint_least32_t类型
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 4, Pack = 4)]
    public struct uint_least32_t
    {
        /// <summary>
        /// 对应的uint类型
        /// </summary>
        [FieldOffset(0)]
        public uint uint_least32;

        public uint_least32_t(uint value)
        {
            uint_least32 = value;
        }

        public static implicit operator uint(uint_least32_t value) => value.uint_least32;
        public static implicit operator uint_least32_t(uint value) => new uint_least32_t(value);
        public unsafe static implicit operator void*(uint_least32_t uintleast32)=> (void*)uintleast32.uint_least32;
        public unsafe static implicit operator uint_least32_t(void* value)=> new uint_least32_t((uint)value);

    }
    /// <summary>
    /// 仿C语言的uint_least64_t类型
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 8, Pack =8)]
    public struct uint_least64_t
    {
        /// <summary>
        /// 对应的ulong类型
        /// </summary>
        [FieldOffset(0)]
        public ulong uint_least64;

        public uint_least64_t(ulong value)
        {
            uint_least64 = value;
        }

        public static implicit operator ulong(uint_least64_t value) => value.uint_least64;
        public static implicit operator uint_least64_t(ulong value) => new uint_least64_t(value);
    }
    /// <summary>
    /// 仿C语言的int_fast8_t类型
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 1, Pack =1)]
    public struct int_fast8_t
    {
        /// <summary>
        /// 对应的sbyte类型
        /// </summary>
        [FieldOffset(0)]
        public sbyte int_fast8;

        public int_fast8_t(sbyte value)
        {
            int_fast8 = value;
        }

        public static implicit operator sbyte(int_fast8_t value) => value.int_fast8;
        public static implicit operator int_fast8_t(sbyte value) => new int_fast8_t(value);
    }
    /// <summary>
    /// 仿C语言的int_fast16_t类型
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 4, Pack = 4)]
    public struct int_fast16_t
    {
        /// <summary>
        /// 对应的int类型
        /// </summary>
        [FieldOffset(0)]
        public int int_fast16;

        public int_fast16_t(int value)
        {
            int_fast16 = value;
        }

        public static implicit operator int(int_fast16_t value) => value.int_fast16;
        public static implicit operator int_fast16_t(int value) => new int_fast16_t(value);
    }
    /// <summary>
    /// 仿C语言的int_fast32_t类型
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 4, Pack = 4)]
    public struct int_fast32_t
    {
        /// <summary>
        /// 对应的int类型
        /// </summary>
        [FieldOffset(0)]
        public int int_fast32;

        public int_fast32_t(int value)
        {
            int_fast32 = value;
        }

        public static implicit operator int(int_fast32_t value) => value.int_fast32;
        public static implicit operator int_fast32_t(int value) => new int_fast32_t(value);
    }
    /// <summary>
    /// 仿C语言的int_fast64_t类型
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 8, Pack = 8)]
    public struct int_fast64_t
    {
        /// <summary>
        /// 对应的long类型
        /// </summary>
        [FieldOffset(0)]
        public long int_fast64;

        public int_fast64_t(long value)
        {
            int_fast64 = value;
        }

        public static implicit operator long(int_fast64_t value) => value.int_fast64;
        public static implicit operator int_fast64_t(long value) => new int_fast64_t(value);
    }
    /// <summary>
    /// 仿C语言的uint_fast8_t类型
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 1, Pack = 1)]
    public struct uint_fast8_t
    {
        /// <summary>
        /// 对应的byte类型
        /// </summary>
        [FieldOffset(0)]
        public byte uint_fast8;

        public uint_fast8_t(byte value)
        {
            uint_fast8 = value;
        }

        public static implicit operator byte(uint_fast8_t value) => value.uint_fast8;
        public static implicit operator uint_fast8_t(byte value) => new uint_fast8_t(value);
    }
    /// <summary>
    /// 仿C语言的uint_fast16_t类型
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 4, Pack = 4)]
    public struct uint_fast16_t
    {
        /// <summary>
        /// 对应的uint类型
        /// </summary>
        [FieldOffset(0)]
        public uint uint_fast16;

        public uint_fast16_t(uint value)
        {
            uint_fast16 = value;
        }
        public unsafe static implicit operator void*(uint_fast16_t uintfast16)=> (void*)uintfast16.uint_fast16;
        public unsafe static implicit operator uint_fast16_t(void* value)=> new uint_fast16_t((uint)value);
        public static implicit operator uint(uint_fast16_t value) => value.uint_fast16;
        public static implicit operator uint_fast16_t(uint value) => new uint_fast16_t(value);
    }

    /// <summary>
    /// MixinGC专用无符号指针类型
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct nuint_8
    {
        /// <summary>
        /// nuint类型无符号指针
        /// </summary>
        public readonly nuint nuintn_8;
        /// <summary>
        /// 将nuint类型转换为unint_8类型
        /// </summary>
        /// <param name="ptr"></param>
        public nuint_8(nuint ptr)
        {
            nuintn_8 = ptr;
        }
        /// <summary>
        /// 隐式转换为nuint类型
        /// </summary>
        /// <param name="value">nuint结构体</param>
        public static implicit operator nuint(nuint_8 value) => value.nuintn_8;
        /// <summary>
        /// 隐式转换为unint_8类型
        /// </summary>
        /// <param name="value">nuint参数类型</param>
        public static implicit operator nuint_8(nuint value) => new nuint_8(value);
        public static unsafe implicit operator void*(nuint_8 nuint8)=> (void*)nuint8.nuintn_8;
        public static unsafe implicit operator nuint_8(void* value)=> new nuint_8((nuint)value);
    }

    /// <summary>
    /// 仿C语言的uint_fast32_t类型
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 4, Pack = 4)]
    public struct uint_fast32_t
    {
        /// <summary>
        /// 对应的uint类型
        /// </summary>
        [FieldOffset(0)]
        public uint uint_fast32;

        public uint_fast32_t(uint value)
        {
            uint_fast32 = value;
        }

        public static implicit operator uint(uint_fast32_t value) => value.uint_fast32;
        public static implicit operator uint_fast32_t(uint value) => new uint_fast32_t(value);
        public unsafe static implicit operator void*(uint_fast32_t uintfast32)=> (void*)uintfast32.uint_fast32;
        public unsafe static implicit operator uint_fast32_t(void* value)=> new uint_fast32_t((uint)value);
    }
    /// <summary>
    /// 仿C语言的uint_fast64_t类型
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 8,Pack = 8)]
    public struct uint_fast64_t
    {
        /// <summary>
        /// 对应的ulong类型
        /// </summary>
        [FieldOffset(0)]
        public ulong uint_fast64;

        public uint_fast64_t(ulong value)
        {
            uint_fast64 = value;
        }

        public static implicit operator ulong(uint_fast64_t value) => value.uint_fast64;
        public static implicit operator uint_fast64_t(ulong value) => new uint_fast64_t(value);
        public unsafe static implicit operator void*(uint_fast64_t uintfast64)=> (void*)uintfast64.uint_fast64;
        public unsafe static implicit operator uint_fast64_t(void* value)=> new uint_fast64_t((ulong)value);
    }
    /// <summary>
    /// 仿C语言的intmax_t类型
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 8, Pack = 8)]
    public struct intmax_t
    {
        /// <summary>
        /// 对应的long类型
        /// </summary>
        [FieldOffset(0)]
        public long intmax;

        public intmax_t(long value)
        {
            intmax = value;
        }

        public static implicit operator long(intmax_t value) => value.intmax;
        public static implicit operator intmax_t(long value) => new intmax_t(value);
    }
    /// <summary>
    /// 仿C语言的uintmax_t类型
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 8, Pack = 8)]
    public struct uintmax_t
    {
        /// <summary>
        /// 对应的ulong类型
        /// </summary>
        [FieldOffset(0)]
        public ulong uintmax;

        public uintmax_t(ulong value)
        {
            uintmax = value;
        }

        public static implicit operator ulong(uintmax_t value) => value.uintmax;
        public static implicit operator uintmax_t(ulong value) => new uintmax_t(value);
        public unsafe static implicit operator void*(uintmax_t uintmax)=> (void*)uintmax.uintmax;
        public unsafe static implicit operator uintmax_t(void* value)=> new uintmax_t((ulong)value);
    }
    //新添加

    /// <summary>
    /// 新添加的浮点数类型，仿C语言
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 8, Pack = 8)]
    public struct udouble8_t
    {
        /// <summary>
        /// 双精度浮点数
        /// </summary>
        [FieldOffset(0)]
        public double long_double8;

        public udouble8_t(double value)
        {
            long_double8 = value;
        }

        public static implicit operator double(udouble8_t value) => value.long_double8;
        public static implicit operator udouble8_t(double value) => new udouble8_t(value);
    }
    /// <summary>
    /// 新添加的浮点数类型，仿C语言
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 4, Pack =4)]
    public struct ufloat4_t
    {
        /// <summary>
        /// 单精度浮点数
        /// </summary>
        [FieldOffset(0)]
        public float float4;

        public ufloat4_t(float value)
        {
            float4 = value;
        }

        public static implicit operator float(ufloat4_t value) => value.float4;
        public static implicit operator ufloat4_t(float value) => new ufloat4_t(value);
    }
    /// <summary>
    /// 在32位系统下的uintptr_t类型
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 4, Pack = 4)]
    public struct uIntPtr32_t
    {
        /// <summary>
        /// 32位指针
        /// </summary>
        [FieldOffset(0)]
        public IntPtr intptr32;

        public uIntPtr32_t(IntPtr value)
        {
            intptr32 = value;
        }

        public static implicit operator nint(uIntPtr32_t value) => value.intptr32;
        public static implicit operator uIntPtr32_t(nint value) => new uIntPtr32_t(value);
        public unsafe static implicit operator void*(uIntPtr32_t uintPtr32)=> (void*)uintPtr32.intptr32;
        public unsafe static implicit operator uIntPtr32_t(void* value)=> new uIntPtr32_t((IntPtr)value);
    }
    /// <summary>
    /// 在64位系统下的uintptr_t类型
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 8, Pack = 8)]
    public struct uIntPtr64_t
    {
        /// <summary>
        /// 64位指针
        /// </summary>
        [FieldOffset(0)]
        public IntPtr intptr64;

        public uIntPtr64_t(IntPtr value)
        {
            intptr64 = value;
        }

        public static implicit operator nint(uIntPtr64_t value) => value.intptr64;
        public static implicit operator uIntPtr64_t(nint value) => new uIntPtr64_t(value);
        public unsafe static implicit operator void*(uIntPtr64_t intPtr64)=> (void*)intPtr64.intptr64;
        public unsafe static implicit operator uIntPtr64_t(void* value)=> new uIntPtr64_t((IntPtr)value);
    }
    /// <summary>
    /// 在32位系统下的uintptr_t类型
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 4, Pack = 4)]
    public struct UintPtr32_t
    {
        /// <summary>
        /// 32位指针
        /// </summary>
        [FieldOffset(0)]
        public UIntPtr uintptr32;

        public UintPtr32_t(UIntPtr value)
        {
            uintptr32 = value;
        }

        public static implicit operator nuint(UintPtr32_t value) => value.uintptr32;
        public static implicit operator UintPtr32_t(nuint value) => new UintPtr32_t(value);
        public unsafe static implicit operator void*(UintPtr32_t uintPtr32)=> (void*)uintPtr32.uintptr32;
        public unsafe static implicit operator UintPtr32_t(void* value)=> new UintPtr32_t((nuint)value);
    }
    /// <summary>
    /// 在64位系统下的uintptr_t类型
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 8, Pack = 8)]
    public struct UintPtr64_t
    {
        /// <summary>
        /// 64位指针
        /// </summary>
        [FieldOffset(0)]
        public nuint uintptr64;

        public UintPtr64_t(UIntPtr value)
        {
            uintptr64 = value;
        }

        public static implicit operator nuint(UintPtr64_t value) => value.uintptr64;
        public static implicit operator UintPtr64_t(nuint value) => new UintPtr64_t(value);
        public unsafe static implicit operator void*(UintPtr64_t uintPtr64)=> (void*)uintPtr64.uintptr64;
        public unsafe static implicit operator UintPtr64_t(void* value)=> new UintPtr64_t((nuint)value);
    }
    /// <summary>
    /// 在32位系统下的无类型指针
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 4, Pack = 4)]
    public struct void32_t
    {
        /// <summary>
        /// 无类型32指针
        /// </summary>
        [FieldOffset(0)]
        public unsafe void* void32;

        public unsafe void32_t(void* value)
        {
            void32 = value;
        }

        public static unsafe implicit operator void*(void32_t value) => value.void32;
        public static unsafe implicit operator void32_t(void* value) => new void32_t(value);
        public static unsafe implicit operator nint(void32_t value) => (nint)value.void32;
        public static unsafe implicit operator void32_t(IntPtr value) => new void32_t((void*)value);
        public static unsafe implicit operator nuint(void32_t value) => (nuint)value.void32;
        public static unsafe implicit operator void32_t(nuint value) => new void32_t((void*)value);
        public static unsafe implicit operator uint(void32_t value) => (uint)value.void32;
        public static unsafe implicit operator void32_t(uint value) => new void32_t((void*)value);
        public static unsafe implicit operator ulong(void32_t value) => (ulong)value.void32;
        public static unsafe implicit operator void32_t(ulong value) => new void32_t((void*)value);
        public static unsafe implicit operator ushort(void32_t value)=> (ushort)value.void32;
        public static unsafe implicit operator void32_t(ushort value)=> new void32_t((void*)value);
    }
    /// <summary>
    /// 在64位系统下的无类型指针
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 8, Pack = 8)]
    public struct void64_t
    {
        /// <summary>
        /// 无类型64指针
        /// </summary>
        [FieldOffset(0)]
        public unsafe void* void64;

        public unsafe void64_t(void* value)
        {
            void64 = value;
        }

        public static unsafe implicit operator void*(void64_t value) => value.void64;
        public static unsafe implicit operator void64_t(void* value) => new void64_t(value);
        public static unsafe implicit operator nint(void64_t value) => (nint)value.void64;
        public static unsafe implicit operator void64_t(nint value) => new void64_t((void*)value);
        public static unsafe implicit operator nuint(void64_t value) => (nuint)value.void64;
        public static unsafe implicit operator void64_t(nuint value) => new void64_t((void*)value);
        public static unsafe implicit operator uint(void64_t value) => (uint)value.void64;
        public static unsafe implicit operator void64_t(uint value) => new void64_t((void*)value);
        public static unsafe implicit operator ulong(void64_t value) => (ulong)value.void64;
        public static unsafe implicit operator void64_t(ulong value) => new void64_t((void*)value);
        public static unsafe implicit operator ushort(void64_t value)=> (ushort)value.void64;
        public static unsafe implicit operator void64_t(ushort value)=> new void64_t((void*)value);
    }

    /// <summary>
    /// 仿C语言的64位系统下的NULL宏数据型
    /// </summary>

    [StructLayout(LayoutKind.Sequential, Pack = 8, Size = 8)]
    public unsafe struct NULL
    {
        public readonly static void* ptr = (void*)0;
        
        public static unsafe implicit operator void*(NULL value) => default;
        public static unsafe implicit operator NULL(void* value) => default;

        public static unsafe implicit operator NULL(LightCarrier.TickCallbacks tickCallbacks) => default;
        public static unsafe implicit operator LightCarrier.TickCallbacks(NULL value) => default;
        public static unsafe implicit operator NULL(LightCarrier.TickCallback tick) => default;
        public static unsafe implicit operator LightCarrier.TickCallback(NULL value) => default;
        public static unsafe implicit operator NULL(Type type) => default;
        public static unsafe implicit operator Type(NULL value) => default;
        public static unsafe implicit operator NULL(Delegate del) => default;
        public static unsafe implicit operator Delegate(NULL value) => default;
        public static unsafe implicit operator NULL(Array array) => default;
        public static unsafe implicit operator Array(NULL value) => default;
        public static unsafe implicit operator NULL(string str) => default;
        public static unsafe implicit operator string(NULL value) => default;
        public static unsafe implicit operator NULL(float @float) => default;
        public static unsafe implicit operator float(NULL value) => default;
        public static unsafe implicit operator NULL(double @double) => default;
        public static unsafe implicit operator double(NULL value) => default;
        public static unsafe implicit operator NULL(decimal @decimal) => default;
        public static unsafe implicit operator decimal(NULL value) => default;
        public static unsafe implicit operator NULL(int @int) => default;
        public static unsafe implicit operator int(NULL value) => default;
        public static unsafe implicit operator NULL(long va)=> default;
        public static unsafe implicit operator long(NULL value)=> default;

        public static unsafe implicit operator NULL(ListenerList<NULL> listenerList)=> default;
        public static unsafe implicit operator ListenerList<NULL>(NULL uLL)=> default;

        

    }

    /// <summary>
    /// 仿C语言的32位系统下的NULL宏数据型
    /// </summary>

    [StructLayout(LayoutKind.Sequential, Pack = 4, Size = 4)]
    public unsafe struct NULL_32
    {
        public readonly static void* ptr = (void*)0;
        
        public static unsafe implicit operator void*(NULL_32 value) => default;
        public static unsafe implicit operator NULL_32(void* value) => default;

        public static unsafe implicit operator NULL_32(LightCarrier.TickCallbacks tickCallbacks) => default;
        public static unsafe implicit operator LightCarrier.TickCallbacks(NULL_32 value) => default;
        public static unsafe implicit operator NULL_32(LightCarrier.TickCallback tick) => default;
        public static unsafe implicit operator LightCarrier.TickCallback(NULL_32 value) => default;
        public static unsafe implicit operator NULL_32(Type type) => default;
        public static unsafe implicit operator Type(NULL_32 value) => default;
        public static unsafe implicit operator NULL_32(Delegate del) => default;
        public static unsafe implicit operator Delegate(NULL_32 value) => default;
        public static unsafe implicit operator NULL_32(Array array) => default;
        public static unsafe implicit operator Array(NULL_32 value) => default;
        public static unsafe implicit operator NULL_32(string str) => default;
        public static unsafe implicit operator string(NULL_32 value) => default;
        public static unsafe implicit operator NULL_32(float @float) => default;
        public static unsafe implicit operator float(NULL_32 value) => default;
        public static unsafe implicit operator NULL_32(double @double) => default;
        public static unsafe implicit operator double(NULL_32 value) => default;
        public static unsafe implicit operator NULL_32(decimal @decimal) => default;
        public static unsafe implicit operator decimal(NULL_32 value) => default;
        public static unsafe implicit operator NULL_32(int @int) => default;
        public static unsafe implicit operator int(NULL_32 value) => default;
        public static unsafe implicit operator NULL_32(long va) => default;
        public static unsafe implicit operator long(NULL_32 value) => default;

        public static unsafe implicit operator NULL_32(ListenerList<NULL_32> listenerList) => default;
        public static unsafe implicit operator ListenerList<NULL_32>(NULL_32 uLL) => default;


    }

    /// <summary>
    /// 多功能非托管堆内存分配类
    /// </summary>
    public class Util
    {
        private static object? obj = new object();
        private static Event @event = new Event();
        /// <summary>
        /// 仿C语言的malloc函数，用于分配内存
        /// </summary>
        /// <param name="ptr">指针</param>
        public static void malloc(nint ptr)
        {
            if (!@event.getGlobalMarkEvent())
            {
                Marshal.AllocHGlobal(ptr);
            }
            else
            {
                Debug.LogError($"{@event.getGlobalMarkEvent()}为true，请检查是否为false");
                return;
            }
        }
        /// <summary>
        /// 仿C语言的free函数，用于释放内存
        /// </summary>
        /// <param name="ptr">指针</param>
        public static void free(IntPtr ptr)
        {
            if (!@event.getGlobalMarkEvent())
            {
                Marshal.FreeHGlobal(ptr);
            }
            else
            {
                Debug.LogError($"{@event.getGlobalMarkEvent()}为true，请检查是否为false");
                return;
            }
        }
    }
}
