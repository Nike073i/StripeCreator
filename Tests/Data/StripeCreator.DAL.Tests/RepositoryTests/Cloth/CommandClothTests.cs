using Shouldly;
using StripeCreator.Core.Models;
using StripeCreator.DAL.Exceptions;
using StripeCreator.DAL.Models;
using StripeCreator.DAL.Repositories;
using StripeCreator.DAL.Tests.Base;
using StripeCreator.DAL.Tests.TestData;
using StripeCreator.Stripe.Models;
using Xunit;
using ClothModel = StripeCreator.Stripe.Models.Cloth;

namespace StripeCreator.DAL.Tests.RepositoryTests.Cloth
{
    public class CommandClothTests : BaseTest
    {
        private readonly DbClothRepository _repository;

        public CommandClothTests()
        {
            _repository = new DbClothRepository(Context);
        }

        [Fact]
        public async Task CreateCloth_Success()
        {
            var newClothModel = MapFrom(TestCloths.Cloth1);
            var savedCloth = await _repository.SaveAsync(newClothModel);

            savedCloth.ShouldNotBeNull();
            savedCloth.Id.ShouldNotBeNull();
            savedCloth.Id.ShouldNotBe(Guid.Empty);
        }

        [Fact]
        public async Task EditCloth_Success()
        {
            Context.Add(TestCloths.Cloth2);
            Context.SaveChanges();
            var storedCloth = MapFrom(TestCloths.Cloth2, true);
            int newCount = 15;
            string newManufacter = "Тестовый производитель";
            string newName = "Тестовое название";
            decimal newPrice = 500m;
            var newType = ClothType.False;

            storedCloth.Count = newCount;
            storedCloth.Manufacturer = newManufacter;
            storedCloth.Name = newName;
            storedCloth.Price = newPrice;
            storedCloth.Type = newType;
            var changedCloth = await _repository.SaveAsync(storedCloth);

            changedCloth.ShouldNotBeNull();
            changedCloth.Id.ShouldBe(storedCloth.Id);
            changedCloth.Count.ShouldBe(newCount);
            changedCloth.Manufacturer.ShouldBe(newManufacter);
            changedCloth.Name.ShouldBe(newName);
            changedCloth.Price.ShouldBe(newPrice);
            changedCloth.Type.ShouldBe(newType);
        }

        [Fact]
        public async Task RemoveCloth_Success()
        {
            var testCloth = TestCloths.Cloth3;
            Context.Add(testCloth);
            Context.SaveChanges();

            var removeId = await _repository.RemoveAsync(testCloth.Id!.Value);
            var clothExists = Context.Find<DbCloth>(testCloth.Id);

            removeId.ShouldBe(testCloth.Id.Value);
            clothExists.ShouldBeNull();
        }

        [Fact]
        public void RemoveClothNonExists_ThrowException()
        {
            var newGuid = Guid.NewGuid();
            Should.ThrowAsync<EntityNotFoundException>(_repository.RemoveAsync(newGuid));
        }

        private static ClothModel MapFrom(DbCloth cloth, bool mapId = false)
        {
            return mapId
                ? (new(
                    name: cloth.Name,
                    manufacturer: cloth.Manufacturer,
                    price: cloth.Price,
                    type: cloth.Type,
                    count: cloth.Count,
                    color: new Color(cloth.ColorHex),
                    id: cloth.Id))
                : (new(
                    name: cloth.Name,
                    manufacturer: cloth.Manufacturer,
                    price: cloth.Price,
                    type: cloth.Type,
                    color: new Color(cloth.ColorHex),
                    count: cloth.Count));
        }
    }
}
