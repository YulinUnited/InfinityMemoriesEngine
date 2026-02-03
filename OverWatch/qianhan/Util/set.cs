using System.Collections;

namespace InfinityMemoriesEngine.OverWatch.qianhan.Util
{
    public interface Set<T> : IList<T>
    {
        int size();
        bool isEmpty();
        bool contains(object o);
        object[] toArray();
        T[] toArray(T[] a);
        bool remove(object o);
        bool containsAll(ICollection c);
        bool removeAll(ICollection c);
    }
}
