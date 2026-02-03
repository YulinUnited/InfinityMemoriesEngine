using InfinityMemoriesEngine.OverWatch.qianhan.Entities.xuechengai.game.nbt;
using InfinityMemoriesEngine.OverWatch.qianhan.Events;
using InfinityMemoriesEngine.OverWatch.qianhan.Events.fml.common.eventhandler;
using InfinityMemoriesEngine.OverWatch.qianhan.Events.util;
using InfinityMemoriesEngine.OverWatch.qianhan.Hooks;
using InfinityMemoriesEngine.OverWatch.qianhan.Inputs;
using InfinityMemoriesEngine.OverWatch.qianhan.Objects;

namespace InfinityMemoriesEngine.OverWatch.qianhan.Entities.xuechengai.game.common.util
{
    public interface INBTSerializable<T>
    {
        T serializeNBT();
        void deserializeNBT(T nbt);
        public class A : NBTBase
        {
            public T data;
            public A(T data)
            {
                this.data = data;
            }
        }
    }
    public class ImportKey : MainObject
    {
        public string keys;
        private KeyCode KeyCode;
        private Input Input;
        private Event @event;
        private bool isInit = false;
        private Entity entityInputKey;
        private Entity entityKeyCore;
        public ImportKey(string key)
        {
            keys = key;
            KeyCode = (KeyCode)System.Enum.Parse(typeof(KeyCode), keys);
            @event = new Event();
            isInit = true;
            entityInputKey = new Entity();
            entityKeyCore = new Entity();
            Input = new Input();
        }
        public void setKey(string key)
        {
            keys = key;
        }
        [Subscribe]
        public void onInputEvent(InputEvent eventArgs)
        {
            if (eventArgs.isInputType == Input.getKeyDown(keys))
            {
                if (eventArgs.key.ToString() == keys)
                {
                    //触发按键事件
                    if ((!ForgeHooks.onInputKey(KeyCode, isInit, entityInputKey, entityKeyCore)) && @event.isGlobalMarEvent()) return;
                    else
                    {
                        Input.Update();
                    }
                }
            }
        }
    }
}
