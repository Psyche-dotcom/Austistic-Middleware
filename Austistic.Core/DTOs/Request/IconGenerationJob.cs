using System.Text.Json.Serialization;

namespace Austistic.Core.DTOs.Request
{
    public class IconGenerationJob
    {
        public int Id { get; set; }
        public string TaskId { get; set; }    // Freepik task ID
        public string Status { get; set; }
        public DateTime ReceivedAt { get; set; }

        public List<IconPreviewImage> PreviewImages { get; set; } = new();
    }

    public class BaseFreePikAPi
    {
        public SymbolToken data { get; set; }
    }

    public class SymbolToken
    {
        public string task_id { get; set; }
        public string status { get; set; }
    }

    public class IconPreviewImage
    {
        public int Id { get; set; }
        public string Url { get; set; }

        public int IconGenerationJobId { get; set; }
        public IconGenerationJob IconGenerationJob { get; set; }
    }


    public class IconPreviewWebhookPayload
    {
        [JsonPropertyName("generated")]
        public List<string> Generated { get; set; } = new();

        [JsonPropertyName("task_id")]
        public string TaskId { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }
    }
}
