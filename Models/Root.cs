using Newtonsoft.Json;

namespace DGRC.Models
{
    public class Root
    {
        [JsonProperty("statusCode")]
        public int StatusCode { get; set; }

        [JsonProperty("message")]
        public string? Message { get; set; }

        [JsonProperty("status")]
        public bool Status { get; set; }

        [JsonProperty("user")]
        public object? User { get; set; }
    }

}
