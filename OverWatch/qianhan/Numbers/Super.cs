using System.Numerics;
using System.Runtime.InteropServices;
using InfiniteMemoriesEngine.OverWatch.qianhan.Bytes;
using InfinityMemoriesEngine.OverWatch.qianhan.Util;

namespace InfinityMemoriesEngine.OverWatch.qianhan.Numbers
{
    public class Super
    {
        public static double E = 2.7182818284590452354;
        public static double PI = 3.14159265358979323846;
        /// <summary>
        /// 限制数值在最小和最大之间，支持单精度浮点、双精度浮点、整数类型
        /// </summary>
        /// <typeparam name="Override"></typeparam>
        /// <param name="value"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static Override Clamped<Override>(Override value, Override min, Override max) where Override : IComparable<Override>
        {
            if (min.CompareTo(max) > 0)
            {
                throw new ArgumentException("min不能小于max");
            }
            if (value.CompareTo(min) < 0)
            {
                return min;
            }
            else if (value.CompareTo(max) > 0)
            {
                return max;
            }
            return value;
        }
        //模拟Unity的Random.Range，目前还存在问题，可能
        public static float Clamps(float value, float v)
        {
            //return (value < v) ? value : v;
            return (value < 0) ? 0 : (value > v ? v : value);
        }
        public static float Min(float a, float b)
        {
            return (a < b) ? a : b;
        }
        public enum Random
        {
            value = 0,
            one = 1,
        }
        public static float Min(params float[] value)
        {
            int num = value.Length;
            if (num == 0)
            {
                return 0.0F;
            }
            float mun2 = value[0];
            for (int o = 1; o < num; o++)
            {
                if (value[o] < mun2)
                {
                    mun2 = value[o];
                }
            }
            return mun2;
        }
        /// <summary>
        /// 限制数值为0和1之间，可模拟抽奖,支持单精度和双精度
        /// </summary>
        /// <typeparam name="E"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static E Clamps<E>(E value) where E : IComparable<E>
        {
            dynamic min = Convert.ChangeType(0, typeof(E));
            dynamic max = Convert.ChangeType(1, typeof(E));
            return Clamped(value, min, max);
        }
        /// <summary>
        /// 线性插值，支持单、双精度浮点数
        /// </summary>
        /// <typeparam name="V"></typeparam>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static V Lest<V>(V a, V b, float t) where V : IConvertible
        {
            t = Clamps(t);
            dynamic da = a;
            dynamic db = b;
            return da + (db - da) * t;
        }
        /// <summary>
        /// 反向线性插值，支持单双精度浮点数
        /// </summary>
        /// <typeparam name="W"></typeparam>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static float Inverselerp<W>(W a, W b, W value) where W : IConvertible
        {
            dynamic da = a;
            dynamic db = b;
            dynamic dvalue = value;
            if (da == db)
            {
                throw new ArgumentException("a和b不能相等");
            }
            return Clamps((float)((dvalue - da) / (db - da)));
        }
        /// <summary>
        /// 适用于单线程的随机数生成器,该方法不建议使用，推荐使用randoms替代
        /// </summary>
        /// <returns></returns>
        //[Obsolete("random已过时，推荐使用randoms替代，randoms的线程是安全的")]
        public static double random()
        {
            return RandomNumberGeneratorHolder.randomNumberGenerator.NextDouble();
        }
        private static class RandomNumberGeneratorHolder { public static System.Random randomNumberGenerator = new System.Random(); }
        /// <summary>
        /// 适用于多线程的随机数生成器，建议使用randoms代替random
        /// </summary>
        /// <returns></returns>
        public static double randoms()
        {
            return System.Random.Shared.NextDouble();
        }

        public static int FloorToInt(float v)
        {
            int i = (int)v;
            return (v < i) ? i - 1 : i;
        }

        public static float PingPong(object value, float v)
        {
            // 将输入值转换为浮动类型
            float floatValue = Convert.ToSingle(value);

            // 使用 Mathf.PingPong 的公式
            return (float)(Mathf.Abs((floatValue % (v * 2))) - v) * 2;
        }
    }
    public static class Mathf
    {
        public static class MathConstants
        {
            public const double PI = 3.14159265358979323846;
            public const double DEG2RAD = PI / 180.0;
            public const double RAD2DEG = 180.0 / PI;
            public const double TAU = 6.28318530717958647692;
            public const double E = 2.71828182845904523536;
            public const double MPSILON = 1e-6f;
            public const double INFINITY = double.PositiveInfinity;

            public const double LOGIC_INFINITE = double.PositiveInfinity;
            public const double LOGIC_MIN = -double.MaxValue;
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct Plane
        {
            private const double kEpsilon = 1e-6f;
            public Vector3 Normal;
            public double D;

            public Plane(Vector3 inNormal, Vector3 inPoint)
            {
                Normal = inNormal;
                D = -Vector3.Dot(inNormal, inPoint);
            }

            public Plane(double x,double y,double z,double d)
            {
                Normal = new Vector3(x, y, z);
                D = d;
            }
        }


        [StructLayout(LayoutKind.Sequential, Size = 16, Pack = 8)]
        public struct Vector2
        {
            public double x, y;

            public Vector2(double x, double y)
            {
                this.x = x;
                this.y = y;
            }

            public static Vector2 operator +(Vector2 a, Vector2 b)
                => new Vector2(a.x + b.x, a.y + b.y);

            public static Vector2 operator -(Vector2 a, Vector2 b)
                => new Vector2(a.x - b.x, a.y - b.y);

            public static Vector2 operator *(Vector2 a, Vector2 b)
                => new Vector2(a.x * b.x, a.y * b.y);

            public static Vector2 operator /(Vector2 a, Vector2 b)
                => new Vector2(a.x / b.x, a.y / b.y);

            //重载运算符
            public static Vector2 operator +(Vector2 a, int b)
            {
                return new Vector2(a.x + b, a.y + b);
            }

            public static Vector2 operator +(Vector2 a,long b)
            {
                return new Vector2(a.x + b, a.y + b);
            }

            public static Vector2 operator +(Vector2 a, double b)
            {
                return new Vector2(a.x + b, a.y + b);
            }

            public static Vector2 operator +(Vector2 a, float b)
            {
                return new Vector2(a.x + b, a.y + b);
            }

            public static Vector2 operator -(Vector2 a, long b)
            {
                return new Vector2(a.x - b, a.y - b);
            }

            public static Vector2 operator -(Vector2 a, int b)
            {
                return new Vector2(a.x - b, a.y - b);
            }

            public static Vector2 operator -(Vector2 a, float b)
            {
                return new Vector2(a.x - b, a.y - b);
            }

            public static Vector2 operator -(Vector2 a,double b)
            {
                return new Vector2(a.x - b, a.y - b);
            }



            public static Vector2 operator *(Vector2 a, double b)
            {
                return new Vector2(a.x * b, a.y * b);
            }

            public static Vector2 operator *(Vector2 a, float b)
            {
                return new Vector2(a.x * b, a.y * b);
            }

            public static Vector2 operator *(Vector2 a,int b)
            {
                return new Vector2(a.x * b, a.y * b);
            }

            public static Vector2 operator *(Vector2 a,long b)
            {
                return new Vector2(a.x * b, a.y * b);
            }


            public static Vector2 operator /(Vector2 a, double b)
            {
                return new Vector2(a.x / b, a.y / b);
            }

            public static Vector2 operator /(Vector2 a, float b)
            {
                return new Vector2(a.x / b, a.y / b);
            }

            public static Vector2 operator /(Vector2 a, int b)
            {
                return new Vector2(a.x / b, a.y / b);
            }

