using System.Text;
using InfinityMemoriesEngine.OverWatch.qianhan.Log.lang;

namespace InfinityMemoriesEngine.OverWatch.qianhan.Entities.xuechengai.game.nbt
{
    public class NBTTagString : NBTBase
    {
        private string data;

        public NBTTagString() : this("") { }

        public NBTTagString(string data)
        {
            if (data == null)
            {
                Debug.LogError(nameof(data), "Null string not allowed");
                return;
            }
            this.data = data;
        }

        public override void write(BinaryWriter output)
        {
            WriteUTF(output, data);
        }

        public override void read(BinaryReader input, int depth, NBTSizeTracker sizeTracker)
        {
            sizeTracker.read(288L);
            data = ReadUTF(input, sizeTracker);
            // Forge: Correctly read String length including header
            NBTSizeTracker.readUTF(sizeTracker, data);
        }

        public override byte getId() => 8;

        public override string toString() => QuoteAndEscape(data);

        public override NBTBase copy() => new NBTTagString(data);

        public bool HasNoTags() => string.IsNullOrEmpty(data);

        public override bool equals(object obj)
        {
            if (!base.Equals(obj)) return false;

            if (obj is NBTTagString other)
            {
                return string.Equals(data, other.data, StringComparison.Ordinal);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() ^ (data?.GetHashCode() ?? 0);
        }

        public string GetString() => data;

        public static string QuoteAndEscape(string input)
        {
            var sb = new StringBuilder("\"");
            foreach (char c in input)
            {
                if (c == '\\' || c == '"')
                    sb.Append('\\');
                sb.Append(c);
            }
            sb.Append('"');
            return sb.ToString();
        }

        // --- Strict Java-style UTF methods ---
        private static void WriteUTF(BinaryWriter writer, string value)
        {
            if (value == null) value = "";

            // Java NBT uses "modified UTF-8" with 2-byte length prefix
            var bytes = Encoding.UTF8.GetBytes(value);
            if (bytes.Length > ushort.MaxValue)
                throw new IOException("Encoded string too long");

            writer.Write((ushort)bytes.Length);
            writer.Write(bytes);
        }

        private static string ReadUTF(BinaryReader reader, NBTSizeTracker tracker)
        {
            ushort length = reader.ReadUInt16();
            byte[] bytes = reader.ReadBytes(length);
            tracker.read(16L * length); // rough equivalent of Forge's tracking
            return Encoding.UTF8.GetString(bytes);
        }
    }
}
