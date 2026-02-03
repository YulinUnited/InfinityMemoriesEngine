using System.Reflection;
using System.Text;
using InfinityMemoriesEngine.OverWatch.qianhan.Start;

namespace InfinityMemoriesEngine.OverWatch.qianhan.Objects
{
    public sealed class MethodFingerprint
    {
        public Type[] ParameterTypes { get; }
        public Type ReturnType { get; }
        public string Name { get; }
        public uint Hash { get; }

        private static readonly Dictionary<(Type, string), MethodInfo?> methodCache = new();
        private static readonly Dictionary<string, MethodInfo> Fingerprints = new();
        private static readonly Dictionary<ChinesePhase, List<MethodInfo>> PhaseMethods = new();
        private static readonly Dictionary<Type, List<MethodInfo>> CachedTypeMethods = new();
        private static readonly Dictionary<Type, MethodInfo[]> methodCaches = new();

        private MethodFingerprint(string name, Type[] paramTypes, Type returnType, uint hash)
        {
            Name = name;
            ParameterTypes = paramTypes;
            ReturnType = returnType;
            Hash = hash;
        }

        public MethodFingerprint(uint hash)
        {
            Hash = hash;
            Name = null;
            ParameterTypes = null;
            ReturnType = null;
        }

        public static MethodInfo? Generate(Type type, string methodName)
        {
            var key = (type, methodName);
            if (methodCache.TryGetValue(key, out var cachedMethod))
                return cachedMethod;

            var method = type.GetMethod(methodName,
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);

            methodCache[key] = method;
            return method;
        }

        public static MethodInfo[] Generates(Type type)
        {
            if (methodCaches.TryGetValue(type, out var cachedMethods))
                return cachedMethods;

            var methods = type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
            methodCaches[type] = methods;
            return methods;
        }

        public static MethodFingerprint Generates(MethodInfo method)
        {
            if (method == null) throw new ArgumentNullException(nameof(method));

            var paramTypes = method.GetParameters().Select(p => p.ParameterType).ToArray();
            var returnType = method.ReturnType;
            uint hash = ComputeHash(method);
            return new MethodFingerprint(method.Name, paramTypes, returnType, hash);
        }

        private static uint ComputeHash(MethodInfo method)
        {
            string declType = method.DeclaringType.FullName;
            string methodName = method.Name;
            string signature = GetSignatureString(method);
            string combined = $"{declType}\0{methodName}\0{signature}";
            return Fnv1a32.ComputeHash(combined);
        }

        private static string GetSignatureString(MethodInfo method)
        {
            var returnType = method.ReturnType.FullName;
            var parameters = string.Join(",", method.GetParameters().Select(p => p.ParameterType.FullName));
            return $"{returnType}({parameters})";
        }

        public override bool Equals(object obj)
        {
            return obj is MethodFingerprint other && Hash == other.Hash;
        }

        public override int GetHashCode()
        {
            return (int)Hash;
        }

        public override string ToString()
        {
            if (Name != null && ParameterTypes != null && ReturnType != null)
            {
                var paramList = string.Join(", ", ParameterTypes.Select(t => t.Name));
                return $"{ReturnType.Name} {Name}({paramList})";
            }
            return $"MethodFingerprint with Hash: {Hash}";
        }

        public static object? FastInvoke(object? instance, string methodName, params object[] args)
        {
            if (instance == null) return null;
            var type = instance.GetType();
            var method = Generate(type, methodName);
            return method?.Invoke(instance, args);
        }

        public static void Register(Type type)
        {
            if (CachedTypeMethods.ContainsKey(type)) return;

            var methods = type.GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
            var methodList = new List<MethodInfo>();

            foreach (var method in methods)
            {
                var attr = method.GetCustomAttribute<ChineseAttribute>();
                if (attr != null)
                {
                    string fingerprint = $"{type.FullName}.{method.Name}.{attr.Phase}";
                    if (!Fingerprints.ContainsKey(fingerprint))
                    {
                        Fingerprints[fingerprint] = method;
                    }

                    if (!PhaseMethods.TryGetValue(attr.Phase, out var list))
                        PhaseMethods[attr.Phase] = list = new List<MethodInfo>();

                    list.Add(method);
                    methodList.Add(method);
                }
            }

            CachedTypeMethods[type] = methodList;
        }

        public static void InvokePhase(object instance, ChinesePhase phase, params object[] args)
        {
            if (!PhaseMethods.TryGetValue(phase, out var methods)) return;

            foreach (var method in methods)
            {
                if (method.IsStatic)
                    method.Invoke(null, args);
                else
                    method.Invoke(instance, args);
            }
        }

        [Obsolete("此方法适用于调试或反射分析场景，不推荐在线程密集或性能关键逻辑中调用")]
        public static IEnumerable<MethodInfo> GetCachedMethods(Type type)
        {
            return CachedTypeMethods.TryGetValue(type, out var list) ? list : Enumerable.Empty<MethodInfo>();
        }
    }

    public static class Fnv1a32
    {
        public static uint ComputeHash(byte[] data)
        {
            const uint prime = 0x1000193;
            uint hash = 0x811C9DC5;
            foreach (byte b in data)
            {
                hash = (hash ^ b) * prime;
            }
            return hash;
        }

        public static uint ComputeHash(string s)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(s);
            return ComputeHash(bytes);
        }
    }
}
