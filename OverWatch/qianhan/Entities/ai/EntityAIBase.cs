namespace InfinityMemoriesEngine.OverWatch.qianhan.Entities.ai
{
    public abstract class EntityAIBase
    {
        private int mutexBits;
        public abstract bool shouldExecute();
        public virtual bool shouldContinueExecuting()
        {
            return shouldExecute();
        }
        public virtual void startExecuting()
        {
            mutexBits = 1;
        }
        public virtual void resetTask()
        {
            mutexBits = 0;
        }
        public virtual void updateTask()
        {

        }
        public virtual void setMutexBits(int bits)
        {
            mutexBits = bits;
        }
        public virtual int getMutexBits()
        {
            return mutexBits;
        }
    }
}
