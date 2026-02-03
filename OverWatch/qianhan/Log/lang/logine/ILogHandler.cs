namespace InfinityMemoriesEngine.OverWatch.qianhan.Log.lang.logine
{
    public interface ILogHandler
    {
        // 格式化日志输出
        void LogFormat(LogType logType, LogOption logOptions, Object context, string format, params object[] args);

        // 异常日志输出
        void LogException(Exception exception, Object context);

        // 普通日志输出
        void Log(string tag, LogType logType, string msg, object context);
        void LogFormat(LogType logType, object value, string formattedMessage);
    }
}
