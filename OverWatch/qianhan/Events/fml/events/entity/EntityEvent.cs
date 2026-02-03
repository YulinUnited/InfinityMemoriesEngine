using InfinityMemoriesEngine.OverWatch.qianhan.Entities;

namespace InfinityMemoriesEngine.OverWatch.qianhan.Events.fml.events.entity
{
    public class EntityEvent : Event
    {
        public EntityEvent(Entity entity)
        {
            this.entity = entity ?? throw new ArgumentNullException(nameof(entity));
        }
        public Entity getEntity()
        {
            return entity;
        }
        public override bool getCanceled() => isCanceled;
    }
    public class EntityConstructing : EntityEvent
    {
        public EntityConstructing(Entity entity) : base(entity)
        {

        }
    }
    public class CanUpdate : EntityEvent
    {
        private bool canUpdate = false;
        public bool getCanUpdate() { return canUpdate; }
        public CanUpdate(Entity entity) : base(entity)
        {
        }
        public void setCanUpdate(bool v)
        {
            this.canUpdate = v;
        }
    }
    public class EnteringChunk : EntityEvent
    {
        private int newChunkX;
        private int newChunkZ;
        private int oldChunkX;
        private int oldChunkZ;
        public EnteringChunk(Entity entity, int newChunkX, int newChunkZ, int oldChunkX, int oldChunkZ) : base(entity)
        {
            this.newChunkX = newChunkX;
            this.newChunkZ = newChunkZ;
            this.oldChunkX = oldChunkX;
            this.oldChunkZ = oldChunkZ;
        }
        public int getNewChunkX()
        {
            return newChunkX;
        }
        public void setNewChunkX(int newChunkX)
        {
            this.newChunkX = newChunkX;
        }
        public int getNewChunkZ() { return newChunkZ; }
        public void setNewChunkZ(int newChunkZ) { this.newChunkZ = newChunkZ; }
        public int getOldChunkX() { return oldChunkX; }
        public void setOldChunkX(int oldChunkX) { this.oldChunkX = oldChunkX; }
        public int getOldChunkZ() { return oldChunkZ; }
        public void setOldChunkZ(int oldChunkZ) { this.oldChunkZ = oldChunkZ; }
    }
}
