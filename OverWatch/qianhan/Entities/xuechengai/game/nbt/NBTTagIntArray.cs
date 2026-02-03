using System.Text;

namespace InfinityMemoriesEngine.OverWatch.qianhan.Entities.xuechengai.game.nbt
{
    public class NBTTagIntArray : NBTBase
    {
        private int[] intArray;

        public NBTTagIntArray()
        {
        }

        public NBTTagIntArray(int[] array)
        {
            intArray = array;
        }

        public NBTTagIntArray(List<int> list)
        {
            intArray = list.ToArray();  // 将 List<int> 转换为 int[]
        }

        public override void write(BinaryWriter output)
        {
            output.Write(intArray.Length);

            foreach (int i in intArray)
            {
                output.Write(i);
            }
        }

        public override void read(BinaryReader input, int depth, NBTSizeTracker sizeTracker)
        {
            sizeTracker.read(192L);
            int size = input.Read();
            sizeTracker.read(32 * size);
            intArray = new int[size];

            for (int i = 0; i < size; ++i)
            {
                intArray[i] = input.Read();
            }
        }

        public override byte getId()
        {
            return 11;
        }

        public override string toString()
        {
            StringBuilder stringbuilder = new StringBuilder("[I;");

            for (int i = 0; i < intArray.Length; ++i)
            {
                if (i != 0)
                {
                    stringbuilder.Append(',');
                }

                stringbuilder.Append(intArray[i]);
            }

            return stringbuilder.Append(']').ToString();
        }

        public override NBTTagIntArray copy()
        {
            int[] copyArray = new int[intArray.Length];
            Array.Copy(intArray, copyArray, intArray.Length);
            return new NBTTagIntArray(copyArray);
        }

        public override bool equals(object obj)
        {
            if (obj is NBTTagIntArray other)
            {
                return base.Equals(obj) && intArray.SequenceEqual(other.intArray);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() ^ intArray.GetHashCode();
        }

        public int[] GetIntArray()
        {
            return intArray;
        }
    }
}
