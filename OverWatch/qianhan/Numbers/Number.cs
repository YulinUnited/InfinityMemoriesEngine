using System.Reflection;
using InfinityMemoriesEngine.OverWatch.qianhan.Log.lang;

namespace InfinityMemoriesEngine.OverWatch.qianhan.Numbers
{
    /// <summary>
    /// Number类，继承自Serializable类，表示一个数字类型的抽象基类
    /// </summary>
    public abstract class Number : Serializable
    {
        public abstract int inValue();
        public abstract long longValue();
        public abstract float floatValue();

        public abstract double doubleValue();

        public byte byteValue() { return (byte)inValue(); }
        public short shortValue() { return (short)longValue(); }
    }
}
namespace InfiniteMemoriesEngine.OverWatch.qianhan.Numbers
{
    public static class StringFieldManager
    {
        private static Dictionary<string, string> fields = [];
        /// <summary>
        /// 设置字段值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void set(string key, string value)
        {
            //首先保证不是null，虽然不需要初始化，但防止被反射
            if (fields != null)
            {
                fields[key] = value;
            }
        }
        /// <summary>
        /// 获取字段值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string get(string key)
        {
            return fields.TryGetValue(key, out var value) ? value : $"{key}为空，默认返回为{"空"}";
        }
        /// <summary>
        /// 是否包含字段
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool Has(string value)
        {
            return fields.ContainsKey(value);
        }

        public static void remove(string key)
        {
            fields.Remove(key);
        }
        /// <summary>
        /// 清空所有字段
        /// </summary>
        public static void Clear()
        {
            fields.Clear();
        }
    }
}
namespace OverWatch.QianHan.Log.network
{
    /// <summary>
    /// DataParameter类，用于定义数据参数的键，泛型类型T表示参数的值类型
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DataParameter<T>
    {
        public string Key { get; private set; }
        /// <summary>
        /// DataParameter类的构造函数，接受一个键参数，用于标识数据条目
        /// </summary>
        /// <param name="key"></param>
        public DataParameter(string key)
        {
            Key = key;
        }
        /// <summary>
        /// 转换为double类型的隐式转换操作符
        /// </summary>
        /// <param name="v"></param>
        public static implicit operator DataParameter<T>(DataParameter<double> v)
        {
            return new DataParameter<T>(v.Key);
        }
    }
    /// <summary>
    /// DataEntry类，用于存储数据条目，包含值和脏状态
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DataEntry<T>
    {
        private T value;
        public bool IsDirty { get; private set; }
        /// <summary>
        /// DataEntry类，用于存储数据条目，包含值和脏状态
        /// </summary>
        /// <param name="initialValue"></param>
        public DataEntry(T initialValue)
        {
            value = initialValue;
            IsDirty = false;
        }
        /// <summary>
        /// 获取条目的值，返回当前存储的值
        /// </summary>
        /// <returns></returns>
        public T GetValue()
        {
            return value;
        }
        /// <summary>
        /// 设置条目的值，并将其标记为脏状态，表示数据已被修改
        /// </summary>
        /// <param name="newValue"></param>
        public void setValue(T newValue)
        {
            value = newValue;
            IsDirty = true;
        }
        /// <summary>
        /// 设置条目为脏状态，表示数据已被修改，需要更新或保存
        /// </summary>
        /// <param name="isDirty"></param>
        public void setDirty(bool isDirty)
        {
            IsDirty = isDirty;
        }
    }
    /// <summary>
    /// DataManager类，用于管理数据条目，提供获取、设置和注册数据参数的方法
    /// </summary>
    public class DataManager
    {
        public Dictionary<string, object> dataEntries = new Dictionary<string, object>();
        public static DataParameter<float> HEALTH = new DataParameter<float>("health"); // 确保这行存在并初始化
        public static DataParameter<double> TrueHealth = new DataParameter<double>("True_Health");

        /// <summary>
        /// DataManager的构造函数，接受一个字典参数用于初始化数据条目
        /// </summary>
        /// <param name="dataEntries"></param>
        public DataManager(Dictionary<string, object> dataEntries)
        {
            this.dataEntries = dataEntries;
        }
        /// <summary>
        /// DataManager的构造函数，初始化数据条目字典
        /// </summary>
        public DataManager()
        {
            ///dataEntries = new Dictionary<string, object>();
        }

