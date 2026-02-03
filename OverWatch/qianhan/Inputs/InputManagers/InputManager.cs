namespace InfinityMemoriesEngine.OverWatch.qianhan.Inputs.InputManagers
{
    public abstract class InputManager
    {
        public abstract void setEventHandler(IInputEventHandler handler);
        public abstract void Bind(string name, KeyCode key);
        public abstract void Update(); // 由引擎生命周期调度

        public abstract bool get(string name);       // 是否按下（持续）
        public abstract bool getDown(string name);   // 是否刚按下
        public abstract bool getUp(string name);     // 是否刚松开
        public abstract bool getMouse(int button);
        public abstract bool getMouseDown(int button);
        public abstract bool getMouseUp(int button);
        public abstract bool getKeyDown(string name);
        public abstract bool getKeyUp(string name);
        // 获取是否按下（持续按下）
        public abstract bool IsKeyPressed(KeyCode key);

        // 获取是否刚按下
        public abstract bool IsKeyDown(KeyCode key);

        // 获取是否刚松开
        public abstract bool IsKeyUp(KeyCode key);

        // 获取鼠标按键状态
        public abstract bool IsMouseButtonPressed(int button);

        // 获取鼠标按键刚按下的状态
        public abstract bool IsMouseButtonDown(int button);

        // 获取鼠标按键刚松开的状态
        public abstract bool IsMouseButtonUp(int button);
    }
}
