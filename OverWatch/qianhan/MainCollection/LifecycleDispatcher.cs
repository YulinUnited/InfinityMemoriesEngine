using System.Reflection;
using InfinityMemoriesEngine.OverWatch.qianhan.Config;
using InfinityMemoriesEngine.OverWatch.qianhan.Log.lang;
using InfinityMemoriesEngine.OverWatch.qianhan.Pool;
using InfinityMemoriesEngine.OverWatch.qianhan.Start;
using static InfinityMemoriesEngine.OverWatch.qianhan.MainCollection.LifecycleRegistryBuilder;

namespace InfinityMemoriesEngine.OverWatch.qianhan.MainCollection
{
    /// <summary>
    /// 生命周期调度器，在启动时注册和调用生命周期方法
    /// </summary>
    public static class LifecycleDispatcher
    {
        private static readonly List<(object Instance, List<MethodInfo> Methods)> instances = new List<(object, List<MethodInfo>)>();
        private static readonly Dictionary<Type, Dictionary<ChinesePhase, MethodInfo>> MethodMap = new Dictionary<Type, Dictionary<ChinesePhase, MethodInfo>>();
        private const string DefaultRegistryPath = "LifecycleRegistry.yulin";
        private static bool oldMethod = false;

        public static void RegisterPhase(Type type)
        {
            if (!MethodMap.TryGetValue(type, out var dict))
                dict = MethodMap[type] = new Dictionary<ChinesePhase, MethodInfo>();

            foreach (var method in type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
            {
                var attr = method.GetCustomAttribute<ChineseAttribute>();
                if (attr != null)
                {
                    dict[attr.Phase] = method;
                }
            }
        }

        public static void InvokeByPhase(object obj, ChinesePhase phase)
        {
            var type = obj.GetType();
            if (MethodMap.TryGetValue(type, out var dict) && dict.TryGetValue(phase, out var method))
            {
                method.Invoke(obj, null);
            }
            else
            {
                Debug.LogError($"[ChinesePhase]: 生命周期 {phase} 未注册或方法不存在于类型 {type.Name} 中");
            }
        }

        public static void Invoke(this (object Instance, List<MethodInfo> Methods) pair, ChinesePhase phase)
        {
            foreach (var method in pair.Methods)
            {
                var attr = method.GetCustomAttribute<ChineseAttribute>();
                if (attr != null && (attr.Phase & phase) != 0)
                {
                    method.Invoke(pair.Instance, null);
                }
            }
        }

        public static void InitializeFromFile(string? registryPath = null)
        {
            registryPath ??= DefaultRegistryPath;
            var classInfoList = LifecycleRegistryBuilder.LoadFromBinary(registryPath);

            foreach (var classInfo in classInfoList)
            {
                var type = Type.GetType(classInfo.TypeName);
                if (type == null)
                {
                    Debug.LogError($"[Dispatcher] Type not found: {classInfo.TypeName}");
                    continue;
                }

                var instance = LifecyclePool.getActivatedInstance(type);
                if (instance == null)
                {
                    Debug.LogError($"[Dispatcher] Could not instantiate: {type.FullName}");
                    continue;
                }

                var methods = ResolveMethods(type, classInfo.Methods);
                instances.Add((instance, methods));
                if (oldMethod) InvokeLifecycle(new LifecycleInstance(instance), classInfo.Registry);
            }
        }

        internal static List<MethodInfo> ResolveMethods(Type type, List<LifecycleMethodInfo> methodInfos)
        {
            return methodInfos
                .Select(m => type.GetMethod(m.MethodName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
                .Where(m => m != null)
                .ToList();
        }

        [Obsolete]
        private static void InvokeLifecycle(LifecycleInstance obj, LifetcycleRegisterys reg)
        {
            static void TryInvoke(LifecycleInstance o, LifetcycleRegisterys r, string name, LifetcycleRegisterys reg)
            {
                if (reg == r || reg.HasFlag(r)) o.Invoke(name);
            }

            TryInvoke(obj, LifetcycleRegisterys.SubscribeAwake, "Awake", reg);
            TryInvoke(obj, LifetcycleRegisterys.SubscribeStart, "Start", reg);
            TryInvoke(obj, LifetcycleRegisterys.SubscribeUpdate, "Update", reg);
            TryInvoke(obj, LifetcycleRegisterys.SubscribeLateUpdate, "LateUpdate", reg);
            TryInvoke(obj, LifetcycleRegisterys.SubscribeFixedUpdate, "FixedUpdate", reg);
        }

        public static void removeLifecycle()
        {
            foreach (var instance in instances)
            {
                var type = instance.Instance.GetType();
                var method = type.GetMethod("remove", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                method?.Invoke(instance.Instance, null);
            }
            instances.Clear();
        }

        internal static List<MethodInfo> ExtractChinesePhaseMethod(object instance)
        {
            return instance.GetType()
                .GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(m => m.GetCustomAttribute<ChineseAttribute>() != null)
                .ToList();
        }

        public static void InitializeFromYulin(string path)
        {
            var infos = YulinRegistryParser.LoadFromYulinFile(path);
            foreach (var info in infos)
            {
                var type = Type.GetType(info.TypeName);
                if (type == null) continue;

                var instance = LifecyclePool.getActivatedInstance(type);
                if (instance == null) continue;

                var lifecycle = new LifecycleInstance(instance);
                instances.Add((instance, ExtractChinesePhaseMethods(instance)));
                InvokeLifecycle(lifecycle, info.Registry);
            }
        }

        internal class LifecycleInstance
        {
            public object Instance;
            public Dictionary<string, MethodInfo> Methods = new Dictionary<string, MethodInfo>();

            public LifecycleInstance(object instance)
            {
                Instance = instance;
                var type = instance.GetType();
                foreach (var name in new[] { "Awake", "Start", "Update", "LateUpdate", "FixedUpdate" })
                {
                    var method = type.GetMethod(name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                    if (method != null)
                        Methods[name] = method;
                }
            }

            public void Invoke(string method)
            {
                if (Methods.TryGetValue(method, out var mi))
                    mi.Invoke(Instance, null);
            }
        }

        public static void Awake() => instances.ForEach(i => i.Invoke(ChinesePhase.Awake));
        public static void onAwake() => instances.ForEach(i => i.Invoke(ChinesePhase.onAwake));
        public static void unAwake() => instances.ForEach(i => i.Invoke(ChinesePhase.unAwake));
        public static void Start() => instances.ForEach(i => i.Invoke(ChinesePhase.Start));
        public static void onStart() => instances.ForEach(i => i.Invoke(ChinesePhase.onStart));
        public static void unStart() => instances.ForEach(i => i.Invoke(ChinesePhase.unStart));
        public static void Update() => instances.ForEach(i => i.Invoke(ChinesePhase.Update));
        public static void onUpdate() => instances.ForEach(i => i.Invoke(ChinesePhase.onUpdate));
        public static void unUpdate() => instances.ForEach(i => i.Invoke(ChinesePhase.unUpdate));
        public static void LateUpdate() => instances.ForEach(i => i.Invoke(ChinesePhase.LateUpdate));
        public static void onLateUpdate() => instances.ForEach(i => i.Invoke(ChinesePhase.onLateUpdate));
        public static void unLateUpdate() => instances.ForEach(i => i.Invoke(ChinesePhase.unLateUpdate));
        public static void FixedUpdate() => instances.ForEach(i => i.Invoke(ChinesePhase.FixedUpdate));
        public static void onFixedUpdate() => instances.ForEach(i => i.Invoke(ChinesePhase.onFixedUpdate));
        public static void unFixedUpdate() => instances.ForEach(i => i.Invoke(ChinesePhase.unFixedUpdate));
        public static void onRender() => instances.ForEach(i => i.Invoke(ChinesePhase.onRender));
        public static void onGUI() => instances.ForEach(i => i.Invoke(ChinesePhase.onGUI));
        public static void onPhysics() => instances.ForEach(i => i.Invoke(ChinesePhase.onPhysics));
        public static void onNetWorkSync() => instances.ForEach(i => i.Invoke(ChinesePhase.onNetWorkSync));
        public static void AiTick() => instances.ForEach(i => i.Invoke(ChinesePhase.AiTick));
        public static void Tick() => instances.ForEach(i => i.Invoke(ChinesePhase.Tick));
        public static void MinTick() => instances.ForEach(i => i.Invoke(ChinesePhase.MinTick));
        public static void CurrentTick() => instances.ForEach(i => i.Invoke(ChinesePhase.CurrentTick));
        public static void removeAwake() => instances.ForEach(i => i.Invoke(ChinesePhase.removeAwake));
        public static void removeStart() => instances.ForEach(i => i.Invoke(ChinesePhase.removeStart));
        public static void removeUpdate() => instances.ForEach(i => i.Invoke(ChinesePhase.removeUpdate));
        public static void removeLateUpdate() => instances.ForEach(i => i.Invoke(ChinesePhase.removeLateUpdate));
        public static void removeFixedUpdate() => instances.ForEach(i => i.Invoke(ChinesePhase.removeFixedUpdate));
        public static void removeAll() => instances.ForEach(i => i.Invoke(ChinesePhase.All));
        public static void Events() => instances.ForEach(i => i.Invoke(ChinesePhase.Events));

        public static void removeLifecycles()
        {
            if (instances.Count == 0) return;
            foreach (var instance in instances)
            {
                var type = instance.Instance.GetType();
                var method = type.GetMethod("remove", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                method?.Invoke(instance.Instance, null);
            }
            instances.Clear();
        }

        internal static List<MethodInfo> ExtractChinesePhaseMethods(object instance)
        {
            return instance.GetType()
                .GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(m => m.GetCustomAttribute<ChineseAttribute>() != null)
                .ToList();
        }

        public static void InitializeFromYulins(string path)
        {
            var infos = YulinRegistryParser.LoadFromYulinFile(path);
            foreach (var info in infos)
            {
                var type = Type.GetType(info.TypeName);
                if (type == null) continue;

                var instance = LifecyclePool.getActivatedInstance(type);
                if (instance == null) continue;
                var lifecycle = new LifecycleInstance(instance);
                instances.Add((instance, ExtractChinesePhaseMethods(instance)));
                InvokeLifecycle(lifecycle, info.Registry);
            }
        }
    }

    internal class LifecycleInstance
    {
        public object Instance;
        public Dictionary<string, MethodInfo> Methods = new Dictionary<string, MethodInfo>();

        public LifecycleInstance(object instance)
        {
            Instance = instance;
            var type = instance.GetType();
            foreach (var name in new[] { "Awake", "Start", "Update", "LateUpdate", "FixedUpdate" })
            {
                var method = type.GetMethod(name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                if (method != null)
                    Methods[name] = method;
            }
        }

        public void Invoke(string method)
        {
            if (Methods.TryGetValue(method, out var mi))
                mi.Invoke(Instance, null);
        }
    }
}
