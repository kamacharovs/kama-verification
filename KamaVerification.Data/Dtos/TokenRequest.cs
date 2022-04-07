using System.Text.Json.Serialization;

namespace KamaVerification.Data.Dtos
{
    public class TokenRequest
    {
        [JsonPropertyName("api_key")]
        public string ApiKey { get; set; }
    }
}