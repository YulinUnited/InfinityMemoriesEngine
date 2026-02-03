using static InfinityMemoriesEngine.OverWatch.qianhan.Numbers.Mathf;

namespace InfinityMemoriesEngine.Overwatch.qianhan.Objects
{
    public class VectorMater
    {
        public struct Vector3
        {
            public double x, y, z;

            public Vector3(double x, double y, double z) { this.x = x; this.y = y; this.z = z; }

            // 运算符重载
            public static Vector3 operator +(Vector3 a, Vector3 b) => new Vector3(a.x + b.x, a.y + b.y, a.z + b.z);
            public static Vector3 operator -(Vector3 a, Vector3 b) => new Vector3(a.x - b.x, a.y - b.y, a.z - b.z);
            public static Vector3 operator -(Vector3 a) => new Vector3(-a.x, -a.y, -a.z);
            public static Vector3 operator *(Vector3 v, double scalar) => new Vector3(v.x * scalar, v.y * scalar, v.z * scalar);
            public static Vector3 operator *(double scalar, Vector3 v) => v * scalar;
            public static Vector3 operator /(Vector3 v, double scalar) => new Vector3(v.x / scalar, v.y / scalar, v.z / scalar);

            

            public override string ToString() => $"Vector3({x}, {y}, {z})";

            public static Vector3 Normalize(Vector3 v)
            {
                double length = Sqrt((float)(v.x * v.x + v.y * v.y + v.z * v.z));
                if (length == 0f) return new Vector3(0f, 0f, 0f);
                float inv = (float)(1f / length);
                return new Vector3(v.x * inv, v.y * inv, v.z * inv);
            }


            public static Vector3 Transform(Vector3 direction, Matrix4x4 rotation)
            {
                var v = new Vector3((float)direction.x, (float)direction.y, (float)direction.z);
                Vector3 result = Vector3.Transform(v, rotation);
                return new Vector3(result.x, result.y, result.z);
            }

            public static Vector3 Transform(Vector3 value, Quaternion rotation)
            {
                float x2 = (float)((float)rotation.x + rotation.x);
                float y2 = (float)((float) rotation.y + rotation.y);
                float z2 = (float)((float) rotation.z + rotation.z);

                float wx2 = (float)rotation.w * x2;
                float wy2 = (float)rotation.w * y2;
                float wz2 = (float)rotation.w * z2;
                float xx2 = (float)rotation.x * x2;
                float xy2 = (float)rotation.x * y2;
                float xz2 = (float)rotation.x * z2;
                float yy2 = (float)rotation.y * y2;
                float yz2 = (float)rotation.y * z2;
                float zz2 = (float)rotation.z * z2;

                return new Vector3(
                    value.x * (1.0f - yy2 - zz2) + value.y * (xy2 - wz2) + value.z * (xz2 + wy2),
                    value.x * (xy2 + wz2) + value.y * (1.0f - xx2 - zz2) + value.z * (yz2 - wx2),
                    value.x * (xz2 - wy2) + value.y * (yz2 + wx2) + value.z * (1.0f - xx2 - yy2)
                );
            }

            public static Vector3 Transform(Vector3 v, Quat q)
            {
                float x2 = (float)(q.x + q.x);
                float y2 = (float)(q.y + q.y);
                float z2 = (float)(q.z + q.z);

                float xx2 = (float)q.x * x2;
                float yy2 = (float)q.y * y2;
                float zz2 = (float)q.z * z2;

                float xy2 = (float)(q.x * y2);
                float xz2 = (float)(q.x * z2);
                float yz2 = (float)(q.y * z2);

                float wx2 = (float)(q.w * x2);
                float wy2 = (float)(q.w * y2);
                float wz2 = (float)(q.w * z2);

                return new Vector3(
                    (1f - yy2 - zz2) * v.x + (xy2 - wz2) * v.y + (xz2 + wy2) * v.z,
                    (xy2 + wz2) * v.x + (1f - xx2 - zz2) * v.y + (yz2 - wx2) * v.z,
                    (xz2 - wy2) * v.x + (yz2 + wx2) * v.y + (1f - xx2 - yy2) * v.z
                );
            }

        }

        public struct Math4
        {
            public double m00, m01, m02, m03;
            public double m10, m11, m12, m13;
            public double m20, m21, m22, m23;
            public double m30, m31, m32, m33;

            // 乘法运算符
            public static Math4 operator *(Math4 a, Math4 b)
            {
                Math4 result = new Math4();

                result.m00 = a.m00 * b.m00 + a.m01 * b.m10 + a.m02 * b.m20 + a.m03 * b.m30;
                result.m01 = a.m00 * b.m01 + a.m01 * b.m11 + a.m02 * b.m21 + a.m03 * b.m31;
                result.m02 = a.m00 * b.m02 + a.m01 * b.m12 + a.m02 * b.m22 + a.m03 * b.m32;
                result.m03 = a.m00 * b.m03 + a.m01 * b.m13 + a.m02 * b.m23 + a.m03 * b.m33;

                result.m10 = a.m10 * b.m00 + a.m11 * b.m10 + a.m12 * b.m20 + a.m13 * b.m30;
                result.m11 = a.m10 * b.m01 + a.m11 * b.m11 + a.m12 * b.m21 + a.m13 * b.m31;
                result.m12 = a.m10 * b.m02 + a.m11 * b.m12 + a.m12 * b.m22 + a.m13 * b.m32;
                result.m13 = a.m10 * b.m03 + a.m11 * b.m13 + a.m12 * b.m23 + a.m13 * b.m33;

                result.m20 = a.m20 * b.m00 + a.m21 * b.m10 + a.m22 * b.m20 + a.m23 * b.m30;
                result.m21 = a.m20 * b.m01 + a.m21 * b.m11 + a.m22 * b.m21 + a.m23 * b.m31;
                result.m22 = a.m20 * b.m02 + a.m21 * b.m12 + a.m22 * b.m22 + a.m23 * b.m32;
                result.m23 = a.m20 * b.m03 + a.m21 * b.m13 + a.m22 * b.m23 + a.m23 * b.m33;

                result.m30 = a.m30 * b.m00 + a.m31 * b.m10 + a.m32 * b.m20 + a.m33 * b.m30;
                result.m31 = a.m30 * b.m01 + a.m31 * b.m11 + a.m32 * b.m21 + a.m33 * b.m31;
                result.m32 = a.m30 * b.m02 + a.m31 * b.m12 + a.m32 * b.m22 + a.m33 * b.m32;
                result.m33 = a.m30 * b.m03 + a.m31 * b.m13 + a.m32 * b.m23 + a.m33 * b.m33;

                return result;
            }

            public override string ToString()
            {
                return $"[{m00}, {m01}, {m02}, {m03}]\n" +
                       $"[{m10}, {m11}, {m12}, {m13}]\n" +
                       $"[{m20}, {m21}, {m22}, {m23}]\n" +
                       $"[{m30}, {m31}, {m32}, {m33}]";
            }
        }
    }
}
