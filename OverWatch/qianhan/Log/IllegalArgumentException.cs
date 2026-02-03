using System.Diagnostics;
using InfinityMemoriesEngine.OverWatch.qianhan.Log.lang;

namespace InfinityMemoriesEngine.OverWatch.qianhan.Log
{
    [DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
    public class IllegalArgumentException : Exception
    {
        public string Message { get; private set; }
        public Throwable InnerException { get; private set; }

        public IllegalArgumentException(string v) : base(v)
        {
        }
        public IllegalArgumentException(string message, Throwable cause) : base(message, cause)
        {
        }
        public IllegalArgumentException(Throwable cause) : base(cause)
        {
        }

        private string GetDebuggerDisplay()
        {
            return ToString();
        }

    }
}
