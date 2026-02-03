using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using static InfinityMemoriesEngine.OverWatch.qianhan.MainCollection.LifecycleRegistryBuilder;

namespace InfinityMemoriesEngine.OverWatch.qianhan.Config
{
    /// <summary>
    /// 生命周期配置类，包含版本、引擎构建信息和生命周期类信息列表
    /// </summary>
    public class LifecycleConfig
    {
        /// <summary>
        /// 生命周期配置版本信息
        /// </summary>
        public string version { get; set; }
        /// <summary>
        /// 引擎构建信息
        /// </summary>
        public string engineBuild { set; get; }
        /// <summary>
        /// 生成时间
        /// </summary>
        public DateTime generateAt { set; get; }
        /// <summary>
        /// 生命周期类信息列表
        /// </summary>
        public List<LifecycleClassInfo> lifecycles { set; get; }
    }




    /// <summary>
    /// 生命周期方法信息类，包含方法名称、参数类型和返回类型等信息
    /// </summary>
    public class LifecycleMethod
    {
        /// <summary>
        /// 方法名称
        /// </summary>
        public string name { set; get; }
        /// <summary>
        /// 路径
        /// </summary>
        public string phase { set; get; }
        /// <summary>
        /// 方法每时刻调用的频率，强制类型
        /// </summary>
        public int forceTick { set; get; }
        /// <summary>
        /// 方法参数类型列表
        /// </summary>
        public string registeredBy { set; get; }
    }




    /// <summary>
    /// 生命周期载入器类，用于加载和保存生命周期配置
    /// </summary>
    public class LifecycleLoader
    {
        /// <summary>
        /// 从指定路径加载生命周期配置
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static LifecycleConfig Load(string path)
        {
            var input = File.ReadAllText(path);
            var deserializer = new DeserializerBuilder().WithNamingConvention(CamelCaseNamingConvention.Instance).Build();
            return deserializer.Deserialize<LifecycleConfig>(input);
        }
        /// <summary>
        /// 将生命周期配置保存到指定路径
        /// </summary>
        /// <param name="path"></param>
        /// <param name="config"></param>
        public static void Save(string path, LifecycleConfig config)
        {
            var serializer = new SerializerBuilder().WithNamingConvention(CamelCaseNamingConvention.Instance).Build();
            var yaml = serializer.Serialize(config);
            File.WriteAllText(path, yaml);
        }
    }
}
