using InfinityMemoriesEngine.OverWatch.qianhan.Entities;

namespace InfinityMemoriesEngine.OverWatch.qianhan.MonoBehaviours
{
    public class Component : Objects.Object
    {
        /// <summary>
        /// 锁定键，用于标识组件的唯一性
        /// </summary>
        public int Key { get; set; } = 0;//初始值为0
        /// <summary>
        /// 组件名称，默认值为"Component"
        /// </summary>
        public string Name { get; set; } = "Component";
        /// <summary>
        /// 组件描述，默认值为"This is a component."
        /// </summary>
        public string Description { get; set; } = "This is a component.";
        /// <summary>
        /// 是否激活组件，默认值为true
        /// </summary>
        public bool isOpent { get; set; } = false;
        /// <summary>
        /// 实体引用绑定
        /// </summary>
        public Entity Owner { get; internal set; } // 绑定的实体引用
        /// <summary>
        /// 当添加时
        /// </summary>
        public virtual void onAdd() { }
        /// <summary>
        /// 当移除时
        /// </summary>
        public virtual void onRemove() { }
        /// <summary>
        /// 当组件更新时
        /// </summary>
        public virtual void onComponentUpdate() { }
        /// <summary>
        /// 组件构造函数1
        /// </summary>
        /// <param name="size">大小</param>
        /// <param name="name">名称</param>
        /// <param name="description">描述</param>
        /// <param name="active">状态</param>
        /// <param name="remove">删除状态</param>
        /// <param name="entity">实体对象</param>
        public Component(int size, string name, string description, bool active, bool remove, Entity entity) : base(size)
        {
            this.Name = name;
            this.Description = description;
            this.Active = active;
            this.isRemove = remove;
            this.Owner = entity;
        }
        /// <summary>
        /// 组件构造函数2
        /// </summary>
        /// <param name="key">id</param>
        /// <param name="name">名称</param>
        /// <param name="description">描述</param>
        /// <param name="isOpent">激活</param>
        /// <param name="owner">实体</param>
        public Component(int key, string name, string description, bool isOpent, Entity owner) : base(key)
        {
            Key = key;
            Name = name;
            Description = description;
            this.isOpent = isOpent;
            Owner = owner;
        }
        public Component()
        {
            Key = 0;
            Name = "Component";
            Description = "This is a component.";
            isOpent = false;
            Owner = null; // 默认没有绑定实体
        }
        public Component(int size) : base(size)
        {
            Key = 0;
            Name = "Component";
            Description = "This is a component.";
            isOpent = false;
            Owner = null; // 默认没有绑定实体
        }
    }
}
