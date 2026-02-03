

using InfinityMemoriesEngine.OverWatch.qianhan.Start;
using InfinityMemoriesEngine.OverWatch.qianhan.Times;
using InfinityMemoriesEngine.OverWatch.qianhan.Worlds;

namespace InfinityMemoriesEngine.OverWatch.qianhan.Entities.items
{
    /// <summary>
    /// 表示掉落物的实体
    /// </summary>
    public class ItemEntity : Entity
    {
        public string name;  // 实体名称
        private float lifetime;  // 掉落物的生命周期
        private float maxLifetime = 300f;  // 最大生存时间，例如 30 秒
        private bool dead;  // 是否已死亡
        private World World;  // 世界对象
        private List<ItemEntity> entities = new List<ItemEntity>();  // 掉落物实体列表
        public ItemEntity() : base()
        {
            // 默认构造函数
            this.name = string.Empty;  // 初始化名称为空
            this.lifetime = 0f;  // 初始化生命周期为 0
            this.maxLifetime = 300;  // 设置最大生存时间
            this.dead = false;  // 初始化死亡状态为 false
        }
        public ItemEntity(string name, long id, double posX, double posY, double posZ, World world)
        {
            this.name = name;  // 设置掉落物名称
            this.lifetime = 0f;  // 初始化生命周期
            this.maxLifetime = 300;  // 设置最大生存时间
            this.dead = false;  // 初始化死亡状态
            this.World = world;  // 设置世界对象
        }

        public override void onEntityStart()
        {
            dead = false;
            lifetime = 0f;
            name = string.Empty;
        }

        // 更新方法，检查掉落物的生存时间
        [Chinese(ChinesePhase.Update)]
        public virtual void onItemUpdate()
        {
            if (dead)
            {
                // 如果物品已死亡，移除实体
                World.removeEntity(this);  // 使用空安全操作符
            }
            else
            {
                // 增加生存时间
                lifetime += Time.deltaTime;  // 使用时间增量来增加生存时间

                // 如果超过最大生存时间，标记为死亡
                if (lifetime > maxLifetime)
                {
                    dead = true;
                    World.removeEntity(this);  // 移除实体
                }
            }
        }
        public virtual List<ItemEntity> AddItemEntityWorld()
        {
            if (World == null) return new();

            var addedEntities = new List<ItemEntity>();

            foreach (var entity in entities)
            {
                if (entity.World == World)
                {
                    World.DropList.Add(entity); // 加入世界中的掉落物列表（假设你有这个字段）
                    addedEntities.Add(entity);
                }
            }

            return addedEntities;
        }
        // 如果物品已死亡，移除
        public void checkAndRemoveIfDead()
        {
            if (dead)
            {
                setDeath();
            }
        }
    }
}
