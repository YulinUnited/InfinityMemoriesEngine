using InfinityMemoriesEngine.OverWatch.qianhan.Entities.xuechengai.game.nbt;
using InfinityMemoriesEngine.OverWatch.qianhan.Log.lang;

namespace InfinityMemoriesEngine.OverWatch.qianhan.Entities.xuechengai.game.common.util
{
    public class Capability<T>
    {
        private string name;
        private IStorage<T> storage;
        private T defaultInstance;
        public interface IStorage<T>
        {
            NBTBase writeNBT(Capability<T> capability, T instance, Enum side);
            void readNBT(Capability<T> capability, T instance, Enum side, NBTBase nbt);
        }
        public string getName()
        {
            return name;
        }
        public IStorage<T> getStorage()
        {
            return storage;
        }
        public void readNBT(T instance, NBTBase nbt, Enum side)
        {
            storage.readNBT(this, defaultInstance, side, nbt);
        }
        [annotations.Nullable]
        public NBTBase writeNBT(T instance, Enum side)
        {
            return storage.writeNBT(this, defaultInstance, side);
        }
        public T getDefaultInstance()
        {
            try
            {
                return defaultInstance.GetHashCode() != 0 ? this.defaultInstance : default;
            }
            catch (Exception e)
            {
                Throwable.ReferenceEquals(e, null);
                throw new Exception("Could not create default instance for capability " + name, e);
            }
        }
        Capability(string name, IStorage<T> storage, T defaultInstance)
        {
            this.name = name;
            this.storage = storage;
            this.defaultInstance = defaultInstance;
        }
    }
}
