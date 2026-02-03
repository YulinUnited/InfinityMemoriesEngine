using System.IO.MemoryMappedFiles;

namespace InfinityMemoriesEngine.OverWatch.qianhan.Worlds.loads
{
    /// <summary>
    /// 为了避免无意义的行为，该告示为告诫所有开发者，它是用于二进制重载
    /// </summary>
    public class ProgramWorld
    {
        public void TheLoadWorlds()
        {
            // 创建一个内存映射文件
            using (var mmf = MemoryMappedFile.CreateFromFile("large_data.dat", FileMode.OpenOrCreate, "test", 1024))
            {
                // 创建视图访问内存映射文件
                using (var accessor = mmf.CreateViewAccessor())
                {
                    // 读取或写入数据
                    int value = accessor.ReadInt32(0); // 读取数据
                    accessor.Write(4, value + 1); // 写入数据
                }
            }
        }
    }
}
