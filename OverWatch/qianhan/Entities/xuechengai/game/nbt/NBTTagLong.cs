namespace InfinityMemoriesEngine.OverWatch.qianhan.Entities.xuechengai.game.nbt
{
    public class NBTTagLong : NBTPrimitive
    {
        private long data;

        public NBTTagLong()
        {
        }

        public NBTTagLong(long data)
        {
            this.data = data;
        }

        public override void write(BinaryWriter output)
        {
            output.Write(data);
        }

        public override void read(BinaryReader input, int depth, NBTSizeTracker sizeTracker)
        {
            sizeTracker.read(128L);
            data = input.ReadInt64();
        }

        public override byte getId()
        {
            return 4;
        }

        public override string toString()
        {
            return data + "L";
        }

        public NBTTagLong copy()
        {
            return new NBTTagLong(data);
        }

        public override bool equals(object p_equals_1_)
        {
            return base.equals(p_equals_1_) && data == ((NBTTagLong)p_equals_1_).data;
        }

        public override int hashCode()
        {
            return base.hashCode() ^ (int)(data ^ data >>> 32);
        }

        public override long getLong()
        {
            return data;
        }

        public override int getInt()
        {
            return (int)(data & -1L);
        }

        public override short getShort()
        {
            return (short)(int)(data & 65535L);
        }

        public override byte getByte()
        {
            return (byte)(int)(data & 255L);
        }

        public override double getDouble()
        {
            return data;
        }

        public override float getFloat()
        {
            return data;
        }
    }
}
