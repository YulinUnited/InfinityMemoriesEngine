namespace InfinityMemoriesEngine.OverWatch.qianhan.Entities.xuechengai.game.nbt
{
    public class NBTTagEnd : NBTBase
    {
        public NBTTagEnd() { }
        private NBTSizeTracker sizeTracker = NBTSizeTracker.INFINITE;
        public override NBTBase copy()
        {
            return new NBTTagEnd();
        }

        public override byte getId()
        {
            return 0;
        }

        public override void read(BinaryReader binaryReader)
        {
            sizeTracker.read(64L);
        }
        public override void read(BinaryReader binaryReader, int dept, NBTSizeTracker sizeTracker)
        {
            base.read(binaryReader, dept, sizeTracker);
            sizeTracker.read(64L);
        }
        public override string toString()
        {
            return "END";
        }

        public override void write(BinaryWriter binaryWriter)
        {
            return;
        }
    }
}