            public static Vector2 operator /(Vector2 a, long b)
            {
                return new Vector2(a.x / b, a.y / b);
            }



            public static Vector2 operator +(int a, Vector2 b)
            {
                return new Vector2(a + b.x, a + b.y);
            }

            public static Vector2 operator +(long a,Vector2 b)
            {
                return new Vector2(a + b.x, a + b.y);
            }

            public static Vector2 operator +(double a, Vector2 b)
            {
                return new Vector2(a + b.x, a + b.y);
            }

            public static Vector2 operator +(float a, Vector2 b)
            {
                return new Vector2(a + b.x, a + b.y);
            }


            public static Vector2 operator -(int a, Vector2 b)
            {
                return new Vector2(a - b.x, a - b.y);
            }

            public static Vector2 operator -(long a, Vector2 b)
            {
                return new Vector2(a - b.x, a - b.y);
            }

            public static Vector2 operator -(double a, Vector2 b)
            {
                return new Vector2(a - b.x, a - b.y);
            }

            public static Vector2 operator -(float a, Vector2 b)
            {
                return new Vector2(a - b.x, a - b.y);
            }



            public static Vector2 operator *(double a, Vector2 b)
            {
                return new Vector2(a * b.x, a * b.y);
            }

            public static Vector2 operator *(float a, Vector2 b)
            {
                return new Vector2(a * b.x, a * b.y);
            }

            public static Vector2 operator *(int a, Vector2 b)
            {
                return new Vector2(a * b.x, a * b.y);
            }

            public static Vector2 operator *(long a, Vector2 b)
            {
                return new Vector2(a * b.x, a * b.y);
            }


            public static Vector2 operator /(double a, Vector2 b)
            {
                return new Vector2(a / b.x, a / b.y);
            }

            public static Vector2 operator /(float a, Vector2 b)
            {
                return new Vector2(a / b.x, a / b.y);
            }

            public static Vector2 operator /(int a, Vector2 b)
            {
                return new Vector2(a / b.x, a / b.y);
            }

            public static Vector2 operator /(long a, Vector2 b)
            {
                return new Vector2(a / b.x, a / b.y);
            }
        }
        [StructLayout(LayoutKind.Sequential, Size = 24, Pack = 8)]
        public struct Vector3
        {
            public double x, y, z;

            //Vector3构造函数


            public Vector3(double X, double Y, double Z)
            {
                x = X;
                y = Y;
                z = Z;
            }

            public static implicit operator Vector3((double x, double y, double z) tuple)
            {
                return new Vector3(tuple.x, tuple.y, tuple.z);
            }

            public static implicit operator double(Vector3 vector3)
            {
                return new Vector3(vector3.x, vector3.y, vector3.z);
            }

            public static Vector3 Cross(Vector3 vector,Vector3 vector1)
            {
                return new Vector3(
                    vector.y * vector1.z - vector.z * vector1.y,
                    vector.z * vector1.x - vector.x * vector1.z,
                    vector.x * vector1.y - vector.y * vector1.x
                );
            }

            public float Length()
            {
                return Sqrtf((float)(x * x), (float)((+y * y) + z * z));
            }

            public double Lenght()
            {
                return Mathf.Mathf_Sqrt(x * x + y * y + z * z);
            }

            public static double dot(Vector3 v1,Vector3 v2)
            {
                return v1.x * v2.x + v1.y * v2.y + v1.z * v2.z;
            }

            public static Vector3 Cross(double v1,Vector3 v2)
            {
                return new Vector3(
                    v1 * v2.y - v1 * v2.z,
                    v1 * v2.z - v1 * v2.x,
                    v1 * v2.x - v1 * v2.y
                );
            }

            public Vector3? Normalize()
            {
                float length = Length();
                if(length>0)
                {
                    return this/ length;
                }
                return new Vector3(0, 0, 0);
            }

            public Vector3 Normalize(Vector3 direction)
            {
                return direction.Normalize() ?? Vector3_Zero();
            }

            public static float Distance(Vector3 a, Vector3 b)
            {
                double dx = a.x - b.x;
                double dy = a.y - b.y;
                double dz = a.z - b.z;
                return Sqrt((float)(dx * dx + dy * dy + dz * dz));
            }

            public static Vector3 ProjectOnPlane(Vector3 movement, Vector3 normal)
            {
                var norm = normal.Normalize() ?? Vector3_Zero();
                var dot = movement.Dot(norm);
                return movement - norm * dot;
            }

            public float Dot(Vector3 v)
            {
                return (float)(x * v.x + y * v.y + z * v.z);
            }

            public static Vector3 Normalizes(Vector3 v)
            {
                double length = Sqrt((float)(v.x * v.x + v.y * v.y + v.z * v.z));
                if (length == 0f) return new Vector3(0f, 0f, 0f);
                float inv = (float)(1f / length);
                return new Vector3(v.x * inv, v.y * inv, v.z * inv);
            }

            public override string ToString() => $"Vector3({x}, {y}, {z})";

            public static Vector3 Transform(Vector3 direction, Matrix4x4 rotation)
            {
                var v = new Vector3((float)direction.x, (float)direction.y, (float)direction.z);
                Vector3 result = Vector3.Transform(v, rotation);
                return new Vector3(result.x, result.y, result.z);
            }

