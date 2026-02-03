namespace InfinityMemoriesEngine.OverWatch.qianhan.Objects.TickManager
{
    /// <summary>
    /// TickDispatcher类，用于管理时刻池中的时刻和对应的动作
    /// </summary>
    internal class TickDispatcher
    {
        private static int mainTick = (int)MainObject.MainTick;
        /// <summary>
        /// 对象池时刻池
        /// </summary>
        private static Dictionary<int, List<Action>> TickPool = new Dictionary<int, List<Action>>();
        /// <summary>
        /// 通过委托的方式将tick添加到列表中
        /// </summary>
        /// <param name="tick"></param>
        /// <param name="action"></param>
        public static void Register(int tick, Action action)
        {
            if (!TickPool.TryGetValue(tick, out var list))
            {
                TickPool[tick] = list = new List<Action>();
                list.Add(action);
            }
        }
        /// <summary>
        /// 调度tick方法
        /// </summary>
        public static void Tick()
        {
            if (TickPool.TryGetValue(mainTick, out var actions))
            {
                foreach (var act in actions)
                {
                    act?.Invoke();
                }
                //因为mainTick等于MainObject.MainTick，所以不需要将其递加
                //mainTick++;
            }
        }

    }
}
