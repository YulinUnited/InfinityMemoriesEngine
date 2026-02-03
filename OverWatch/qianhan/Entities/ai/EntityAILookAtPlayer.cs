namespace InfinityMemoriesEngine.OverWatch.qianhan.Entities.ai
{
    public class EntityAILookAtPlayer : EntityAIBase
    {
        private Entity entity;
        private Entity target;
        private float maxDistance;

        public EntityAILookAtPlayer(Entity entity, float maxDistance)
        {
            this.entity = entity;
            this.maxDistance = maxDistance;
            target = entity;
        }

        public override bool shouldExecute()
        {
            target = entity.world.getClosestPlayer(maxDistance);
            return target != null;
        }

        public override void updateTask()
        {
            entity.lookAt((float)target.posX, (float)target.posY, (float)target.posZ);
        }

    }
}
