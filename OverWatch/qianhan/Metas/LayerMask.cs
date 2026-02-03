namespace InfinityMemoriesEngine.OverWatch.qianhan.Metas
{
    [System.Serializable]
    public struct LayerMask
    {
        public int value;

        public static implicit operator int(LayerMask mask) => mask.value;
        public static implicit operator LayerMask(int intVal) => new LayerMask { value = intVal };

        public static LayerMask GetMask(params int[] layers)
        {
            int mask = 0;
            foreach (var layer in layers)
                mask |= 1 << layer;
            return new LayerMask { value = mask };
        }

        public bool Contains(int layer)
        {
            return (value & (1 << layer)) != 0;
        }
    }
}