        // 获取或创建 DataEntry
        private DataEntry<T> GetEntry<T>(DataParameter<T> parameter)
        {
            if (dataEntries.ContainsKey(parameter.Key))
            {
                return (DataEntry<T>)dataEntries[parameter.Key];
            }
            else
            {
                var newEntry = new DataEntry<T>(default);
                dataEntries.Add(parameter.Key, newEntry);
                return newEntry;
            }
        }
        /// <summary>
        /// 设置指定键的值，如果键不存在则创建一个新的DataEntry<T>并更新值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parameter"></param>
        /// <param name="value"></param>
        // 通用的 set 方法
        public void set<T>(DataParameter<T> parameter, T value)
        {
            //Debug.Log($"Setting value for {parameter.Key}: {value}");
            DataEntry<T> dataEntry = getEntry(parameter);

            // 如果新值和旧值不同，进行更新
            if (!EqualityComparer<T>.Default.Equals(value, dataEntry.GetValue()))
            {
                var entry = getEntry(parameter);
                dataEntry.setValue(value);
                NotifyDataManagerChange(parameter);
                dataEntry.setDirty(true);
                //Debug.Log($"Successfully set {parameter.Key} to {value}");
            }
            else
            {
                //Debug.Log($"No change for {parameter.Key}, value is the same: {value}");
            }
        }
        /// <summary>
        /// 注册一个新的DataParameter<T>，如果键不存在则创建一个新的DataEntry<T>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parameter"></param>
        public void registerKey<T>(DataParameter<T> parameter)
        {
            if (!dataEntries.ContainsKey(parameter.Key))
            {
                var entry = new DataEntry<T>(default);
                dataEntries.Add(parameter.Key, entry);
                //Debug.Log($"Key'{parameter.Key}'以显示注册");
            }
        }
        /// <summary>
        /// 获取指定键的值，如果不存在则创建一个新的DataEntry<T>并返回默认值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public T get<T>(DataParameter<T> parameter)
        {
            //Debug.Log($"尝试获取键值：{parameter.Key}|当前数据条目:{dataEntries.Count}");
            //访问字典
            if (dataEntries.TryGetValue(parameter.Key, out object entry))
            {
                if (entry is DataEntry<T> typedEntry)
                {
                    //Debug.Log($"成功获取键值:{parameter.Key}={typedEntry.GetValue()}");
                    return typedEntry.GetValue();
                }
                else
                {
                    // Debug.Log($"类型不匹配，键{parameter.Key}的类型为{entry.GetType()}而非{typeof(DataEntry<T>)}");
                    HandleTypeMismatch(parameter.Key, typeof(T));
                    return default;//这里是default隶属于T，因此是简化的
                }
            }
            //Debug.LogWarning($"键{parameter.Key}不存在，正在进行修复");
            var newEntry = new DataEntry<T>(default);
            dataEntries.Add(parameter.Key, newEntry);
            //Debug.Log($"新建条目栈轨迹：\n{Environment.StackTrace}");
            return newEntry.GetValue();
        }
        /// <summary>
        /// 处理类型不匹配的情况，将数据条目转换为预期类型或重置为默认值，并通知全局事件系统
        /// </summary>
        /// <param name="key"></param>
        /// <param name="expectedType"></param>
        protected void HandleTypeMismatch(string key, Type expectedType)
        {
            var catualEntry = dataEntries[key];
            try
            {
                dynamic converted = Convert.ChangeType(catualEntry, expectedType);
                dataEntries[key] = new DataEntry<T>(converted);
                //Debug.LogWarning($"以强制转换{key}到{expectedType}");
            }
            catch
            {
                dataEntries[key] = new DataEntry<T>(default);
                //Debug.LogError($"无法转换{key}，已重置为默认值");
            }
            GlobalEventSystem.NotifyDataCorruption(key, expectedType);
        }
        private DataEntry<T> getEntry<T>(DataParameter<T> parameter)
        {
            /*if (dataEntries.ContainsKey(parameter.Key))
            {
                Debug.Log("在这里，" + "");
                return (DataEntry<T>)dataEntries[parameter.Key];
            }
            else
            {
                Debug.Log("这个问题{parameter.key},DataEntry的else部分");
                var newEntry = new DataEntry<T>(default(T));
                dataEntries.Add(parameter.Key, newEntry);
                return newEntry;
            }*/
            if (dataEntries.TryGetValue(parameter.Key, out object entry))
            {
                //Debug.Log($"访问已存在的键:{parameter.Key}");
                return (DataEntry<T>)entry;
            }
            else
            {
                //Debug.LogWarning($"隐式创建新键:{parameter.Key}");
                var newEntry = new DataEntry<T>(default);
                dataEntries.Add(parameter.Key, newEntry);
                return newEntry;
            }
        }
        /// <summary>
        /// 通知数据管理器数据变化的事件处理方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parameter"></param>
        private void NotifyDataManagerChange<T>(DataParameter<T> parameter)
        {
            // 在这里处理数据变化的逻辑
            //Debug.Log($"Data changed: {parameter.Key}");
        }
        /// <summary>
        /// 隐式转换为bool类型，检查DataManager是否存在且包含数据条目
        /// </summary>
        /// <param name="v"></param>
        public static implicit operator bool(DataManager v)
        {
            //Debug.Log("已成功转换为bool/隐式转换");
            return v != null
                   && v.dataEntries.Count > 0;
        }
        public static explicit operator ExplicitDataManager(DataManager v)
        {
            return new ExplicitDataManager(v);
        }
    }
    /// <summary>
    /// T类，作为一个占位符或示例类，可能用于其他目的
    /// </summary>
    internal class T
    {
    }
    /// <summary>
    /// GlobalEventSystem类，用于处理全局事件系统的通知
    /// </summary>
    internal class GlobalEventSystem
    {
        internal static void NotifyDataCorruption(string key, Type expectedType)
        {
            throw new NotImplementedException("键异常");
        }
    }
    /// <summary>
    /// ExplicitDataManager类，用于显式转换DataManager为bool类型
    /// </summary>
    public class ExplicitDataManager
    {
        private DataManager dataManager;

