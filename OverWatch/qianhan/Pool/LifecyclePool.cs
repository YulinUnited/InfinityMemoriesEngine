using System.Reflection;
using System.Text;
using InfinityMemoriesEngine.OverWatch.qianhan.BinaryHexadecimalFileMapper;
using InfinityMemoriesEngine.OverWatch.qianhan.Objects;
using OverWatch.qianhan.Util;

namespace InfinityMemoriesEngine.OverWatch.qianhan.Pool
{
    public static class LifecyclePool
    {
        private static readonly Dictionary<MethodFingerprint, Queue<object>> methodPool = new Dictionary<MethodFingerprint, Queue<object>>();
        private static readonly Dictionary<Type, Queue<object>> instancePool = new Dictionary<Type, Queue<object>>();

        static LifecyclePool()
        {
            InitializeFromBinary("LifecycleRegistry.yulin");
        }

        public static object getActivatedInstance(Type type)
        {
            if (instancePool.TryGetValue(type, out var pool) && pool.Count > 0)
            {
                return pool.Dequeue();
            }
            return Activator.CreateInstance(type);
        }

        public static LifecycleInvoker getInvoker(MethodInfo method)
        {
            var fingerprint = MethodFingerprint.Generates(method);
            if (methodPool.TryGetValue(fingerprint, out var pool) && pool.TryDequeue(out var invoker))
            {
                return (LifecycleInvoker)invoker;
            }
            return new LifecycleInvoker(fingerprint, method);
        }

        private static void InitializeFromBinary(string path)
        {
            if (!File.Exists(path))
                return;

            IntPtr reader = NativeMethods.LP_OpenReaderW(path);
            if (reader == IntPtr.Zero)
                return;

            try
            {
                uint methodCount = 0;
                uint instanceCount = 0;
                if (!NativeMethods.LP_QueryCounts(reader, out methodCount, out instanceCount))
                    return;

                for (uint i = 0; i < instanceCount; i++)
                {
                    uint typeHash;
                    byte[] typeBuffer = new byte[NativeMethods.YLP_MAX_NAME];
                    byte[] asmBuffer = new byte[NativeMethods.YLP_MAX_ASM];
                    ushort typeLen, asmLen;

                    if (NativeMethods.LP_NextInstance(reader, out typeHash,
                        typeBuffer, (ushort)typeBuffer.Length, out typeLen,
                        asmBuffer, (ushort)asmBuffer.Length, out asmLen))
                    {
                        string typeName = Encoding.UTF8.GetString(typeBuffer, 0, typeLen);
                        string assemblyName = Encoding.UTF8.GetString(asmBuffer, 0, asmLen);
                        Type type = Type.GetType($"{typeName}, {assemblyName}");
                        if (type != null && !instancePool.ContainsKey(type))
                        {
                            instancePool[type] = new Queue<object>();
                        }
                    }
                }

                for (uint i = 0; i < methodCount; i++)
                {
                    uint fingerprintHash;
                    byte[] declBuffer = new byte[NativeMethods.YLP_MAX_NAME];
                    byte[] nameBuffer = new byte[NativeMethods.YLP_MAX_NAME];
                    byte[] sigBuffer = new byte[NativeMethods.YLP_MAX_SIG];
                    ushort declLen, nameLen, sigLen;

                    if (NativeMethods.LP_NextMethod(reader, out fingerprintHash,
                        declBuffer, (ushort)declBuffer.Length, out declLen,
                        nameBuffer, (ushort)nameBuffer.Length, out nameLen,
                        sigBuffer, (ushort)sigBuffer.Length, out sigLen))
                    {
                        var fingerprint = new MethodFingerprint(fingerprintHash);
                        if (!methodPool.ContainsKey(fingerprint))
                        {
                            methodPool[fingerprint] = new Queue<object>();
                        }
                    }
                }
            }
            finally
            {
                NativeMethods.LP_CloseReader(reader);
            }
        }
    }
}
