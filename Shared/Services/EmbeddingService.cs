using BERTTokenizers;
using Microsoft.Extensions.Options;
using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;
using SixLabors.ImageSharp.Drawing;
using Sukalibur.Shared.Options;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Sukalibur.Shared.Services
{
    public class EmbeddingService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly CommonOptions _commonOptions;

        public EmbeddingService(IHttpClientFactory httpClientFactory, IOptions<CommonOptions> commonOptions)
        {
            _httpClientFactory = httpClientFactory;
            _commonOptions = commonOptions.Value;
        }

        // Process the sentence and get its embedding
        public async Task<float[]> GenerateEmbeddingAsync(string str)
        {
            var httpRequestBody = JsonSerializer.Serialize(new
            {
                data = str
            });
            var baseUrl = _commonOptions.EmbeddingServiceBaseUrl;
            using var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, $"{baseUrl}/embed");
            httpRequestMessage.Content = new StringContent(httpRequestBody, Encoding.UTF8, "application/json");

            var httpClient = _httpClientFactory.CreateClient();
            var httpResponseMessage = httpClient.Send(httpRequestMessage);

            if (!httpResponseMessage.IsSuccessStatusCode)
                throw new Exception("Failed");
            using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

            var httpResponseBody = await JsonSerializer.DeserializeAsync<EmbedResult>(contentStream);
            return httpResponseBody!.Data;
        }

        public class EmbedResult
        {
            [JsonPropertyName("message")]
            public string Message { get; set; } = null!;
            [JsonPropertyName("data")]
            public float[] Data { get; set; } = [];
        }
    }
}
