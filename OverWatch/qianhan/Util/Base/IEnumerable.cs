using InfinityMemoriesEngine.OverWatch.qianhan.Entities;
using InfinityMemoriesEngine.OverWatch.qianhan.Objects;

namespace InfinityMemoriesEngine.OverWatch.qianhan.Util.Base
{
    public interface IEnumerable
    {
        IEnumerable GetEnumerable();
    }
    public interface IEnumerable<T>:IEnumerable
    {
        IEnumerable<T> GetEnumerator();
    }

    public interface ICollection<T> : IEnumerable<T>, IEnumerable
    {
        int Count { get; }
        boolean IsReadOnly { get; }
        boolean Remove(T item);
        boolean Clear();
        boolean Contains(T item);
        void CopyTo(T[] array, int arrayIndex);
    }

    public class Collection<T> : ICollection<T>
    {
        public boolean Remove(T item)
        {
            if(item is MixinObject&&item is MainObject&&item is Entity)
            {
                var @var = item as MixinObject;
                var @var1 = item as Entity;
                var @var2 = item as MainObject;

                if(var.isRemove||var1.forceDead||var2.isRemove)
                {

                }
            }
            return false;
        }
    }
}
