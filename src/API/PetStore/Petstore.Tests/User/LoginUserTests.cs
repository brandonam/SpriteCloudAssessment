using FluentAssertions;
using Flurl.Http;
using Petstore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petstore.Tests.User
{
    public class LoginUserTests
    {
        private const string _userEndpoint = "https://petstore.swagger.io/v2/user";

        [Fact]
        public async Task Given_new_user_When_registering_details_with_Id_0_Should_return_new_user_Id()
        {
            // Arrange
            var request = new FlurlClient(_userEndpoint).Request();
            request.WithHeader("Content-Type", "application/json");
            // it wont be know what the user Id will be until the response is recieved, ignore the message property
            ApiResponse expectedApiResponse = new ApiResponse
            {
                Code = 200,
                Type = "unknown"
            };
            var mockUser = new UserModel { Id = 0 };

            // Act
            var response = await request.PostJsonAsync(mockUser);
            var result = await response.GetJsonAsync<ApiResponse>();

            // Assert
            result.Should().BeEquivalentTo(expectedApiResponse,
              assertionOptions => assertionOptions.Excluding(x => x.Message));
        }
    }
}
