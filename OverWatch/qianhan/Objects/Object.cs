using InfinityMemoriesEngine.OverWatch.qianhan.GarbageCollection;
using InfinityMemoriesEngine.OverWatch.qianhan.Log.lang;
using InfinityMemoriesEngine.OverWatch.qianhan.Numbers;
using InfinityMemoriesEngine.OverWatch.qianhan.Util.Base;

namespace InfinityMemoriesEngine.OverWatch.qianhan.Objects
{
    /// <summary>
    /// 引擎内部的对象类，继承自<see cref="MainObject"/>，对于默认的object是否要显式转换本质上没什么必要，但为了保持一致性和可读性，仍然使用了Object类名。
    /// </summary>
    public unsafe class Object : MainObject, IDisposable
    {
        internal static readonly List<MainObject> objects = [];
        private bool disposedValue;
        internal int referenceCount;

        /// <summary>
        /// 分配内存大小的构造函数，带有内存分配上限限制的危机警告。
        /// 请务必合理分配内存大小，避免超过11MB（12048KB）的限制。
        /// 超出限制时将抛出<see cref="OutOfMemoryException"/>异常，
        /// 以防止程序因大内存分配导致的不稳定或崩溃风险,建议配合内存监控工具使用，避免频繁大内存分配引发性能问题。
        /// </summary>
        /// <param name="size">请求分配的内存大小，单位KB</param>
        /// <exception cref="OutOfMemoryException">当请求分配大小超过允许最大值时抛出</exception>
        public Object(int size)
        {
            if (size <= 0 || size > MaxAllocSizeKB)
            {
                throw new ArgumentOutOfRangeException(nameof(size), $"size 必须大于0且不超过 {MaxAllocSizeKB} KB");
            }
            if (AlloctedSizeKB > MaxAllocSizeKB)
            {
                //模拟数学运算
                int currentSizeKB = (int)Mathf.Max(AlloctedSizeKB - MaxAllocSizeKB, 0);
                throw new OutOfMemoryException($"请求分配的内存大小{AlloctedSizeKB}KB超过最大值限制的{MaxAllocSizeKB}，超出的数值为{currentSizeKB}");
            }
            this.nativePtr = (nuint)MixinGC.MixinGC_Allocate(nativePtr);
            if (nativePtr == nuint.Zero)
            {
                Debug.LogWarning("内存分配失败，nativePtr为null/0");
                throw new OutOfMemoryException();
            }
        }
        /// <summary>
        /// 构造函数，无需参数，分配默认大小的内存。
        /// </summary>
        /// <exception cref="OutOfMemoryException">内存分配失败</exception>
        public Object()
        {
            // 默认构造函数，分配一个默认大小的内存
            this.nativePtr = (nuint)MixinGC.MixinGC_Allocate(nativePtr); // 分配1KB内存
            if (nativePtr == nuint.Zero)
            {
                Debug.LogWarning("内存分配失败，nativePtr为null/0");
                throw new OutOfMemoryException();
            }
        }
        /// <summary>
        /// 对象的唯一id标识符。
        /// </summary>
        public nint id { get; internal set; }
        /// <summary>
        /// 设置对象的活跃状态。
        /// </summary>
        /// <param name="v"></param>
        public virtual void setActive(bool v)
        {
            this.Active = v;

        }
        /// <summary>
        /// 对象的活跃状态。
        /// </summary>
        /// <returns></returns>
        public bool getActive()
        {
            return this.Active;
        }
        /// <summary>
        /// 保留方法：未来用于设置对象非活跃状态时的特殊逻辑
        /// </summary>
        public virtual void setInactive()
        {
            if (Active) return;
            if (!Active)
            {
                if (this.isRemove)
                {
                    Debug.LogWarning("对象已经被移除，无法设置为非活跃状态！");
                }
                else
                {
                    Debug.LogWarning("对象未被移除，设置为非活跃状态。");
                    this.Active = false;
                }
            }
        }
        /// <summary>
        /// 回路逻辑，你可以把它认为是能够延迟重载的逻辑
        /// </summary>
        public virtual void BaseLoad(int @int)
        {
            if (@int >= 0)
            {
                Debug.LogWarning($"{@int}不能为0或小于");
            }
        }
        /// <summary>
        /// 执行对象的释放逻辑。
        /// </summary>
        /// <param name="disposing"></param>
        public virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (!isRemove)
                    {
                        ForceGCCollect(disposing);
                    }
                    else if (nativePtr == nuint.Zero)
                    {
                        Debug.LogWarning($"对象{this}已经被移除，无法释放内存！");
                        return;
                    }
                }

                // TODO: 释放未托管的资源(未托管的对象)并重写终结器
                // TODO: 将大型字段设置为 null
                // TODO：将数值类型、字段的地址释放掉
                //释放整数类型，int32位
                Int();
                //释放整数类型，int64位
                Long();
                //释放字段，string类型
                objectname();
                disposedValue = true;
                //移除完毕，返回true
                isRemove = true;
            }
        }

        private void Int()
        {
            remove();
        }
        private void Long()
        {
            remove();
        }
        private void objectname()
        {
            remove();
        }
        // // TODO: 仅当“Dispose(bool disposing)”拥有用于释放未托管资源的代码时才替代终结器
        // ~Object()
        // {
        //     // 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”方法中
        //     Dispose(disposing: false);
        // }

        /// <summary>
        /// object自带的Dispose方法，用于释放对象资源。
        /// </summary>
        public void Dispose()
        {
            // 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”方法中
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        /// <summary>
        /// 通过线程安全的方式注册对象到全局对象列表中。
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        internal static IEnumerable<object> getAll()
        {
            foreach (var @object in objects)
            {
                LoadAll();
                if (objects.IsEmpty())
                {
                    throw new NullReferenceException("对象列表为空，无法获取所有对象。请确保至少有一个对象被创建并添加到列表中。");
                }
                if (@object is Object obj && !obj.isRemove)
                {
                    yield return obj;
                }
            }
        }
        /// <summary>
        /// 默认情况下，加载所有对象到全局对象列表中，简写。
        /// </summary>
        /// <returns></returns>
        internal static IEnumerable<object> LoadAll()
        {
            foreach (var @object in objects)
            {
                objects.Add(@object);
                yield return @object;
            }
        }
        /// <summary>
        /// 将浮点数转换为其对应的整数位表示。
        /// </summary>
        /// <param name="value">浮点数</param>
        /// <returns>整数</returns>
        public static int FloatToIntBits(float value)
        {
            return BitConverter.ToInt32(BitConverter.GetBytes(value), 0);
        }
    }
}
