using InfinityMemoriesEngine.OverWatch.qianhan.Events;

namespace InfinityMemoriesEngine.OverWatch.qianhan.Log.lang
{
    public class LogEvent : Event
    {
        public LogEvent() { }
        private bool IsDebug = false;

        public bool getLogEvent() => IsDebug;
        public void setLogEvent(bool isDebug)
        {
            IsDebug = isDebug;
        }
        public bool getLogEventStatus()
        {
            return IsDebug;
        }
        public bool isLogEvent()
        {
            return IsDebug;
        }
    }
}
