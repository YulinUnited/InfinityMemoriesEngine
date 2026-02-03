using InfinityMemoriesEngine.OverWatch.qianhan.Util;
using InfinityMemoriesEngine.OverWatch.qianhan.Worlds;

namespace InfinityMemoriesEngine.OverWatch.qianhan.Entities
{
    public interface IEntityMultiPart
    {
        World getWorld();
        bool attackEntityFromPart(MultiPartEntityPart dragonPart, DamageSource source, float damage);
    }
}
