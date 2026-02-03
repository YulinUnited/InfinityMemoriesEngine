using InfinityMemoriesEngine.OverWatch.qianhan.MainCollection;
using InfinityMemoriesEngine.OverWatch.qianhan.Start;
using static InfinityMemoriesEngine.OverWatch.qianhan.MainCollection.LifecycleRegistryBuilder;

namespace InfinityMemoriesEngine.OverWatch.qianhan.Config
{
    public static class YulinConfigGenerator
    {
        private static readonly string FixedFileName = "YulinConfig.yulin";

        /// <summary>
        /// 获取固定路径：<引擎根目录>/config/YulinConfig/YulinConfig.yulin
        /// </summary>
        public static string GetFixedYulinConfigPath()
        {
            string engineRoot = AppDomain.CurrentDomain.BaseDirectory;
            string configDir = Path.Combine(engineRoot, "config", "YulinConfig");

            if (!Directory.Exists(configDir))
                Directory.CreateDirectory(configDir);

            return Path.Combine(configDir, FixedFileName);
        }

        /// <summary>
        /// 生成配置文件，自动合并已有配置，保持唯一性
        /// </summary>
        public static void GenerateYulinConfig(List<LifecycleClassInfo> newInfos)
        {
            string filePath = GetFixedYulinConfigPath();
            Dictionary<string, LifecycleClassInfo> finalMap = new();

            // 先读取旧配置（如果有）
            if (File.Exists(filePath))
            {
                var oldLines = File.ReadAllLines(filePath);
                LifecycleClassInfo? current = null;

                foreach (var line in oldLines)
                {
                    var trimmed = line.Trim();
                    if (trimmed.StartsWith("- TypeName:"))
                    {
                        string typeName = trimmed.Substring(11).Trim();
                        current = new LifecycleClassInfo
                        {
                            TypeName = typeName,
                            Methods = new List<LifecycleMethodInfo>()
                        };
                        finalMap[typeName] = current;
                    }
                    else if (trimmed.StartsWith("Registry:") && current != null)
                    {
                        string regStr = trimmed.Substring(9).Trim();
                        Enum.TryParse(regStr, out LifetcycleRegisterys reg);
                        current.Registry = reg;
                    }
                    else if (trimmed.StartsWith("- MethodName:") && current != null)
                    {
                        var method = new LifecycleMethodInfo
                        {
                            MethodName = trimmed.Substring(13).Trim()
                        };
                        current.Methods.Add(method);
                    }
                    else if (trimmed.StartsWith("Phase:") && current != null && current.Methods.Count > 0)
                    {
                        var last = current.Methods[^1];
                        Enum.TryParse(trimmed.Substring(6).Trim(), out ChinesePhase phase);
                        last.Phase = phase;
                    }
                }
            }

            // 合并新数据
            foreach (var info in newInfos)
            {
                if (!finalMap.TryGetValue(info.TypeName, out var existing))
                {
                    finalMap[info.TypeName] = info;
                    continue;
                }

                existing.Registry |= info.Registry;

                foreach (var method in info.Methods)
                {
                    if (!existing.Methods.Any(m => m.MethodName == method.MethodName))
                    {
                        existing.Methods.Add(method);
                    }
                }
            }

            // 写入覆盖文件
            using var writer = new StreamWriter(filePath);
            writer.WriteLine("# Yulin Lifecycle Configuration File");
            writer.WriteLine($"# Auto-generated at {DateTime.Now}");
            writer.WriteLine();

            foreach (var info in finalMap.Values.OrderBy(x => x.TypeName))
            {
                writer.WriteLine($"- TypeName: {info.TypeName}");
                writer.WriteLine($"  Registry: {info.Registry}");
                writer.WriteLine($"  Methods:");
                foreach (var method in info.Methods.OrderBy(m => m.MethodName))
                {
                    writer.WriteLine($"    - MethodName: {method.MethodName}");
                    writer.WriteLine($"      Phase: {method.Phase}");
                }
                writer.WriteLine();
            }
        }
    }
}
