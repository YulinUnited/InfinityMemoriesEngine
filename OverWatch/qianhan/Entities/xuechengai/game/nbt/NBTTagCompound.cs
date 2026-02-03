using System.Text.RegularExpressions;

namespace InfinityMemoriesEngine.OverWatch.qianhan.Entities.xuechengai.game.nbt
{
    public class NBTTagCompound : NBTBase
    {
        private static readonly Regex SIMPLE_VALUE = new Regex("[A-Za-z0-9._+-]+");
        private readonly Dictionary<string, NBTBase> tagMap = new Dictionary<string, NBTBase>();

        public override void write(BinaryWriter output)
        {
            foreach (var entry in tagMap)
            {
                WriteEntry(entry.Key, entry.Value, output);
            }

            output.Write((byte)0); // End of tag list
        }

        public override void read(BinaryReader input, int depth, NBTSizeTracker sizeTracker)
        {
            sizeTracker.read(384L);

            if (depth > 512)
            {
                throw new Exception("Tried to read NBT tag with too high complexity, depth > 512");
            }

            tagMap.Clear();
            byte tagType;

            while ((tagType = ReadType(input, sizeTracker)) != 0)
            {
                string key = ReadKey(input, sizeTracker);
                sizeTracker.read(224 + 16 * key.Length);

                NBTBase tag = ReadNBT(tagType, key, input, depth + 1, sizeTracker);
                if (tagMap.ContainsKey(key))
                {
                    sizeTracker.read(288L); // Conflict resolution
                }
                else
                {
                    tagMap[key] = tag;
                }
            }
        }

        public override byte getId() => 10;

        public int GetSize() => tagMap.Count;

        public void SetTag(string key, NBTBase value)
        {
            if (value == null) throw new ArgumentException($"Invalid null NBT value with key {key}");
            tagMap[key] = value;
        }

        public void SetByte(string key, byte value)
        {
            tagMap[key] = new NBTTagByte(value);
        }

        public void SetShort(string key, short value)
        {
            tagMap[key] = new NBTTagShort(value);
        }

        public void SetInteger(string key, int value)
        {
            tagMap[key] = new NBTTagInt(value);
        }

        public void SetLong(string key, long value)
        {
            tagMap[key] = new NBTTagLong(value);
        }

        public void SetUniqueId(string key, Guid value)
        {
            var bytes = value.ToByteArray();
            SetLong(key + "Most", BitConverter.ToInt64(bytes, 0));
            SetLong(key + "Least", BitConverter.ToInt64(bytes, 8));
        }

        public Guid GetUniqueId(string key)
        {
            // 获取高 8 字节和低 8 字节对应的 long 值
            var most = GetLong(key + "Most");
            var least = GetLong(key + "Least");

            // 将两个 long 组合成一个 Guid
            var mostBytes = BitConverter.GetBytes(most);
            var leastBytes = BitConverter.GetBytes(least);

            var bytes = mostBytes.Concat(leastBytes).ToArray();
            return new Guid(bytes);
        }

        public bool HasUniqueId(string key) =>
            HasKey(key + "Most") && HasKey(key + "Least");

        public void SetFloat(string key, float value)
        {
            tagMap[key] = new NBTTagFloat(value);
        }

        public void SetDouble(string key, double value)
        {
            tagMap[key] = new NBTTagDouble(value);
        }

        public void SetString(string key, string value)
        {
            tagMap[key] = new NBTTagString(value);
        }

        public void SetByteArray(string key, byte[] value)
        {
            tagMap[key] = new NBTTagByteArray(value);
        }

        public void SetIntArray(string key, int[] value)
        {
            tagMap[key] = new NBTTagIntArray(value);
        }

        public void SetBoolean(string key, bool value)
        {
            SetByte(key, (byte)(value ? 1 : 0));
        }

        public NBTBase GetTag(string key) => tagMap.TryGetValue(key, out var tag) ? tag : null;

        public byte GetTagId(string key)
        {
            return tagMap.TryGetValue(key, out var nbtBase) ? nbtBase.getId() : (byte)0;
        }

