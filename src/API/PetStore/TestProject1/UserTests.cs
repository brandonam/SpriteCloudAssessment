using Flurl.Http.Testing;
using PetStore.Model;
using PetStore.Tests.Helper;
using Xunit;
using FluentAssertions;

namespace PetStore.Tests
{
    public class UserTests
    {
        private const string _userEndpoint = "https://petstore.swagger.io/v2/user";

        [Theory]
        [MemberData(nameof(NewUserRequestTestData), MemberType = typeof(UserTests))]
        public async Task Given_a_new_user_When_registring_Then_creation_successfulAsync(Create expected)
        {
            // Arrange
            using var stubbedHttpClient = new HttpTest();
            stubbedHttpClient.RespondWithJson(expected.ApiResponse, 200);
            var mockedHttpClient = HttpClientMock.Create(_userEndpoint, expected.ApiResponse);

            // Act
            var userRequest = new User(mockedHttpClient);
            var response = await userRequest.Create(expected.NewUserRequest);

            // Assert
            response.Should().BeEquivalentTo(expected.ApiResponse);
            stubbedHttpClient.ShouldHaveCalled(_userEndpoint)
              .WithVerb(HttpMethod.Post)
              .WithContentType("application/json");
        }

        public class Create
        {
            public UserModel NewUserRequest = new UserModel
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
                Message = "",
                Type = ""
            };
        }

        public static IEnumerable<object[]> NewUserRequestTestData =>
          new List<object[]>
          {
            new object[] { new Create() }
          };
    }
}