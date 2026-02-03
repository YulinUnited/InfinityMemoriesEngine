namespace InfinityMemoriesEngine.OverWatch.qianhan.Entities.xuechengai.game.nbt
{
    public class NBTTagShort : NBTPrimitive
    {
        private NBTSizeTracker NBTSizeTracker;
        private short data;
        public NBTTagShort()
        { }
        public NBTTagShort(short data)
        {
            this.data = data;
        }
        public override void write(BinaryWriter binaryWriter)
        {
            binaryWriter.Write(data);
        }
        public override byte getId()
        {
            return 2;
        }
        public override void read(BinaryReader binaryReader)
        {
            NBTSizeTracker.read(80L);
            data = binaryReader.ReadInt16();
        }
        public override void read(BinaryReader binaryReader, int dept, NBTSizeTracker sizeTracker)
        {
            NBTSizeTracker = sizeTracker;
            data = binaryReader.ReadInt16();
            NBTSizeTracker.read(80L);
        }
        public override string toString()
        {
            return data + "s";
        }
        public override NBTBase copy()
        {
            return new NBTTagShort(data);
        }
        public override bool equals(object obj)
        {
            return base.equals(obj) && data == ((NBTTagShort)obj).data;
        }
        public override int hashCode()
        {
            return base.hashCode() ^ data;
        }
        public override byte getByte()
        {
            return (byte)(data & 255);
        }

        public override double getDouble()
        {
            return data;
        }

        public override float getFloat()
        {
            return data;
        }

        public override int getInt()
        {
            return data;
        }

        public override long getLong()
        {
            return data;
        }

        public override short getShort()
        {
            return data;
        }
    }
}
