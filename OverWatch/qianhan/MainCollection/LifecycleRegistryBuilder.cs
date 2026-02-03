using System.Reflection;
using System.Text;
using InfinityMemoriesEngine.OverWatch.qianhan.BinaryHexadecimalFileMapper;
using InfinityMemoriesEngine.OverWatch.qianhan.Objects;
using InfinityMemoriesEngine.OverWatch.qianhan.Start;

namespace InfinityMemoriesEngine.OverWatch.qianhan.MainCollection
{
    /// <summary>
    /// 生命周期注册信息管理器
    /// </summary>
    public static class LifecycleRegistryBuilder
    {
        public static List<LifecycleRegistryInfo> LoadFromBinary(string path)
        {
            var registryList = new List<LifecycleRegistryInfo>();
            if (!File.Exists(path))
                return registryList;

            IntPtr reader = NativeMethods.LP_OpenReaderW(path);
            if (reader == IntPtr.Zero)
                return registryList;

            try
            {
                uint methodCount = 0;
                uint instanceCount = 0;
                if (!NativeMethods.LP_QueryCounts(reader, out methodCount, out instanceCount))
                    return registryList;

                var typeMap = new Dictionary<string, LifecycleRegistryInfo>();

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
                        var info = new LifecycleRegistryInfo
                        {
                            TypeName = typeName,
                            Methods = new List<LifecycleMethodInfo>()
                        };
                        typeMap[typeName] = info;
                        registryList.Add(info);
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
                        string declType = Encoding.UTF8.GetString(declBuffer, 0, declLen);
                        string methodName = Encoding.UTF8.GetString(nameBuffer, 0, nameLen);
                        string signature = Encoding.UTF8.GetString(sigBuffer, 0, sigLen);

                        if (typeMap.TryGetValue(declType, out var info))
                        {
                            info.Methods.Add(new LifecycleMethodInfo
                            {
                                MethodName = methodName,
                                Phase = ChinesePhase.All // Placeholder, need to parse from signature if needed
                            });
                        }
                    }
                }
            }
            finally
            {
                NativeMethods.LP_CloseReader(reader);
            }

            return registryList;
        }

        public static void SaveToBinary(string path, IEnumerable<Type> types)
        {
            IntPtr writer = NativeMethods.LP_OpenWriterW(path, 0);
            if (writer == IntPtr.Zero)
                return;

            try
            {
                var typeList = types.ToList();
                uint methodCount = 0;
                uint instanceCount = (uint)typeList.Count;

                foreach (var type in typeList)
                {
                    string? typeName = type.FullName;
                    string? assemblyName = type.Assembly.GetName().Name;
                    uint typeHash = Fnv1a32.ComputeHash(typeName);

                    byte[] typeBytes = Encoding.UTF8.GetBytes(typeName);
                    byte[] asmBytes = Encoding.UTF8.GetBytes(assemblyName);

                    if (!NativeMethods.LP_WriteInstance(writer, typeHash,
                        typeBytes, (ushort)typeBytes.Length,
                        asmBytes, (ushort)asmBytes.Length))
                    {
                        throw new Exception("Failed to write instance");
                    }

                    var methods = GetLifecycleMethods(type).ToList();
                    foreach (var method in methods)
                    {
                        string methodName = method.MethodName;
                        string signature = GetSignatureString(method);
                        uint hash = ComputeHash(typeName, methodName, signature);

                        byte[] declBytes = Encoding.UTF8.GetBytes(typeName);
                        byte[] nameBytes = Encoding.UTF8.GetBytes(methodName);
                        byte[] sigBytes = Encoding.UTF8.GetBytes(signature);

                        if (!NativeMethods.LP_WriteMethod(writer, hash,
                            declBytes, (ushort)declBytes.Length,
                            nameBytes, (ushort)nameBytes.Length,
                            sigBytes, (ushort)sigBytes.Length))
                        {
                            throw new Exception("Failed to write method");
                        }
                        methodCount++;
                    }
                }

                if (!NativeMethods.LP_CloseWriter(writer, methodCount, instanceCount))
                {
                    throw new Exception("Failed to close writer");
                }
            }
            finally
            {
                // Ensure writer is closed if error
            }
        }

        private static IEnumerable<LifecycleMethodInfo> GetLifecycleMethods(Type type)
        {
            return type.GetMethods(BindingFlags.Public | BindingFlags.Instance)
                .Where(m => m.GetCustomAttributes(typeof(ChineseAttribute), false).Any())
                .Select(m => new LifecycleMethodInfo
                {
                    MethodName = m.Name,
                    Phase = ((ChineseAttribute)m.GetCustomAttributes(typeof(ChineseAttribute), false).First()).Phase
                });
        }

        private static string GetSignatureString(LifecycleMethodInfo method)
        {
            // This should be implemented based on how you want to generate the signature
            return $"{method.Phase}";
        }

        private static uint ComputeHash(string declType, string methodName, string signature)
        {
            string combined = $"{declType}\0{methodName}\0{signature}";
            return Fnv1a32.ComputeHash(combined);
        }

        /// <summary>
        /// 生命周期注册信息
        /// </summary>
        [Serializable]
        public class LifecycleRegistryInfo
        {
            public string TypeName { get; set; } = string.Empty;
            public List<LifecycleMethodInfo> Methods { get; set; } = new List<LifecycleMethodInfo>();
            public LifetcycleRegisterys Registry { get; internal set; }
        }

        [Serializable]
        public class LifecycleClassInfo
        {
            public string TypeName { get; set; } = string.Empty;
            public List<LifecycleMethodInfo> Methods { get; set; } = new();
            public LifetcycleRegisterys Registry { get; set; }

            public virtual string getTypeName()
            {
                return TypeName;
            }
            public void setTypeName(string typeName)
            {
                TypeName = typeName;
            }
        }

        /// <summary>
        /// 生命周期方法信息
        /// </summary>
        [Serializable]
        public class LifecycleMethodInfo
        {
            public string MethodName { get; set; } = string.Empty;
            /// <summary>
            /// 默认阶段为 Start
            /// </summary>
            public ChinesePhase Phase { get; set; } = ChinesePhase.Start;
        }
    }
}
