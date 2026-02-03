using System.Collections;

namespace InfinityMemoriesEngine.OverWatch.qianhan.MonoBehaviours
{
    public class CoroutineRunner
    {
        private class Coroutine
        {
            public IEnumerator? Enumerator;
            public IYieldInstruction? CurrentYield;
        }

        private readonly List<Coroutine> routines = new List<Coroutine>();

        public void StartCoroutine(IEnumerator enumerator)
        {
            if (enumerator != null)
            {
                routines.Add(new Coroutine { Enumerator = enumerator });
            }
        }

        public void StopAllCoroutines()
        {
            routines.Clear();
        }

        public void Update(float deltaTime)
        {
            for (int i = 0; i < routines.Count; i++)
            {
                var routine = routines[i];

                if (routine.CurrentYield != null)
                {
                    if (routine.CurrentYield.KeepWaiting(deltaTime))
                        continue;

                    routine.CurrentYield = null;
                }

                if (!routine.Enumerator.MoveNext())
                {
                    routines.RemoveAt(i--);
                    continue;
                }

                if (routine.Enumerator.Current is IYieldInstruction yieldInstruction)
                {
                    routine.CurrentYield = yieldInstruction;
                }
            }
        }
    }
}
