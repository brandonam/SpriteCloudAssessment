using FluentAssertions;
using Flurl.Http;
using Moq;
using PetStore.Model;

namespace PetStore.Tests.User
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
            // Mock user expected Id and all other properties randomized
            var mockUser = Mock.Of<UserModel>();
            mockUser.Id = expected.User.Id;
            mockUser.Email = It.IsAny<string>();
            mockUser.FirstName = It.IsAny<string>();
            mockUser.LastName = It.IsAny<string>();
            mockUser.Password = It.IsAny<string>();
            mockUser.Phone = It.IsAny<string>();
            mockUser.UserStatus = It.IsAny<int>();

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

        [Fact]
        public async Task Given_invalid_input_When_registering_new_user_Should_return_405_response_message_no_data()
        {
            // Arrange
            var request = new FlurlClient(_userEndpoint).Request();
            request.WithHeader("Content-Type", "application/json");
            // allow 405 status to prevent exception throwing
            request.AllowHttpStatus("405");
            // mock request with no user json object
            UserModel? mockUser = null;
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

        public class ExistingUser
        {
            public UserModel User = new UserModel
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

            public int HttpStatusCode = 200;
        }

        public static IEnumerable<object[]> ExistingUserTestData =>
          new List<object[]>
          {
            new object[] { new ExistingUser() },
            new object[] { new ExistingUser {
                    User = new UserModel { Id = 1 },
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
            public UserModel User = new UserModel
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

            public int HttpStatusCode = 200;
        }

        public static IEnumerable<object[]> NewUserTestData =>
          new List<object[]>
          {
            new object[] { new CreateNewUser() },
            new object[] { new CreateNewUser { User = new UserModel { Username = "BrandonTest01", Password = "Test01" } } },
            new object[] { new CreateNewUser { User = new UserModel { Username = "BrandonTest02", Password = "Test02" } } }
          };

    }
}