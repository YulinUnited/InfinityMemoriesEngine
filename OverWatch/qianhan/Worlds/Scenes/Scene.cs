using InfinityMemoriesEngine.OverWatch.qianhan.App;
using InfinityMemoriesEngine.OverWatch.qianhan.Entities;
using InfinityMemoriesEngine.OverWatch.qianhan.Log.lang;

namespace InfinityMemoriesEngine.OverWatch.qianhan.Worlds.Scenes
{
    [WorldPriority(WorldTypePriority.Scene)]
    public class Scene : World
    {
        public Scene()
        {
            id = 01;
        }
        /// <summary>
        /// 加载场景，由使用者实现
        /// </summary>
        /// <param name="scene"></param>
        public override void LoadScene(Scene scene)
        {

        }
        /// <summary>
        /// 允许重载，当场景被加载时调用，由基类World统一管理，用于加载必要逻辑
        /// </summary>
        public override void onSceneAwake()
        {
            Debug.Log($"id是{id}");
        }
        /// <summary>
        /// 场景开始时调用，由使用者实现
        /// </summary>
        public override void onSceneStart()
        {

        }
        /// <summary>
        /// 场景更新，由使用者实现
        /// </summary>
        public override void onSceneUpdate()
        {

        }
        /// <summary>
        /// 移除场景，由使用者实现
        /// </summary>
        /// <param name="scene"></param>
        public override void removeScene(Scene scene)
        {

        }
        /// <summary>
        /// 添加实体，由使用者实现
        /// </summary>
        /// <param name="entity"></param>
        public override void AddEntity(Entity entity)
        {

        }
    }
}
