using System.Runtime.InteropServices;
using InfinityMemoriesEngine.OverWatch.qianhan.Events;
using InfinityMemoriesEngine.OverWatch.qianhan.Inputs.InputManagers;
using InfinityMemoriesEngine.OverWatch.qianhan.Start;
using InfinityMemoriesEngine.OverWatch.qianhan.Util;
using static InfinityMemoriesEngine.OverWatch.qianhan.Numbers.Mathf;
using KeyCode = InfinityMemoriesEngine.OverWatch.qianhan.Events.util.KeyCode;

namespace InfinityMemoriesEngine.OverWatch.qianhan.Inputs
{
    public class Input : InputManager
    {
        [DllImport("user32.dll")]
        private static extern short GetAsyncKeyState(int key);
        private readonly HashSet<int> currentMouseButtons = new HashSet<int>();
        private readonly HashSet<int> lastMouseButtons = new HashSet<int>();
        private Dictionary<string, InputManagers.KeyCode> keyValues = new Dictionary<string, InputManagers.KeyCode>();
        private HashSet<InputManagers.KeyCode> pressedKeys = new HashSet<InputManagers.KeyCode>();
        private HashSet<InputManagers.KeyCode> justPressedKeys = new HashSet<InputManagers.KeyCode>();
        private HashSet<InputManagers.KeyCode> justReleasedKeys = new HashSet<InputManagers.KeyCode>();
        private IInputEventHandler eventHandler;
        internal Vector2 mousePosition;
        private static readonly Dictionary<int, int> mouseButtonToVK = new()
        {
            { 0, 0x01 }, // Left
            { 1, 0x02 }, // Right
            { 2, 0x04 }, // Middle
            { 3, 0x05 }, // XButton1
            { 4, 0x06 }, // XButton2
            //预留扩展位
        };
        [DllImport("user32.dll")]
        private static extern bool GetCursorPos(out POINT lpPoint);
        private string _text = string.Empty;
        private readonly List<Action<string>> _listeners = new();
        public InputFieldEvent onEndEdit { get; set; }
        public string Placeholder { get; set; } = "Enter text...";
        public bool IsPasswordMode { get; set; } = false;
        public bool IsRememberMode { get; set; } = false;
        public Input(string texts, List<Action<string>> listeners, InputFieldEvent onEndEdit, string placeholder, bool isPasswordMode, bool isRememberMode, string text)
        {
            Text = texts;
            _listeners = listeners;
            this.onEndEdit = onEndEdit;
            Placeholder = placeholder;
            IsPasswordMode = isPasswordMode;
            IsRememberMode = isRememberMode;
            Text = text;
        }
        internal Input()
        {
        }
        public string Text
        {
            get => _text;
            set
            {
                if (_text != value)
                {
                    _text = value;
                    TriggerValueChanged(value);
                }
            }
        }
        public virtual void AddListeners(Action<string> callback)
        {
            if (callback != null && !_listeners.Contains(callback))
                _listeners.Add(callback);
        }

        public virtual void RemoveListener(Action<string> callback)
        {
            if (callback != null)
                _listeners.Remove(callback);
        }

        public virtual void TriggerValueChanged(string newText)
        {
            var evt = new InputValueChangedEvent(newText);
            if (!evt.getCanceled()) // 如果事件被取消，就不继续执行
            {
                foreach (var listener in _listeners)
                    listener?.Invoke(newText);
            }
        }

        public virtual void Display()
        {
            if (string.IsNullOrEmpty(_text))
            {
                Console.WriteLine($"[InputField] {Placeholder}");
            }
            else if (IsPasswordMode)
            {
                Console.WriteLine($"[InputField] {"".PadLeft(_text.Length, '*')}");
            }
            else
            {
                Console.WriteLine($"[InputField] {_text}");
            }
        }

        public virtual void SimulateInput(string input)
        {
            Text = input;

            if (IsRememberMode)
            {
                RememberManager.Save(input); // 假设你有自己的记忆系统，这里只是接口留口子
            }
        }

