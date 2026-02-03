using System.Diagnostics;
using System.Text;
using static InfinityMemoriesEngine.OverWatch.qianhan.Util.utilBase;

namespace InfinityMemoriesEngine.OverWatch.qianhan.Log.lang.logine
{
    public sealed class DebugLogHandler : ILogHandler
    {
        public bool enableTimestamp = true;
        public bool enableCallerTrace = true;
        public bool enableThreadId = true;

        public Func<string, bool> tagFilter = null;

        // 多目标输出支持
        private readonly List<Array<LogType, string>> _logSinks = new();

        public void RegisterSink(Array<LogType, string> sink)
        {
            _logSinks.Add(sink);
        }

        public void ClearSinks()
        {
            _logSinks.Clear();
        }

        private string ComposeMessage(LogType type, string tag, string msg)
        {
            var sb = new StringBuilder();

            // 时间戳
            if (enableTimestamp)
                sb.Append($"[{DateTime.Now:HH:mm:ss.fff}] ");

            // 线程信息
            if (enableThreadId)
                sb.Append($"[T:{Thread.CurrentThread.ManagedThreadId}] ");

            // Tag + 类型
            if (!string.IsNullOrEmpty(tag))
                sb.Append($"[{tag}] ");

            sb.Append(type.ToString().ToUpper()).Append(": ");

            sb.Append(msg);

            // 溯源：调用位置
            if (enableCallerTrace)
            {
                var stackTrace = new StackTrace(3, true); // 忽略本身调用栈
                var frame = stackTrace.GetFrame(0);
                if (frame != null)
                {
                    var file = frame.GetFileName();
                    var line = frame.GetFileLineNumber();
                    var method = frame.GetMethod();
                    sb.Append($" ({method?.DeclaringType?.Name}.{method?.Name} @ {System.IO.Path.GetFileName(file)}:{line})");
                }
            }

            return sb.ToString();
        }

        // 实际日志处理
        private void Internal_Log(LogType logType, LogOption logOptions, string message, object context)
        {
            // 示例：将日志输出到控制台
            switch (logType)
            {
                case LogType.Error:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case LogType.Warning:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case LogType.Log:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case LogType.Exception:
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    break;
            }

            // 输出日志
            Console.WriteLine(message);
            Console.ResetColor();
        }

        // 处理异常日志
        private void Internal_LogException(Exception exception, object context)
        {
            string exceptionMessage = $"EXCEPTION: {exception}\n{exception.StackTrace}";

            // 示例：将异常输出到控制台
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(exceptionMessage);
            Console.ResetColor();
        }

        public void LogFormat(LogType logType, Object context, string format, params object[] args)
        {
            string msg = string.Format(format, args);
            string final = ComposeMessage(logType, null, msg);
            Internal_Log(logType, LogOption.None, final, context);
            foreach (var sink in _logSinks) sink?.Invoke(logType, final);
        }

        public void LogFormat(LogType logType, LogOption logOptions, Object context, string format, params object[] args)
        {
            string msg = string.Format(format, args);
            string final = ComposeMessage(logType, null, msg);
            Internal_Log(logType, logOptions, final, context);
            foreach (var sink in _logSinks) sink?.Invoke(logType, final);
        }

        public void Log(string tag, LogType type, string message, Object context)
        {
            if (tagFilter != null && !tagFilter(tag)) return;

            string final = ComposeMessage(type, tag, message);
            Internal_Log(type, LogOption.None, final, context);
            foreach (var sink in _logSinks) sink?.Invoke(type, final);
        }

        public void LogException(Exception exception, Object context)
        {
            if (exception == null)
                throw new ArgumentNullException(nameof(exception));

            Internal_LogException(exception, context);
            foreach (var sink in _logSinks)
                sink?.Invoke(LogType.Exception, $"EXCEPTION: {exception}\n{exception.StackTrace}");
        }

        public void LogFormat(LogType logType, object value, string formattedMessage)
        {
            // 将 value 转换为字符串，确保不会抛出异常
            string valueString = value?.ToString() ?? "null";

            // 通过 ComposeMessage 来生成最终的日志信息
            string finalMessage = ComposeMessage(logType, null, string.Format(formattedMessage, valueString));

            // 输出日志
            Internal_Log(logType, LogOption.None, finalMessage, null);

            // 将日志发送到其他注册的 sinks
            foreach (var sink in _logSinks)
            {
                sink?.Invoke(logType, finalMessage);
            }
        }
    }
}
