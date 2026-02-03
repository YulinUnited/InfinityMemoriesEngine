using InfinityMemoriesEngine.OverWatch.qianhan.MainCollection;
using static InfinityMemoriesEngine.OverWatch.qianhan.MainCollection.LifecycleRegistryBuilder;

namespace InfinityMemoriesEngine.OverWatch.qianhan.Config
{
    /// <summary>
    /// Yulin注册表解析器
    /// </summary>
    public static class YulinRegistryParser
    {
        public static List<LifecycleClassInfo> LoadFromYulinFile(string path)
        {
            var list = new List<LifecycleClassInfo>();
            if (!File.Exists(path)) return list;

            var lines = File.ReadAllLines(path);
            foreach (var rawLine in lines)
            {
                var line = rawLine.Trim();
                if (string.IsNullOrEmpty(line) || line.StartsWith("#")) continue;

                var parts = line.Split('|', StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length < 2) continue;

                string typeName = parts[0].Trim();
                LifetcycleRegisterys registry = 0;

                for (int i = 1; i < parts.Length; i++)
                {
                    if (Enum.TryParse<LifetcycleRegisterys>(parts[i].Trim(), out var flag))
                        registry |= flag;
                }
                list.Add(new LifecycleClassInfo { TypeName = typeName });

                //list.Add(new LifecycleRegistryInfo { TypeName = typeName, Registry = registry });
            }

            return list;
        }
    }
}
