using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace InfinityMemoriesEngine.OverWatch.qianhan.AI
{
    /// <summary>
    /// AIClient类，用于与DeepSeek AI服务进行交互。
    /// </summary>
    internal class AIClient:IDisposable
    {
        private readonly string apiKey;
        private readonly string endpoint;
        private readonly string model;
        private readonly HttpClient httpClient;

        public AIClient(string apiKey, string endpoint, string model)
        {
            this.apiKey = apiKey;
            this.endpoint = endpoint;
            this.model = model;
            this.httpClient = new HttpClient(); // 独立实例，更好控制生命周期
        }

        public async Task<string> SendPromptAsync(string prompt)
        {
            var payload = new
            {
                model = this.model,
                messages = new[]
                {
                    new { role = "user", content = prompt }
                }
            };

            var request = new HttpRequestMessage(HttpMethod.Post, endpoint);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
            request.Content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");

            var response = await httpClient.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();

            string responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            using var doc = JsonDocument.Parse(responseBody);

            return doc.RootElement
                      .GetProperty("choices")[0]
                      .GetProperty("message")
                      .GetProperty("content")
                      .GetString() ?? string.Empty;
        }

        public void Dispose()
        {
            httpClient.Dispose();
        }
    }
}
