using FluentAssertions;
using Flurl.Http;
using Petstore.Model;


namespace Petstore.Tests.Pet
{
    public class FindPetTests
    {
        private const string _userEndpoint = "https://petstore.swagger.io/v2/pet";

        // [Theory]
        // [InlineData("available", "available")]
        // [InlineData("available", null)]
        // [InlineData(null, "available")]
        // public async Task Given_valid_status_When_searching_by_status_Should_return_collection_pets_with_status(string statusOne, string statusTwo)
        // {
        //     // Arrange
        //     var request = new FlurlClient(_userEndpoint).Request("findByStatus");
        //     request.SetQueryParam("status", statusOne);
        //     request.SetQueryParam("status", statusTwo);
        //     // a known pet response
        //     PetModel singleExpectedPetFromCollection = new PetModel
        //     {
        //         Id = 9223372036854024412,
        //         Category = new Category()
        //         {
        //             Id = 0,
        //             Name = "string"
        //         },
        //         Name = "doggie",
        //         PhotoUrls = new[] { "string" },
        //         Tags = new[] {
        //             new Tag {
        //                 Id = 0,
        //                 Name = "string"
        //             } 
        //         },
        //         Status = "available"
        //     };

        //     // Act
        //     var response = await request.GetAsync();
        //     var result = await response.GetJsonAsync<List<PetModel>>();

        //     // Assert
        //     result.Should().Contain(singleExpectedPetFromCollection);
        // }
    }
}

