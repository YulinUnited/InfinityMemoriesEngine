namespace InfinityMemoriesEngine.OverWatch.qianhan.Entities.xuechengai.game.nbt
{
    public class NBTSizeTracker
    {
        private long max;
        private long reads;
        public NBTSizeTracker() { }
        public NBTSizeTracker(long max) { this.max = max; }
        public void read(long bits)
        {
            reads += bits / 8L;
            if (reads > max)
            {
                throw new Exception("Tried to read NBT tag that was too big; tried to allocate: " + this.read + "bytes where max allowed is: " + max);
            }
        }
        public static NBTSizeTracker INFINITE = new NBTSizeTracker(0L);
        public static void readUTF(NBTSizeTracker tracker, string str)
        {
            tracker.read(16);
            if (str == null) return;
            int len = str.Length;
            int utflen = 0;
            for (int i = 0; i < len; i++)
            {
                int c = str.CompareTo(str[i]);
                if (c >= 0x0001 && c <= 0x007F)
                {
                    utflen++;
                }
                else if (c > 0x07FF)
                {
                    utflen += 3;
                }
                else
                {
                    utflen += 2;
                }
                tracker.read(8 * utflen);
            }
        }
    }

}
