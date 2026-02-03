using System.Runtime.Serialization.Formatters.Binary;

namespace InfinityMemoriesEngine.OverWatch.qianhan.Config
{
    /// <summary>
    /// 将Yulin格式的注册表转换为二进制文件的工具类
    /// </summary>
    public static class YulinToBinaryConverter
    {
        public static void ConvertYulinToBinary(string yulinPath, string binaryPath)
        {
            var registryList = YulinRegistryParser.LoadFromYulinFile(yulinPath);
            using var fs = new FileStream(binaryPath, FileMode.Create);
#pragma warning disable SYSLIB0011
            new BinaryFormatter().Serialize(fs, registryList);
#pragma warning restore SYSLIB0011
        }
    }
}
