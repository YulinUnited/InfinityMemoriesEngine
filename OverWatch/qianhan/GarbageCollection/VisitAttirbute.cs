namespace InfinityMemoriesEngine.OverWatch.qianhan.GarbageCollection
{
    [AttributeUsage(AttributeTargets.All)]
    internal class VisitAttribute : Attribute
    {
        private VisitType VisitType;
        public VisitAttribute(VisitType visitType)
        {
            VisitType = visitType;
        }
        public VisitType Type => VisitType;
    }
    public enum VisitType
    {
        None = 0,
        Internal = 1,
        Public = 2,
        Protected = 4,
        Private = 8,
    }
}
