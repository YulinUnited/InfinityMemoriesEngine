using InfinityMemoriesEngine.OverWatch.qianhan.annotations;

namespace InfinityMemoriesEngine.OverWatch.qianhan.Events.fml.common.eventhandler
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class Cancelable : Attribute
    {

    }
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
    [Retention(RetentionPolicy.RUNTIME)]
    [Target(ElementType.METHOD)]
    public class Remove : Attribute
    {

    }
}
