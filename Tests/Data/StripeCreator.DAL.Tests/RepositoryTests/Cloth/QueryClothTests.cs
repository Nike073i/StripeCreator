using Shouldly;
using StripeCreator.DAL.Repositories;
using StripeCreator.DAL.Tests.Fixtures;
using Xunit;

namespace StripeCreator.DAL.Tests.RepositoryTests.Cloth
{
    [Collection("ClothCollection")]
    public class QueryClothTests
    {
        private readonly DbClothRepository _clothRepository;

        public QueryClothTests(ClothQueryTextFixture fixture)
        {
            _clothRepository = fixture.ClothRepository;
        }

        [Fact]
        public async Task GetAllCloths_Success()
        {
            var cloths = await _clothRepository.GetAllAsync();

            cloths.ShouldNotBeNull();
            cloths.Count().ShouldBe(ClothQueryTextFixture.Cloths.Length);
        }

        [Fact]
        public async Task GetClothById_Success()
        {
            var storedCloth = ClothQueryTextFixture.Cloths[0];
            var receivedCloth = await _clothRepository.GetByIdAsync(storedCloth.Id!.Value);

            receivedCloth.ShouldNotBeNull();
            receivedCloth.Id.ShouldBe(storedCloth.Id);
            receivedCloth.Name.ShouldBe(storedCloth.Name);
            receivedCloth.Manufacturer.ShouldBe(storedCloth.Manufacturer);
            receivedCloth.Price.ShouldBe(storedCloth.Price);
            receivedCloth.Type.ShouldBe(storedCloth.Type);
            receivedCloth.Count.ShouldBe(storedCloth.Count);
            receivedCloth.Color.HexValue.ShouldBe(storedCloth.ColorHex);
        }

        [Fact]
        public async Task GetClothById_NotFound()
        {
            var receivedCloth = await _clothRepository.GetByIdAsync(Guid.NewGuid());
            receivedCloth.ShouldBeNull();
        }


        [Fact]
        public void GetClothsSkipNegative_Fail()
        {
            int skipCount = -1;
            int takeCount = 5;

            Should.ThrowAsync<ArgumentOutOfRangeException>(_clothRepository.GetAsync(skipCount, takeCount));
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void GetClothsTakeIncorrect_Fail(int takeCount)
        {
            int skipCount = 5;

            Should.ThrowAsync<ArgumentOutOfRangeException>(_clothRepository.GetAsync(skipCount, takeCount));
        }

        [Fact]
        public async Task GetCloths_Success()
        {
            var skip = 1;
            var take = 2;
            var cloths = await _clothRepository.GetAsync(skip, take);

            cloths.ShouldNotBeNull();
            cloths.Count().ShouldBeLessThanOrEqualTo(take);
            cloths.ShouldNotContain(cloth => cloth.Id == ClothQueryTextFixture.Cloths[0].Id);
        }
    }
}
