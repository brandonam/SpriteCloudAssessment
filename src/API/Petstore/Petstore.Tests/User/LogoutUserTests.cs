using FluentAssertions;
using Flurl.Http;
using Petstore.Model;

namespace Petstore.Tests.User
{
    public class LogoutUserTests
    {
        private const string _userEndpoint = "https://petstore.swagger.io/v2/user/";

        [Fact]
        public async Task Given_any_user_logged_in_When_logging_out_Should_return_200_response_message_ok()
        {
            // Arrange
            var apiResponse = new ApiResponse { Code = 200, Type = "unknown", Message = "ok" };
            var request = new FlurlClient(_userEndpoint).Request("logout");
            // Flurl will only allow 200 HttpStatusCodes to be returned without explicitly throwing an exception unless setting the following option
            request.AllowAnyHttpStatus();

            // Act
            var response = await request.GetAsync();
            var result = await response.GetJsonAsync<ApiResponse>();

            // Assert
            result.Should().BeEquivalentTo(apiResponse);
        }
    }
}
