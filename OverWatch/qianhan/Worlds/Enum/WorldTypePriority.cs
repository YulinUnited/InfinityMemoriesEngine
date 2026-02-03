using InfinityMemoriesEngine.OverWatch.qianhan.Worlds;
using InfinityMemoriesEngine.OverWatch.qianhan.Worlds.Scenes;

namespace InfinityMemoriesEngine.OverWatch.qianhan.Worlds.Enum
{
    /// <summary>
    /// 世界类型优先级
    /// </summary>
    public enum WorldTypePriority
    {
        /// <summary>
        /// 什么都没有
        /// </summary>
        None = 0,
        /// <summary>
        /// 世界
        /// </summary>
        World = 1,
        /// <summary>
        /// 场景
        /// </summary>
        Scene = 2,
        /// <summary>
        /// 内部世界
        /// </summary>
        InternalWorld = 4,
        /// <summary>
        /// 内部场景
        /// </summary>
        InternalScene = 8,
        /// <summary>
        /// 外部世界
        /// </summary>
        ExternalWorld = 16,
        /// <summary>
        /// 外部场景
        /// </summary>
        ExternalScene = 32,
        /// <summary>
        /// 开放世界
        /// </summary>
        OpenWorld = 64,
        /// <summary>
        /// 开放场景
        /// </summary>
        OpenScene = 128,
        /// <summary>
        /// 公开世界
        /// </summary>
        PublicWorld = 256,
        /// <summary>
        /// 公开场景
        /// </summary>
        PublicScene = 512,
        /// <summary>
        /// 私有世界
        /// </summary>
        PrivateWorld = 1024,
        /// <summary>
        /// 私有场景
        /// </summary>
        PrivateScene = 2048,
    }
    /// <summary>
    /// 世界优先级
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class WorldPriority : Attribute
    {
        public WorldTypePriority WorldTypeProiority { get; set; }
        public Type WorldType { get; set; }
        /// <summary>
        /// 通过该构造函数传入世界优先级
        /// </summary>
        /// <param name="worldTypeProiority"></param>
        public WorldPriority(WorldTypePriority worldTypePriority)
        {
            WorldTypeProiority = worldTypePriority;
        }
        /// <summary>
        /// 通过构造函数传入世界类型
        /// </summary>
        /// <param name="worldType"></param>
        public WorldPriority(Type worldType)
        {
            if (worldType != typeof(World) && worldType != typeof(Scene))
            {
                throw new ArgumentException("只能传入World或者Scene类型的类");
            }
            WorldType = worldType;
        }
    }
}
