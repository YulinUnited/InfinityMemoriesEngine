namespace InfinityMemoriesEngine.OverWatch.qianhan.Entities.ai
{
    public class EntityAITasks
    {
        private readonly List<EntityAITaskEntry> taskEntries = new();
        private readonly List<EntityAITaskEntry> executingTaskEntries = new();

        // 添加 AI（带优先级）
        public void addTask(int priority, EntityAIBase task)
        {
            taskEntries.Add(new EntityAITaskEntry(priority, task));
            taskEntries.Sort((a, b) => a.Priority.CompareTo(b.Priority));
        }

        // 移除 AI
        public void removeTask(EntityAIBase task)
        {
            taskEntries.RemoveAll(t => t.Action == task);
            executingTaskEntries.RemoveAll(t => t.Action == task);
        }

        // 每 tick 调用，驱动 AI 系统
        public void onUpdateTasks()
        {
            // 1. 移除不再继续执行的任务
            var toRemove = new List<EntityAITaskEntry>();
            foreach (var entry in executingTaskEntries)
            {
                if (!entry.Action.shouldContinueExecuting())
                {
                    entry.Action.resetTask();
                    toRemove.Add(entry);
                }
            }
            foreach (var r in toRemove) executingTaskEntries.Remove(r);

            // 2. 检查新任务是否可以启动
            foreach (var entry in taskEntries)
            {
                if (!executingTaskEntries.Contains(entry))
                {
                    if (canUse(entry) && entry.Action.shouldExecute())
                    {
                        entry.Action.startExecuting();
                        executingTaskEntries.Add(entry);
                    }
                }
            }

            // 3. 更新正在执行的任务
            foreach (var entry in executingTaskEntries)
            {
                entry.Action.updateTask();
            }
        }

        // 检查是否能并行执行（mutexBits 控制）
        private bool canUse(EntityAITaskEntry entry)
        {
            foreach (var exec in executingTaskEntries)
            {
                if ((entry.Action.getMutexBits() & exec.Action.getMutexBits()) != 0)
                    return false;
            }
            return true;
        }

        // 内部包装类
        private class EntityAITaskEntry
        {
            public int Priority { get; }
            public EntityAIBase Action { get; }

            public EntityAITaskEntry(int priority, EntityAIBase action)
            {
                Priority = priority;
                Action = action;
            }
        }
    }
}
