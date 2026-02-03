using InfinityMemoriesEngine.OverWatch.qianhan.Log.lang.logine;

namespace InfinityMemoriesEngine.OverWatch.qianhan.Util
{
    public class utilBase
    {
        public delegate void Array<T>(T data);
        public delegate void Array<T,V>(T data,V date);
        public delegate void Array(LogType LogType, string value);
        public delegate void Array<T, V, E>(T t, V v, E e);
    }
}
