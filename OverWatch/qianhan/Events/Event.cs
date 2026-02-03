using InfinityMemoriesEngine.OverWatch.qianhan.Entite.xuechengai.game.common.util;
using InfinityMemoriesEngine.OverWatch.qianhan.Events.fml.common.eventhandler;
using InfinityMemoriesEngine.OverWatch.qianhan.priority;
using InfinityMemoriesEngine.OverWatch.qianhan.Start;

namespace InfinityMemoriesEngine.OverWatch.qianhan.Events
{
    /// <summary>
    /// 事件类型，用于监听事件并阻止其发生
    /// </summary>
    public class Event
    {
        /// <summary>
        /// 注解事件标记
        /// </summary>
        public AnnotationEvent annotationEvent = AnnotationEvent.None;
        private static readonly Dictionary<Type, List<Delegate>> eventListeners = new Dictionary<Type, List<Delegate>>();
        /// <summary>
        /// 实体类型
        /// </summary>
        protected Entities.Entity entity = new Entities.Entity();
        internal bool isCanceled { set; get; } = false;
        internal bool isGlobalMark { set; get; } = false;
        private static ListenerList listeners = new ListenerList();
        private EventPriority priority;
        /// <summary>
        /// 事件构造函数
        /// </summary>
        public Event()
        {

            //entity = new Entity();
            setUp();
            isCanceled = false;
            isGlobalMark = false;
            checkAnnotationFlags();
        }
        /// <summary>
        /// 用于检查注解标记并设置相应的事件状态
        /// </summary>
        protected virtual void checkAnnotationFlags()
        {
            if ((annotationEvent & AnnotationEvent.GlobalCancel) != 0 || (annotationEvent & AnnotationEvent.GlobalRemove) != 0)
            {
                isCanceled = true;
                isGlobalMark = true;
            }
        }
        /// <summary>
        /// 注解事件处理方法
        /// </summary>
        /// <param name="event"></param>
        /// <param name="annotationEvent"></param>
        public static void Annotation(Event @event, AnnotationEvent annotationEvent)
        {
            if ((annotationEvent & AnnotationEvent.Cancel) != 0)
            {
                @event.setCanceled(true);
            }
            if ((annotationEvent & AnnotationEvent.GlobalSetCancel) != 0)
            {
                //取消全局事件，全局事件只有是false才不会被取消
                @event.setGlobalMark(true);
            }
        }

        /// <summary>
        /// 全局事件开关，如果是false才不会影响全部事件的发生
        /// </summary>
        /// <param name="v">true还是false</param>
        /// <returns>全局变量标记</returns>
        public bool setGlobalMark(bool v)
        {
            isCanceled = v;
            return isGlobalMark = v;
        }
        /// <summary>
        /// Event事件只需要在初始化之前注册就行了
        /// </summary>
        [Chinese(ChinesePhase.Events)]
        public virtual void onEventAlave()
        {

        }
        /// <summary>
        /// 将当前事件标记为取消状态
        /// </summary>
        /// <param name="E">true或false</param>
        public virtual void setCanceled(bool E)
        {
            if (!isGlobalMark)
            {
                if (entity.forceDead)
                {
                    return;
                }
                else
                {
                    isCanceled = E;
                }
            }
            else { return; }
        }
        /// <summary>
        /// 状态结果枚举
        /// </summary>
        public enum Result
        {
            /// <summary>
            /// 否认事件发生
            /// </summary>
            DENY,
            /// <summary>
            /// 不允许事件发生
            /// </summary>
            DEFAULT,
            /// <summary>
            /// 允许事件发生
            /// </summary>
            ALLOW
        }
        /// <summary>
        /// 取消事件标记
        /// </summary>
        /// <returns>返回是否取消</returns>
        public bool onCanceled() { return isCanceled; }
        /// <summary>
        /// 全局事件标记
        /// </summary>
        /// <returns></returns>
        public bool onGlobalMarks() { return isGlobalMark; }
        /// <summary>
        /// 当前事件是否可取消
        /// </summary>
        /// <returns>默认为false</returns>
        internal virtual bool isCancelable() { return false; }
        /// <summary>
        /// 当前事件是否为全局标记事件
        /// </summary>
        /// <returns>默认为false</returns>
        internal virtual bool isGlobalMarks() { return false; }
        private Result result = Result.DEFAULT;
        private EventPriority? phase = null;
        /// <summary>
        /// 获取全局标记事件状态
        /// </summary>
        /// <returns>全局事件不是true</returns>
        public bool getGlobalMarkEvent() => !isGlobalMark;
        /// <summary>
        /// 添加结果状态
        /// </summary>
        /// <returns></returns>
        public bool hasResult() => result != Result.DEFAULT;
        /// <summary>
        /// 当前事件是否为全局标记事件
        /// </summary>
        /// <returns></returns>
        public bool isGlobalMarEvent() => isGlobalMark;
        /// <summary>
        /// 当前事件是否被取消
        /// </summary>
        /// <returns></returns>
        public bool isCanceledEvent() => isCanceled;
        /// <summary>
        /// 获取取消状态
        /// </summary>
        /// <returns></returns>
        public virtual bool getCanceled() { return isCanceled; }
        /// <summary>
        /// 获取结果状态
        /// </summary>
        /// <returns></returns>
        public Result getResult()
        {
            return result;
        }
        /// <summary>
        /// 设置结果状态
        /// </summary>
        /// <param name="results"></param>
        public virtual void setResult(Result results)
        {
            result = results;
        }
        /// <summary>
        /// 设置事件阶段
        /// </summary>
        protected void setUp()
        {

        }
        /// <summary>
        /// 获取发布阶段
        /// </summary>
        /// <returns></returns>
        public EventPriority getPhase()
        {
            return phase;
        }
        /// <summary>
        /// 获取监听器列表
        /// </summary>
        /// <returns></returns>
        public static ListenerList getListenerList()
        {
            return listeners;
        }
    }
}
