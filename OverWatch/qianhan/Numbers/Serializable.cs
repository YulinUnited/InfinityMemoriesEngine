using System.Reflection;

namespace InfinityMemoriesEngine.OverWatch.qianhan.Numbers
{
    /// <summary>
    /// 空的序列化接口，用于标记可序列化的类。
    /// </summary>
    public interface Serializable
    {
        public static readonly long serialVersionUID = 1L; // 序列化版本号
        public static void writeObject(object obj, Stream stream)
        {
            var writer = new BinaryWriter(stream);
            var type = obj.GetType();
            writer.Write(serialVersionUID);

            var fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            foreach (var field in fields)
            {
                var value = field.GetValue(obj);
                if (value is int i)
                    writer.Write(i);
                else if (value is string s)
                    writer.Write(s);
                else
                    throw new NotSupportedException("Unsupported type in writeObject");
            }
        }
    }
}
