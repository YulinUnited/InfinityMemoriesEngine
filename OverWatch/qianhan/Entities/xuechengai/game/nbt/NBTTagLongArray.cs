using System.Text;

namespace InfinityMemoriesEngine.OverWatch.qianhan.Entities.xuechengai.game.nbt
{
    public class NBTTagLongArray : NBTBase
    {
        private long[] data;

        public NBTTagLongArray()
        {
        }

        public NBTTagLongArray(long[] array)
        {
            data = array;
        }

        public NBTTagLongArray(List<long> list)
        {
            data = list.ToArray();  // 将 List<long> 转换为 long[]
        }

        public override void write(BinaryWriter output)
        {
            output.Write(data.Length);

            foreach (long value in data)
            {
                output.Write(value);
            }
        }

        public override void read(BinaryReader input, int depth, NBTSizeTracker sizeTracker)
        {
            sizeTracker.read(192L);
            int size = input.Read();
            sizeTracker.read(64 * size);  // Read the size of long*size in bytes
            data = new long[size];

            for (int i = 0; i < size; ++i)
            {
                data[i] = input.Read();
            }
        }

        public override byte getId()
        {
            return 12;  // Return type ID for long array
        }

        public override string toString()
        {
            StringBuilder stringbuilder = new StringBuilder("[L;");

            for (int i = 0; i < data.Length; ++i)
            {
                if (i != 0)
                {
                    stringbuilder.Append(',');
                }

                stringbuilder.Append(data[i]).Append('L');
            }

            return stringbuilder.Append(']').ToString();
        }

        public override NBTTagLongArray copy()
        {
            long[] copyArray = new long[data.Length];
            Array.Copy(data, copyArray, data.Length);
            return new NBTTagLongArray(copyArray);
        }

        public override bool equals(object obj)
        {
            if (obj is NBTTagLongArray other)
            {
                return base.Equals(obj) && data.SequenceEqual(other.data);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() ^ data.GetHashCode();
        }

        public long[] GetLongArray()
        {
            return data;
        }
    }
}
