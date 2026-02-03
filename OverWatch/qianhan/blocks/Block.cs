using InfinityMemoriesEngine.OverWatch.qianhan.Start;

namespace InfinityMemoriesEngine.OverWatch.qianhan.blocks
{
    public abstract class Block
    {
        public double posX;
        public double posY;
        public double posZ;
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }

        public BlockType blockType { get; protected set; }

        public Block Parent { get; private set; }
        public List<Block> Children { get; private set; } = new List<Block>();

        public Dictionary<string, object> Metadata { get; private set; } = new Dictionary<string, object>();

        public virtual void Execute()
        {

        }
        public virtual void Render()
        {

        }
        public void AddChild(Block child)
        {
            child.Parent = this;
            Children.Add(child);
        }

        public void RemoveChild(Block child)
        {
            if (Children.Contains(child))
            {
                Children.Remove(child);
            }
        }
        public enum BlockType
        {
            Structure,//场景构建块
            Logic,//逻辑/方法块
            Data//数据/配置块
        }
        /// <summary>
        /// 更新，默认不更新，需重写
        /// </summary>
        /// <returns></returns>
        [Chinese(ChinesePhase.Update)]
        public virtual bool Update()
        {
            return false;
        }

        public double getX()
        {
            return posX;
        }

        public double getY()
        {
            return posY;
        }

        public double getZ()
        {
            return posZ;
        }
    }
}
