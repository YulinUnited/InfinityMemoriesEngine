namespace InfinityMemoriesEngine.OverWatch.qianhan.Entities.xuechengai.game.nbt
{
    public class NBTTagByte : NBTPrimitive
    {
        private byte data;
        public NBTTagByte() { }
        public NBTTagByte(byte data)
        {
            this.data = data;
        }
        public override void write(BinaryWriter binaryWriter)
        {
            binaryWriter.Write(data);
        }
        public override void read(BinaryReader binaryReader, int depth, NBTSizeTracker sizeTracker)
        {
            sizeTracker.read(72L);
            data = binaryReader.ReadByte();
        }
        public override byte getId()
        {
            return 1;
        }
        public override string toString()
        {
            return data + "b";
        }
        public override NBTTagByte copy()
        {
            return new NBTTagByte(data);
        }
        public override bool equals(object obj)
        {
            return base.equals(obj) && data == ((NBTTagByte)obj).data;
        }
        public override int hashCode()
        {
            return base.hashCode() ^ data;
        }
        public override byte getByte()
        {
            return data;
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
