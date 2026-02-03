using InfinityMemoriesEngine.OverWatch.qianhan.Log.lang.logine;

namespace InfinityMemoriesEngine.OverWatch.qianhan.Entities.xuechengai.game.nbt
{
    public class NBTTagFloat : NBTPrimitive
    {
        private float data;
        public NBTTagFloat()
        { }
        public NBTTagFloat(float data) { this.data = data; }
        public override void write(BinaryWriter binaryWriter)
        {
            binaryWriter.Write(data);
        }
        public override void read(BinaryReader binaryReader, int dept, NBTSizeTracker sizeTracker)
        {
            sizeTracker.read(96L);
            data = binaryReader.ReadSingle();
        }
        public override byte getId()
        {
            return 5;
        }
        public override string toString()
        {
            return data + "f";
        }
        public override NBTBase copy()
        {
            return new NBTTagFloat(data);
        }
        public override bool equals(object obj)
        {
            return base.equals(obj) && data == ((NBTTagFloat)obj).data;
        }
        public override int hashCode()
        {
            return base.hashCode() ^ Objects.Object.FloatToIntBits(data);
        }
        public override long getLong()
        {
            return (long)data;
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
            return data;
        }
    }
}
