using InfinityMemoriesEngine.OverWatch.qianhan.Log.lang;

namespace InfinityMemoriesEngine.OverWatch.qianhan.Objects.Modules.ModuleAccess.ModuleObject
{
    /// <summary>
    /// 综合性模块
    /// </summary>
    public class Module : ModuleBase
    {
        private readonly Dictionary<Type, object> @object = new();
        private static bool EnableModuleWarnings = true;
        private readonly Dictionary<Type, ModuleBase> _modules = new Dictionary<Type, ModuleBase>();
        /// <summary>
        /// 添加模块
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T AddModule<T>() where T : ModuleBase, new()
        {
            var type = typeof(T);
            if (_modules.ContainsKey(type)) return (T)_modules[type];
            var module = new T { Host = this };
            module.OnAttach();
            _modules[type] = module;
            return module;
        }
        /// <summary>
        /// 添加类模块
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T AddClassModule<T>() where T : class, new()
        {
            var type = typeof(T);
            if (@object.ContainsKey(type)) return @object[type] as T;
            var instance = new T();
            @object[type] = instance;
            //这里可能不太需要手动Add，但保留它预防万一
            //@object.Add(GetType(),instance);
            return instance;
        }
        /// <summary>
        /// 获取模块，安全无异常版本，如果模块不存在返回null但不崩溃
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T? GetModule<T>() where T : ModuleBase
        {
            _modules.TryGetValue(typeof(T), out var module);
            if (EnableModuleWarnings)
            {
                Debug.Log("你调用的是可为null版本的获取模块，虽然是合法的，但不建议在需要模块的逻辑中调度它");
            }
            return module as T;
        }
        /// <summary>
        /// 获取可为null的类模块
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T? GetModules<T>() where T : class
        {
            @object.TryGetValue(typeof(T), out var @class);
            if (EnableModuleWarnings)
            {
                Debug.Log("你调用的是可为null版本的获取模块，虽然是合法的，但不建议在需要模块的逻辑中调度它");
            }
            return @class as T;
        }
        /// <summary>
        /// 获取类模块
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public T GetClassModule<T>() where T : class
        {
            if (!@object.TryGetValue(typeof(T), out var instance) || instance is not T result)
            {
                throw new NullReferenceException($"模块类型 {typeof(T).Name} 未注册，或类型转换失败。");
            }
            return result;
        }
        /// <summary>
        /// 获取模块，需要强制要求存在模块的版本，如果不存在则抛出异常
        /// </summary>
        /// <typeparam name="T">添加模块的实例体</typeparam>
        /// <returns></returns>
        /// <exception cref="NullReferenceException">如果模块小于等于0时</exception>
        public T GetRequiredModule<T>() where T : ModuleBase
        {
            var type = typeof(T);
            if (!_modules.TryGetValue(type, out var module) || module == null)
                throw new NullReferenceException($"模块类型 {type.Name} 不存在，模块数：{_modules.Count}");

            Debug.Log($"[Module] 已获取模块：{type.Name}，当前总模块数：{_modules.Count}");
            return (T)module;
        }


        /// <summary>
        /// 将指定模块移除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void RemoveModule<T>() where T : ModuleBase
        {
            var type = typeof(T);
            if (_modules.TryGetValue(type, out var module))
            {
                module.OnDetach();
                _modules.Remove(type);
            }
        }
        /// <summary>
        /// 模块更新
        /// </summary>
        public void UpdateModules()
        {
            foreach (var m in _modules.Values)
            {
                if (m.Enabled) m.OnUpdate();
            }
        }
    }
}
