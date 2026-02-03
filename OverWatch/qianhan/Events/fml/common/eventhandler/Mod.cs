using InfinityMemoriesEngine.OverWatch.qianhan.annotations;

namespace InfinityMemoriesEngine.OverWatch.qianhan.Events.fml.common.eventhandler
{
    /// <summary>
    /// 仿Java的模组注解
    /// </summary>
    [Retention(RetentionPolicy.RUNTIME)]
    [Target(ElementType.TYPE)]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = false)]
    public class Mod : Attribute
    {
        string modid;
        string name;
        string version;
        public Mod(string modid, string name, string version)
        {
            this.modid = modid;
            this.name = name;
            this.version = version;
        }
        public Mod(string modid, string name)
        {
            this.modid = modid;
            this.name = name;
            this.version = "1.0.0"; // 默认版本
        }
        public Mod(string modid)
        {
            this.modid = modid;
            this.name = "Unknown Mod"; // 默认名称
            this.version = "1.0.0"; // 默认版本
        }
        public void setModID(string modid)
        {
            this.modid = modid;
        }
        public string getModID()
        {
            return modid;
        }
        public void setName(string name)
        {
            this.name = name;
        }
        public string getName()
        {
            return name;
        }
        public void setVersion(string version)
        {
            this.version = version;
        }
        public string getVersion()
        {
            return version;
        }
    }
}
