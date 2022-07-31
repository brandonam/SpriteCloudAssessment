using FluentAssertions;
using Flurl.Http;
using Petstore.Model;
using Petstore.Tests.Helpers;

namespace Petstore.Tests.Pet
{
    public class AddPetTests
    {
        private const string _userEndpoint = "https://petstore.swagger.io/v2/pet";

        [Theory]
        [MemberData(nameof(GeneratedPetsTestData), MemberType = typeof(AddPetTests))]
        public async Task Given_any_pet_data_When_added_Should_return_200_and_posted_pet_data(PetModel expected)
        {
            // Arrange
            var request = new FlurlClient(_userEndpoint).Request();
            request.WithHeader("Content-Type", "application/json");

            // Act
            var response = await request.PostJsonAsync(expected);
            var result = await response.GetJsonAsync<PetModel>();

            // Assert
            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task Given_invalid_input_When_adding_new_pet_Should_return_405_response_message_no_data()
        {
            // Arrange
            var request = new FlurlClient(_userEndpoint).Request();
            request.WithHeader("Content-Type", "application/json");
            // allow 405 status to prevent exception throwing
            request.AllowHttpStatus("405");
            // mock request with no user json object
            ApiResponse expectedApiResponse = new ApiResponse
            {
                Code = 405,
                Message = "no data",
                Type = "unknown"
            };

            // Act
            var response = await request.PostJsonAsync(null);
            var result = await response.GetJsonAsync<ApiResponse>();

            // Assert
            result.Should().BeEquivalentTo(expectedApiResponse);
        }

        #region Testdata
        public class CreatePet
        {
            public PetModel[] PetCollection { get; private set; }
            public CreatePet()
            {
                var petModel = new PetModel();
                PetCollection = new[] 
                { 
                    PetHelper.FakePetGenerator(1),
                    PetHelper.FakePetGenerator(2),
                    PetHelper.FakePetGenerator(3),
                    PetHelper.FakePetGenerator(4),
                    // Use an existing known tag
                    PetHelper.FakePetGenerator(5, null, new Tag[]{ new Tag { Id = 0, Name = "Mouse" } })
                };
            }
        }

        public static IEnumerable<object[]> GeneratedPetsTestData =>
          new List<object[]>
          {
            new object[] { new CreatePet().PetCollection[0] },
            new object[] { new CreatePet().PetCollection[1] },
            new object[] { new CreatePet().PetCollection[2] },
            new object[] { new CreatePet().PetCollection[3] },
            new object[] { new CreatePet().PetCollection[4] }
          };
        #endregion
    }
}
