using Newtonsoft.Json;

namespace Petstore.Model;

/// <summary>
/// Represents the User Model
/// </summary>
public class UserModel
{
    [JsonProperty("id")]
    public int Id { get; set; }
    [JsonProperty("username")]
    public string? Username { get; set; }
    [JsonProperty("firstName")]
    public string? FirstName { get; set; }
    [JsonProperty("lastName")]
    public string? LastName { get; set; }
    [JsonProperty("email")]
    public string? Email { get; set; }
    [JsonProperty("passowrd")]
    public string? Password { get; set; }
    [JsonProperty("phone")]
    public string? Phone { get; set; }
    [JsonProperty("userStatus")]
    public int UserStatus { get; set; }
}