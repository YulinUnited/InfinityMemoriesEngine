using InfinityMemoriesEngine.OverWatch.qianhan.Log.lang.logine;

namespace InfinityMemoriesEngine.OverWatch.qianhan.Log.lang
{
    public interface ILogger : ILogHandler
    {
        // 日志处理器
        ILogHandler LogHandler { set; get; }

        // 是否启用日志输出
        bool LogEnabled { set; get; }

        // 当前的最低日志等级
        LogType LogLevel { get; set; }

        // 检查日志类型是否允许输出
        bool IsLogTypeAllowed(LogType logType);

        // 添加日志输出目标（例如，输出到控制台或文件）
        void RegisterLogSink(Action<LogType, string> sink);

        // 清空所有日志输出目标
        void ClearLogSinks();

        // 普通日志输出
        void Log(LogType logType, object message);
        void Log(string tag, object message);
        void Log(LogType logType, string tag, object message, Object context = null);
        void Log(LogType logType, string format, params object[] args);

        // 警告日志
        void LogWarning(string tag, object message);
        void LogWarning(string tag, object message, Object context);

        // 错误日志
        void LogError(string tag, object message);
        void LogError(string tag, object message, Object context);

        // 格式化日志
        void LogFormat(LogType logType, string format, params object[] args);
        void LogFormat(LogType logType, LogOption logOptions, string format, params object[] args);
        // 异常日志
        new void LogException(Exception exception, Object context = null);
    }
}
