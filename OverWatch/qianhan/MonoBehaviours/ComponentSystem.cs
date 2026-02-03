using InfinityMemoriesEngine.OverWatch.qianhan.Entities;

namespace InfinityMemoriesEngine.OverWatch.qianhan.MonoBehaviours
{
    public static class ComponentSystem
    {
        // 每个实体对应一个组件字典
        private static readonly Dictionary<Entity, Dictionary<Type, Component>> componentMap = new();

        public static bool AddComponent<T>(this Entity entity, T component) where T : Component
        {
            if (entity == null || component == null) return false;

            if (!componentMap.ContainsKey(entity))
                componentMap[entity] = new Dictionary<Type, Component>();

            var type = typeof(T);
            if (componentMap[entity].ContainsKey(type))
            {
                Console.WriteLine($"[ComponentSystem] {type.Name} 已存在于实体 {entity.Name} 中。");
                return false;
            }

            component.Owner = entity;
            component.Active = true;
            componentMap[entity][type] = component;
            component.onAdd();

            Console.WriteLine($"[ComponentSystem] 添加组件 {type.Name} 到实体 {entity.Name}");
            return true;
        }

        public static bool removeComponent<T>(this Entity entity) where T : Component
        {
            if (entity == null || !componentMap.ContainsKey(entity)) return false;

            var type = typeof(T);
            if (!componentMap[entity].TryGetValue(type, out var component)) return false;

            component.Active = false;
            component.isRemove = true;
            component.onRemove();
            componentMap[entity].Remove(type);

            Console.WriteLine($"[ComponentSystem] 从实体 {entity.Name} 移除组件 {type.Name}");
            return true;
        }
        /// <summary>
        /// 获取组件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static T? GetComponent<T>(this Entity entity) where T : Component
        {
            if (entity == null || !componentMap.ContainsKey(entity)) return null;

            var type = typeof(T);
            return componentMap[entity].TryGetValue(type, out var comp) ? comp as T : null;
        }

        public static void DisableAll(Entity entity)
        {
            if (entity == null || !componentMap.ContainsKey(entity)) return;

            foreach (var kv in componentMap[entity])
            {
                kv.Value.Active = false;
                kv.Value.isRemove = true;
                kv.Value.onRemove();
                Console.WriteLine($"[ComponentSystem] 已禁用组件 {kv.Key.Name} 于实体 {entity.Name}");
            }

            componentMap[entity].Clear();
        }
    }
}
