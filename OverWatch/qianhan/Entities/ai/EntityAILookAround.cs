namespace InfinityMemoriesEngine.OverWatch.qianhan.Entities.ai
{
    public class EntityAILookAround : EntityAIBase
    {
        private Entity entity;
        private Random rand = new Random();

        public EntityAILookAround(Entity entity)
        {
            this.entity = entity;
        }

        public override bool shouldExecute() => true;

        public override void updateTask()
        {
            if (rand.Next(40) == 0) // 偶尔转头
            {
                float yaw = (float)(rand.NextDouble() * 360.0);
                entity.setRotationYaw(yaw);
            }
        }
    }
}