            public static Vector3 Transform(Vector3 value, Quaternion rotation)
            {
                float x2 = (float)((float)rotation.x + rotation.x);
                float y2 = (float)((float)rotation.y + rotation.y);
                float z2 = (float)((float)rotation.z + rotation.z);

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

            public readonly float LengthSquared()
            {
                return Dot(this, this);
            }

            public static float Dot(Vector3 vector1, Vector3 vector2)
            {
                return (float)((vector1.x * vector2.x)
                     + (vector1.y * vector2.y)
                     + (vector1.z * vector2.z));
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

            //运算符重载
            /// <summary>
            /// 加法运算符重载
            /// </summary>
            /// <param name="a">Vector3参数1</param>
            /// <param name="b">Vector3参数2</param>
            /// <returns>重载成功</returns>
            public static Vector3 operator +(Vector3 a, Vector3 b)
            => new Vector3(a.x + b.x, a.y + b.y, a.z + b.z);

            /// <summary>
            /// double加法运算符重载
            /// </summary>
            /// <param name="a">Vector3参数1</param>
            /// <param name="b">double参数1</param>
            /// <returns>重载成功</returns>
            public static Vector3 operator +(Vector3 a, double b)
            {
                return new Vector3(a.x + b, a.y + b, a.z + b);
            }

            public static Vector3 operator +(Vector3 a, float b)
            {
                return new Vector3(a.x + b, a.y + b, a.z + b);
            }

            public static Vector3 operator +(Vector3 a, int b)
            {
                return new Vector3(a.x + b, a.y + b, a.z + b);
            }

            public static Vector3 operator +(Vector3 a, long b)
            {
                return new Vector3(a.x + b, a.y + b, a.z + b);
            }

            public static Vector3 operator -(Vector3 a, Vector3 b)
                => new Vector3(a.x - b.x, a.y - b.y, a.z - b.z);

            public static Vector3 operator -(Vector3 a, double b)
            {
                return new Vector3(a.x - b, a.y - b, a.z - b);
            }

            public static Vector3 operator -(Vector3 a, float b)
            {
                return new Vector3(a.x - b, a.y - b, a.z - b);
            }

            public static Vector3 operator -(Vector3 a, int b)
            {
                return new Vector3(a.x - b, a.y - b, a.z - b);
            }

            public static Vector3 operator -(Vector3 a, long b)
            {
                return new Vector3(a.x - b, a.y - b, a.z - b);
            }

            public static Vector3 operator *(Vector3 a, double d)
                => new Vector3(a.x * d, a.y * d, a.z * d);

            public static Vector3 operator *(Vector3 left, Vector3 right)
            {
                return new Vector3(left.x * right.x, left.y * right.y, left.z * right.z);
            }

            public static Vector3 operator *(Vector3 a, float d)
            {
                return new Vector3(a.x * d, a.y * d, a.z * d);
            }

            public static Vector3 operator *(Vector3 a, int d)
            {
                return new Vector3(a.x * d, a.y * d, a.z * d);
            }

            public static Vector3 operator *(Vector3 a, long d)
            {
                return new Vector3(a.x * d, a.y * d, a.z * d);
            }

            public static Vector3 operator /(Vector3 a, double d)
                => new Vector3(a.x / d, a.y / d, a.z / d);

            public static Vector3 operator /(Vector3 a, Vector3 b)
            {
                return new Vector3(a.x / b.x, a.y / b.y, a.z / b.z);
            }

            public static Vector3 operator /(Vector3 a, float d)
            {
                return new Vector3(a.x / d, a.y / d, a.z / d);
            }

            public static Vector3 operator /(Vector3 a, int d)
            {
                return new Vector3(a.x / d, a.y / d, a.z / d);
            }

            public static Vector3 operator /(Vector3 a, long d)
            {
                return new Vector3(a.x / d, a.y / d, a.z / d);
            }

            //逆位重载法，兼容颠倒重载型

            public static Vector3 operator +(int a, Vector3 b)
            {
                return new Vector3(a + b.x, a + b.y, a + b.z);
            }

            public static Vector3 operator +(float a, Vector3 b)
            {
                return new Vector3(a + b.x, a + b.y, a + b.z);
            }

            public static Vector3 operator +(double a, Vector3 b)
            {
                return new Vector3(a + b.x, a + b.y, a + b.z);
            }

            public static Vector3 operator +(long a, Vector3 b)
            {
                return new Vector3(a + b.x, a + b.y, a + b.z);

            }

            public static Vector3 operator -(double a, Vector3 b)
            {
                return new Vector3(a - b.x, a - b.y, a - b.z);
            }

            public static Vector3 operator -(float a, Vector3 b)
            {
                return new Vector3(a - b.x, a - b.y, a - b.z);
            }

            public static Vector3 operator -(int a, Vector3 b)
            {
                return new Vector3(a - b.x, a - b.y, a - b.z);
            }

            public static Vector3 operator -(long a, Vector3 b)
            {
                return new Vector3(a - b.x, a - b.y, a - b.z);
            }

            public static Vector3 operator *(double d, Vector3 a)
            {
                return new Vector3(a.x * d, a.y * d, a.z * d);
            }

            public static Vector3 operator *(float d, Vector3 a)
            {
                return new Vector3(a.x * d, a.y * d, a.z * d);
            }

            public static Vector3 operator *(int d, Vector3 a)
            {
                return new Vector3(a.x * d, a.y * d, a.z * d);
            }

            public static Vector3 operator *(long d, Vector3 a)
            {
                return new Vector3(a.x * d, a.y * d, a.z * d);
            }

            public static Vector3 operator /(double d, Vector3 a)
            {
                return new Vector3(d / a.x, d / a.y, d / a.z);
            }

            public static Vector3 operator /(float d, Vector3 a)
            {
                return new Vector3(d / a.x, d / a.y, d / a.z);
            }

            public static Vector3 operator /(int d, Vector3 a)
            {
                return new Vector3(d / a.x, d / a.y, d / a.z);
            }

            public static Vector3 operator /(long d, Vector3 a)
            {
                return new Vector3(d / a.x, d / a.y, d / a.z);
            }

            public static explicit operator Vector3(double v)
            {
                return new Vector3(v, v, v);
            }

            public static Vector3 Zero()
            {
                return new Vector3(0, 0, 0);
            }

            public static Vector3 Left()
            {
                return new Vector3(-1, 0, 0);
            }
            public static Vector3 Right()
            {
                return new Vector3(1, 0, 0);
            }

            public static Vector3 Up()
            {
                return new Vector3(0, 1, 0);
            }

            public static Vector3 Down()
            {
                return new Vector3(0, -1, 0);
            }

            public static Vector3 One
            {
                 get => new Vector3(1, 1, 1);
            }

            public static Vector3 UnitX
            {
                get => new Vector3(1.0f, 0.0f, 0.0f);
            }

            public static Vector3 UnitY
            {
                get => new Vector3(0.0f, 1.0f, 0.0f);
            }

            public static Vector3 UnitZ
            {
                get => new Vector3(0.0f, 0.0f, 1.0f);
            }

        }
        [StructLayout(LayoutKind.Sequential, Size = 128, Pack = 8)]
        public struct Matrix4x4
        {
            public double M1, M2, M3, M4;
            public double M5, M6, M7, M8;
            public double M9, M10, M11, M12;
            public double M13, M14, M15, M16;

            public static Matrix4x4 CreateScale(Vector3 scales)
            {
                Matrix4x4 result = new Matrix4x4();
                result.M1 = scales.x;
                result.M6 = scales.y;
                result.M11 = scales.z;
                result.M16 = 1.0;
                return result;
            }

            public static Matrix4x4 CreateScale(Vector3 scales, Vector3 centerPoint)
            {
                Matrix4x4 result = new Matrix4x4();
                result.M1 = scales.x;
                result.M6 = scales.y;
                result.M11 = scales.z;
                result.M16 = 1.0;
                result.M4 = centerPoint.x * (1 - scales.x);
                result.M8 = centerPoint.y * (1 - scales.y);
                result.M12 = centerPoint.z * (1 - scales.z);
                return result;
            }

            public static Matrix4x4 CreateScale(float scale)
            {
                Matrix4x4 result = new Matrix4x4();
                result.M1 = scale;
                result.M6 = scale;
                result.M11 = scale;
                result.M16 = 1.0;
                return result;
            }

            public static Matrix4x4 CreateScale(float scale, Vector3 centerPoint)
            {
                Matrix4x4 result = new Matrix4x4();
                result.M1 = scale;
                result.M6 = scale;
                result.M11 = scale;
                result.M16 = 1.0;
                result.M4 = centerPoint.x * (1 - scale);
                result.M8 = centerPoint.y * (1 - scale);
                result.M12 = centerPoint.z * (1 - scale);
                return result;
            }

            public static Matrix4x4 CreateScale(float xScale, float yScale, float zScale)
            {
                Matrix4x4 result = new Matrix4x4();
                result.M1 = xScale;
                result.M6 = yScale;
                result.M11 = zScale;
                result.M16 = 1.0;
                return result;
            }

            public static Matrix4x4 CreateScale(float xScale, float yScale, float zScale, Vector3 centerPoint)
            {
                Matrix4x4 result = new Matrix4x4();
                result.M1 = xScale;
                result.M6 = yScale;
                result.M11 = zScale;
                result.M16 = 1.0;
                result.M4 = centerPoint.x * (1 - xScale);
                result.M8 = centerPoint.y * (1 - yScale);
                result.M12 = centerPoint.z * (1 - zScale);
                return result;
            }

            public static Matrix4x4 CreateShadow(Vector3 lightDirection, Plane plane)
            {
                Matrix4x4 result = new Matrix4x4();
                float dot = (float)(plane.Normal.x * lightDirection.x + plane.Normal.y * lightDirection.y + plane.Normal.z * lightDirection.z);
                result.M1 = dot - lightDirection.x * plane.Normal.x;
                result.M5 = 0.0f - lightDirection.x * plane.Normal.y;
                result.M9 = 0.0f - lightDirection.x * plane.Normal.z;
                result.M13 = 0.0f - lightDirection.x * plane.D;
                result.M2 = 0.0f - lightDirection.y * plane.Normal.x;
                result.M6 = dot - lightDirection.y * plane.Normal.y;
                result.M10 = 0.0f - lightDirection.y * plane.Normal.z;
                result.M14 = 0.0f - lightDirection.y * plane.D;
                result.M3 = 0.0f - lightDirection.z * plane.Normal.x;
                result.M7 = 0.0f - lightDirection.z * plane.Normal.y;
                result.M11 = dot - lightDirection.z * plane.Normal.z;
                result.M15 = 0.0f - lightDirection.z * plane.D;
                result.M4 = 0.0f;
                result.M8 = 0.0f;
                result.M12 = 0.0f;
                result.M16 = 1.0f;
                return result;
            }

            public static Matrix4x4 CreateTranslation(Vector3 position)
            {
                Matrix4x4 result = new Matrix4x4();
                result.M1 = 1.0;
                result.M6 = 1.0;
                result.M11 = 1.0;
                result.M16 = 1.0;
                result.M4 = position.x;
                result.M8 = position.y;
                result.M12 = position.z;
                return result;
            }

            public static Matrix4x4 CreateTranslation(float xPosition, float yPosition, float zPosition)
            {
                Matrix4x4 result = new Matrix4x4();
                result.M1 = 1.0;
                result.M6 = 1.0;
                result.M11 = 1.0;
                result.M16 = 1.0;
                result.M4 = xPosition;
                result.M8 = yPosition;
                result.M12 = zPosition;
                return result;
            }

            public static Matrix4x4 CreateViewport(float x, float y, float width, float height, float minDepth, float maxDepth)
            {
                Matrix4x4 result = new Matrix4x4();
                result.M1 = width / 2.0f;
                result.M6 = -height / 2.0f;
                result.M11 = maxDepth - minDepth;
                result.M4 = x + width / 2.0f;
                result.M8 = y + height / 2.0f;
                result.M12 = minDepth;
                result.M16 = 1.0f;
                return result;
            }

            public static Matrix4x4 CreateViewportLeftHanded(float x, float y, float width, float height, float minDepth, float maxDepth)
            {
                Matrix4x4 result = new Matrix4x4();
                result.M1 = width / 2.0f;
                result.M6 = height / 2.0f;
                result.M11 = maxDepth - minDepth;
                result.M4 = x + width / 2.0f;
                result.M8 = y + height / 2.0f;
                result.M12 = minDepth;
                result.M16 = 1.0f;
                return result;
            }

            public static Matrix4x4 CreateWorld(Vector3 position, Vector3 forward, Vector3 up)
            {
                Vector3 vector3 = Vector3.Normalizes(forward);
                Vector3 vector31 = Vector3.Normalizes(Vector3.Cross(up, vector3));
                Vector3 vector32 = Vector3.Cross(vector3, vector31);
                Matrix4x4 result = new Matrix4x4();
                result.M1 = vector31.x;
                result.M2 = vector31.y;
                result.M3 = vector31.z;
                result.M4 = 0.0f;
                result.M5 = vector32.x;
                result.M6 = vector32.y;
                result.M7 = vector32.z;
                result.M8 = 0.0f;
                result.M9 = vector3.x;
                result.M10 = vector3.y;
                result.M11 = vector3.z;
                result.M12 = 0.0f;
                result.M13 = position.x;
                result.M14 = position.y;
                result.M15 = position.z;
                result.M16 = 1.0f;
                return result;
            }


            public static bool Decompose(Matrix4x4 matrix, out Vector3 scale, out Quaternion rotation, out Vector3 translation)
            {
                rotation = new Quaternion();
                translation = new Vector3(matrix.M4, matrix.M8, matrix.M12);

                Vector3 vector3 = new Vector3(matrix.M1, matrix.M2, matrix.M3);
                Vector3 vector31 = new Vector3(matrix.M5, matrix.M6, matrix.M7);
                Vector3 vector32 = new Vector3(matrix.M9, matrix.M10, matrix.M11);

                scale = new Vector3(vector3.Length(), vector31.Length(), vector32.Length());

                if (scale.x == 0.0f || scale.y == 0.0f || scale.z == 0.0f)
                {
                    rotation = new Quaternion(0.0f, 0.0f, 0.0f, 1f);
                    return false;
                }

                // Normalize the matrix to get the rotation part
                Matrix4x4 tempMatrix = new Matrix4x4();
                tempMatrix.M1 = matrix.M1 / scale.x;
                tempMatrix.M2 = matrix.M2 / scale.x;
                tempMatrix.M3 = matrix.M3 / scale.x;
                tempMatrix.M5 = matrix.M5 / scale.y;
                tempMatrix.M6 = matrix.M6 / scale.y;
                tempMatrix.M7 = matrix.M7 / scale.y;
                tempMatrix.M9 = matrix.M9 / scale.z;
                tempMatrix.M10 = matrix.M10 / scale.z;
                tempMatrix.M11 = matrix.M11 / scale.z;

                float trace = (float)(tempMatrix.M1 + tempMatrix.M6 + tempMatrix.M11);

                if (trace > 0.0f)
                {
                    float s = Mathf.Sqrt(trace + 1.0f) * 2f;
                    rotation.w = s * 0.25f;
                    rotation.x = (tempMatrix.M7 - tempMatrix.M10) / s;
                    rotation.y = (tempMatrix.M9 - tempMatrix.M3) / s;
                }

                return true;
            }

            public static bool Invert(Matrix4x4 matrix, out Matrix4x4 result)
            {
                result = new Matrix4x4();
                float b0 = (float)(matrix.M1 * matrix.M6 - matrix.M2 * matrix.M5);
                float b1 = (float)(matrix.M1 * matrix.M7 - matrix.M3 * matrix.M5);
                float b2 = (float)(matrix.M1 * matrix.M8 - matrix.M4 * matrix.M5);
                float b3 = (float)(matrix.M2 * matrix.M7 - matrix.M3 * matrix.M6);
                float b4 = (float)(matrix.M2 * matrix.M8 - matrix.M4 * matrix.M6);
                float b5 = (float)(matrix.M3 * matrix.M8 - matrix.M4 * matrix.M7);

                float det = b0 * b5 - b1 * b4 + b2 * b3;

                if (det == 0.0f)
                {
                    return false;
                }

                float invDet = 1.0f / det;

                result.M1 = (matrix.M6 * b5 - matrix.M7 * b4 + matrix.M8 * b3) * invDet;
                result.M2 = (matrix.M3 * b4 - matrix.M2 * b5 - matrix.M8 * b2) * invDet;
                result.M3 = (matrix.M2 * b3 - matrix.M3 * b2 + matrix.M7 * b1) * invDet;
                result.M4 = (matrix.M3 * b4 - matrix.M2 * b3 - matrix.M7 * b0) * invDet;

                // Fill out the rest of the matrix...

                return true;
            }

            public static Matrix4x4 Lerp(Matrix4x4 matrix1, Matrix4x4 matrix2, float amount)
            {
                Matrix4x4 result = new Matrix4x4();
                result.M1 = matrix1.M1 + (matrix2.M1 - matrix1.M1) * amount;
                result.M2 = matrix1.M2 + (matrix2.M2 - matrix1.M2) * amount;
                result.M3 = matrix1.M3 + (matrix2.M3 - matrix1.M3) * amount;
                result.M4 = matrix1.M4 + (matrix2.M4 - matrix1.M4) * amount;
                result.M5 = matrix1.M5 + (matrix2.M5 - matrix1.M5) * amount;
                result.M6 = matrix1.M6 + (matrix2.M6 - matrix1.M6) * amount;
                result.M7 = matrix1.M7 + (matrix2.M7 - matrix1.M7) * amount;
                result.M8 = matrix1.M8 + (matrix2.M8 - matrix1.M8) * amount;
                result.M9 = matrix1.M9 + (matrix2.M9 - matrix1.M9) * amount;
                result.M10 = matrix1.M10 + (matrix2.M10 - matrix1.M10) * amount;
                result.M11 = matrix1.M11 + (matrix2.M11 - matrix1.M11) * amount;
                result.M12 = matrix1.M12 + (matrix2.M12 - matrix1.M12) * amount;
                result.M13 = matrix1.M13 + (matrix2.M13 - matrix1.M13) * amount;
                result.M14 = matrix1.M14 + (matrix2.M14 - matrix1.M14) * amount;
                result.M15 = matrix1.M15 + (matrix2.M15 - matrix1.M15) * amount;
                result.M16 = matrix1.M16 + (matrix2.M16 - matrix1.M16) * amount;

                return result;
            }

            public static Matrix4x4 Multiply(Matrix4x4 value1, Matrix4x4 value2)
            {
                Matrix4x4 result = new Matrix4x4();
                result.M1 = value1.M1 * value2.M1 + value1.M2 * value2.M5 + value1.M3 * value2.M9 + value1.M4 * value2.M13;
                result.M2 = value1.M1 * value2.M2 + value1.M2 * value2.M6 + value1.M3 * value2.M10 + value1.M4 * value2.M14;
                result.M3 = value1.M1 * value2.M3 + value1.M2 * value2.M7 + value1.M3 * value2.M11 + value1.M4 * value2.M15;
                result.M4 = value1.M1 * value2.M4 + value1.M2 * value2.M8 + value1.M3 * value2.M12 + value1.M4 * value2.M16;
                result.M5 = value1.M5 * value2.M1 + value1.M6 * value2.M5 + value1.M7 * value2.M9 + value1.M8 * value2.M13;
                result.M6 = value1.M5 * value2.M2 + value1.M6 * value2.M6 + value1.M7 * value2.M10 + value1.M8 * value2.M14;
                result.M7 = value1.M5 * value2.M3 + value1.M6 * value2.M7 + value1.M7 * value2.M11 + value1.M8 * value2.M15;
                result.M8 = value1.M5 * value2.M4 + value1.M6 * value2.M8 + value1.M7 * value2.M12 + value1.M8 * value2.M16;
                result.M9 = value1.M9 * value2.M1 + value1.M10 * value2.M5 + value1.M11 * value2.M9 + value1.M12 * value2.M13;
                result.M10 = value1.M9 * value2.M2 + value1.M10 * value2.M6 + value1.M11 * value2.M10 + value1.M12 * value2.M14;
                result.M11 = value1.M9 * value2.M3 + value1.M10 * value2.M7 + value1.M11 * value2.M11 + value1.M12 * value2.M15;
                result.M12 = value1.M9 * value2.M4 + value1.M10 * value2.M8 + value1.M11 * value2.M12 + value1.M12 * value2.M16;
                result.M13 = value1.M13 * value2.M1 + value1.M14 * value2.M5 + value1.M15 * value2.M9 + value1.M16 * value2.M13;
                result.M14 = value1.M13 * value2.M2 + value1.M14 * value2.M6 + value1.M15 * value2.M10 + value1.M16 * value2.M14;
                result.M15 = value1.M13 * value2.M3 + value1.M14 * value2.M7 + value1.M15 * value2.M11 + value1.M16 * value2.M15;
                result.M16 = value1.M13 * value2.M4 + value1.M14 * value2.M8 + value1.M15 * value2.M12 + value1.M16 * value2.M16;

                return result;
            }

            public static Matrix4x4 Multiply(Matrix4x4 value1, float value2)
            {
                Matrix4x4 result = new Matrix4x4();
                result.M1 = value1.M1 * value2;
                result.M2 = value1.M2 * value2;
                result.M3 = value1.M3 * value2;
                result.M4 = value1.M4 * value2;
                result.M5 = value1.M5 * value2;
                result.M6 = value1.M6 * value2;
                result.M7 = value1.M7 * value2;
                result.M8 = value1.M8 * value2;
                result.M9 = value1.M9 * value2;
                result.M10 = value1.M10 * value2;
                result.M11 = value1.M11 * value2;
                result.M12 = value1.M12 * value2;
                result.M13 = value1.M13 * value2;
                result.M14 = value1.M14 * value2;
                result.M15 = value1.M15 * value2;
                result.M16 = value1.M16 * value2;

                return result;
            }

            public static Matrix4x4 Negate(Matrix4x4 value)
            {
                Matrix4x4 result = new Matrix4x4();
                result.M1 = -value.M1;
                result.M2 = -value.M2;
                result.M3 = -value.M3;
                result.M4 = -value.M4;
                result.M5 = -value.M5;
                result.M6 = -value.M6;
                result.M7 = -value.M7;
                result.M8 = -value.M8;
                result.M9 = -value.M9;
                result.M10 = -value.M10;
                result.M11 = -value.M11;
                result.M12 = -value.M12;
                result.M13 = -value.M13;
                result.M14 = -value.M14;
                result.M15 = -value.M15;
                result.M16 = -value.M16;

                return result;
            }

            public static Matrix4x4 Subtract(Matrix4x4 value1, Matrix4x4 value2)
            {
                Matrix4x4 result = new Matrix4x4();
                result.M1 = value1.M1 - value2.M1;
                result.M2 = value1.M2 - value2.M2;
                result.M3 = value1.M3 - value2.M3;
                result.M4 = value1.M4 - value2.M4;
                result.M5 = value1.M5 - value2.M5;
                result.M6 = value1.M6 - value2.M6;
                result.M7 = value1.M7 - value2.M7;
                result.M8 = value1.M8 - value2.M8;
                result.M9 = value1.M9 - value2.M9;
                result.M10 = value1.M10 - value2.M10;
                result.M11 = value1.M11 - value2.M11;
                result.M12 = value1.M12 - value2.M12;
                result.M13 = value1.M13 - value2.M13;
                result.M14 = value1.M14 - value2.M14;
                result.M15 = value1.M15 - value2.M15;
                result.M16 = value1.M16 - value2.M16;

                return result;
            }

            public static Matrix4x4 Transform(Matrix4x4 value, Quaternion rotation)
            {
                Matrix4x4 result = new Matrix4x4();
                result.M1 = value.M1 * rotation.x + 1.0f;
                result.M2 = value.M2 * rotation.y + 1.0f;
                result.M3 = value.M3 * rotation.z + 1.0f;
                result.M4 = value.M4 * rotation.w + 1.0f;
                result.M5 = value.M5 + 1.0f;
                result.M6 = value.M6 + 1.0f;
                result.M7 = value.M7 + 1.0f;
                result.M8 = value.M8 + 1.0f;
                result.M9 = value.M9 + 1.0f;
                result.M10 = value.M10 + 1.0f;
                result.M11 = value.M11 + 1.0f;
                result.M12 = value.M12 + 1.0f;
                result.M13 = value.M13 + 1.0f;
                result.M14 = value.M14 + 1.0f;
                result.M15 = value.M15 + 1.0f;
                result.M16 = value.M16 + 1.0f;

                return result;
            }

            public static Matrix4x4 Transpose(Matrix4x4 m)
            {
                Matrix4x4 result;
                result.M1 = m.M1; result.M2 = m.M5; result.M3 = m.M9; result.M4 = m.M13;
                result.M5 = m.M2; result.M6 = m.M6; result.M7 = m.M10; result.M8 = m.M14;
                result.M9 = m.M3; result.M10 = m.M7; result.M11 = m.M11; result.M12 = m.M15;
                result.M13 = m.M4; result.M14 = m.M8; result.M15 = m.M12; result.M16 = m.M16;

                return result;
            }


            public static Matrix4x4 CreateFromQuaternion(Quaternion localRotation)
            {
                // 计算四元数分量
                float xx = (float)(localRotation.x * localRotation.x);
                float yy = (float)(localRotation.y * localRotation.y);
                float zz = (float)(localRotation.z * localRotation.z);
                float xy = (float)(localRotation.x * localRotation.y);
                float xz = (float)(localRotation.x * localRotation.z);
                float xw = (float)(localRotation.x * localRotation.w);
                float yz = (float)(localRotation.y * localRotation.z);
                float yw = (float)(localRotation.y * localRotation.w);
                float zw = (float)(localRotation.z * localRotation.w);

                // 填充旋转矩阵
                Matrix4x4 result = new Matrix4x4();
                result.M1 = 1.0f - 2.0f * (yy + zz); // M1
                result.M2 = 2.0f * (xy - zw);       // M2
                result.M3 = 2.0f * (xz + yw);       // M3
                result.M4 = 0.0f;

                result.M5 = 2.0f * (xy + zw);       // M5
                result.M6 = 1.0f - 2.0f * (xx + zz); // M6
                result.M7 = 2.0f * (yz - xw);       // M7
                result.M8 = 0.0f;

                result.M9 = 2.0f * (xz - yw);       // M9
                result.M10 = 2.0f * (yz + xw);      // M10
                result.M11 = 1.0f - 2.0f * (xx + yy); // M11
                result.M12 = 0.0f;

                result.M13 = 0.0f;  // M13
                result.M14 = 0.0f;  // M14
                result.M15 = 0.0f;  // M15
                result.M16 = 1.0f;  // M16

                return result;
            }

            public static Matrix4x4 operator +(Matrix4x4 a,Matrix4x4 b)
            {
                return new Matrix4x4
                {
                    M1 = a.M1 + b.M1,
                    M2 = a.M2 + b.M2,
                    M3 = a.M3 + b.M3,
                    M4 = a.M4 + b.M4,
                    M5 = a.M5 + b.M5,
                    M6 = a.M6 + b.M6,
                    M7 = a.M7 + b.M7,
                    M8 = a.M8 + b.M8,
                    M9 = a.M9 + b.M9,
                    M10 = a.M10 + b.M10,
                    M11 = a.M11 + b.M11,
                    M12 = a.M12 + b.M12,
                    M13 = a.M13 + b.M13,
                    M14 = a.M14 + b.M14,
                    M15 = a.M15 + b.M15,
                    M16 = a.M16 + b.M16,
                };
            }

            public static Matrix4x4 operator -(Matrix4x4 a, Matrix4x4 b)
            {
                return new Matrix4x4
                {
                    M1 = a.M1 - b.M1,
                    M2 = a.M2 - b.M2,
                    M3 = a.M3 - b.M3,
                    M4 = a.M4 - b.M4,
                    M5 = a.M5 - b.M5,
                    M6 = a.M6 - b.M6,
                    M7 = a.M7 - b.M7,
                    M8 = a.M8 - b.M8,
                    M9 = a.M9 - b.M9,
                    M10 = a.M10 - b.M10,
                    M11 = a.M11 - b.M11,
                    M12 = a.M12 - b.M12,
                    M13 = a.M13 - b.M13,
                    M14 = a.M14 - b.M14,
                    M15 = a.M15 - b.M15,
                    M16 = a.M16 - b.M16,
                };
            }

            public static Matrix4x4 operator -(Matrix4x4 m)
            {
                return new Matrix4x4
                {
                    M1 = -m.M1,
                    M2 = -m.M2,
                    M3 = -m.M3,
                    M4 = -m.M4,
                    M5 = -m.M5,
                    M6 = -m.M6,
                    M7 = -m.M7,
                    M8 = -m.M8,
                    M9 = -m.M9,
                    M10 = -m.M10,
                    M11 = -m.M11,
                    M12 = -m.M12,
                    M13 = -m.M13,
                    M14 = -m.M14,
                    M15 = -m.M15,
                    M16 = -m.M16,
                };
            }

            public static Matrix4x4 operator *(Matrix4x4 a, Matrix4x4 b)
            {
                return Multiply(a, b);
            }

            public static Matrix4x4 operator *(Matrix4x4 m, float s)
            {
                return Multiply(m, s);
            }

            public static Matrix4x4 operator *(float s, Matrix4x4 m)
            {
                return Multiply(m, s);
            }

            public static Matrix4x4 operator /(Matrix4x4 m, float s)
            {
                float inv = 1.0f / s;
                return Multiply(m, inv);
            }


        }
        [StructLayout(LayoutKind.Sequential, Size = 32, Pack = 8)]
        public struct Color
        {
            public double r, g, b, a;
            public Color(double red, double green, double blue, double alpha)
            {
                r = red;
                g = green;
                b = blue;
                a = alpha;
            }

            public static Color operator +(Color a, Color b)
                => new Color(a.r + b.r, a.g + b.g, a.b + b.b, a.a + b.a);

            public static Color operator -(Color a, Color b)
                => new Color(a.r - b.r, a.g - b.g, a.b - b.b, a.a - b.a);

            public static Color operator *(Color a,Color b)
                => new Color(a.r * b.r, a.g * b.g, a.b * b.b, a.a * b.a);

            public static Color operator /(Color a, Color b)
            => new Color(a.r / b.r, a.g / b.g, a.b / b.b, a.a / b.a);



            public static Color operator +(Color a, double b)
            {
                return new Color(a.r + b, a.g + b, a.b + b, a.a + b);
            }

            public static Color operator +(Color a,float b)
            {
                return new Color(a.r + b, a.g + b, a.b + b, a.a + b);
            }

            public static Color operator +(Color a,int b)
            {
                return new Color(a.r + b, a.g + b, a.b + b, a.a + b);
            }

            public static Color operator +(Color a,long b)
            {
                return new Color(a.r + b, a.g + b, a.b + b, a.a + b);
            }

            public static Color operator -(Color a, float b)
            {
                return new Color(a.r - b, a.g - b, a.b - b, a.a - b);
            }


            public static Color operator -(Color a, double b)
            {
                return new Color(a.r - b, a.g - b, a.b - b, a.a - b);
            }

            public static Color operator -(Color a, int b)
            {
                return new Color(a.r - b, a.g - b, a.b - b, a.a - b);
            }

            public static Color operator -(Color a,long b)
            {
                return new Color(a.r - b, a.g - b, a.b - b, a.a - b);
            }

            public static Color operator *(Color a, double b)
            {
                return new Color(a.r * b, a.g * b, a.b * b, a.a * b);
            }

            public static Color operator *(Color a, float b)
            {
                return new Color(a.r * b, a.g * b, a.b * b, a.a * b);
            }

            public static Color operator *(Color a, int b)
            {
                return new Color(a.r * b, a.g * b, a.b * b, a.a * b);
            }

            public static Color operator *(Color a, long b)
            {
                return new Color(a.r * b, a.g * b, a.b * b, a.a * b);
            }

            public static Color operator /(Color a, double b)
            {
                return new Color(a.r / b, a.g / b, a.b / b, a.a / b);
            }

            public static Color operator /(Color a, float b)
            {
                return new Color(a.r / b, a.g / b, a.b / b, a.a / b);
            }

            public static Color operator /(Color a, int b)
            {
                return new Color(a.r / b, a.g / b, a.b / b, a.a / b);
            }

            public static Color operator /(Color a,long b)
            {
                return new Color(a.r / b, a.g / b, a.b / b, a.a / b);
            }


            //逆向重载
            public static Color operator +(double a, Color b)
            {
                return new Color(a + b.r, a + b.g, a + b.b, a + b.a);
            }

            public static Color operator +(float a, Color b)
            {
                return new Color(a + b.r, a + b.g, a + b.b, a + b.a);
            }

            public static Color operator +(int a, Color b)
            {
                return new Color(a + b.r, a + b.g, a + b.b, a + b.a);
            }

            public static Color operator +(long a, Color b)
            {
                return new Color(a + b.r, a + b.g, a + b.b, a + b.a);
            }

            public static Color operator -(double a, Color b)
            {
                return new Color(a - b.r, a - b.g, a - b.b, a - b.a);
            }

            public static Color operator -(float a,Color b)
            {
                return new Color(a - b.r, a - b.g, a - b.b, a - b.a);
            }

            public static Color operator -(int a,Color b)
            {
                return new Color(a - b.r, a - b.g, a - b.b, a - b.a);
            }

            public static Color operator -(long a, Color b)
            {
                return new Color(a - b.r, a - b.g, a - b.b, a - b.a);
            }

            public static Color operator *(double a, Color b)
            {
                return new Color(a * b.r, a * b.g, a * b.b, a * b.a);
            }

            public static Color operator *(float a, Color b)
            {
                return new Color(a * b.r, a * b.g, a * b.b, a * b.a);
            }

            public static Color operator *(int a, Color b)
            {
                return new Color(a * b.r, a * b.g, a * b.b, a * b.a);
            }

            public static Color operator *(long a, Color b)
            {
                return new Color(a * b.r, a * b.g, a * b.b, a * b.a);
            }

            public static Color operator /(int a,Color b)
            {
                return new Color(a / b.r, a / b.g, a / b.b, a / b.a);
            }

            public static Color operator /(long a, Color b)
            {
                return new Color(a / b.r, a / b.g, a / b.b, a / b.a);
            }

            public static Color operator /(float a, Color b)
            {
                return new Color(a / b.r, a / b.g, a / b.b, a / b.a);
            }

            public static Color operator /(double a, Color b)
            {
                return new Color(a / b.r, a / b.g, a / b.b, a / b.a);
            }
        }
        [StructLayout(LayoutKind.Sequential, Size = 32, Pack = 8)]
        public struct Quaternion
        {
            public double x, y, z, w;

            public Quaternion(double X, double Y, double Z, double W)
            {
                x = X;
                y = Y;
                z = Z;
                w = W;
            }

            public static Quaternion Identity()
            {
                return new Quaternion(0, 0, 0, 1);
            }
        }
        [StructLayout(LayoutKind.Sequential, Size = 16, Pack = 8)]
        public struct Point
        {
            public double x, y;
        }
        [StructLayout(LayoutKind.Sequential, Size = 16, Pack = 8)]
        public struct Size
        {
            public double width, height;
        }
        [StructLayout(LayoutKind.Sequential, Size = 32, Pack = 8)]
        public struct Rect
        {
            Point origin;
            Size size;
        }
        [StructLayout(LayoutKind.Sequential, Size = 32, Pack = 8)]
        public struct Bounds
        {
            public double x, y, width, height;
        }
        [StructLayout(LayoutKind.Sequential, Size = 48, Pack = 8)]
        public struct Bounds3D
        {
            public double x, y, z, width, height, depth;
        }
        [StructLayout(LayoutKind.Sequential, Size = 48, Pack = 8)]
        public struct Box
        {
            public double x, y, z, w, h, d;
        }
        [StructLayout(LayoutKind.Sequential, Size = 24, Pack = 8)]
        public struct Euler
        {
            public double x, y, z;
        }
        [StructLayout(LayoutKind.Sequential, Size = 24, Pack = 8)]
        public struct AxisAngle
        {
            public double x, y, z;
        }
        [StructLayout(LayoutKind.Sequential, Size = 16, Pack = 8)]
        public struct Complex
        {
            public double real, imag;
        }
        [StructLayout(LayoutKind.Sequential, Size = 32, Pack = 8)]
        public struct QuaternionComplex
        {
            public double real, imag, jmag, kmag;
        }
        [StructLayout(LayoutKind.Sequential, Size = 48, Pack = 8)]
        public struct HyperBox
        {
            public double x, y, z, w, h, d;
        }
        [StructLayout(LayoutKind.Sequential, Size = 56, Pack = 8)]
        public struct HyperBox5D
        {
            public double x, y, z, w, h, d, e;
        }
        [StructLayout(LayoutKind.Explicit, Size = 13, Pack = 1)]
        public struct LogicDouble
        {
            [FieldOffset(0)]
            public uint64_t mantissa;
            [FieldOffset(8)]
            public int32_t exponent;
            [FieldOffset(12)]
            public uint8_t sign;
        }
        [StructLayout(LayoutKind.Sequential, Size = 32, Pack = 8)]
        public struct Quat
        {
            public double x, y, z, w;
        }
        [StructLayout(LayoutKind.Sequential, Size = 128, Pack = 8)]
        public struct Math4
        {
            public double M1, M2, M3, M4;
            public double M5, M6, M7, M8;
            public double M9, M10, M11, M12;
            public double M13, M14, M15, M16;


            public static Math4 operator *(Math4 a, Math4 b)
            {
                Math4 result = new Math4();

                result.M1 = a.M1 * b.M1 + a.M2 * b.M5 + a.M3 * b.M9 + a.M4 * b.M13;
                result.M2 = a.M1 * b.M2 + a.M2 * b.M6 + a.M3 * b.M10 + a.M4 * b.M14;
                result.M3 = a.M1 * b.M3 + a.M2 * b.M7 + a.M3 * b.M11 + a.M4 * b.M15;
                result.M4 = a.M1 * b.M4 + a.M2 * b.M8 + a.M3 * b.M12 + a.M4 * b.M16;

                result.M5 = a.M5 * b.M1 + a.M6 * b.M5 + a.M7 * b.M9 + a.M8 * b.M13;
                result.M6 = a.M5 * b.M2 + a.M6 * b.M6 + a.M7 * b.M10 + a.M8 * b.M14;
                result.M7 = a.M5 * b.M3 + a.M6 * b.M7 + a.M7 * b.M11 + a.M8 * b.M15;
                result.M8 = a.M5 * b.M4 + a.M6 * b.M8 + a.M7 * b.M12 + a.M8 * b.M16;

                result.M9 = a.M9 * b.M1 + a.M10 * b.M5 + a.M11 * b.M9 + a.M12 * b.M13;
                result.M10 = a.M9 * b.M2 + a.M10 * b.M6 + a.M11 * b.M10 + a.M12 * b.M14;
                result.M11 = a.M9 * b.M3 + a.M10 * b.M7 + a.M11 * b.M11 + a.M12 * b.M15;
                result.M12 = a.M9 * b.M4 + a.M10 * b.M8 + a.M11 * b.M12 + a.M12 * b.M16;

                result.M13 = a.M13 * b.M1 + a.M14 * b.M5 + a.M15 * b.M9 + a.M16 * b.M13;
                result.M14 = a.M13 * b.M2 + a.M14 * b.M6 + a.M15 * b.M10 + a.M16 * b.M14;
                result.M15 = a.M13 * b.M3 + a.M14 * b.M7 + a.M15 * b.M11 + a.M16 * b.M15;
                result.M16 = a.M13 * b.M4 + a.M14 * b.M8 + a.M15 * b.M12 + a.M16 * b.M16;

                return result;
            }

            public override string ToString()
            {
                return $"[{M1}, {M2}, {M3}, {M4}]\n" +
                       $"[{M5}, {M6}, {M7}, {M8}]\n" +
                       $"[{M9}, {M10}, {M11}, {M12}]\n" +
                       $"[{M13}, {M14}, {M15}, {M16}]";
            }
        }


        //Mathf函数
        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern double Mathf_Clamp(double x, double min, double max);
        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern double Mathf_Log(double value);
        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern double Mathf_Sin(double angle);
        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern double Mathf_Cos(double angle);
        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern double Mathf_Tan(double angle);

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern double Mathf_Asin(double value);

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern double Mathf_Acos(double value);

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern double Mathf_Atan(double value);

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern double Mathf_Atan2(double y, double x);

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern double Mathf_Sqrt(double value);

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern double Mathf_Sqrtf(double a, double b);

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern double Mathf_Abs(double value);

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern double Mathf_Min(double a, double b);

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern double Mathf_Max(double a, double b);

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern double Mathf_Lerp(double a, double b, double t);

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern double Mathf_Clamp01(double value);

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern double Mathf_Exp(double x);

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern double Mathf_LerpUnclamped(double a, double b, double t);

        //float版Mahtf函数
        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern float Sqrt(float value);

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern float Sqrtf(float value, float value1);

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern float Sin(float angle);

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern float Cos(float angle);

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern float Abs(float value);

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern float Sign(float x);

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern float Lerp(float a, float b, float t);

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern float Clamps(float value);

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern float LerpUnclamped(float a, float b, float t);

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern float Min(float a, float b);

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern float Max(float a, float b);

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern float Clamp(float x, float min, float max);

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern float Exp(float x);

        //Quat函数
        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern Quat Quat_Identity();

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern Quat Quat_FromAxisAngle(Vector3 axis, double angle);

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern Quat Quat_Multiply(Quat a, Quat b);

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern Vector3 Quat_RotateVec3(Quat q, Vector3 v);

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern Quat Quat_Normalize(Quat q);

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern Quat Quat_Inverse(Quat q);

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern Quat Quat_Slerp(Quat a, Quat b, double t);

        //Math4函数
        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern Math4 Math4_Translate(Vector3 v);

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern Math4 Math4_Scale(Vector3 v);

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern Math4 Math4_RotateX(double angleRad);

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern Math4 Math4_RotateY(double angleRad);

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern Math4 Math4_RotateZ(double angleRad);

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern Math4 Math4_Multiply(Math4 a, Math4 b);

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern Vector4 Math4_MultiplyVector4(Math4 m, Vector4 v);

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern Math4 Math4_Identity();

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern Math4 Math4_LookAt(Vector3 eye, Vector3 target, Vector3 up);

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern Math4 Math4_Perspective(double fovY, double aspect, double nears, double fars);

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern Math4 Math4_Ortho(double left, double right, double bottom, double top, double nears, double fars);

        //Vector2函数

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern Vector2 Vector2_Add(Vector2 a, Vector2 b);

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern Vector2 Vector2_Sub(Vector2 a, Vector2 b);

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern Vector2 Vector2_Scale(Vector2 v, double s);

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern double Vector2_Dot(Vector2 a, Vector2 b);

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern double Vector2_Length(Vector2 v);

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern Vector2 Vector2_Normalize(Vector2 v);

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern Vector2 Vector2_Zero();

        //Vector3函数

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern Vector3 Vector3_Add(Vector3 a, Vector3 b);

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern Vector3 Vector3_Sub(Vector3 a, Vector3 b);

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern Vector3 Vector3_Scale(Vector3 v, float s);

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern double Vector3_Dot(Vector3 a, Vector3 b);

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern Vector3 Vector3_Cross(Vector3 a, Vector3 b);

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern double Vector3_Length(Vector3 v);

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern Vector3 Vector3_Normalize(Vector3 v);

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern Vector3 Vector3_Zero();







        //可用函数

        public static float Dot_Float(Vector3 vector, Vector3 vector1)
        {
            return (float)((vector.x * vector1.x) + (vector.y * vector1.y) + (vector.z * vector1.z));
        }

        public static double Dot_Double(Vector3 vector, Vector3 vector1)
        {
            return (vector.x * vector1.x) + (vector.y * vector1.y) + (vector.z * vector1.z);
        }

        public static int Dot_Int(Vector3 vector, Vector3 vector1)
        {
            return (int)((vector.x * vector1.x) + (vector.y * vector1.y) + (vector.z * vector1.z));
        }

        public static long Dot_Long(Vector3 vector, Vector3 vector1)
        {
            return (long)((vector.x * vector1.x) + (vector.y * vector1.y) + (vector.z * vector1.z));
        }


        public static Vector3 Lerp_Float(Vector3 value, Vector3 vector, float amount)
        {
            return (value * (1.0f - amount)) + (vector * amount);
        }


        public static Vector3 Lerp_Double(Vector3 value, Vector3 vector, double amount)
        {
            return (value * (1.0 - amount)) + (vector * amount);
        }

        public static Vector3 Lerp_Int(Vector3 value, Vector3 vector, int amount)
        {
            return (value * (1 - amount)) + (vector * amount);
        }

        public static Vector3 Lerp_Long(Vector3 value, Vector3 vector, long amount)
        {
            return (value * (1 - amount)) + (vector * amount);
        }


        public static Vector3 Max_Double(Vector3 value1, Vector3 value2)
        {
            return new Vector3(
                (value1.x > value2.x) ? value1.x : value2.x,
                (value1.y > value2.y) ? value1.y : value2.y,
                (value1.z > value2.z) ? value1.z : value2.z
            );
        }



        public static Vector3 Min_Double(Vector3 value1, Vector3 value2)
        {
            return new Vector3(
                (value1.x < value2.x) ? value1.x : value2.x,
                (value1.y < value2.y) ? value1.y : value2.y,
                (value1.z < value2.z) ? value1.z : value2.z
            );
        }

        public static Vector3 Multiply(Vector3 left, Vector3 right)
        {
            return left * right;
        }

        public static double Dot(Vector3 a, Vector3 b) => a.x * b.x + a.y * b.y + a.z * b.z;

        // 叉乘
        public static Vector3 Cross(Vector3 a, Vector3 b)
        {
            return new Vector3(
                a.y * b.z - a.z * b.y,
                a.z * b.x - a.x * b.z,
                a.x * b.y - a.y * b.x
            );
        }



        //Vector4函数
        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern Vector4 Vector4_Add(Vector4 a, Vector4 b);

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern Vector4 Vector4_Sub(Vector4 a, Vector4 b);

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern Vector4 Vector4_Scale(Vector4 v, float s);

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern float Vector4_Dot(Vector4 a, Vector4 b);

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern float Vector4_Length(Vector4 v);

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern Vector4 Vector4_Normalize(Vector4 v);

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern Vector4 Vector4_Cross(Vector4 a, Vector4 b);

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern Vector4 Vector4_FromVector3(Vector3 v, float w);

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern Vector4 Vector4_FromAxisAngle(Vector3 axis, float angle);


        public struct Ray
        {
            public Vector3 origins;
            public Vector3 directions;

            public Ray(Vector3 origins, Vector3 directions)
            {
                this.origins = origins;
                this.directions = directions.Normalize() ?? Vector3_Zero();
            }
        }
    }
}
