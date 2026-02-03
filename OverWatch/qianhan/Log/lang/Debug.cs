using System.Runtime.CompilerServices;
using InfinityMemoriesEngine.OverWatch.qianhan.Log.lang.logine;

namespace InfinityMemoriesEngine.OverWatch.qianhan.Log.lang
{
    public class Debug
    {
        static LogEvent logEvent;
        internal static readonly ILogger DefaultLogger = new Logger(new DebugLogHandler());
        static ILogger Logger = new Logger(new DebugLogHandler());
        public static ILogger logger => Logger;
        /// <summary>
        /// 普通日志记录方法，记录普通日志信息。
        /// </summary>
        /// <param name="message"></param>
        public static void Log(object message)
        {
            if (logEvent.getLogEvent())
            {
                LogInternal(LogType.Log, message?.ToString() ?? string.Empty);
                //logger.Log(LogType.Log, message);
            }
        }
        /// <summary>
        /// 针对日志记录的简化方法，记录普通日志信息。
        /// </summary>
        /// <param name="message"></param>
        /// <param name="context"></param>
        public static void Log(object message, Object context)
        {
            if (logEvent.getLogEvent())
            {
                // 确保 message 是一个字符串类型
                string messageStr = message?.ToString() ?? string.Empty;
                //logger.Log(LogType.Log, messageStr, context);
                LogInternal(LogType.Log, messageStr, context);
            }
        }
        /// <summary>
        /// 当警告日志需要记录时，使用此方法。
        /// </summary>
        /// <param name="message"></param>
        public static void LogWarning(object message)
        {
            if (logEvent.getLogEvent())
            {
                // 确保 message 是一个字符串类型
                string messageStr = message?.ToString() ?? string.Empty;
                //logger.Log(LogType.Warning, messageStr);
                LogInternal(LogType.Warning, messageStr);
            }
        }
        /// <summary>
        /// 当错误日志需要记录时，使用此方法。
        /// </summary>
        /// <param name="message"></param>
        public static void LogError(object message)
        {
            if (logEvent.getLogEvent())
            {
                string messageStr = message?.ToString() ?? string.Empty;
                LogInternal(LogType.Error, messageStr);
            }
        }
        public static void LogError(object message, Object context)
        {
            if (logEvent.getLogEvent())
            {
                // 确保 message 是一个字符串类型
                string messageStr = message?.ToString() ?? string.Empty;
                //logger.Log(LogType.Error, messageStr, context);
                LogInternal(LogType.Error, messageStr, context);
            }
        }
        public static void LogException(Exception exception)
        {
            if (logEvent.getLogEvent())
            {
                if (exception == null) return;
                //logger.Log(LogType.Exception, exception.ToString());
                LogInternal(LogType.Exception, exception.ToString());
            }
        }
        public static void LogException(Exception exception, Object context)
        {
            if (logEvent.getLogEvent())
            {
                if (exception == null) return;
                //logger.Log(LogType.Exception, exception.ToString(), context);
                LogInternal(LogType.Exception, exception.ToString(), context);
            }
        }
        /// <summary>
        /// 当需要记录警告日志时，使用此方法。
        /// </summary>
        /// <param name="message"></param>
        /// <param name="context"></param>
        public static void LogWarning(object message, Object context)
        {
            if (logEvent.getLogEvent())
            {
                // 确保 message 是一个字符串类型
                string messageStr = message?.ToString() ?? string.Empty;
                //logger.Log(LogType.Warning, messageStr, context);
                LogInternal(LogType.Warning, messageStr, context);
            }
        }
        /// <summary>
        /// 私有方法，内部使用，记录日志信息。
        /// </summary>
        /// <param name="type"></param>
        /// <param name="message"></param>
        /// <param name="context"></param>
        /// <param name="callerFile"></param>
        /// <param name="callerMember"></param>
        /// <param name="callerLine"></param>
        private static void LogInternal(LogType type, string message, Object context = null,
            [CallerFilePath] string callerFile = "",
            [CallerMemberName] string callerMember = "",
            [CallerLineNumber] int callerLine = 0)
        {
            // 提取文件名（不含路径）
            var fileName = System.IO.Path.GetFileName(callerFile);

            // 组合调用位置信息，格式：文件:行号 方法名()
            string prefix = $"{fileName}:{callerLine} {callerMember}()";

            // 拼接完整信息
            string fullMessage = $"[{prefix}] {message}";

            if (context == null)
            {
                logger.Log(type, fullMessage);
            }
            else
            {
                logger.Log(type, fullMessage, context);
            }
        }

        internal static void LogException(object message)
        {
            if (logEvent.getLogEvent())
            {
                string messageStr = message?.ToString() ?? string.Empty;
                //logger.Log(LogType.Warning, messageStr, context);
                LogInternal(LogType.Error, messageStr);
            }
        }
    }
}
