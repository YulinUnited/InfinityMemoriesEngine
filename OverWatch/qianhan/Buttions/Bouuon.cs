using InfinityMemoriesEngine.OverWatch.qianhan.Events;
using InfinityMemoriesEngine.OverWatch.qianhan.Events.fml.common.eventhandler;
using InfinityMemoriesEngine.OverWatch.qianhan.Log.lang;

namespace InfinityMemoriesEngine.OverWatch.qianhan.Buttions
{
    public class Buttons : Button
    {
        public readonly ButtonClickedEvent onClick = new();
        private ButtonState state = ButtonState.Normal;
        public int X { get; set; }
        public int Y { get; set; }
        public new int Width { get; set; } = 120;
        public new int Height { get; set; } = 40;
        public new string Text { get; set; }
        public bool Interactable => state != ButtonState.Disabled;

        public Buttons(string text)
        {
            Text = text;
        }

        public virtual void Click()
        {
            if (!Interactable) return;

            Console.WriteLine($"[Button: {Text}] Clicked");
            onClick.Invoke();
            SetState(ButtonState.Pressed);
        }

        public virtual void SetState(ButtonState newState)
        {
            if (state == newState) return;
            state = newState;
            OnStateChanged?.Invoke(state);
            Console.WriteLine($"[Button: {Text}] State: {newState}");
        }

        public virtual void SimulateHover() => SetState(ButtonState.Hovered);
        public virtual void SimulateNormal() => SetState(ButtonState.Normal);
        public virtual void SimulateDisable() => SetState(ButtonState.Disabled);
        public Action<ButtonState> OnStateChanged;

        public virtual void SimulateEnable()
        {
            if (state == ButtonState.Disabled)
                SetState(ButtonState.Normal);
        }

        public enum ButtonState
        {
            Normal,
            Hovered,
            Pressed,
            Disabled
        }
        public ButtonState GetState() => state;
    }

    /// <summary>
    /// 事件就要有事件的样子，Cancelable表示可以被取消，嗯，我有一个邪恶的想法【坏笑】
    /// </summary>
    [Cancelable]
    public class ButtonClickedEvent : Event
    {
        private readonly List<Action> listeners = new();


        // 添加监听器
        public virtual void AddListener(Action callback)
        {
            if (callback != null && !listeners.Contains(callback))
                listeners.Add(callback);
        }

        // 移除监听器
        public virtual void RemoveListener(Action callback)
        {
            if (callback != null)
                listeners.Remove(callback);
        }

        // 触发事件
        public virtual void Invoke()
        {
            if (isCanceled)
            {
                Debug.LogWarning("按钮事件已被取消，请检查是否在逻辑内套入了setCanceled(true)？");
                return; // 如果事件被取消，直接返回
            }

            foreach (var callback in listeners)
            {
                callback?.Invoke();
            }
        }

        // 取消事件
        public virtual void Cancel()
        {
            isCanceled = true;
        }

        // 清除所有监听器
        public virtual void ClearAll()
        {
            listeners.Clear();
        }
    }
}
