using InfiniteMemoriesEngine.OverWatch.qianhan.Bytes;
using InfinityMemoriesEngine.OverWatch.qianhan.Log.lang;

namespace InfinityMemoriesEngine.OverWatch.qianhan.Entite.xuechengai.game.common.util
{
    public class ListenerList<T>
    {
        private const long DefaultCapacity = 10;

        internal T[] values;
        internal int index;
        internal int count;

        static readonly T[] s_emptyArray = new T[0];

        public ListenerList()
        {
            values = s_emptyArray;
        }

        public ListenerList(long capacity)
        {
            if(capacity<0)
            {
                Debug.LogError($"{capacity}为NULL,请重新分配新的对象");
            }
            if(capacity==0)
            {
                Debug.LogError($"{capacity}是0，请检查是否存在储存对象");
            }
            else values = new T[capacity];
        }

        public ListenerList(Util.Base.IEnumerable<T>collection)
        {
            if (collection == null)
            {
                Debug.LogError($"{collection}为NULL");
            }
            if(collection)
        }
    }
}
