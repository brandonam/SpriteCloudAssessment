using Bogus;
using FluentAssertions;
using Flurl.Http;
using Moq;
using Petstore.Model;

namespace Petstore.Tests.User
{
    public class CreateUserTests
    {
        private const string _userEndpoint = "https://petstore.swagger.io/v2/user";

        [Theory]
        [MemberData(nameof(ExistingUserTestData), MemberType = typeof(CreateUserTests))]
        public async Task Given_existing_user_When_registering_the_same_user_details_Should_return_existing_user_Id(ExistingUser expected)
        {
            // Arrange
            var request = new FlurlClient(_userEndpoint).Request();
            request.WithHeader("Content-Type", "application/json");

            // Act
            var response = await request.PostJsonAsync(expected.User);
            var result = await response.GetJsonAsync<ApiResponse>();

            // Assert
            result.Should().BeEquivalentTo(expected.ApiResponse);
        }

        [Theory]
        [MemberData(nameof(ExistingUserTestData), MemberType = typeof(CreateUserTests))]
        public async Task Given_existing_user_When_registering_new_user_with_same_user_Id_details_Should_return_existing_user_id(ExistingUser expected)
        {
            // Arrange
            var request = new FlurlClient(_userEndpoint).Request();
            request.WithHeader("Content-Type", "application/json");

            // Act
            var response = await request.PostJsonAsync(expected.User);
            var result = await response.GetJsonAsync<ApiResponse>();

            // Assert
            result.Should().BeEquivalentTo(expected.ApiResponse);
        }

        [Theory]
        [MemberData(nameof(ExistingUserTestData), MemberType = typeof(CreateUserTests))]
        public async Task Given_existing_user_When_registering_new_user_with_same_user_Id_details_Should_return_200(ExistingUser expected)
        {
            // Arrange
            var request = new FlurlClient(_userEndpoint).Request();
            request.WithHeader("Content-Type", "application/json");
            // Flurl will only allow 200 HttpStatusCodes to be returned without explicitly throwing an exception
            request.AllowAnyHttpStatus();

            // Act
            var response = await request.PostJsonAsync(expected.User);
            var result = response.StatusCode;

            // Assert
            result.Should().Be(200);
        }

        [Theory]
        [MemberData(nameof(NewUserTestData), MemberType = typeof(CreateUserTests))]
        public async Task Given_new_user_When_registering_details_without_id_property_Should_return_random_user_Id(CreateNewUser expected)
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

            // Act
            var response = await request.PostJsonAsync(expected.User);
            var result = await response.GetJsonAsync<ApiResponse>();

            // Assert
            result.Should().BeEquivalentTo(expectedApiResponse,
              assertionOptions => assertionOptions.Excluding(x => x.Message));
        }

        [Fact]
        public async Task Given_invalid_input_When_registering_new_user_Should_return_405_response_message_no_data()
        {
            // Arrange
            var request = new FlurlClient(_userEndpoint).Request();
            request.WithHeader("Content-Type", "application/json");
            // allow 405 status to prevent exception throwing
            request.AllowHttpStatus("405");
            // mock request with no user json object
            Model.UserModel? mockUser = null;
            ApiResponse expectedApiResponse = new ApiResponse
            {
                Code = 405,
                Message = "no data",
                Type = "unknown"
            };

            // Act
            var response = await request.PostJsonAsync(mockUser);
            var result = await response.GetJsonAsync<ApiResponse>();

            // Assert
            result.Should().BeEquivalentTo(expectedApiResponse);
        }

        #region Testdata
        public class ExistingUser
        {
            public Model.UserModel User = new Model.UserModel
            {
                Id = 4038946,
                Username = "ut laboris eiusmod",
                FirstName = "in esse ea",
                LastName = "",
                Email = "exercitation mollit",
                Password = "minim elit consequat reprehenderit",
                Phone = "sed aute sint est",
                UserStatus = 34493482
            };

            public ApiResponse ApiResponse = new ApiResponse
            {
                Code = 200,
                Message = "4038946",
                Type = "unknown"
            };
        }

        public static IEnumerable<object[]> ExistingUserTestData =>
          new List<object[]>
          {
            // use default values
            new object[] { new ExistingUser() },
            // use inline created values
            new object[] { new ExistingUser {
                    User = new Model.UserModel { Id = 1 },
                    ApiResponse = new ApiResponse
                    {
                        Code = 200,
                        Message = "1",
                        Type = "unknown"
                    }
                }
            }
          };

        public class CreateNewUser
        {
            public Model.UserModel User = new Model.UserModel
            {
                Username = "ut laboris eiusmod",
                FirstName = "in esse ea",
                LastName = "",
                Email = "exercitation mollit",
                Password = "minim elit consequat reprehenderit",
                Phone = "sed aute sint est",
                UserStatus = 34493482
            };

            public ApiResponse ApiResponse = new ApiResponse
            {
                Code = 200,
                Message = It.IsAny<string>(),
                Type = "unknown"
            };
        }

        public static IEnumerable<object[]> NewUserTestData =>
          new List<object[]>
          {
            new object[] { new CreateNewUser() },
            new object[] { new CreateNewUser { User = new Model.UserModel { Username = "BrandonTest01", Password = "Test01" } } },
            new object[] { new CreateNewUser { User = new Model.UserModel { Username = "BrandonTest02", Password = "Test02" } } },
            new object[] { new CreateNewUser { User = new Model.UserModel { Username = "BrandonTest03", Password = "Test03" } } },
            new object[] { new CreateNewUser { User = new Model.UserModel { Username = "BrandonTest04", Password = "Test04" } } }
          };
        #endregion
    }
}