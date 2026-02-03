namespace InfinityMemoriesEngine.OverWatch.qianhan.Util
{
    /// <summary>
    /// 移动方式枚举
    /// </summary>
    public enum MoveDirection:int
    {
        Node,
        Walk,
        Run,
        Trot,
        Jump
    }

    public static class Move
    {
        public static bool TheLeft;
        public static bool TheRight;
        public static bool TheUp;
        public static bool TheDown;

        public static MoveDirection currentMoveDirection = MoveDirection.Node;
    }
}
