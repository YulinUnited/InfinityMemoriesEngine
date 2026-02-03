using InfinityMemoriesEngine.OverWatch.qianhan.Log.lang;
using static InfinityMemoriesEngine.OverWatch.qianhan.Util.utilBase;

namespace InfinityMemoriesEngine.OverWatch.qianhan.Log
{
    internal class DebugManager
    {
        public static readonly Debug debug = new Debug();

        public static Array<Debug>? OutPut;

        public void Log(string smg)
        {
            OutPut?.Invoke(Debug.Log())
        }
    }
}
