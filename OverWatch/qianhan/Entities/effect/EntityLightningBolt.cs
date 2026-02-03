using InfinityMemoriesEngine.OverWatch.qianhan.Worlds;

namespace InfinityMemoriesEngine.OverWatch.qianhan.Entities.effect
{
    public class EntityLightningBolt : EntityWeatherEffect
    {
        private int lightningState;
        public long boltVertex;
        private int boltLivingTime;
        private bool effectOnly;

        public EntityLightningBolt(World world, double x, double y, double z, bool effectOnly) : base(world)
        {
            this.setPosition(x, y, z);
            this.effectOnly = effectOnly;
            this.lightningState = 2;
            this.boltVertex = new Random().NextInt64();
            this.boltLivingTime = new Random().Next(1, 4);
        }
        //未完
    }
}
