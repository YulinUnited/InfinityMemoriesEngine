using InfinityMemoriesEngine.OverWatch.qianhan.annotations;
using InfinityMemoriesEngine.OverWatch.qianhan.Entities.PlayerEntity;
using InfinityMemoriesEngine.OverWatch.qianhan.Items;
using InfinityMemoriesEngine.OverWatch.qianhan.Worlds;

namespace InfinityMemoriesEngine.OverWatch.qianhan.Events.fml.events.entity.players
{
    /// <summary>
     /// 用于玩家使用锄头的事件（如果有）
     /// </summary>
    public class UseHoeEvent : PlayerEvent
    {
        private ItemStack current;
        private World World;

        public UseHoeEvent(EntityPlayer player, [Nonnull] ItemStack current, World world) : base(player)
        {
            this.current = current;
            this.World = world;
        }
        [Nonnull]
        public ItemStack getCurrent()
        {
            return current;
        }
        public World getWorld()
        {
            return World;
        }
    }
}
