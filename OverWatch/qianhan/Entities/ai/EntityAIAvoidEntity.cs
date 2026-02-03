using InfinityMemoriesEngine.OverWatch.qianhan.Numbers;
namespace InfinityMemoriesEngine.OverWatch.qianhan.Entities.ai
{
    public class EntityAIAvoidEntity : EntityAIBase
    {
        private Entity entity;
        private Entity threat;
        private float avoidDistance;

        public EntityAIAvoidEntity(Entity entity, float avoidDistance)
        {
            this.entity = entity;
            this.avoidDistance = avoidDistance;
        }

        public void setThreat(Entity threat)
        {
            this.threat = threat;
        }

        public override bool shouldExecute()
        {
            return threat != null && entity.getDistanceSq(threat) < avoidDistance * avoidDistance;
        }

        public override void updateTask()
        {
            float dx = (float)(entity.posX - threat.posX);
            float dz = (float)(entity.posZ - threat.posZ);
            float dist = (float)Mathf.Sqrt(dx * dx + dz * dz);
            if (dist > 0.001f)
            {
                dx /= dist; dz /= dist;
                entity.moveTo((float)(entity.posX + dx * 2), (float)entity.posY, (float)(entity.posZ + dz * 2));
            }
        }
    }
}
