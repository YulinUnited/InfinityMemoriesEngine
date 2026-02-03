namespace InfinityMemoriesEngine.OverWatch.qianhan.Inputs.InputManagers
{
    public interface IInputEventHandler
    {
        void onKeyPressed(KeyCode key);
        void onKeyReleased(KeyCode key);
        void onMouseButtonPressed(int button);
        void onMouseButtonReleased(int button);
    }
}
