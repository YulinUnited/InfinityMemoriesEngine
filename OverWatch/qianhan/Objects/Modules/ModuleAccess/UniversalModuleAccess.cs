using InfinityMemoriesEngine.OverWatch.qianhan.CommandManager;
using InfinityMemoriesEngine.OverWatch.qianhan.Entities;
using InfinityMemoriesEngine.OverWatch.qianhan.Events;
using InfinityMemoriesEngine.OverWatch.qianhan.Items;
using InfinityMemoriesEngine.OverWatch.qianhan.Objects.Modules.ModuleAccess.ModuleObject;
using InfinityMemoriesEngine.OverWatch.qianhan.Worlds;

namespace InfinityMemoriesEngine.OverWatch.qianhan.Objects.Modules.ModuleAccess
{
    public static class UniversalModuleAccess
    {
        public static object GetModuleFor(object target)
        {
            if (target == null) return null;
            switch (target)
            {
                case Entity entity:
                    return entity.getEntityModule<object>();
                case Item item:
                    return item.getItemModule<object>();
                case CommandStar command:
                    int level = command.GetCommandLevel();
                    return CommandModuleSystem.GetCommandLevelModule<object>(level);
                case World world:
                    return world.getWorldModule<object>();
                case Event @event:
                    return EventModuleSystem.getEventModule<object>(@event);
                default:
                    return null;
            }
        }
        public static T GetModule<T>(this object target) where T : class
        {
            return GetModuleFor(target) as T;
        }
    }
}
