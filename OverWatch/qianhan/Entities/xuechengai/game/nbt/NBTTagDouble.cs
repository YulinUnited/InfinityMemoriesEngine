namespace InfinityMemoriesEngine.OverWatch.qianhan.Entities.xuechengai.game.nbt
{
    public class NBTTagDouble : NBTPrimitive
    {
        private double data;

        public NBTTagDouble()
        {
        }

        public NBTTagDouble(double data)
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
            data = input.ReadDouble();
        }

        public override byte getId()
        {
            return 6;
        }

        public override string toString()
        {
            return data + "d";
        }

        public NBTTagDouble copy()
        {
            return new NBTTagDouble(data);
        }

        public override bool equals(object p_equals_1_)
        {
            return base.equals(p_equals_1_) && data == ((NBTTagDouble)p_equals_1_).data;
        }

        public override int hashCode()
        {
            long i = BitConverter.DoubleToInt64Bits(data);
            return base.hashCode() ^ (int)(i ^ i >>> 32);
        }

        public override long getLong()
        {
            return (long)Math.Floor(data);
        }

        public override int getInt()
        {
            return MathHelper.floor(data);
        }

        public override short getShort()
        {
            return (short)(MathHelper.floor(data) & 65535);
        }

        public override byte getByte()
        {
            return (byte)(MathHelper.floor(data) & 255);
        }

        public override double getDouble()
        {
            return data;
        }

        public override float getFloat()
        {
            return (float)data;
        }
    }
}