        public virtual void LoadRemembered()
        {
            if (IsRememberMode)
            {
                var remembered = RememberManager.Load();
                if (!string.IsNullOrEmpty(remembered))
                {
                    Text = remembered;
                    Console.WriteLine($"[InputField] Loaded remembered text.");
                }
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;
        }

        public void UpdateMousePosition()
        {
            if (GetCursorPos(out POINT p))
            {
                mousePosition = new Vector2(p.X, p.Y);
            }
        }

        public override void setEventHandler(IInputEventHandler handler)
        {
            this.eventHandler = handler;
        }
        public override void Bind(string name, InputManagers.KeyCode key)
        {
            keyValues[name] = key;
        }
        [Chinese(ChinesePhase.Update)]
        public override void Update()
        {
            justPressedKeys.Clear();
            justReleasedKeys.Clear();
            foreach (var key in keyValues.Values)
            {
                if (IsKeyPressed(key))
                {
                    if (!pressedKeys.Contains(key))
                    {
                        justPressedKeys.Add(key);
                        pressedKeys.Add(key);
                        eventHandler?.onKeyPressed(key);
                    }
                }
                else
                {
                    if (pressedKeys.Contains(key))
                    {
                        justReleasedKeys.Add(key);
                        pressedKeys.Remove(key);
                        eventHandler?.onKeyReleased(key);
                    }
                }
            }
            lastMouseButtons.Clear();
            foreach (var btn in currentMouseButtons)
                lastMouseButtons.Add(btn);

            currentMouseButtons.Clear();
            foreach (var kv in mouseButtonToVK)
            {
                if ((GetAsyncKeyState(kv.Value) & 0x8000) != 0)
                {
                    currentMouseButtons.Add(kv.Key);
                }
            }
            UpdateMousePosition();
        }
        public override bool get(string name)
        {
            if (keyValues.TryGetValue(name, out InputManagers.KeyCode key))
            {
                return IsKeyPressed(key);
            }
            return false;
        }
        public override bool getKeyDown(string name)
        {
            if (keyValues.TryGetValue(name, out var key))
            {
                return justPressedKeys.Contains(key);
            }
            return false;
        }

        public override bool getKeyUp(string name)
        {
            if (keyValues.TryGetValue(name, out var key))
            {
                return justReleasedKeys.Contains(key);
            }
            return false;
        }
        public override bool getMouse(int button)
        {
            return IsMouseButtonPressed(button);
        }
        public override bool getMouseDown(int button)
        {
            return IsMouseButtonDown(button);
        }
        public override bool getMouseUp(int button)
        {
            return IsMouseButtonUp(button);
        }
        public override bool IsKeyPressed(InputManagers.KeyCode key)
        {
            //暂时留空
            return false;
        }
        public override bool IsKeyDown(InputManagers.KeyCode key)
        {
            return justPressedKeys.Contains(key);
        }
        public override bool IsKeyUp(InputManagers.KeyCode key)
        {
            return justReleasedKeys.Contains(key);
        }
        public override bool IsMouseButtonPressed(int button)
        {
            return currentMouseButtons.Contains(button);
        }
        public override bool IsMouseButtonDown(int button)
        {
            return currentMouseButtons.Contains(button) && !lastMouseButtons.Contains(button);
        }
        public override bool IsMouseButtonUp(int button)
        {
            return !currentMouseButtons.Contains(button) && lastMouseButtons.Contains(button);
        }
        public override bool getDown(string name)
        {
            //暂时留空
            return false;
        }
        public override bool getUp(string name)
        {
            //暂时留空
            return false;
        }
        public static bool getKey()
        {
            return new InputManagers.KeyCode() != InputManagers.KeyCode.None;
        }
    }
    public static class RememberManager
    {
        private static string _remembered = string.Empty;

        public static void Save(string value)
        {
            _remembered = value;
            Console.WriteLine($"[RememberManager] Value remembered.");
        }

        public static string Load()
        {
            return _remembered;
        }

        public static void Clear()
        {
            _remembered = string.Empty;
        }
    }
    public class InputFieldEvent : Event
    {
        private readonly List<Action<string>> listeners = new();
        public virtual void AddListener(Action<string> executeCommand)
        {
            if (executeCommand != null)
            {
                listeners.Add(executeCommand);
            }
        }
        public virtual void RemoveListener(Action<string> executeCommand)
        {
            listeners.Remove(executeCommand);
        }

        public virtual void Invoke(string value)
        {
            if (getGlobalMarkEvent())
            {
                foreach (var listener in listeners)
                {
                    listener?.Invoke(value);
                }
            }
        }
    }
    public class InputValueChangedEvent : Event
    {
        public string Value { get; private set; }

        public InputValueChangedEvent(string value)
        {
            Value = value;
        }
    }
}

namespace InfiniteMemoriesEngine.OverWatch.qianhan.Inputs.InputManagerine
{
    /// <summary>
    /// 按键事件结构体
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Size = 5)]
    public struct InputEvent
    {
        /// <summary>
        /// 全局事件，对齐C中的Boolean函数
        /// </summary>
        public boolean isGlobalMark;
        /// <summary>
        /// 折中方案
        /// </summary>
        public int reserved; // 保留字段，确保结构体大小对齐

        /// <summary>
        /// 按键事件转换方法，将C提供的结构体转换为C#方法，保证平移量为1字节
        /// </summary>
        /// <param name="input">事件</param>
        /// <returns>转换方式</returns>
        public static InputEvent From(InfinityMemoriesEngine.OverWatch.qianhan.Events.util.InputEvent input)
        {
            return new InputEvent
            {
                isGlobalMark = input.isGlobalMark,
            };
        }
        unsafe void Example(nint ptr)
        {
            InputEvent* inputEvent = (InputEvent*)ptr;
            bool globalMark = inputEvent->isGlobalMark;
        }
    }
    /// <summary>
    /// 以C写的Input.dll为主要目标的输入类
    /// </summary>
    public unsafe static class Input
    {
        static readonly InputEvent @event;
        private static KeyCode Code = (KeyCode)@event.reserved;
        private const string DllName = "Input";

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void BindInputEvent(InputEvent* ptr);
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void UpdateInputState();
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetKey(KeyCode key);
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetKeyDown(KeyCode key);
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetKeyUp(KeyCode key);
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetMouseButton(nint x, nint y);

    }
}
