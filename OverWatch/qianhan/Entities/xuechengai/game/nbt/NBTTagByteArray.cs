namespace InfinityMemoriesEngine.OverWatch.qianhan.Entities.xuechengai.game.nbt
{
    public class NBTTagByteArray : NBTBase
    {
        private byte[] data;

        public NBTTagByteArray()
        {
        }

        public NBTTagByteArray(byte[] data)
        {
            this.data = data ?? Array.Empty<byte>();
        }

        public NBTTagByteArray(List<byte> list)
        {
            data = ToArray(list);
        }

        private static byte[] ToArray(List<byte> list)
        {
            if (list == null) return Array.Empty<byte>();
            var result = new byte[list.Count];
            for (int i = 0; i < list.Count; i++)
            {
                // C# 的 List<byte> 不能存 null，这里保留逻辑：0 代替无效值
                result[i] = list[i];
            }
            return result;
        }

        public override void write(BinaryWriter output)
        {
            output.Write(data.Length);
            output.Write(data);
        }

        public override void read(BinaryReader input, int depth, NBTSizeTracker sizeTracker)
        {
            sizeTracker.read(192L);
            int length = input.ReadInt32();
            sizeTracker.read(8L * length);
            data = input.ReadBytes(length);
        }

        public override byte getId() => 7;

        public override string ToString()
        {
            return "[B;" + string.Join(",", data.Select(b => b + "B")) + "]";
        }

        public override NBTBase copy()
        {
            var copy = new byte[data.Length];
            Array.Copy(data, copy, data.Length);
            return new NBTTagByteArray(copy);
        }

        public override bool equals(object obj)
        {
            return obj is NBTTagByteArray other &&
                   base.Equals(obj) &&
                   data.SequenceEqual(other.data);
        }

        public override int GetHashCode()
        {
            var hash = new HashCode();
            foreach (var b in data) hash.Add(b);
            return base.GetHashCode() ^ hash.ToHashCode();
        }

        public byte[] GetByteArray() => data;
    }
}