        public ExplicitDataManager(DataManager dataManager)
        {
            this.dataManager = dataManager;
        }

        public static explicit operator bool(ExplicitDataManager v)
        {
            //Debug.Log("显式转换为 bool");
            return v != null && v.dataManager.dataEntries.Count > 0;
        }
    }
    /// <summary>
    /// DefaultValueAttribute类，用于指定泛型参数或方法参数的默认值
    /// </summary>
    [Serializable]
    [AttributeUsage(AttributeTargets.GenericParameter | AttributeTargets.Parameter)]
    public class DefaultValueAttribute : Attribute
    {
        private object DefaultValue;

        public object Value => DefaultValue;

        public DefaultValueAttribute(string value)
        {
            DefaultValue = value;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is DefaultValueAttribute defaultValueAttribute))
            {
                return false;
            }

            if (DefaultValue == null)
            {
                return defaultValueAttribute.Value == null;
            }

            return DefaultValue.Equals(defaultValueAttribute.Value);
        }
        /// <summary>
        /// 获取哈希码，使用DefaultValue的哈希码，如果DefaultValue为null，则使用基类的哈希码
        /// </summary>
        /// <returns></returns>

        public override int GetHashCode()
        {
            if (DefaultValue == null)
            {
                return base.GetHashCode();
            }

            return DefaultValue.GetHashCode();
        }
    }
    /// <summary>
    /// ClassType类，用于反射调用类的实例方法或静态方法
    /// </summary>
    public class ClassType
    {
        private object? instance;
        private Type? type;
        public bool IsClass => type != null && (instance == null || type.IsClass);
        public bool IsValid => type != null && (instance != null || type.IsAbstract || type.IsSealed);
        public ClassType(object instance)
        {
            if (instance == null)
            {
                Debug.LogWarning($"传入的实例为Null");
                return;
            }
            this.instance = instance;
            this.type = instance.GetType();
        }
        public ClassType(Type types)
        {
            if (types == null)
            {
                Debug.LogWarning($"ClassType 内部的 Type 尚未初始化，请确认是否通过构造函数赋值");
                return;
            }
            this.type = types;
            instance = null;
        }
        public object? Invoke(object[]? args = null, string methodName = "MainTick")
        {
            if (type == null)
                throw new NullReferenceException("Type 尚未初始化");

            args ??= Array.Empty<object>();

            var binding = instance != null ? BindingFlags.Instance : BindingFlags.Static;
            binding |= BindingFlags.Public | BindingFlags.NonPublic;

            var method = type.GetMethod(methodName, binding);
            if (method == null)
                throw new MissingMethodException($"在类型 {type.FullName} 中找不到方法 {methodName}");

            return method.Invoke(instance, args);
        }
        public Type getClass()
        {
            if (type == null)
            {
                throw new NullReferenceException($"{type}类型为Null，请考虑初始化它或废弃它");
            }
            return type;
        }
        public object? InvokesStatic(object?[]? parameters, string methodName)
        {
            parameters ??= Array.Empty<object>();
            if (type == null)
            {
                throw new NullReferenceException($"{type}类型为Null");
            }
            var method = type.GetMethod(methodName, BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
            if (method == null)
                throw new MissingMethodException($"类型 {type.Name} 没有找到静态方法 {methodName}");

            return method.Invoke(null, parameters);
        }
    }
}
