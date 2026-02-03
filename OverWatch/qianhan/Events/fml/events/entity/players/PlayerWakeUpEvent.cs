using InfinityMemoriesEngine.OverWatch.qianhan.Entities.PlayerEntity;

namespace InfinityMemoriesEngine.OverWatch.qianhan.Events.fml.events.entity.players
{
    public class PlayerWakeUpEvent : PlayerEvent
    {
        private bool wakeImmediately;
        private bool updateWorld;
        private bool setSpawn;
        public PlayerWakeUpEvent(EntityPlayer player, bool wakeImmediately, bool updateWorld, bool setSpawn) : base(player)
        {
            this.wakeImmediately = wakeImmediately;
            this.updateWorld = updateWorld;
            this.setSpawn = setSpawn;
        }
        public bool WakeImmediately()
        {
            return wakeImmediately;
        }
        public bool UpdateWorld()
        {
            return updateWorld;
        }
        public bool SetSpawn()
        {
            return setSpawn;
        }
    }
}
