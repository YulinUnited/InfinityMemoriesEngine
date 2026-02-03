using System.Reflection;
using InfinityMemoriesEngine.OverWatch.qianhan.Worlds.Enum;

namespace InfinityMemoriesEngine.OverWatch.qianhan.Worlds.loads
{
    /// <summary>
    /// 世界加载器,警告，这是非特殊情况下的世界加载器，会造成性能损耗，非特殊情况下请将其作为兜底手段
    /// </summary>
    public class WorldLoader
    {
        public static bool Yes { get; private set; }
        private static Dictionary<WorldType, Type> worldTypeCache = new Dictionary<WorldType, Type>();
        /// <summary>
        /// 老式加载世界，仅限作为兜底手段，不建议使用,因为会反射所有带有WorldLoaderAttribute的类，对性能损耗较大，因此只将其作为兜底手段   
        /// 性能损耗较大，不建议使用，建议使用NewLoadWorlds()方法；
        /// </summary>
        public static void OldLoadWorlds()
        {
            var worldTypes = Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => t.GetCustomAttributes(typeof(WorldLoaderAttribute), false).Length > 0);

            foreach (var worldType in worldTypes)
            {
                var attribute = (WorldLoaderAttribute)worldType.GetCustomAttributes(typeof(WorldLoaderAttribute), false).FirstOrDefault();
                if (attribute != null)
                {
                    // 根据枚举加载相应的世界
                    if (attribute.WorldType == WorldType.SpecialWorld)
                    {
                        var worldInstance = Activator.CreateInstance(worldType);
                        // 加载世界逻辑
                        Console.WriteLine($"Loading world: {worldType.Name}");
                    }
                }
            }
        }
        /// <summary>
        /// 新世界加载器，使用缓存的方式加载世界，性能更高，首次调用时扫描所有带有WorldLoaderAttribute的类，并缓存到字典中，此后只从缓存中加载
        /// 性能损耗较小，建议使用，但请注意，新老世界加载器的使用场景不同，老世界加载器是为了兼容旧代码或兼容对场景要求更加严格的情况下而存在的
        /// 该双方法在Main中共同存在，但老世界加载器被作为兜底使用；
        /// </summary>
        public static void NewLoadWorlds()
        {
            if (worldTypeCache.Count == 0)
            {
                var worldTypes = Assembly.GetExecutingAssembly().GetTypes()
                    .Where(t => t.GetCustomAttributes(typeof(WorldLoaderAttribute), false).Length > 0);
                foreach (var worldType in worldTypes)
                {
                    var attribute = (WorldLoaderAttribute)worldType.GetCustomAttributes(typeof(WorldLoaderAttribute), false).FirstOrDefault();
                    if (attribute != null)
                    {
                        worldTypeCache[attribute.WorldType] = worldType;
                    }
                }
            }

            // 之后只从缓存中加载
            foreach (var worldType in worldTypeCache)
            {
                var worldInstance = Activator.CreateInstance(worldType.Value);
                // 加载世界逻辑
            }
        }
        /// <summary>
        /// 按需加载指定类型的世界（从缓存中获取并实例化），如果缓存未命中则返回 null
        /// 这只是为了配合NewLoadWorlds()方法而存在的，实际上单独使用并没有什么意义；
        /// </summary>
        public static object LoadWorld(WorldType type)
        {
            if (worldTypeCache.TryGetValue(type, out var worldType))
            {
                Yes = true;
                return Activator.CreateInstance(worldType);
            }
            Yes = false;
            return null;
        }
        /// <summary>
        /// 卸载世界，使用反射调用卸载方法，该方法属于兜底方法，请自行复写新NewUnLoadWorld逻辑
        /// </summary>
        /// <param name="worldType"></param>
        /// <param name="bool"></param>
        /// <returns></returns>
        public virtual bool UnLoadWorld(WorldType worldType, bool @bool)
        {
            if (worldTypeCache.TryGetValue(worldType, out var worldTypeValue))
            {
                // 反射调用卸载方法
                var method = worldTypeValue.GetMethod("UnLoadWorld");
                if (method != null)
                {
                    return (bool)method.Invoke(null, new object[] { @bool });
                }
            }
            return false;
        }
        /// <summary>
        /// 卸载世界，可以被重写
        /// </summary>
        /// <param name="worldType"></param>
        public virtual void NewUnLoadWorld(WorldType worldType)
        {

        }
    }
}
