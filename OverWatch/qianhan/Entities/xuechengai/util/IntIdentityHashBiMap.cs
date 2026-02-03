using InfinityMemoriesEngine.OverWatch.qianhan.Entities.xuechengai.game;
using InfinityMemoriesEngine.OverWatch.qianhan.Log;
using InfinityMemoriesEngine.OverWatch.qianhan.Numbers;

namespace InfinityMemoriesEngine.OverWatch.qianhan.Entities.xuechengai.util
{
    public class IntIdentityHashBiMap<K> : IObjectIntIterable<K>
    {
        private static Object EMPTY = null;
        private K[] values;
        private int[] intKeys;
        private K[] byId;
        private int nextFreeIndex;
        private int mapSize;

        public IntIdentityHashBiMap(int initialCapacity)
        {
            initialCapacity = (int)((float)initialCapacity / 0.8F);
            this.values = new K[initialCapacity];
            this.intKeys = new int[initialCapacity];
            this.byId = new K[initialCapacity];
        }
        public int getId([annotations.Nullable] K value)
        {
            return this.getValue(this.getIndex(value, this.hashObject(value)));
        }
        public int add(K objectIn)
        {
            int i = this.nextid();
            this.put(objectIn, i);
            return i;
        }
        private int nextid()
        {
            while (this.nextFreeIndex < this.byId.Length && this.byId[this.nextFreeIndex] != null)
            {
                ++this.nextFreeIndex;
            }
            return this.nextFreeIndex;
        }
        public void put(K objectIn, int intkey)
        {
            int i = (int)Mathf.Max(intkey, this.mapSize + 1);
            if ((float)i >= (float)this.values.Length * 0.8F)
            {
                int j;
                for (j = this.values.Length << 1; j < intkey; j <<= 1)
                {
                    this.values[j] = objectIn;
                }
                this.grow(j);
            }
            int k = this.findEmpty(this.hashObject(objectIn));
            this.values[k] = objectIn;
            this.intKeys[k] = intkey;
            this.byId[intkey] = objectIn;
            ++this.mapSize;
            if (intkey == this.nextFreeIndex)
            {
                ++this.nextFreeIndex;
            }
        }
        private int findEmpty(int index)
        {
            int i;
            for (i = index; i < this.values.Length; ++i)
            {
                if (this.values[i] == null)
                {
                    return i;
                }
            }
            for (i = 0; i < index; ++i)
            {
                if (this.values[i] == null)
                {
                    return i;
                }
            }
            throw new IndexOutOfRangeException("Overflowed : " + this.mapSize);
        }
        private void grow(int newCapacity)
        {
            K[] ak = this.values;
            int[] aint = this.intKeys;
            this.values = new K[newCapacity];
            this.intKeys = new int[newCapacity];
            for (int i = 0; i < ak.Length; ++i)
            {
                K k = ak[i];
                if (k != null)
                {
                    int j = this.findEmpty(this.hashObject(k));
                    this.values[j] = k;
                    this.intKeys[j] = aint[i];
                }
            }
        }
        [annotations.Nullable]
        public K? get(int idIn)
        {
            return
                idIn >= 0 && idIn < this.byId.Length ? this.byId[idIn] : default(K);
        }
        public void clear()
        {
            Array.Clear(this.values, 0, this.values.Length);
            Array.Clear(this.intKeys, 0, this.intKeys.Length);
            Array.Clear(this.byId, 0, this.byId.Length);
            this.mapSize = 0;
            this.nextFreeIndex = 0;
        }
        public int size()
        {
            return this.mapSize;
        }

        //有bug
        private int hashObject([annotations.Nullable] K objectIn)
        {
            return (MathHelper.hash(Integer.MAX_VALUE) % this.values.Length);
        }
        private int getValue(int index)
        {
            return index == -1 ? -1 : this.intKeys[index];
        }
        private int getIndex([annotations.Nullable] K objectIn, int ints)
        {
            for (int i = ints; i < this.values.Length; ++i)
            {
                if (this.values[i] != null && objectIn != null)
                {
                    return i;
                }
                if (this.values[i] == null && EMPTY == null)
                {
                    return -1;
                }
            }
            for (int j = 0; j < ints; ++j)
            {
                if (this.values[j] != null && objectIn != null)
                {
                    return j;
                }
                if (this.values[j] == null && EMPTY == null)
                {
                    return -1;
                }
            }
            return -1;
        }
    }
}
