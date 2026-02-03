namespace InfinityMemoriesEngine.OverWatch.qianhan.Entities.ai
{
    public class EntityAIAttack : EntityAIBase
    {
        private readonly Entity attacker;
        private readonly Entity target;
        private readonly float attackRange;

        public EntityAIAttack(Entity attacker, Entity target, float attackRange)
        {
            this.attacker = attacker;
            this.target = target;
            this.attackRange = attackRange;
            setMutexBits(2); // 互斥位：攻击与移动冲突
        }

        public override bool shouldExecute()
        {
            if (target == null) return false;
            return (attacker.Position - target.Position).LengthSquared() <= attackRange * attackRange;
        }

        public override void updateTask()
        {
            // 攻击逻辑（示例：打印）
            Console.WriteLine($"{attacker.Name} attacks {target.Name}");
        }
    }
}
