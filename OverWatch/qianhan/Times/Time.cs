using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InfinityMemoriesEngine.OverWatch.qianhan.annotations;
using InfinityMemoriesEngine.OverWatch.qianhan.annotations.meta;
using InfinityMemoriesEngine.OverWatch.qianhan.Enums;
using InfinityMemoriesEngine.OverWatch.qianhan.Numbers;
using InfinityMemoriesEngine.OverWatch.qianhan.Start;

namespace InfinityMemoriesEngine.OverWatch.qianhan.Times
{
    /// <summary>
    /// 时间
    /// </summary>
    [StaticAccessor("getTimeManager()", StaticAccessorType.Dot)]
    public static class Time
    {
        /// <summary>
        /// 时间自启动以来的总时长，单位为秒。
        /// </summary>
        public static float TimeSinceStartup => time;
        /// <summary>
        /// 时间间隔，上一时刻到当前时刻的时间差，受时间缩放影响，单位为秒。
        /// </summary>
        public static float DeltaTime => deltaTime;
        /// <summary>
        /// 未缩放的时间间隔，上一时刻到当前时刻的时间差，不受时间缩放影响，单位为秒。
        /// </summary>
        public static float UnscaledDeltaTime => rawDelta;
        /// <summary>
        /// 时间缩放因子，控制时间的流逝速度。默认值为1.0，表示正常速度。设置为0.0将暂停时间流逝。
        /// </summary>
        public static float TimeScale
        {
            get => timeScale;
            set => timeScale = Mathf.Max(0f, value);
        }
        /// <summary>
        /// 未缩放时间自启动以来的总时长，单位为秒。
        /// </summary>
        public static float UnscaledTime => unscaledTime;
        /// <summary>
        /// 固定时间间隔，用于物理更新，单位为秒。默认值为0.02秒。
        /// </summary>
        public static float FixedDeltaTime { get; set; } = 0.02f;
        /// <summary>
        /// 固定时间自启动以来的总时长，单位为秒。
        /// </summary>
        public static float FixedTime => fixedTime;
 
        /// <summary>
        /// 固定时间的更新状态指示器，表示当前是否处于固定时间步长的更新过程中。
        /// </summary>
        public static bool InFixedTimeStep { get; private set; } = false;

        /// <summary>
        /// 时间
        /// </summary>
        public static float time;
        /// <summary>
        /// 时间间隔
        /// </summary>
        public static float deltaTime;
        /// <summary>
        /// 未缩放时间
        /// </summary>
        public static float unscaledTime;
        /// <summary>
        /// 原始时间间隔
        /// </summary>
        public static float rawDelta;
        /// <summary>
        /// 时间尺度
        /// </summary>
        public static float timeScale = 1f;
        /// <summary>
        /// 固定时间
        /// </summary>
        public static float fixedTime;
        /// <summary>
        /// 蓄能器，用于固定时间步长的计算。
        /// </summary>
        public static float accumulator;


        // 每时刻调用
        /// <summary>
        /// 更新时间状态的方法，应在每时刻调用。
        /// </summary>
        /// <param name="delta">间隔</param>
        [Chinese(ChinesePhase.Update)]
        public static void Update(float delta)
        {
            rawDelta = delta;
            deltaTime = delta * timeScale;
            time += deltaTime;
            unscaledTime += delta;

            accumulator += deltaTime;
            while (accumulator >= FixedDeltaTime)
            {
                InFixedTimeStep = true;
                fixedTime += FixedDeltaTime;
                accumulator -= FixedDeltaTime;
                InFixedTimeStep = false;
                // 此处你可以触发 FixedUpdate 的委托或调度器
            }
        }
        /// <summary>
        /// 重置
        /// </summary>
        public static void Reset()
        {
            time = 0f;
            deltaTime = 0f;
            unscaledTime = 0f;
            fixedTime = 0f;
            accumulator = 0f;
        }
    }
    /// <summary>
    /// 时间管理器，提供时间相关功能和属性。
    /// </summary>
    public static class TimeManager
    {
        /// <summary>
        /// 时间
        /// </summary>
        public static float time { get; private set; }
        /// <summary>
        /// 时间间隔
        /// </summary>
        public static float deltaTime { get; private set; }
        /// <summary>
        /// 时间尺度
        /// </summary>
        public static float timeScale { get; set; } = 1f;

        // 更新方法，每时刻调用
        /// <summary>
        /// 更新时间状态的方法，应在每时刻调用。
        /// </summary>
        /// <param name="delta">间隔</param>
        [Chinese(ChinesePhase.Update)]
        public static void Update(float delta)
        {
            deltaTime = delta * timeScale;
            time += deltaTime;
        }
    }
    /// <summary>
    /// 时间调度器，允许在指定延迟后执行回调函数，支持重复执行和使用未缩放时间。
    /// </summary>
    public static class TimeScheduler
    {
        private class Task
        {
            public float Delay;
            public float Elapsed;
            public int RepeatCount; // -1 = 无限
            public float Interval;
            public required Action Callback;
            public bool UseUnscaled;
            public bool IsRepeating;
        }

        private static readonly List<Task> tasks = new();
        private static readonly List<Task> temp = new(); // 用于避免遍历时修改集合
        /// <summary>
        /// 调度
        /// </summary>
        /// <param name="delay">延迟时间</param>
        /// <param name="callback">回调动作</param>
        /// <param name="useUnscaledTime">是否使用</param>
        public static void Schedule(float delay, Action callback, bool useUnscaledTime = false)
        {
            if (callback == null || delay < 0f) return;
            tasks.Add(new Task
            {
                Delay = delay,
                Callback = callback,
                UseUnscaled = useUnscaledTime,
                RepeatCount = 1
            });
        }
        /// <summary>
        /// 定时重复
        /// </summary>
        /// <param name="interval">浮点数间隔</param>
        /// <param name="callback">动作回调</param>
        /// <param name="repeatCount">重复次数</param>
        /// <param name="useUnscaledTime">是否使用未缩放时间</param>
        public static void ScheduleRepeat(float interval, Action callback, int repeatCount = -1, bool useUnscaledTime = false)
        {
            if (callback == null || interval <= 0f) return;
            tasks.Add(new Task
            {
                Delay = interval,
                Interval = interval,
                Callback = callback,
                UseUnscaled = useUnscaledTime,
                IsRepeating = true,
                RepeatCount = repeatCount
            });
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="delta">间隔</param>
        [Chinese(ChinesePhase.onUpdate)]
        public static void Update(float delta)
        {
            if (tasks.Count == 0) return;
            temp.Clear();
            temp.AddRange(tasks);

            foreach (var task in temp)
            {
                float dt = task.UseUnscaled ? Time.UnscaledDeltaTime : Time.DeltaTime;
                task.Elapsed += dt;

                if (task.Elapsed >= task.Delay)
                {
                    task.Callback?.Invoke();
                    task.Elapsed = 0f;

                    if (task.IsRepeating)
                    {
                        if (task.RepeatCount > 0)
                        {
                            task.RepeatCount--;
                            if (task.RepeatCount == 0)
                                tasks.Remove(task);
                        }
                    }
                    else
                    {
                        tasks.Remove(task);
                    }
                }
            }
        }
        /// <summary>
        /// 清除时间调度器中的所有任务。
        /// </summary>
        [Removal(RemovalFlags.Return)]
        public static void Clear()
        {
            tasks.Clear();
        }
    }
}
