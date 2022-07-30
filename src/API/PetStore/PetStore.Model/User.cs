using Flurl.Http;
using Newtonsoft.Json;

namespace PetStore.Model;

public class User
{
    private readonly IFlurlClient _flurlClient;

    public User(HttpClient httpClient)
    {
        _flurlClient = new FlurlClient(httpClient);
    }

    public async Task<ApiResponse> Create(UserModel user)
    {
        try
        {
            var request = _flurlClient.Request();
            var response = await request
                .WithHeader("Content-Type", "application/json")
                .PostJsonAsync(user)
                .ConfigureAwait(false);
            var result = await response
                .GetJsonAsync<ApiResponse>()
                .ConfigureAwait(false);
            return result;
        }
        catch (FlurlHttpException e)
        {
            var internalServerResponseError = await e.GetResponseJsonAsync<ApiResponse>();
            return internalServerResponseError;
        }
    }
}

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