namespace InfinityMemoriesEngine.OverWatch.qianhan.Entities.ai
{
    public class EntityAIFollow : EntityAIBase
    {
        private readonly Entity follower;
        private readonly Entity target;
        private readonly float followDistance;
        private readonly float speed;

        public EntityAIFollow(Entity follower, Entity target, float followDistance, float speed)
        {
            this.follower = follower;
            this.target = target;
            this.followDistance = followDistance;
            this.speed = speed;
        }

        public override bool shouldExecute()
        {
            if (target == null) return false;
            float distSq = (follower.Position - target.Position).LengthSquared();
            return distSq > followDistance * followDistance;
        }

        public override void updateTask()
        {
            if (target == null) return;
            var dir = target.Position - follower.Position;
            var dist = dir.Length();
            if (dist > 0.01f)
            {
                dir /= dist; // 单位化
                follower.Position += dir * speed;
            }
        }
    }
}
