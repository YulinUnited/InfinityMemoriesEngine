using System.Reflection;

namespace InfinityMemoriesEngine.OverWatch.qianhan.MainCollection
{
    public class LifetcycleRegistery
    {
        private List<(object Instance, List<MethodInfo> Methods)> _registry = new();

        // 添加实例及其生命周期方法
        public void Register(object instance, List<MethodInfo> methods)
        {
            _registry.Add((instance, methods));
        }

        // 获取所有注册的实例
        public IEnumerable<(object Instance, List<MethodInfo> Methods)> GetAll()
        {
            return _registry;
        }
    }
}
