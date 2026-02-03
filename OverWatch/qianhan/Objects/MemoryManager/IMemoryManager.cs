namespace InfinityMemoriesEngine.OverWatch.qianhan.Objects.MemoryManager
{
    /// <summary>
    /// 如果不依赖MainObject或其他基类的情况下，请使用此接口实现内置的释放接口自己实现或直接直接引用NewMemoryManager。
    /// 本引擎只提供最基础的内存管理器API，但您可以具体实现您自己的内存管理逻辑。
    /// </summary>
    public interface IMemoryManager
    {
        /// <summary>
        /// 用于释放当前对象的内存。
        /// </summary>
        void Release();
        /// <summary>
        /// 用于释放所有对象的内存。
        /// </summary>
        void ReleaseAll();
        /// <summary>
        /// 删除当前对象。
        /// </summary>
        /// <returns></returns>
        unsafe bool removeObject(void* @object);
    }
}
