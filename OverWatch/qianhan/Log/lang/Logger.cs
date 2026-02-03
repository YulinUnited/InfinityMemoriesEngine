using InfinityMemoriesEngine.OverWatch.qianhan.Log.lang.logine;

namespace InfinityMemoriesEngine.OverWatch.qianhan.Log.lang
{
    public class Logger : ILogger
    {
        private ILogHandler _logHandler;
        private LogType _logLevel = LogType.Log; // 默认最低级别是 Log
        private bool _logEnabled = true;

        public Logger(ILogHandler logHandler = null)
        {
            // 默认使用 DebugLogHandler，也可以传入其他自定义的 ILogHandler 实现
            _logHandler = logHandler ?? new DebugLogHandler();
        }

        // 控制是否启用日志输出
        public bool LogEnabled
        {
            get => _logEnabled;
            set => _logEnabled = value;
        }

        // 控制最低日志级别
        public LogType LogLevel
        {
            get => _logLevel;
            set => _logLevel = value;
        }
        public ILogHandler LogHandler { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        // 检查日志类型是否允许输出
        public bool IsLogTypeAllowed(LogType logType)
        {
            return logType >= _logLevel;
        }

        // 注册 Sink（目标输出：文件、UI 控制台等）
        public void RegisterLogSink(Action<LogType, string> sink)
        {
            if (_logHandler is DebugLogHandler debugHandler)
            {
                debugHandler.RegisterSink(sink);
            }
        }

        // 清空所有 Sink
        public void ClearLogSinks()
        {
            if (_logHandler is DebugLogHandler debugHandler)
            {
                debugHandler.ClearSinks();
            }
        }

        // 普通日志输出
        public void Log(LogType logType, object message)
        {
            if (!_logEnabled || !IsLogTypeAllowed(logType)) return;

            string msg = message?.ToString() ?? "null";
            _logHandler.Log(string.Empty, logType, msg, null);
        }

        public void Log(string tag, object message)
        {
            if (!_logEnabled || !IsLogTypeAllowed(LogType.Log)) return;

            string msg = message?.ToString() ?? "null";
            _logHandler.Log(tag, LogType.Log, msg, null);
        }

        public void Log(LogType logType, string tag, object message, Object context = null)
        {
            if (!_logEnabled || !IsLogTypeAllowed(logType)) return;

            string msg = message?.ToString() ?? "null";
            _logHandler.Log(tag, logType, msg, context);
        }

        public void Log(LogType logType, string format, params object[] args)
        {
            if (!_logEnabled || !IsLogTypeAllowed(logType)) return;

            string formattedMessage = string.Format(format, args);
            _logHandler.LogFormat(logType, null, formattedMessage);
        }

        // 格式化日志输出
        public void LogFormat(LogType logType, string format, params object[] args)
        {
            if (!_logEnabled || !IsLogTypeAllowed(logType)) return;

            string formattedMessage = string.Format(format, args);
            _logHandler.LogFormat(logType, null, formattedMessage);
        }

        public void LogFormat(LogType logType, LogOption logOptions, string format, params object[] args)
        {
            if (!_logEnabled || !IsLogTypeAllowed(logType)) return;

            string formattedMessage = string.Format(format, args);
            _logHandler.LogFormat(logType, logOptions, null, formattedMessage);
        }

        // 异常日志输出
        public void LogException(Exception exception, Object context = null)
        {
            if (!_logEnabled) return;

            _logHandler.LogException(exception, context);
        }

        // 便捷方法：直接记录 Log
        public void LogInfo(string tag, string message, Object context = null)
        {
            Log(LogType.Log, tag, message, context);
        }

        public void LogWarning(string tag, string message, Object context = null)
        {
            Log(LogType.Warning, tag, message, context);
        }

        public void LogError(string tag, string message, Object context = null)
        {
            Log(LogType.Error, tag, message, context);
        }

        public void LogAssert(string tag, string message, Object context = null)
        {
            Log(LogType.Assert, tag, message, context);
        }

        // 供外部调用设置最低日志等级
        public void SetLogLevel(LogType logType)
        {
            _logLevel = logType;
        }

        public void LogWarning(string tag, object message)
        {
            Log(LogType.Warning, tag, message);
        }

        public void LogWarning(string tag, object message, object context)
        {
            Log(LogType.Warning, tag, message, context);
        }

        public void LogError(string tag, object message)
        {
            Log(LogType.Error, tag, message);
        }

        public void LogError(string tag, object message, object context)
        {
            Log(LogType.Error, tag, message, context);
        }

        public void LogFormat(LogType logType, LogOption logOptions, object context, string format, params object[] args)
        {
            if (!_logEnabled || !IsLogTypeAllowed(logType)) return;

            string formattedMessage = string.Format(format, args);
            _logHandler.LogFormat(logType, logOptions, context, formattedMessage);
        }

        public void Log(string tag, LogType logType, string msg, object context)
        {
            if (!_logEnabled || !IsLogTypeAllowed(logType)) return;

            _logHandler.Log(tag, logType, msg, context);
        }

        public void LogFormat(LogType logType, object value, string formattedMessage)
        {
            if (!_logEnabled || !IsLogTypeAllowed(logType)) return;

            // 格式化日志的消息，可以根据具体的需求对 value 进行处理
            string finalMessage = string.Format(formattedMessage, value);

            // 使用 _logHandler 处理格式化的日志消息
            _logHandler.LogFormat(logType, null, finalMessage);
        }
    }
}
