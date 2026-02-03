using System.Runtime.InteropServices;
using InfinityMemoriesEngine.OverWatch.qianhan.Log.lang;
using InfinityMemoriesEngine.OverWatch.qianhan.Util;

namespace InfinityMemoriesEngine.OverWatch.qianhan.InfinityMemoriesEngine.MainManager
{
    /// <summary>
    /// 【危险警告：此类所有的方法限制在非托管内存中使用，请务必在非托管上下文中调用！】
    /// 如果你正在使用Visual Studio + GitHub Copilot，请不要在托管环境中补全所有的非托管函数，危险等级：高；
    /// 如果你在托管环境中使用，他将会无效/失效/返回NULL或抛出非法访问异常。
    /// </summary>
    /// <remarks>
    /// 本类提供的所有方法都只能在非托管内存中使用，任何在托管内存中调用这些方法都会导致未定义行为。
    /// 必须检查是否处于非托管上下文（MainMemoryManager.Managed==false）
    /// 否则建议使用官方建议的方式进行托管调用，本类不提供任何非托管调用;
    public static unsafe class MainMemoryManager
    {
        /// <summary>
        /// 警告，该结构体为非托管内存对象的句柄，使用时请注意是否在非托管内存中操作，新手请谨慎使用！
        /// 危险等级：高
        /// </summary>
        public unsafe readonly struct ObjectHandle
        {
            public readonly void* ptr;
            readonly bool IsManaged;

            public ObjectHandle(void* ptr, bool isManaged)
            {
                this.ptr = ptr;
                IsManaged = isManaged;
            }
        }

        /// <summary>
        /// 用于非托管对象的析构函数委托，注意，这个方法只能在非托管内存中使用。
        /// </summary>
        /// <param name="obj"></param>
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void Destructor(void* obj);

        static Destructor _destructor;

        public static unsafe IntPtr CreateNativeEntity(string typeName, nuint sizeBytes)
        {
            // 1) ANSI 字符串 → sbyte*
            IntPtr namePtr = Marshal.StringToHGlobalAnsi(typeName);
            // 2) 只注册一次（无析构器可传 null，有的话要把委托存到静态字段避免被回收）
            if (_destructor == null)
            {
                // 如果你暂时没有析构逻辑，传 null 就行
                // 否则：_entityDtor = MyEntityDestructor; 并传 _entityDtor
                MainMemoryManager.registerType((sbyte*)namePtr, null);
            }

            // 3) 分配非托管对象，大小在这里传
            void* p = MainMemoryManager.createObject((sbyte*)namePtr, (UIntPtr)sizeBytes);
            return (IntPtr)p;
        }

        public static unsafe IntPtr CreateNative<T>(string typeName, Destructor? dtor = null) where T : unmanaged
        {
            IntPtr namePtr = Marshal.StringToHGlobalAnsi(typeName);
            // 根住委托，防 GC（示意：可用字典按类型名存起来）
            if (dtor != null)
            {
                (_root ??= new()).TryAdd(typeName, dtor);
            }

            MainMemoryManager.registerType((sbyte*)namePtr, dtor);
            void* p = MainMemoryManager.createObject((sbyte*)namePtr, (UIntPtr)sizeof(T));
            return (IntPtr)p;
        }
        static Dictionary<string, Destructor>? _root;

        public static bool EnableManagedCheck { get; internal set; } = true;

