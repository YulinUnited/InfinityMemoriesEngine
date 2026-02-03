using System.Reflection;
using System.Text.RegularExpressions;
using InfinityMemoriesEngine.OverWatch.qianhan.Numbers;

namespace InfinityMemoriesEngine.OverWatch.qianhan.Metas
{
    /// <summary>
    /// 高级命令解析器：扫描指定程序集中的方法（带 MetaCompileAttirbute），并把自然语言命令转换成方法调用代码。
    /// </summary>
    public static class CommandParserAdvanced
    {
        // 扫描程序集缩小为引擎核心程序集数组以提升性能
        private static readonly Assembly[] ScanAssemblies = new[] { Assembly.GetExecutingAssembly() };

        // 数字正则（支持整数和小数以及负数）
        private static readonly Regex NumberRegex = new Regex(@"-?\d+(\.\d+)?", RegexOptions.Compiled);

        // 简单标识参数名与命令中词的映射（后续可扩充）
        private static readonly Dictionary<string, string[]> ParamAliases = new()
    {
        { "x", new[] { "x", "横", "横坐标", "到" } },
        { "y", new[] { "y", "纵", "纵坐标" } },
        { "z", new[] { "z", "深", "高度" } },
        { "health", new[] { "血", "生命", "生命值", "hp" } },
        { "damage", new[] { "伤害", "损失", "掉血" } },
    };

        public static string Parse(string command)
        {
            if (string.IsNullOrWhiteSpace(command)) return "// empty command";

            // 1. 直接按关键词精确匹配（优先级高 -> 高优先级先）
            var candidates = FindCandidateMethods(command);
            if (!candidates.Any()) return "// 未找到可匹配的方法";

            // 2. 选最高优先级，若相同按关键词命中数或模糊距离
            var chosen = ChooseBestCandidate(command, candidates);

            // 3. 生成调用代码（会尝试把数字按参数顺序或按参数名匹配）
            return GenerateCallCode(chosen.Method, command);
        }

        private record Candidate(MethodInfo Method, int Priority, int KeywordMatches, int FuzzyScore);

        private static List<Candidate> FindCandidateMethods(string command)
        {
            var list = new List<Candidate>();
            foreach (var asm in ScanAssemblies)
            {
                foreach (var type in asm.GetTypes())
                {
                    foreach (var method in type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static))
                    {
                        var attr = method.GetCustomAttribute<MetaCompileAttirbute>();
                        if (attr == null) continue;

                        int keywordMatches = 0;
                        foreach (var k in attr.KeyWords)
                            if (!string.IsNullOrEmpty(k) && command.Contains(k, StringComparison.OrdinalIgnoreCase)) keywordMatches++;

                        // if direct keywords matched, add immediately
                        if (keywordMatches > 0)
                        {
                            list.Add(new Candidate(method, attr.Priority, keywordMatches, 0));
                            continue;
                        }

                        // 否则尝试模糊匹配：取关键词最短的距离
                        int bestFuzzy = int.MaxValue;
                        foreach (var k in attr.KeyWords)
                        {
                            if (string.IsNullOrWhiteSpace(k)) continue;
                            int d = LevenshteinDistance(k, command);
                            if (d < bestFuzzy) bestFuzzy = d;
                        }

                        // 可接受阈值（经验值）：关键词长度的一半以内
                        int threshold = 6; // 稍宽松
                        if (bestFuzzy <= threshold)
                            list.Add(new Candidate(method, attr.Priority, 0, bestFuzzy));
                    }
                }
            }
            return list;
        }

        private static Candidate ChooseBestCandidate(string command, List<Candidate> candidates)
        {
            // 优先按 Priority, 再按 KeywordMatches, 再按 FuzzyScore(越小越好), 再按参数数量接近
            return candidates
                .OrderByDescending(c => c.Priority)
                .ThenByDescending(c => c.KeywordMatches)
                .ThenBy(c => c.FuzzyScore == 0 ? int.MinValue : c.FuzzyScore)
                .ThenBy(c => Mathf.Abs(c.Method.GetParameters().Length - CountNumbers(command)))
                .First();
        }

        private static int CountNumbers(string s) => NumberRegex.Matches(s).Count;

        private static string GenerateCallCode(MethodInfo method, string command)
        {
            var ps = method.GetParameters();
            var foundNumbers = NumberRegex.Matches(command).Select(m => m.Value).ToList();
            int numIdx = 0;
            var args = new List<string>();

            foreach (var p in ps)
            {
                string arg = p.Name; // default fallback is parameter name

                // numeric types get numbers in order
                if (p.ParameterType == typeof(int) || p.ParameterType == typeof(float) || p.ParameterType == typeof(double))
                {
                    // 先尝试按参数名在命令中搜别名
                    var aliasFound = TryFindByParamAliases(command, p.Name);
                    if (aliasFound is string aliasValue) { arg = aliasValue; }
                    else if (numIdx < foundNumbers.Count) { arg = foundNumbers[numIdx++]; }
                    else { arg = p.ParameterType == typeof(int) ? "0" : "0.0"; }
                }
                else
                {
                    // 非数值类型：尝试在命令中找标识词（比如实体名）
                    var maybe = TryExtractByNameToken(command, p.Name);
                    if (!string.IsNullOrEmpty(maybe)) arg = maybe;
                }

                args.Add(arg);
            }

            string call = $"{method.Name}({string.Join(", ", args)})";
            // 如果是实例方法，需要上下文，这里只产生调用表达式
            return call + ";";
        }

        private static string TryFindByParamAliases(string command, string paramName)
        {
            if (ParamAliases.TryGetValue(paramName, out var aliases))
            {
                foreach (var a in aliases)
                {
                    // 简单匹配 "名字 后面跟数字" 或 "到 数字"
                    var m = Regex.Match(command, $@"{Regex.Escape(a)}\s*[:=]?\s*(-?\d+(\.\d+)?)");
                    if (m.Success) return m.Groups[1].Value;
                    // 或者 "到 100"
                    m = Regex.Match(command, $@"{Regex.Escape(a)}\s+(-?\d+(\.\d+)?)");
                    if (m.Success) return m.Groups[1].Value;
                }
            }
            return null;
        }

        private static string TryExtractByNameToken(string command, string paramName)
        {
            // 如果命令里出现了参数名本身，尝试抓紧随其后的词作为值：比如 "设置生命值为 20"
            var m = Regex.Match(command, $@"{Regex.Escape(paramName)}\s*(?:为|=|:)?\s*([^\s,，。]+)");
            if (m.Success) return m.Groups[1].Value;
            return null;
        }

        // 小型 Levenshtein 算法（字符串编辑距离）
        private static int LevenshteinDistance(string a, string b)
        {
            if (string.IsNullOrEmpty(a)) return string.IsNullOrEmpty(b) ? 0 : b.Length;
            if (string.IsNullOrEmpty(b)) return a.Length;
            int[,] d = new int[a.Length + 1, b.Length + 1];
            for (int i = 0; i <= a.Length; i++) d[i, 0] = i;
            for (int j = 0; j <= b.Length; j++) d[0, j] = j;
            for (int i = 1; i <= a.Length; i++)
            {
                for (int j = 1; j <= b.Length; j++)
                {
                    int cost = char.ToLowerInvariant(a[i - 1]) == char.ToLowerInvariant(b[j - 1]) ? 0 : 1;
                    d[i, j] = (int)Mathf.Min(Mathf.Min(d[i - 1, j] + 1, d[i, j - 1] + 1), d[i - 1, j - 1] + cost);
                }
            }
            return d[a.Length, b.Length];
        }
    }
}
