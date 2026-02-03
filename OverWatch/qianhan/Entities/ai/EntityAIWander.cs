namespace InfinityMemoriesEngine.OverWatch.qianhan.Entities.ai
{
    public class EntityAIWander : EntityAIBase
    {
        private Entity entity;
        private Random rand = new Random();
        private int wanderCooldown;

        public EntityAIWander(Entity entity)
        {
            this.entity = entity;
        }

        public override bool shouldExecute()
        {
            return wanderCooldown-- <= 0;
        }

        public override void startExecuting()
        {
            wanderCooldown = 40 + rand.Next(60); // 随机间隔
            float dx = (float)(rand.NextDouble() * 2 - 1);
            float dz = (float)(rand.NextDouble() * 2 - 1);
            entity.moveTo((float)(entity.posX + dx * 2), (float)entity.posY, (float)(entity.posZ + dz * 2));
        }
    }
}