        /// <summary>
        /// 为非托管对象注册类型，注意，这个方法只能在非托管内存中使用。
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="destructor"></param>
        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.StdCall)]
        public extern static void registerType(sbyte* typeName, Destructor destructor);
        /// <summary>
        /// 创建一个非托管对象，同时向内存索引器要一块内存，注意，这个函数分配的内存属于非托管内存，在不使用时必须要使用removeObject或removeAllObjects或removeObjectSafe或removeAllObjectsSafe来释放内存。
        /// 要注意，非托管对象不会被GC自动回收，必须手动管理内存。
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.StdCall)]
        public extern static void* createObject(sbyte* typeName, UIntPtr size);
        /// <summary>
        /// 为非托管对象类型增加引用计数，注意，这个方法只能在非托管内存中使用。
        /// </summary>
        /// <param name="obj"></param>
        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.StdCall)]
        public extern static void incrementRefCount(void* obj);
        /// <summary>
        /// 为非托管对象类型减少引用计数，前提是这个非托管对象必须有可以递减的计数，注意，这个方法只能在非托管内存中使用。
        /// </summary>
        /// <param name="obj"></param>
        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.StdCall)]
        public extern static void decrementRefCount(void* obj);
        /// <summary>
        /// 将由createObject创建的非托管对象从内存中移除，注意，这个方法只能在非托管内存中使用。
        /// </summary>
        /// <param name="obj"></param>
        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.StdCall)]
        public extern static void removeObject(void* obj);
        /// <summary>
        /// 将由createObject创建的所有非托管对象从内存中移除，注意，这个方法只能在非托管内存中使用。
        /// </summary>
        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.StdCall)]
        public extern static void removeAllObjects();
        /// <summary>
        /// 获取当前非托管内存中对象的计数，注意，这个方法只能在非托管内存中使用。
        /// </summary>
        /// <returns></returns>
        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.StdCall)]
        public extern static UIntPtr getObjectCount();
        /// <summary>
        /// 调试打印所有非托管对象的信息，注意，这个方法只能在非托管内存中使用。
        /// </summary>
        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.StdCall)]
        public extern static void debugPrintAllObjects();

        [DllImport(ImportName.NameDll, CallingConvention = CallingConvention.StdCall)]
        public extern static long getRefCount(void* obj);

        [System.Diagnostics.Conditional("DEBUG")]
        private static void AssertUnmanaged(bool isManaged, string operation)
        {
            if (isManaged) throw new InvalidOperationException($"操作 {operation} 只能在非托管内存中执行，当前对象为托管内存。请检查对象是否正确分配或使用。");
        }
        /// <summary>
        /// 为了兼容托管和非托管内存，提供一个安全的创建对非托管象方法，注意，这只是托管隔离。
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="size"></param>
        /// <returns>返回创建非托管内存对象</returns>
        public static ObjectHandle CreateUnmanagedObject(sbyte* typeName, UIntPtr size) => new ObjectHandle(createObjectSafe(typeName, size, false), false);
        /// <summary>
        /// 创建一个非托管内存对象，注意，这个方法只能在非托管内存中使用。
        /// </summary>
        /// <param name="typeName">非托管对象名称</param>
        /// <param name="size">非托管对象指针</param>
        /// <param name="Managed">是否是非托管对象,true为托管对象</param>
        /// <returns>返回并创建一个非托管对象</returns>
        /// <exception cref="InvalidOperationException">如果是托管对象则抛出异常</exception>
        public static void* createObjectSafe(sbyte* typeName, UIntPtr size, bool Managed)
        {
            if (!Managed) return createObject(typeName, size);
            throw new InvalidOperationException($"{Managed}为true，这属于托管内存，无法通过createObjectSafe创建对象");
        }

        private static void ExecuteIfUnmanaged(bool managed, Action action, string failReason)
        {
            AssertUnmanaged(managed, "ExecuteIfUnmanaged");
            if (!managed) action();
            else Debug.Log($"{managed}为true，这属于托管内存，无法执行操作: {failReason}，已自动为您转接为托管{action}");
        }

        /// <summary>
        /// 将非托管对象注册为类型，注意，这个方法只能在非托管内存中使用。
        /// </summary>
        /// <param name="typeName">注册对象</param>
        /// <param name="destructor">注册类型</param>
        /// <param name="Managed">是否是非托管对象，如果是true，则不属于非托管对象</param>
        public static void registerTypeSafe(sbyte* typeName, Destructor destructor, bool Managed)
        {
            AssertUnmanaged(Managed, "registerTypeSafe");
            ExecuteIfUnmanaged(Managed, () => registerType(typeName, destructor), "属于托管内存，无法通过registerTypeSafe注册类型");
        }
        /// <summary>
        /// 为非托管对象创建一个引用计数并增加引用计数，注意，这个方法只能在非托管内存中使用。
        /// </summary>
        /// <param name="obj">非托管对象</param>
        /// <param name="Managed">是否是非托管对象</param>
        public static void incrementRefCountSafe(void* obj, bool Managed)
        {
            AssertUnmanaged(Managed, "incrementRefCountSafe");
            ExecuteIfUnmanaged(Managed, () => incrementRefCount(obj), "属于托管内存，无法通过incrementRefCountSafe增加引用计数");
        }
        /// <summary>
        /// 为非托管对象减少引用计数，前提是这个非托管对象必须有可以递减的计数，注意，这个方法只能在非托管内存中使用。
        /// </summary>
        /// <param name="obj">非托管对象名称</param>
        /// <param name="Managed">是否是非托管对象</param>
        public static void decrementRefCountSafe(void* obj, bool Managed)
        {
            AssertUnmanaged(Managed, "decrementRefCountSafe");
            ExecuteIfUnmanaged(Managed, () => decrementRefCount(obj), "属于托管内存，无法通过decrementRefCountSafe减少引用计数");
        }
        /// <summary>
        /// 将非托管对象从内存中移除，注意，非托管对象必须有引用计数为0才能被移除且必须由createObjectSafe创建的才能使用，注意，这个方法只能在非托管内存中使用。
        /// </summary>
        /// <param name="obj">非托管对象类型</param>
        /// <param name="Managed">是否是非托管对象</param>
        public static void removeObjectSafe(void* obj, bool Managed)
        {
            AssertUnmanaged(Managed, "removeObjectSafe");
            ExecuteIfUnmanaged(Managed, () => removeObject(obj), "属于托管内存，无法通过removeObjectSafe移除对象");
        }
        /// <summary>
        /// 将所有的非托管对象从内存中移除，注意，非托管对象必须有引用计数为0才能被移除且必须由createObjectSafe创建的才能使用，注意，这个方法只能在非托管内存中使用。
        /// </summary>
        /// <param name="Managed">是否是非托管对象</param>
        public static void removeAllObjectsSafe(bool Managed)
        {
            AssertUnmanaged(Managed, "removeAllObjectsSafe");
            ExecuteIfUnmanaged(Managed, () => removeAllObjects(), "属于托管内存，无法通过removeAllObjectsSafe移除所有对象");
        }
    }
}
