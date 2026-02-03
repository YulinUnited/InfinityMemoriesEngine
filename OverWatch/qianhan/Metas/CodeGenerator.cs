using System.Text;
using InfinityMemoriesEngine.OverWatch.qianhan.Start;

namespace InfinityMemoriesEngine.OverWatch.qianhan.Metas
{
    /// <summary>
    /// 代码生成器，用于生成调用代码文件。
    /// </summary>
    public static class CodeGenerator
    {
        private static string @NameSpace = "";
        private static object obj = new object();
        private static ChinesePhase Phase;
        /// <summary>
        /// 生成一个简单类文件，包含调用代码（可以直接编译/查看）。
        /// </summary>
        public static void GenerateFile(string methodCallCode, string outputPath, string @namespace = "GeneratedByMeta")
        {
            var sb = new StringBuilder();
            sb.AppendLine("using System;");
            sb.AppendLine();
            sb.AppendLine($"namespace {@namespace}");
            sb.AppendLine("{");
            sb.AppendLine("    public static class MetaGenerated");
            sb.AppendLine("    {");
            sb.AppendLine("        public static void Execute()");
            sb.AppendLine("        {");
            sb.AppendLine("            // 自动生成的调用代码");
            sb.AppendLine("            " + methodCallCode);
            sb.AppendLine("        }");
            sb.AppendLine("    }");
            sb.AppendLine("}");

            File.WriteAllText(outputPath, sb.ToString(), Encoding.UTF8);
        }

        /// <summary>
        /// 包含生命周期的文件生成。
        /// </summary>
        /// <param name="methodCallCode">方法</param>
        /// <param name="outputPath">输出</param>
        /// <param name="namespace">命名空间</param>
        /// <param name="phase">生命周期</param>
        public static void GenerateFiles(string methodCallCode, string outputPath, string @namespace, ChinesePhase phase)
        {
            var Override = new StringBuilder();
            var methods = obj.GetType()
                             .GetMethods(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public);
            foreach (var method in methods)
            {
                //利用反射获取方法名并生成调用代码，只是大致思路，不作为具体实现
            }
        }
        /// <summary>
        /// 设置命名空间，强烈建议优先通过setNameSpace设置一个你自己的命名空间
        /// </summary>
        /// <param name="name">名称</param>
        public static void setNameSpace(string name)
        {
            @NameSpace = name;
        }
        /// <summary>
        /// 获取命名空间
        /// </summary>
        /// <returns>命名空间</returns>
        public static string getNameSpace()
        {
            return @NameSpace;
        }

    }
}
