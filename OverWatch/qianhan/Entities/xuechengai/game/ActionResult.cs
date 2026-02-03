namespace InfinityMemoriesEngine.OverWatch.qianhan.Entities.xuechengai.game
{
    public class ActionResult<T>
    {
        private EnumActionResult type;
        private T result;

        public ActionResult(EnumActionResult type, T result)
        {
            this.type = type;
            this.result = result;
        }
        public EnumActionResult getType()
        {
            return type;
        }

        public T getResult()
        {
            return result;
        }

        public static ActionResult<T> newResult(EnumActionResult result, T value)
        {
            return new ActionResult<T>(result, value);
        }
    }
    public enum EnumActionResult
    {
        SUCCESS,
        PASS,
        FAIL
    }
}
