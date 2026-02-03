using InfinityMemoriesEngine.OverWatch.qianhan.Entities;
using InfinityMemoriesEngine.OverWatch.qianhan.Events.fml.common.eventhandler;

namespace InfinityMemoriesEngine.OverWatch.qianhan.Events.util
{
    [Cancelable]
    public class InputEvent : Event
    {
        private Entity entityInputKey;
        private Entity entityBeingKey;
        private bool inputType;
        public KeyCode key;

        public InputEvent(bool inputType, KeyCode key, Entity entityKey, Entity entityKeyCore)
        {
            this.inputType = inputType;
            this.key = key;
            this.entityBeingKey = entityKey;
            this.entityInputKey = entityKeyCore;
        }

        public InputEvent(KeyCode keyCode, bool isCancelable1, Entity entity, Entity vimcn)
        {
            this.key = keyCode;
            this.inputType = isCancelable1;
            this.entityInputKey = entity;
            this.entityBeingKey = vimcn;
        }

        public bool isInputType { get { return inputType; } }
        public bool isDisPressed() { return !inputType; }
        public Entity getEntityInputKey() { return entityInputKey; }
        public Entity getEntityBeingKey() { return entityBeingKey; }
    }
}
