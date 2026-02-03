using InfinityMemoriesEngine.OverWatch.qianhan.Entities;

namespace InfinityMemoriesEngine.OverWatch.qianhan.Worlds
{
    public interface IWorld
    {
        void removeEntity(Entity entity);
        void removeEntityDangerously(Entity entityIn);
    }
}
