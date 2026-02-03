namespace InfinityMemoriesEngine.OverWatch.qianhan.Entities.ai
{
    public class EntityAIGuardArea : EntityAIBase
    {
        private Entity entity;
        private float guardX, guardY, guardZ, range;

        public EntityAIGuardArea(Entity entity, float x, float y, float z, float range)
        {
            this.entity = entity;
            guardX = x; guardY = y; guardZ = z;
            this.range = range;
        }

        public override bool shouldExecute()
        {
            return entity.getDistanceSq(guardX, guardY, guardZ) > (range * range);
        }

        public override void updateTask()
        {
            entity.moveTo(guardX, guardY, guardZ);
        }
    }
}