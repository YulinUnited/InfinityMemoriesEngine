using static InfinityMemoriesEngine.OverWatch.qianhan.Numbers.Mathf;

namespace InfinityMemoriesEngine.OverWatch.qianhan.Entities
{
    public class Transform
    {
        public Vector3 LocalPosition { set; get; } = Vector3.Zero();

        public Quaternion LocalRotation { set; get; } = Quaternion.Identity();

        public Vector3 LocalScale { set; get; } = Vector3.One;

        public Transform? Parent { set; get; } = null;

        public Matrix4x4 LocalMatrix =>
            Matrix4x4.CreateScale(LocalScale) * Matrix4x4.CreateFromQuaternion(LocalRotation) *
            Matrix4x4.CreateTranslation(LocalPosition);

        public Matrix4x4 GlobalMatrix
        {
            get
            {
                if (Parent == null)
                {
                    return LocalMatrix;
                }
                else
                {
                    return LocalMatrix * Parent.GlobalMatrix;
                }
            }
        }
        public Vector3 GlobalPosition
        {
            get
            {
                var matrix = GlobalMatrix;
                return new Vector3(matrix.M13, matrix.M14, matrix.M15);
            }
        }
        /// <summary>
        /// 下面的内容只是简写，不作为真正逻辑看待
        /// </summary>
        public EntityLivingBase livingBase { get; set; }
        public double currentX;
        public double currentY;
        public double currentZ;
        public double currentPos;
        public void LocalEntity(double x, double y, double z)
        {
            if (livingBase != null)
            {
                livingBase.posX = x;
                livingBase.posY = y;
                livingBase.posZ = z;
                x = currentX;
                y = currentY;
                z = currentZ;
            }
            if (x != currentX && y != currentY && z != currentZ)
            {
                getCurentPos(x, y, z);
            }
        }
        public double getCurentPos(double x, double y, double z)
        {
            if (livingBase != null)
            {
                livingBase.posX = x;
                livingBase.posY = y;
                livingBase.posZ = z;
                x = currentX;
                y = currentY;
                z = currentZ;
                currentX = currentPos;
                currentY = currentPos;
                currentZ = currentPos;
            }
            return currentPos;
        }
    }
}
