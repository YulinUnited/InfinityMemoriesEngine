using InfinityMemoriesEngine.OverWatch.qianhan.Enums;
using InfinityMemoriesEngine.OverWatch.qianhan.Log.lang;
using InfinityMemoriesEngine.OverWatch.qianhan.Objects;

namespace InfinityMemoriesEngine.OverWatch.qianhan.Pool
{
    /// <summary>
    /// 轻型泛类对象池，适用于轻量级对象的重复使用场景，但请注意，里面部分方法含有危险行为，请务必谨慎使用
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class GenericPool<T> where T : class, new()
    {
        private static readonly Stack<T> _pool = new Stack<T>();
        //对象池最大数量限制
        private const int MaxPoolSize = 2060;
        /// <summary>
        /// 从对象池中获取一个对象，如果池为空则创建新实例
        /// </summary>
        /// <returns>可用的对象实例</returns>
        public static T Get()
        {
            if (_pool.TryPop(out var instance))
            {
                return instance;
            }
            return new T();
        }
        /// <summary>
        /// 设置一个对象回收，等价于Return，但增加了容量检查，请注意，该逻辑可能会被移除
        /// </summary>
        /// <param name="instance">需要回收的对象</param>
        [Obsolete]
        [Removal(RemovalFlags.Release)]
        public static void Set(T instance)
        {
            if (_pool.Count >= MaxPoolSize)
            {
                Debug.LogWarning($"警告，{_pool}的对象数量为{_pool.Count}，请确保当前对象池不大于最大值");
                return;
            }
            Return(instance);
        }
        /// <summary>
        /// 回收对象并放入池中，如果实现IPoolCleanable则调用清理
        /// </summary>
        /// <param name="instance">需要回收的对象</param>
        public static void Return(T instance)
        {
            if (instance == null) return;
            if (instance is IPoolCleanable cleanable)
            {
                cleanable.Clean();
            }
            if (_pool.Count < MaxPoolSize)
            {
                _pool.Push(instance);
            }
        }
        /// <summary>
        /// 清理池对象
        /// </summary>
        public static void Clear()
        {
            _pool.Clear();
        }
        /// <summary>
        /// 返回所有的对象
        /// </summary>
        /// <param name="instances">带回收对象合集</param>
        public static void ReturnAll(IEnumerable<T> instances)
        {
            foreach (var item in instances)
            {
                Return(item);
            }
        }
        /// <summary>
        /// 接口的简单实现,警告，在使用时会导致全线程暂停，请不要在非必须的情况下使用，特此提醒，严重等级：Light
        /// </summary>
        /// <returns></returns>
        public static IPoolCleanable ForceCleanAndRemove()
        {
            if (_pool.Count > 0)
            {
                _pool.Clear();
                MainObject.ForceGCCollect(true);
            }
            return null;
        }
    }
    /// <summary>
    /// 对象池清理接口，用于对象在被回收前执行自定义清理逻辑
    /// </summary>
    public interface IPoolCleanable
    {
        void Clean();
    }
}
