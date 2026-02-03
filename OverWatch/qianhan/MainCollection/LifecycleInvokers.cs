using System.Reflection;
using InfinityMemoriesEngine.OverWatch.qianhan.Objects;
using InfinityMemoriesEngine.OverWatch.qianhan.Start;
using static InfinityMemoriesEngine.OverWatch.qianhan.MainCollection.LifecycleRegistryBuilder;

namespace InfinityMemoriesEngine.OverWatch.qianhan.MainCollection
{
    /// <summary>
    /// 无状态机的组件接口
    /// </summary>
    public static class LifecycleInvokers
    {
        public static void InvokeByKeyword(object obj, string keyword)
        {
            var methods = obj.GetType()
                             .GetMethods(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

            foreach (var method in methods)
            {
                if (method.Name.Contains(keyword, StringComparison.OrdinalIgnoreCase) &&
                    method.GetParameters().Length == 0)
                {
                    method.Invoke(obj, null);
                }
            }
        }

        public static void InvokeStart(object obj) => InvokeByKeyword(obj, "Start");
        public static void InvokeUpdate(object obj) => InvokeByKeyword(obj, "Update");
    }
}
namespace OverWatch.qianhan.Util
{
    /// <summary>
    /// 生命周期方法调用器，自动调用对象的生命周期方法
    /// </summary>
    public class LifecycleInvoker
    {
        private static ILifecycleContextProvider contextProvider;
        private static Dictionary<Type, List<MethodInfo>> cache = new Dictionary<Type, List<MethodInfo>>();
        private MethodFingerprint fingerprint;
        private MethodInfo method;
        private static readonly Dictionary<ChinesePhase, List<Action>> handlers = new Dictionary<ChinesePhase, List<Action>>();

        public LifecycleInvoker(MethodFingerprint fingerprint, MethodInfo method)
        {
            this.fingerprint = fingerprint;
            this.method = method;
        }

        public object Invoke(object target, object[] args = null)
        {
            return method.Invoke(target, args ?? Array.Empty<object>());
        }

        public static void SetContextProvider(ILifecycleContextProvider provider)
        {
            contextProvider = provider;
        }

        private static List<MethodInfo> GetCachedMethods(Type type)
        {
            if (cache.TryGetValue(type, out var methods)) return methods;
            string[] lifecycleKeywords = { "Awake", "Start", "Update", "LateUpdate", "FixedUpdate" };
            methods = type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
              .Where(m => lifecycleKeywords.Any(k =>
                  m.Name.Contains(k, StringComparison.OrdinalIgnoreCase)))
              .ToList();
            cache[type] = methods;
            return methods;
        }

        public static void InvokeByPhase(object obj, ChinesePhase phase)
        {
            if (contextProvider == null)
                throw new InvalidOperationException("ContextProvider is not set.");

            if ((phase & ChinesePhase.Awake) != 0)
                InvokeAwake(obj);
            if ((phase & ChinesePhase.Start) != 0)
                InvokeStart(obj);
            if ((phase & ChinesePhase.Update) != 0)
                InvokeUpdate(obj);
            if ((phase & ChinesePhase.LateUpdate) != 0)
                InvokeLateUpdate(obj);
            if ((phase & ChinesePhase.FixedUpdate) != 0)
                InvokeFixedUpdate(obj);
        }

        public static void InvokeAwake(object obj) => InvokeByKeyword(obj, "Awake");
        public static void InvokeStart(object obj) => InvokeByKeyword(obj, "Start");
        public static void InvokeUpdate(object obj) => InvokeByKeyword(obj, "Update");
        public static void InvokeLateUpdate(object obj) => InvokeByKeyword(obj, "LateUpdate");
        public static void InvokeFixedUpdate(object obj) => InvokeByKeyword(obj, "FixedUpdate");

        private static void InvokeByKeyword(object obj, string keyword)
        {
            if (contextProvider == null)
                throw new InvalidOperationException("ContextProvider is not set.");

            foreach (var method in GetCachedMethods(obj.GetType()))
            {
                if (!method.Name.Contains(keyword, StringComparison.OrdinalIgnoreCase)) continue;

                var parameters = method.GetParameters();
                var args = new object[parameters.Length];

                for (int i = 0; i < parameters.Length; i++)
                {
                    args[i] = contextProvider.GetContext(parameters[i].ParameterType);
                }

                try
                {
                    method.Invoke(obj, args);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[LifecycleInvoker] Error in {method.Name}: {ex.InnerException?.Message ?? ex.Message}");
                }
            }
        }

        public static void Register(LifecycleClassInfo info)
        {
            Type type = Type.GetType(info.TypeName);
            if (type == null) return;

            object instance = Activator.CreateInstance(type);

            foreach (var methodInfo in info.Methods)
            {
                MethodInfo mi = type.GetMethod(methodInfo.MethodName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                if (mi == null) continue;

                var del = Delegate.CreateDelegate(typeof(Action), instance, mi, false) as Action;
                if (del == null) continue;

                if (!handlers.TryGetValue(methodInfo.Phase, out var list))
                    handlers[methodInfo.Phase] = list = new List<Action>();

                list.Add(del);
            }
        }
    }
}
namespace OverWatch.qianhan.Util
{
    public interface ILifecycleContextProvider
    {
        object GetContext(Type type);
    }

    public class DefaultLifecycleContextProvider : ILifecycleContextProvider
    {
        private readonly Dictionary<Type, object> contextMap = new Dictionary<Type, object>();

        public void Register<T>(T instance)
        {
            if (instance != null)
                contextMap[typeof(T)] = instance;
        }

        public object GetContext(Type type)
        {
            foreach (var kv in contextMap)
            {
                if (type.IsAssignableFrom(kv.Key))
                    return kv.Value;
            }

            return type.IsValueType ? Activator.CreateInstance(type) : null;
        }
    }
}
