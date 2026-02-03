using InfinityMemoriesEngine.OverWatch.qianhan.Events;
using InfinityMemoriesEngine.OverWatch.qianhan.Events.fml;
using InfinityMemoriesEngine.OverWatch.qianhan.Events.fml.common.eventhandler;
using InfinityMemoriesEngine.OverWatch.qianhan.Log.lang;

namespace InfinityMemoriesEngine.OverWatch.qianhan.annotations
{
    /// <summary>
    /// 事件异常处理器接口，用于处理事件总线中的异常情况
    /// </summary>
    public interface IEventExceptionHandler
    {
        void handleException(EventBus bus, Event @event, IEventListener[] listeners, int index, Throwable throwable);
        void handleException(EventBus eventBus, Event eventInstance, IEventListener[] eventListeners, int index, Exception ex);
        void handleException(Exception ex);
    }
}
