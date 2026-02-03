using System.Diagnostics;
using InfinityMemoriesEngine.OverWatch.qianhan.Log.lang;

namespace InfinityMemoriesEngine.OverWatch.qianhan.Log
{
    public class Exception : Throwable
    {
        static long serialVersionUID = -338751699312422948L;
        private string messages; // 异常消息
        private Throwable cause; // 异常原因

        public string getMassage() => messages; // 获取异常消息
        public Exception(string message)
        {
            messages = message; // 初始化异常消息
        }

        public Exception(string message, Throwable cause)
        {
            messages = message; // 初始化异常消息
            this.cause = cause; // 设置异常原因
        }

        public Exception(Throwable cause)
        {
            this.cause = cause; // 设置异常原因
        }

        // StackTrace 属性：捕获堆栈跟踪信息
        public string StackTrace
        {
            get
            {
                var stackTrace = new StackTrace(true); // 设置 true 以包含文件信息
                return stackTrace.ToString(); // 返回完整的堆栈信息
            }
        }
        public override string ToString()
        {
            return $"Exception: {messages}\nCaused by: {cause?.ToString() ?? "None"}\n{StackTrace}"; // 返回异常
        }
        public string FullStackTrace
        {
            get
            {
                var builder = new System.Text.StringBuilder();
                var current = this as Throwable;
                while (current != null)
                {
                    builder.AppendLine(current.ToString());
                    current = current.getCause();
                }
                return builder.ToString();
            }
        }

    }
}
