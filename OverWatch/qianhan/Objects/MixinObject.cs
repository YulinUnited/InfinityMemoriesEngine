using InfiniteMemoriesEngine.OverWatch.qianhan.Bytes;
using Boolean = InfinityMemoriesEngine.OverWatch.qianhan.Util.Boolean;

namespace InfinityMemoriesEngine.OverWatch.qianhan.Objects
{
    public unsafe partial class MixinObject
    {
        public int64_t mixin_id;

        public Boolean isRemove;

        public Boolean isAlive;

        public nuint mixin_data;

        public Boolean isMixinObject;
    }
}
