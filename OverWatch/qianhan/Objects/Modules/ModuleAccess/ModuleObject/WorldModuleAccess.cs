using System.Runtime.CompilerServices;
using InfinityMemoriesEngine.OverWatch.qianhan.Log.lang;
using InfinityMemoriesEngine.OverWatch.qianhan.Worlds;
using InfinityMemoriesEngine.OverWatch.qianhan.Worlds.Scenes;

namespace InfinityMemoriesEngine.OverWatch.qianhan.Objects.Modules.ModuleAccess.ModuleObject
{
    public static class WorldModuleSystem
    {
        private static readonly ConditionalWeakTable<World, Dictionary<Type, object>> WorldModule = new();
        private static readonly ConditionalWeakTable<Scene, Dictionary<string, object>> SceneModule = new();

        /// <summary>
        /// 将模块挂载到世界对象上
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="world"></param>
        /// <param name="module"></param>
        public static void AttachWorldModule<T>(this World world, T module) where T : class
        {
            if (!WorldModule.TryGetValue(world, out var moduleDict))
            {
                moduleDict = new Dictionary<Type, object>();
                WorldModule.Add(world, moduleDict);
            }
            moduleDict[typeof(T)] = module;
        }
        /// <summary>
        /// 将模块挂载到世界对象上
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="scene"></param>
        /// <param name="module"></param>
        public static void AttachSceneModule<T>(this Scene scene, T module) where T : class
        {
            if (!SceneModule.TryGetValue(scene, out var moduleDict))
            {
                moduleDict = new Dictionary<string, object>();
                SceneModule.Add(scene, moduleDict);
            }
            moduleDict[typeof(T).FullName] = module;
        }
        /// <summary>
        /// 获取世界模块
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="world"></param>
        /// <returns></returns>
        public static T getWorldModule<T>(this World world) where T : class
        {
            if (WorldModule.TryGetValue(world, out var moduleDict) && moduleDict.TryGetValue(typeof(T), out var module))
            {
                return (T)module;
            }
            return null;
        }
        /// <summary>
        /// 获取场景模块
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="scene"></param>
        /// <returns></returns>
        public static T getSceneModule<T>(this Scene scene) where T : class
        {
            if (SceneModule.TryGetValue(scene, out var moduleDict) && moduleDict.TryGetValue(typeof(T).FullName, out var module))
            {
                return (T)module;
            }
            return null;
        }
        /// <summary>
        /// 判断对象是否为 World 类型
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsWorld(this object obj)
        {
            return obj is World;
        }
        /// <summary>
        /// 判断对象是否为 Scene 类型
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsScene(this object obj)
        {
            return obj is Scene;
        }
        /// <summary>
        /// 将对象转换为 Scene 类型并执行操作
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="action"></param>
        public static void AsSene(this object obj, Action<Scene> action)
        {
            if (obj is Scene scene)
            {
                action(scene);
                Debug.Log($"执行 Scene 模块操作：{scene.Name}");
            }
            else
            {
                Debug.LogError($"对象 {obj} 不是 Scene 类型，无法执行操作。");
            }
        }
        /// <summary>
        /// 将对象转换为 World 类型并执行操作
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="action"></param>
        public static void AsWorld(this object obj, Action<World> action)
        {
            if (obj is World world)
            {
                action(world);
                Debug.Log($"执行 World 模块操作：{world.Name}");
            }
            else
            {
                Debug.LogError($"对象 {obj} 不是 World 类型，无法执行操作。");
            }
        }
    }
}
