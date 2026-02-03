using InfinityMemoriesEngine.OverWatch.qianhan.Log.lang;
using InfinityMemoriesEngine.OverWatch.qianhan.Log.lang.logine;

namespace InfinityMemoriesEngine.OverWatch.qianhan.profiler
{
    public class Profiler
    {
        private static Logger logger;
        private List<string> sectionList = new List<string>();
        private List<long> timestampList = new List<long>();
        public bool profilingEnabled;
        private string profilingSection = "";
        private Dictionary<string, long> profilingMap = new Dictionary<string, long>();

        public void clearProfiling()
        {
            this.profilingMap.Clear();
            this.profilingSection = "";
            this.sectionList.Clear();
        }

        public void startSection(string name)
        {
            if (!this.profilingEnabled)
            {
                return;
            }
            if (this.profilingSection.Length > 0)
            {
                this.profilingSection = this.profilingSection + ".";
            }
            this.profilingSection = this.profilingSection + name;
            this.sectionList.Add(this.profilingSection);
            this.timestampList.Add(DateTimeOffset.Now.ToUnixTimeMilliseconds());
        }

        public void endSection()
        {
            if (this.profilingEnabled)
            {
                long i = 0;
                long j = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                this.sectionList.RemoveAt(this.sectionList.Count - 1);
                long k = i - j;

                if (this.profilingMap.ContainsKey(this.profilingSection))
                {
                    this.profilingMap[this.profilingSection] += k;
                }
                else
                {
                    this.profilingMap[this.profilingSection] = k;
                }
                if (k > 1000000000L)
                {
                    logger.Log(LogType.Warning, "Something's taking too long! '" + this.profilingSection + "' took aprox " + (k / 1000000.0D) + " ms");
                }
                this.profilingSection = this.sectionList.Count > 0 ? this.sectionList[this.sectionList.Count - 1] : "";
            }
        }
    }
}
