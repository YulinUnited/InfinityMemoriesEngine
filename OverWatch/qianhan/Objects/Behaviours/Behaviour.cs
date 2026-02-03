using InfinityMemoriesEngine.OverWatch.qianhan.Entities;
using InfinityMemoriesEngine.OverWatch.qianhan.MonoBehaviours;

namespace InfinityMemoriesEngine.OverWatch.qianhan.Objects.Behaviours
{
    /// <summary>
    /// 行为类，继承自组件类
    /// </summary>
    public class Behaviour : Component
    {
        private static int globalInstanceCounter = 1;
        public Behaviour(int key, string name, string description, bool active, bool remove, Entity entity) : base(key, name, description, active, remove, entity)
        {
            instanceID = Interlocked.Increment(ref globalInstanceCounter);
        }
        public Behaviour()
        {
            instanceID = Interlocked.Increment(ref globalInstanceCounter);
        }
        /// <summary>
        /// 内存构造函数，用于创建一个新的行为实例，分配一个唯一的实例ID。
        /// </summary>
        /// <param name="key"></param>
        public Behaviour(int key) : base(key)
        {
            instanceID = Interlocked.Increment(ref globalInstanceCounter);
            Key = key;
            name = $"Behaviour_{key}";
            tag = "Untagged";
            layer = 0;
            enabled = true;
        }
        public bool enabled { get; set; } = true;
        public bool isActiveAndEnabled => enabled && !isRemove;
        public string name { get; set; } = "Behaviour";
        public string tag { get; set; } = "Untagged";
        public int layer { get; set; } = 0;
        public int instanceID { get; set; } = 0;
        public string fullName => $"{tag}.{name}:{instanceID}";
        public string typeName => GetType().Name;
        /// <summary>
        /// 停止运行
        /// </summary>
        public virtual void Stop()
        {
            if (isRemove)
            {
                Console.WriteLine($"{name} 已经被移除，无法停止！");
                return;
            }
            if (enabled)
            {
                enabled = false;
                Console.WriteLine($"{name} 停止运行！");
            }
            else
            {
                Console.WriteLine($"{name} 已经停止运行！");
            }
        }
    }
}
