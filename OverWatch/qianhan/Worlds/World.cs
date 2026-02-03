using System.Runtime.InteropServices;
using InfinityMemoriesEngine.OverWatch.qianhan.App;
using InfinityMemoriesEngine.OverWatch.qianhan.Entities;
using InfinityMemoriesEngine.OverWatch.qianhan.Entities.items;
using InfinityMemoriesEngine.OverWatch.qianhan.Entities.PlayerEntity;
using InfinityMemoriesEngine.OverWatch.qianhan.Enums;
using InfinityMemoriesEngine.OverWatch.qianhan.InfinityMemoriesEngine.MainManager;
using InfinityMemoriesEngine.OverWatch.qianhan.Items;
using InfinityMemoriesEngine.OverWatch.qianhan.profiler;
using InfinityMemoriesEngine.OverWatch.qianhan.Start;
using InfinityMemoriesEngine.OverWatch.qianhan.Util;
using InfinityMemoriesEngine.OverWatch.qianhan.Worlds.Scenes;
using Debug = InfinityMemoriesEngine.OverWatch.qianhan.Log.lang.Debug;

namespace InfinityMemoriesEngine.OverWatch.qianhan.Worlds
{
    [WorldPriority(WorldTypePriority.World)]
    public abstract class World : IWorld
    {
        public Profiler profiler;

        public bool isRemote;
        private bool isRemove = false;
        internal string Name { set; get; }
        protected long id { set; get; }

        //protected List<Entity>entities=new List<Entity>();
        protected List<EntityPlayer> entities = new List<EntityPlayer>();
        protected Dictionary<EntityLivingBase, Entity> entityList = new Dictionary<EntityLivingBase, Entity>();
        protected List<Item> Items = new List<Item>();
        protected Item items;
        public List<ItemEntity> DropList = new List<ItemEntity>();
        private List<Entity> allEntities = new List<Entity>();
        private Entity entity1 = new Entity();

