using InfinityMemoriesEngine.OverWatch.qianhan.Entities.PlayerEntity;

namespace InfinityMemoriesEngine.OverWatch.qianhan.Events.fml.events.entity.players
{
    /// <summary>
    /// 用于检查玩家是否可以睡觉的事件。
    /// </summary>
    public class SleepingTimeCheckEvent : PlayerEvent
    {
        public SleepingTimeCheckEvent(EntityPlayer player) : base(player)
        {
        }
        //可根据后续追进玩家休息互动的对象或方块体进行扩展
    }
}
