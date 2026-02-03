using InfinityMemoriesEngine.OverWatch.qianhan.AI;

namespace InfinityMemoriesEngine.OverWatch.qianhan.MainCollection.Colliders
{
    internal class AIManager
    {
        private static AIClient client;
        private static string? pendingPrompt;
        private static bool isAsking;

        // 光携调度器回调 → 每个 tick 内调用
        public static void Update(double tickSec, ulong frameIndex)
        {
            if (!isAsking && !string.IsNullOrEmpty(pendingPrompt))
            {
                _ = ProcessPromptAsync(pendingPrompt);
                pendingPrompt = null;
            }
        }

        public static void Init()
        {
            client = new AIClient(
                apiKey: "sk-aaa45df19208418b9c3e60ed45b6b048",
                endpoint: "https://api.deepseek.com/v1/chat/completions",
                model: "deepseek-chat"
            );
        }

        public static void SetPrompt(string prompt)
        {
            pendingPrompt = prompt;
        }

        private static async Task ProcessPromptAsync(string prompt)
        {
            isAsking = true;
            try
            {
                string response = await client.SendPromptAsync(prompt);
                OnAIResponse?.Invoke(response);
            }
            catch (Exception ex)
            {
                OnAIError?.Invoke(ex);
            }
            finally
            {
                isAsking = false;
            }
        }

        // 回调事件，由引擎决定怎么处理
        public static event Action<string>? OnAIResponse;
        public static event Action<Exception>? OnAIError;

        public static void Dispose()
        {
            client?.Dispose();
        }
    }
}
