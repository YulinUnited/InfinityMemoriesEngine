using System.Runtime.InteropServices;
using InfinityMemoriesEngine.OverWatch.qianhan.InfinityMemoriesEngine.MainManager;

namespace InfinityMemoriesEngine.OverWatch.qianhan.Hooks
{
    /// <summary>
    /// NativeFactoryHooks 类提供了"傻瓜式调用"方式，用于注册、创建和管理非托管对象。
    /// 支持泛型T类型（必须是unmanaged），可选析构委托，内部维护委托字典防止GC；
    /// </summary>
    public unsafe class NativeFactoryHooks
    {
        //内部保存所有注册的析构函数，防止GC回收
        private static Dictionary<string, MainMemoryManager.Destructor> _destructorMap = new Dictionary<string, MainMemoryManager.Destructor>();
        /// <summary>
        /// 注册类型并创建非托管对象。
        /// </summary>
        /// <typeparam name="T">非托管类型</typeparam>
        /// <param name="typeName">类型名，用于注册到非托管对象管理器</param>
        /// <param name="dtor">可选析构委托，如果不传则默认null</param>
        /// <returns>非托管对象的指针(IntPtr)</returns>
        public static IntPtr CreateNative<T>(string typeName, MainMemoryManager.Destructor? dtor = null)
            where T : unmanaged
        {
            // 1. 类型名转 ANSI 指针
            IntPtr namePtr = Marshal.StringToHGlobalAnsi(typeName);
            sbyte* typeEntity = (sbyte*)namePtr.ToPointer();

            // 2. 注册类型（只注册一次）
            if (!_destructorMap.ContainsKey(typeName))
            {
                if (dtor != null) _destructorMap[typeName] = dtor;
                MainMemoryManager.registerType(typeEntity, dtor);
            }

            // 3. 创建对象
            void* p = MainMemoryManager.createObject(typeEntity, (UIntPtr)sizeof(T));

            // 4. 不需要释放 namePtr，因为 DLL 内部已拷贝字符串
            return (IntPtr)p;
        }

        /// <summary>
        /// 将非托管对象的IntPtr转换为T*指针。
        /// </summary>
        /// <typeparam name="T">非托管类型</typeparam>
        /// <param name="ptr">非托管对象指针</param>
        /// <returns>返回非托管对象T*指针</returns>
        public static unsafe T* As<T>(IntPtr ptr) where T : unmanaged
        {
            return (T*)ptr.ToPointer();
        }
        #region 设置析构委托

        /// <summary>
        /// 设置指定类型名的析构委托，如果已经存在则覆盖
        /// </summary>
        public static void setDestructor<T>(string typeName, MainMemoryManager.Destructor dtor)
            where T : unmanaged
        {
            if (_destructorMap.ContainsKey(typeName))
            {
                _destructorMap[typeName] = dtor;
            }
            else
            {
                _destructorMap.Add(typeName, dtor);
            }
            MainMemoryManager.registerType((sbyte*)Marshal.StringToHGlobalAnsi(typeName).ToPointer(), dtor);
        }
        /// <summary>
        /// 使用 T 类型名自动注册析构委托
        /// </summary>
        public static void setDestructor<T>(MainMemoryManager.Destructor dtor)
            where T : unmanaged
        {
            string typeName = typeof(T).FullName ?? throw new InvalidOperationException("Type name cannot be null.");
            setDestructor<T>(typeName, dtor);
        }
        /// <summary>
        /// 使用 T 类型名注册默认 null 析构委托
        /// </summary>
        public static void setDestructor<T>()
            where T : unmanaged
        {
            string typeName = typeof(T).FullName ?? throw new InvalidOperationException("Type name cannot be null.");
            setDestructor<T>(typeName, null);
        }
        #endregion

        #region 获取析构委托

        /// <summary>
        /// 获取指定类型名的析构委托，如果不存在会抛异常
        /// </summary>
        public static void getDestructor<T>(string typeName, out MainMemoryManager.Destructor dtor)
            where T : unmanaged
        {
            if (_destructorMap.TryGetValue(typeName, out dtor))
            {
                return;
            }
            else
            {
                dtor = null;
                throw new KeyNotFoundException($"Destructor for type '{typeName}' not found.");
            }
        }
        /// <summary>
        /// 尝试获取析构委托，如果存在返回 true，否则返回 false
        /// </summary>
        public static bool tryGetDestructor<T>(string typeName, out MainMemoryManager.Destructor dtor)
            where T : unmanaged
        {
            return _destructorMap.TryGetValue(typeName, out dtor);
        }
        #endregion
        public static void removeNative<T>(IntPtr ptr, string typeName)
            where T : unmanaged
        {
            if (ptr != IntPtr.Zero)
            {
                MainMemoryManager.removeObject(ptr.ToPointer());
                _destructorMap.Remove(typeName);
            }
        }
    }
}
