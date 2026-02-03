using InfinityMemoriesEngine.OverWatch.qianhan.Entities;
using static InfinityMemoriesEngine.OverWatch.qianhan.Numbers.Mathf;

namespace InfinityMemoriesEngine.OverWatch.qianhan.MonoBehaviours
{
    public class AABBCollider : Collider
    {
        public Vector3 Size;

        public AABBCollider(int key, string name, string description, bool active, bool remove, Entity entity) : base(key, name, description, active, remove, entity)
        {
            this.Size = new Vector3(1, 1, 1); // 默认大小
        }

        public override bool Intersects(Ray ray, out float distance)
        {
            // 实现射线与盒子碰撞检测的逻辑
            distance = 0f;
            Vector3 min = Position - Size * 0.5f;
            Vector3 max = Position + Size * 0.5f;
            float tMin = (float)((float)(min.x - ray.origins.x) / ray.directions.x);
            float tMax = (float)((float)(max.x - ray.origins.x) / ray.directions.x);
            if (tMin > tMax) (tMin, tMax) = (tMax, tMin);

            float tymin = (float)((min.y - ray.origins.y) / ray.directions.y);
            float tymax = (float)((max.y - ray.origins.y) / ray.directions.y);

            if (tymin > tymax) (tymin, tymax) = (tymax, tymin);

            if ((tMin > tymax) || (tymin > tMax))
                return false;

            distance = Max(tMin, tymin);
            return true;
        }
    }
}
