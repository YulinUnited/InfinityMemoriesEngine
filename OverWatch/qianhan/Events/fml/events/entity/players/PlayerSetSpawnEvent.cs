
using InfinityMemoriesEngine.OverWatch.qianhan.Entities;
using InfinityMemoriesEngine.OverWatch.qianhan.Entities.PlayerEntity;
using InfinityMemoriesEngine.OverWatch.qianhan.Events.fml.common.eventhandler;

namespace InfinityMemoriesEngine.OverWatch.qianhan.Events.fml.events.entity.players
{
    /// <summary>
    /// 适用于设置玩家重生点的事件
    /// </summary>
    [Cancelable]
    public class PlayerSetSpawnEvent : PlayerEvent
    {
        private bool forced;
        private Transform transform;
        public PlayerSetSpawnEvent(EntityPlayer entityPlayer, bool forced, Transform transform) : base(entityPlayer)
        {
            this.forced = forced;
            this.transform = transform;
        }
        public bool IsForced()
        {
            return forced;
        }
        public Transform GetTransform()
        {
            return transform;
        }
    }
}
