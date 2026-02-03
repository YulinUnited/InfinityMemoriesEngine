namespace InfinityMemoriesEngine.OverWatch.qianhan.Log.lang.logine
{
    public static class Objects
    {
        public static bool isRemove { get; set; }
        public static T requireNonNull<T>(T obj, string message)
        {
            if (obj == null)
            {
                throw new NullReferenceException(message);
            }
            return obj;
        }
        public static T requireNonNull<T>(T obj)
        {
            if (obj == null)
            {
                throw new NullReferenceException();
            }
            return obj;
        }
        public static bool isNull(Object obj)
        {
            return obj == null;
        }
        public static bool isNullOrUndefined(Object obj) { return obj == null; }
        public static bool nonNull(Object obj) { return obj != null; }
    }
}
