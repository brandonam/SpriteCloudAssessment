using FluentAssertions;
using Flurl.Http;
using Petstore.Model;

namespace Petstore.Tests.User
{
    public class LoginUserTests
    {
        private const string _userEndpoint = "https://petstore.swagger.io/v2/user/login";

        [Theory]
        [InlineData("BrandonTest01", "Test01", 200)]
        [InlineData("", "Test02", 200)]
        [InlineData(null, "Test03", 200)]
        [InlineData("BrandonTest04", "", 200)]
        [InlineData("", "", 200)]
        public async Task Given_any_username_password_When_logging_in_Should_return_new_user_session(string username, string password, int code)
        {
            // Arrange
            var apiResponse = new ApiResponse { Code = code, Type = "unknown" };
            var request = new FlurlClient(_userEndpoint).Request();
            request.SetQueryParam("username", username);
            request.SetQueryParam("password", password);

            // Act
            var response = await request.GetAsync();
            var result = await response.GetJsonAsync<ApiResponse>();

            // Assert
            result.Should().BeEquivalentTo(apiResponse,
              assertionOptions => assertionOptions.Excluding(x => x.Message));
        }

        [Fact]
        public async Task Given_no_query_parameters_When_login_called_Should_return_new_user_session()
        {
            // Arrange
            var apiResponse = new ApiResponse { Code = 200, Type = "unknown" };
            var request = new FlurlClient(_userEndpoint).Request();

            // Act
            var response = await request.GetAsync();
            var result = await response.GetJsonAsync<ApiResponse>();

            // Assert
            result.Should().BeEquivalentTo(apiResponse,
              assertionOptions => assertionOptions.Excluding(x => x.Message));
        }
    }
}
