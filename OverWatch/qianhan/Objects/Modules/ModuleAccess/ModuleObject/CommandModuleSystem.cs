using InfinityMemoriesEngine.OverWatch.qianhan.CommandManager;

namespace InfinityMemoriesEngine.OverWatch.qianhan.Objects.Modules.ModuleAccess.ModuleObject
{
    public static class CommandModuleSystem
    {
        private static readonly Dictionary<int, Dictionary<Type, object>> commandLevelModules = new();
        /// <summary>
        /// 将模块挂载到命令发布器上，请注意，命令是无法单独使用的，它需要一个命令发布器来发布命令，所以请确保在使用这个方法之前，已经有了一个命令发布器实例。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="commandLevel"></param>
        /// <param name="module"></param>
        public static void AttachCommandModule<T>(int commandLevel, T module) where T : class
        {
            if (!commandLevelModules.TryGetValue(commandLevel, out var moduleDict))
            {
                moduleDict = new Dictionary<Type, object>();
                commandLevelModules.Add(commandLevel, moduleDict);
            }
            moduleDict[typeof(T)] = module;
        }
        /// <summary>
        /// 为了区分外部库特采取大写驼峰式命名，所以这个方法是获取命令模块的，注意，这个方法是获取命令发布器的模块，而不是实体的模块。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="level"></param>
        /// <returns></returns>
        public static T GetCommandLevelModule<T>(int level) where T : class
        {
            if (commandLevelModules.TryGetValue(level, out var moduleDict) && moduleDict.TryGetValue(typeof(T), out var module))
            {
                return (T)module;
            }
            return null;
        }
        /// <summary>
        /// 判断对象是否是命令发布器，命令发布器是一个特殊的实体，它用于发布命令，通常用于游戏中或其他的指令系统。
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsCommand(this object obj)
        {
            return obj is CommandStar;
        }
        /// <summary>
        /// 获取命令的等级，命令等级是指命令发布器的等级，它是一个整数值，通常用于区分不同等级的命令。
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int GetCommandLevel(this object obj)
        {
            if (obj is CommandStar commandStar)
            {
                return commandStar.commandLevel;
            }
            return -1; // 如果不是命令，则返回-1
        }
    }
}
