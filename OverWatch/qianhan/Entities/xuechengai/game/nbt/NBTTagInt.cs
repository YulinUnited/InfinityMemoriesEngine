namespace InfinityMemoriesEngine.OverWatch.qianhan.Entities.xuechengai.game.nbt
{
    public class NBTTagInt : NBTPrimitive
    {
        private int data;
        //private NBTSizeTracker sizeTracker;
        public NBTTagInt()
        {
            //sizeTracker = new NBTSizeTracker(); 
        }
        public NBTTagInt(int value)
        {
            data = value;
        }
        public override void read(BinaryReader binaryReader, int depth, NBTSizeTracker sizeTracker)
        {
            sizeTracker.read(96L);
            data = binaryReader.Read();
        }
        public override void write(BinaryWriter binaryWriter)
        {
            binaryWriter.Write(data);
        }
        public override byte getId()
        {
            return 3;
        }
        public override string toString()
        {
            return data + "e";
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
            return (short)(data & 65535);
        }
        public override NBTBase copy()
        {
            return new NBTTagInt(data);
        }
        public override bool equals(object obj)
        {
            return base.equals(obj) && data == ((NBTTagInt)obj).data;
        }
        public override int hashCode()
        {
            return base.hashCode() ^ data;
        }
    }
}
