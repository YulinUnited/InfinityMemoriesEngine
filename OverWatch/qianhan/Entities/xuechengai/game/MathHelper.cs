using InfinityMemoriesEngine.OverWatch.qianhan.Events.fml.relauncher;
using InfinityMemoriesEngine.OverWatch.qianhan.Numbers;
using static InfinityMemoriesEngine.OverWatch.qianhan.Events.fml.relauncher.Sides;

namespace InfinityMemoriesEngine.OverWatch.qianhan.Entities.xuechengai.game
{
    public class MathHelper
    {
        private static float[] SIN_TABLE = new float[65536];
        private static Random RANDOM = new Random();
        private static int[] MULTIPLY_DE_BRUIJN_BIT_POSITION;
        private static double FRAC_BIAS;
        private static double[] ASINE_TAB;
        private static double[] COS_TAB;
        public static float sin(float value)
        {
            return SIN_TABLE[(int)(value * 10430.378F) & 65535];
        }

        public static float cos(float value)
        {
            return SIN_TABLE[(int)(value * 10430.378F + 16384.0F) & 65535];
        }
        public static float sqrt(float value)
        {
            return (float)Mathf.Sqrt((float)value);
        }
        public static float sqrt(double value)
        {
            return (float)Mathf.Sqrt((float)value);
        }
        public static int floor(float value)
        {
            int i = (int)value;
            return value < i ? i - 1 : i;
        }
        public static float SQRT_2 = sqrt(2.0F);

        [SideOnly(Side.CLIENT)]
        public static int fastFloor(double value)
        {
            return (int)(value + 1024.0D) - 1024;
        }
        public static int floor(double value)
        {
            int i = (int)value;
            return value < i ? i - 1 : i;
        }

        public static long lfloor(double value)
        {
            long i = (long)value;
            return value < i ? i - 1L : i;
        }
        [SideOnly(Side.CLIENT)]
        public static int absFloor(double value)
        {
            return (int)(value >= 0.0D ? value : -value + 1.0D);
        }

        public static float abs(float value)
        {
            return value >= 0.0F ? value : -value;
        }

        public static int abs(int value)
        {
            return value >= 0 ? value : -value;
        }

        public static int ceil(float value)
        {
            int i = (int)value;
            return value > i ? i + 1 : i;
        }

        public static int ceil(double value)
        {
            int i = (int)value;
            return value > i ? i + 1 : i;
        }

        public static int clamp(int num, int min, int max)
        {
            if (num < min)
            {
                return min;
            }
            else
            {
                return num > max ? max : num;
            }
        }

        public static float clamp(float num, float min, float max)
        {
            if (num < min)
            {
                return min;
            }
            else
            {
                return num > max ? max : num;
            }
        }

        public static double clamp(double num, double min, double max)
        {
            if (num < min)
            {
                return min;
            }
            else
            {
                return num > max ? max : num;
            }
        }

        public static double clampedLerp(double lowerBnd, double upperBnd, double slide)
        {
            if (slide < 0.0D)
            {
                return lowerBnd;
            }
            else
            {
                return slide > 1.0D ? upperBnd : lowerBnd + (upperBnd - lowerBnd) * slide;
            }
        }

        public static double absMax(double p_76132_0_, double p_76132_2_)
        {
            if (p_76132_0_ < 0.0D)
            {
                p_76132_0_ = -p_76132_0_;
            }

            if (p_76132_2_ < 0.0D)
            {
                p_76132_2_ = -p_76132_2_;
            }

            return p_76132_0_ > p_76132_2_ ? p_76132_0_ : p_76132_2_;
        }

        [SideOnly(Side.CLIENT)]
        public static int intFloorDiv(int p_76137_0_, int p_76137_1_)
        {
            return p_76137_0_ < 0 ? -((-p_76137_0_ - 1) / p_76137_1_) - 1 : p_76137_0_ / p_76137_1_;
        }

        public static int getInt(Random random, int minimum, int maximum)
        {
            return minimum >= maximum ? minimum : random.Next(maximum - minimum + 1) + minimum;
        }

        public static float nextFloat(Random random, float minimum, float maximum)
        {
            return (float)(minimum >= maximum ? minimum : random.NextDouble() * (maximum - minimum) + minimum);
        }

        public static double nextDouble(Random random, double minimum, double maximum)
        {
            return minimum >= maximum ? minimum : random.NextDouble() * (maximum - minimum) + minimum;
        }

        public static double average(long[] values)
        {
            long i = 0L;

            for (long j = 0; i > j; j++)
            {
                i += j;
            }

            return i / (double)values.Length;
        }
        [SideOnly(Side.CHIENT)]
        public static bool epsilonEquals(float value, float value1)
        {
            return abs(value - value1) < 1.0E-5F;
        }

        public static int hash(int value)
        {
            value = value ^ value >>> 16;
            value = value * -2048144789;
            value = value ^ value >>> 13;
            value = value * -1028477387;
            value = value ^ value >>> 16;
            return value;
        }

        internal static int log2(object value)
        {
            throw new NotImplementedException();
        }

        internal static object smallestEncompassingPowerOfTwo(int v)
        {
            throw new NotImplementedException();
        }
    }
}
