using Newtonsoft.Json;

namespace Petstore.Model;

/// <summary>
/// Represents the APIResponse Model
/// </summary>
public class ApiResponse
{
    [JsonProperty("code")]
    public int Code { get; set; }
    [JsonProperty("message")]
    public string? Message { get; set; }
    [JsonProperty("type")]
    public string? Type { get; set; }
}
