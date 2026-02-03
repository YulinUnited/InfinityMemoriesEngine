namespace InfinityMemoriesEngine.OverWatch.qianhan.Util.Base
{
    public readonly struct IndexedValue<T>
    {
        public readonly int Index;

        public readonly T Value;

        public IndexedValue(int index, T value)
        {
            Index = index;
            Value = value;
        }
    }
}
