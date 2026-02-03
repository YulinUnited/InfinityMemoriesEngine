using System.Runtime.CompilerServices;
using InfinityMemoriesEngine.OverWatch.qianhan.Entities;

namespace InfinityMemoriesEngine.OverWatch.qianhan.Objects.Modules.ModuleAccess.ModuleObject
{
    /// <summary>
    /// 适用于实体模块的类，可添加和获取模块
    /// </summary>
    public static class EntityModuleSystem
    {
        private static readonly ConditionalWeakTable<Entity, Dictionary<Type, object>> Module = new();
        /// <summary>
        /// 将模块挂载到实体对象上
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        public static void AttachModule<T>(this Entity entity, T module) where T : class
        {
            if (!Module.TryGetValue(entity, out var moduleDict))
            {
                moduleDict = new Dictionary<Type, object>();
                Module.Add(entity, moduleDict);
            }
            moduleDict[typeof(T)] = module;
        }
        /// <summary>
        /// 无参挂模块，不强制依赖某一指定模块
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        public static void AttachModule<T>(this Entity entity) where T : class, new()
        {
            var instance = new T();
            entity.AttachModule(instance);
        }

        /// <summary>
        /// 获取实体模块
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static T getEntityModule<T>(this Entity entity) where T : class
        {
            if (Module.TryGetValue(entity, out var moduleDict) && moduleDict.TryGetValue(typeof(T), out var module))
            {
                return (T)module;
            }
#pragma warning disable CS8603 // 可能返回 null 引用。
            return null;
#pragma warning restore CS8603 // 可能返回 null 引用。
        }
        /// <summary>
        /// 如果只是单纯的判断实体类型，可以直接使用Entity里面的getEntity方法，它是直接返回当前实体类型，这个只是用于在不同逻辑下的模块判断
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsEntity(this object obj)
        {
            return obj is Entity;
        }
        /// <summary>
        /// 用于安全的获取实体，或者是转换？当然，它只负责在实体类型的判断
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static Entity AsEntity(this object obj)
        {
#pragma warning disable CS8603 // 可能返回 null 引用。
            return obj as Entity;
#pragma warning restore CS8603 // 可能返回 null 引用。
        }
    }
}
