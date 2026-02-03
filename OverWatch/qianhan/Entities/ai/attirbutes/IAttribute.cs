namespace InfinityMemoriesEngine.OverWatch.qianhan.Entities.ai.attirbutes
{
    public interface IAttribute
    {
        string getName();
        double clampValue(double value);
        double getDefaultValue();
        bool getShouldWatch();
        IAttribute getParent();
        string Name { get; }
    }
}
