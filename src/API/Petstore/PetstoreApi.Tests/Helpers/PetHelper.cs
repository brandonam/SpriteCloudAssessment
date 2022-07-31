using Bogus;
using Petstore.Model;

namespace Petstore.Tests.Helpers
{
    public static class PetHelper
    {
        /// <summary>
        /// Generates a fake pet model object.
        /// </summary>
        /// <param name="category">If null a category will be generated.</param>
        /// <param name="tags">If null tags will be generated.</param>
        /// <returns></returns>
        public static PetModel FakePetGenerator(int seed = 999, Category? category = null, Tag[]? tags = null)
        {
            //Set the randomizer seed if you wish to generate repeatable data sets.
            Randomizer.Seed = new Random(seed);

            if (category is null)
            {
                var categoryTestData = new Faker<Category>()
                    .RuleFor(u => u.Name, (f, u) => f.Name.FirstName());
                category = categoryTestData.Generate();
            }

            if (tags is null)
            {
                var tagTestData = new Faker<Tag>()
                    .RuleFor(u => u.Name, (f, u) => f.Name.FirstName());
                tags = new[] { tagTestData.Generate() };
            }

            // Mock user expected Id and all other properties can randomized as the only property we are concerned with here is the Id.
            var petTestData = new Faker<PetModel>()
                .RuleFor(u => u.Name, (f, u) => f.Name.FirstName())
                .RuleFor(p => p.PhotoUrls, f => f.Make(2, () => f.Internet.UrlWithPath()).ToArray())
                .RuleFor(u => u.Id, f => f.UniqueIndex)
                .RuleFor(u => u.Status, f => $"{f.UniqueIndex}");
            var pet = petTestData.Generate();
            pet.Category = category;
            pet.Tags = tags;
            return pet;
        }
    }
}
