using System.Diagnostics;
using InfinityMemoriesEngine.OverWatch.qianhan.Numbers;

namespace InfinityMemoriesEngine.OverWatch.qianhan.Log.lang
{
    [Serializable]
    public class Throwable : Serializable
    {
        private string detailMessage;
        private Throwable cause;
        private List<StackTraceElement> stackTrace;

        public class StackTraceElement
        {
            public string DeclaringClass { get; }
            public string MethodName { get; }
            public string FileName { get; }
            public int LineNumber { get; }

            public StackTraceElement(string declaringClass, string methodName, string fileName, int lineNumber)
            {
                DeclaringClass = declaringClass;
                MethodName = methodName;
                FileName = fileName;
                LineNumber = lineNumber;
            }

            public override string ToString()
            {
                return $"{DeclaringClass}.{MethodName}({FileName}:{LineNumber})";
            }
        }

        public Throwable(string message = "", Throwable cause = null)
        {
            this.detailMessage = message;
            this.cause = cause;
            this.stackTrace = CaptureStackTrace();
        }

        private List<StackTraceElement> CaptureStackTrace()
        {
            var elements = new List<StackTraceElement>();
            var trace = new StackTrace(2, true); // 跳过当前构造器本身
            foreach (var frame in trace.GetFrames())
            {
                var method = frame.GetMethod();
                if (method == null) continue;

                string cls = method.DeclaringType?.FullName ?? "<anonymous>";
                string methodName = method.Name;
                string file = frame.GetFileName() ?? "<unknown>";
                int line = frame.GetFileLineNumber();

                elements.Add(new StackTraceElement(cls, methodName, file, line));
            }
            return elements;
        }

        public override string ToString()
        {
            var sb = new System.Text.StringBuilder();
            sb.AppendLine($"{GetType().Name}: {detailMessage}");
            foreach (var elem in stackTrace)
            {
                sb.AppendLine($"\tat {elem}");
            }
            if (cause != null)
            {
                sb.AppendLine("Caused by: " + cause.ToString());
            }
            return sb.ToString();
        }

        internal Throwable getCause() => cause; // 获取异常原因
    }
}
