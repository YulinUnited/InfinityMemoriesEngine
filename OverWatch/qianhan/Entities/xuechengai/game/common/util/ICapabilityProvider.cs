namespace InfinityMemoriesEngine.OverWatch.qianhan.Entities.xuechengai.game.common.util
{
    public interface ICapabilityProvider
    {
        bool hasCapability(Capability<object> capability, Enum enumActionResult);
        T getCapability<T>(Capability<T> capability, Enum @enum);
    }
}
