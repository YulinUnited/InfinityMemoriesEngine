using InfinityMemoriesEngine.OverWatch.qianhan.annotations;

namespace InfinityMemoriesEngine.OverWatch.qianhan.Events.fml.common.eventhandler
{
    [Retention(RetentionPolicy.RUNTIME)]
    [Target(ElementType.TYPE)]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = false)]
    public class AddonAttribute : Attribute
    {
        private string AddonID;
        private string AddonName;
        private string AddonVersion;
        public AddonAttribute(string addonID, string addonName, string addonVersion)
        {
            AddonID = addonID;
            AddonName = addonName;
            AddonVersion = addonVersion;
        }
        public AddonAttribute(string addonID, string addonName)
        {
            AddonID = addonID;
            AddonName = addonName;
            AddonVersion = "1.0.0"; // Default version if not provided
        }
        public AddonAttribute(string addonID)
        {
            AddonID = addonID;
            AddonName = "Unknown"; //默认名称
            AddonVersion = "1.0.0"; // 默认版本
        }
        public void setAddonID(string addonID)
        {
            AddonID = addonID;
        }
        public void setAddonName(string addonName)
        {
            AddonName = addonName;
        }
        public void setAddonVersion(string addonVersion)
        {
            AddonVersion = addonVersion;
        }
        public string GetAddonID()
        {
            return AddonID;
        }
        public string GetAddonName()
        {
            return AddonName;
        }
        public string GetAddonVersion()
        {
            return AddonVersion;
        }
    }
}