        public bool HasKey(string key) => tagMap.ContainsKey(key);

        public bool HasKey(string key, int type)
        {
            return GetTagId(key) == type || type == 99;
        }

        public byte GetByte(string key) =>
            HasKey(key, 99) ? ((NBTPrimitive)tagMap[key]).getByte() : (byte)0;

        public short GetShort(string key) =>
            HasKey(key, 99) ? ((NBTPrimitive)tagMap[key]).getShort() : (short)0;

        public int GetInteger(string key) =>
            HasKey(key, 99) ? ((NBTPrimitive)tagMap[key]).getInt() : 0;

        public long GetLong(string key) =>
            HasKey(key, 99) ? ((NBTPrimitive)tagMap[key]).getLong() : 0L;

        public float GetFloat(string key) =>
            HasKey(key, 99) ? ((NBTPrimitive)tagMap[key]).getFloat() : 0f;

        public double GetDouble(string key) =>
            HasKey(key, 99) ? ((NBTPrimitive)tagMap[key]).getDouble() : 0.0;

        public string GetString(string key) =>
            HasKey(key, 8) ? tagMap[key].getString() : string.Empty;

        public byte[] GetByteArray(string key)
        {
            if (HasKey(key, 7))
            {
                return ((NBTTagByteArray)tagMap[key]).GetByteArray();
            }
            return new byte[0];
        }

        public int[] GetIntArray(string key)
        {
            if (HasKey(key, 11))
            {
                return ((NBTTagIntArray)tagMap[key]).GetIntArray();
            }
            return new int[0];
        }

        public NBTTagCompound GetCompoundTag(string key)
        {
            if (HasKey(key, 10))
            {
                return (NBTTagCompound)tagMap[key];
            }
            return new NBTTagCompound();
        }

        public NBTTagList GetTagList(string key, int type)
        {
            if (GetTagId(key) == 9)
            {
                var tagList = (NBTTagList)tagMap[key];
                if (!tagList.HasNoTags() && tagList.GetTagType() != type)
                {
                    return new NBTTagList();
                }
                return tagList;
            }
            return new NBTTagList();
        }

        public bool GetBoolean(string key) => GetByte(key) != 0;

        public void RemoveTag(string key) => tagMap.Remove(key);

        public override string ToString()
        {
            var sb = new System.Text.StringBuilder("{");

            foreach (var entry in tagMap.OrderBy(e => e.Key))
            {
                if (sb.Length > 1) sb.Append(',');
                sb.Append(HandleEscape(entry.Key)).Append(':').Append(entry.Value);
            }

            return sb.Append('}').ToString();
        }

        public bool HasNoTags() => tagMap.Count == 0;

        public NBTTagCompound Copy()
        {
            var copy = new NBTTagCompound();
            foreach (var entry in tagMap)
            {
                copy.SetTag(entry.Key, entry.Value.copy());
            }
            return copy;
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj) && tagMap.SequenceEqual(((NBTTagCompound)obj).tagMap);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() ^ tagMap.GetHashCode();
        }

        private static void WriteEntry(string name, NBTBase data, BinaryWriter output)
        {
            output.Write(data.getId());
            if (data.getId() != 0)
            {
                output.Write(name);
                data.write(output);
            }
        }

        private static byte ReadType(BinaryReader input, NBTSizeTracker sizeTracker)
        {
            sizeTracker.read(8);
            return input.ReadByte();
        }

        private static string ReadKey(BinaryReader input, NBTSizeTracker sizeTracker)
        {
            return input.ReadString();
        }

        private static NBTBase ReadNBT(byte id, string key, BinaryReader input, int depth, NBTSizeTracker sizeTracker)
        {
            sizeTracker.read(32);
            var nbtBase = createNewByType(id);
            try
            {
                nbtBase.read(input, depth, sizeTracker);
                return nbtBase;
            }
            catch (IOException ioException)
            {
                throw new Exception("Error loading NBT data", ioException);
            }
        }

        private static string HandleEscape(string str)
        {
            return SIMPLE_VALUE.IsMatch(str) ? str : NBTTagString.QuoteAndEscape(str);
        }
    }
}
