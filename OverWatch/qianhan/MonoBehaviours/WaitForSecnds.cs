namespace InfinityMemoriesEngine.OverWatch.qianhan.MonoBehaviours
{
    public class WaitForSeconds : IYieldInstruction
    {
        private float waitTime;
        private float elapsedTime;

        public WaitForSeconds(float seconds)
        {
            waitTime = seconds;
            elapsedTime = 0f;
        }

        public bool KeepWaiting(float deltaTime)
        {
            elapsedTime += deltaTime;
            return elapsedTime < waitTime;
        }
    }
}