        public void setActive(bool value)
        {
            isRemove = value;
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct WorldStruct
        {
            public int worldId; // 世界ID
            public int entityCount; // 实体数量
        }
        /// <summary>
        /// 移除实体
        /// </summary>
        /// <param name="entity">指定实体类型</param>
        public virtual void removeEntity(Entity entity)
        {
            if (entity != null)
            {
                if (entity.isBeingRidden())
                {
                    entity.removePassengers();
                }
                if (entity.isRiding())
                {
                    entity.disomuntRidingEntity();
                }
                entity.setDeath();
                if (entity is EntityPlayer)
                {
                    this.entities.Remove((EntityPlayer)entity);
                    entity.setDeath();
                    entity.onDeath(DamageSource.GENERIC);
                }
                if (entity != null)
                {
                    entity.onDeath(DamageSource.GENERIC);
                    entity.setDropItemsWhenDead(false);
                    if (entity is EntityPlayer player)
                    {
                        this.entities.Remove(player);
                    }
                }
            }
        }



        /// <summary>
        /// 强制移除实体，由非托管的createObject或者createObjectSafe创建的非托管对象调用，注意，这个方法只能在非托管内存中使用。
        /// 只能移除由非托管内存创建的实体对象。
        /// </summary>
        /// <param name="entity">移除的非托管实体对象</param>
        public unsafe void removeForceEntity(Entity* entity)
        {
            if (entity != null)
            {
                long refCount = MainMemoryManager.getRefCount(entity);
                if (refCount > 0)
                {
                    Debug.Log($"Entity {entity->Name} 还有 {MainMemoryManager.getRefCount(entity)} 个引用，暂时不会移除，请保证已经不存在引用的情况下再尝试移除，目前正在尝试减少引用计数.");
                    for (long i = 0; i < refCount; i++)
                    {
                        MainMemoryManager.decrementRefCount(entity);
                        if (MainMemoryManager.getRefCount(entity) <= 0)
                        {
                            MainMemoryManager.removeObject(entity);
                            break;
                        }
                    }
                }
                MainMemoryManager.removeObject(entity);
                Debug.Log($"Entity{entity->Name}已经被强制移除");
            }
        }
        /// <summary>
        /// 危险移除实体
        /// </summary>
        /// <param name="entityIn"></param>
        public virtual void removeEntityDangerously(Entity entityIn)
        {
            entityIn.setDropItemsWhenDead(false);
            entityIn.setDeath();
            entityIn.onDeath(DamageSource.GENERIC);
            if (entityIn is EntityPlayer)
            {
                this.entities.Remove((EntityPlayer)entityIn);
                entityIn.setDeath();
                entityIn.onDeath(DamageSource.GENERIC);
            }
            GC.Collect();
        }
        /// <summary>
        /// 抽象载入世界
        /// </summary>
        /// <param name="world"></param>
        public virtual void LoadWorld(World world) { }
        /// <summary>
        /// 抽象载入场景
        /// </summary>
        /// <param name="scene"></param>
        public virtual void LoadScene(Scene scene) { }
        /// <summary>
        /// 移除世界，可被重写
        /// </summary>
        /// <param name="world"></param>
        [Removal(RemovalFlags.remove)]
        public virtual void removeWorld(World world)
        {
            if (world != null)
            {
                isRemove = true;

            }
        }
        /// <summary>
        /// 强制删除世界，由非托管的createObject或者createObjectSafe创建的非托管对象调用，注意，这个方法只能在非托管内存中使用。
        /// 请注意，如果没有将局部计数置为0，该方法将自动执行置零操作。
        /// 这个方法会强制删除世界对象，并且会尝试减少引用计。
        /// </summary>
        /// <param name="world">要移除的世界/场景</param>
        public unsafe void removeForceWorld(World* world)
        {
            if (world != null)
            {
                isRemove = true;
                long refCount = MainMemoryManager.getRefCount(world);
                if (refCount > 0)
                {
                    Debug.Log($"World {world->Name} 还有 {MainMemoryManager.getRefCount(world)} 个引用，暂时不会移除，请保证已经不存在引用的情况下再尝试移除，目前正在尝试减少引用计数.");
                    for (long Count = 0; Count < refCount; Count++)
                    {
                        MainMemoryManager.decrementRefCount(world);
                        if (MainMemoryManager.getRefCount(world) <= 0)
                        {
                            MainMemoryManager.removeObject(world);
                            break;
                        }
                    }
                }
                MainMemoryManager.removeObject(world);
                Debug.Log($"World {world->Name} 已经被强制删除.");
            }
        }
        /// <summary>
        /// 可被重写的移除场景
        /// </summary>
        /// <param name="scene"></param>
        [Removal(RemovalFlags.remove)]
        public virtual void removeScene(Scene scene)
        {
            if (scene != null)
            {
                isRemove = true;
                MainMemoryManager.removeAllObjects();
            }
        }
        [Chinese(ChinesePhase.onUpdate)]
        public virtual void onWorldUpdate() { }
        [Chinese(ChinesePhase.onUpdate)]
        public virtual void onSceneUpdate() { }
        [Removal(RemovalFlags.Release)]
        public virtual void onWorldRemove() { }
        [Removal(RemovalFlags.Release)]
        public virtual void onSceneRemove() { }
        [Chinese(ChinesePhase.onAwake)]
        public virtual void onWorldAwake() { }
        [Chinese(ChinesePhase.onAwake)]
        public virtual void onSceneAwake() { }
        [Chinese(ChinesePhase.onStart)]
        public virtual void onWorldStart() { }
        [Chinese(ChinesePhase.onStart)]
        public virtual void onSceneStart() { }

        public virtual void AddEntity(Entity entity) => entities.Add((EntityPlayer)entity);
        public virtual void onWorldEntityUpdate()
        {
            foreach (var entity in entities)
            {
                if (entity.isRemoved)
                {
                    continue;
                }
            }
        }
        public virtual void SpawnEntity(Entity entity)
        {
            if (entity != null)
            {
                if (entity is EntityLivingBase livingBase)
                {
                    entityList.Add((EntityLivingBase)livingBase.getEntity(), entity.getEntity());
                }
                else
                {
                    //如果物品列表不为空，则添加到物品列表中，
                    if (Items.Count > 0 && Items != null)
                    {
                        Items.Add(items);
                    }

                }
            }
        }

        public virtual void SpawnItemEntity(ItemEntity itemEntity)
        {
            if (itemEntity != null)
            {
                DropList.Add(itemEntity);
            }
        }
        public virtual void removeObjectDestroy(string name)
        {
            if (Items.Count > 0 && Items != null)
            {
                Items.RemoveAll(item => item.getItemName() == name);
            }
        }
        public virtual void removeObjects()
        {
            if (Items.Count > 0 && Items != null)
            {
                Items.Clear();
            }
        }
        public virtual void removeAllEntities()
        {
            if (entities.Count > 0 && entities != null)
            {
                entities.Clear();
            }
        }
        public List<T> getEntities<T>(Type classEntity, Predicate<Entity> filter = null) where T : Entity
        {
            var result = new List<T>();

            foreach (var e in allEntities)
            {
                if (!classEntity.IsAssignableFrom(e.GetType()))
                    continue;

                if (filter != null && !filter(e))
                    continue;

                result.Add((T)e);
            }

            return result;
        }
        public EntityPlayer getClosestPlayer(float maxDistance)
        {
            EntityPlayer closest = null;
            float minDistSq = maxDistance * maxDistance;

            foreach (var player in entity1.world.getEntities<EntityPlayer>(typeof(EntityPlayer)))
            {
                float distSq = entity1.getDistanceSq(player);
                if (distSq < minDistSq)
                {
                    minDistSq = distSq;
                    closest = player;
                }
            }

            return closest;
        }

    }
}
