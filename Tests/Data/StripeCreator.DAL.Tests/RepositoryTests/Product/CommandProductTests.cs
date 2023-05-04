using Shouldly;
using StripeCreator.DAL.Exceptions;
using StripeCreator.DAL.Models;
using StripeCreator.DAL.Repositories;
using StripeCreator.DAL.Tests.Base;
using StripeCreator.DAL.Tests.TestData;
using Xunit;
using ProductModel = StripeCreator.Business.Models.Product;

namespace StripeCreator.DAL.Tests.RepositoryTests.Product
{
    public class CommandProductTests : BaseTest
    {
        private readonly DbProductRepository _repository;

        public CommandProductTests()
        {
            _repository = new DbProductRepository(Context);
        }

        [Fact]
        public async Task CreateProduct_Success()
        {
            var newProductModel = MapFrom(TestProducts.Product1);
            var savedProduct = await _repository.SaveAsync(newProductModel);

            savedProduct.ShouldNotBeNull();
            savedProduct.Id.ShouldNotBeNull();
            savedProduct.Id.ShouldNotBe(Guid.Empty);
        }

        [Fact]
        public async Task EditProduct_Success()
        {
            Context.Add(TestProducts.Product2);
            Context.SaveChanges();
            var storedProduct = MapFrom(TestProducts.Product2, true);
            string newName = "Тестовое название";
            string newDescription = "Тестовое описание";
            decimal newPrice = 500m;

            storedProduct.Name = newName;
            storedProduct.Description = newDescription;
            storedProduct.Price = newPrice;
            var changedProduct = await _repository.SaveAsync(storedProduct);

            changedProduct.ShouldNotBeNull();
            changedProduct.Id.ShouldBe(storedProduct.Id);
            changedProduct.Name.ShouldBe(newName);
            changedProduct.Description.ShouldBe(newDescription);
            changedProduct.Price.ShouldBe(newPrice);
        }

        [Fact]
        public async Task RemoveProduct_Success()
        {
            var testProduct = TestProducts.Product3;
            Context.Add(testProduct);
            Context.SaveChanges();

            var removeId = await _repository.RemoveAsync(testProduct.Id!.Value);
            var productExists = Context.Find<DbProduct>(testProduct.Id);

            removeId.ShouldBe(testProduct.Id.Value);
            productExists.ShouldBeNull();
        }

        [Fact]
        public void RemoveProductNonExists_ThrowException()
        {
            var newGuid = Guid.NewGuid();
            Should.ThrowAsync<EntityNotFoundException>(_repository.RemoveAsync(newGuid));
        }

        private static ProductModel MapFrom(DbProduct product, bool mapId = false)
        {
            return mapId
                ? (new(
                    name: product.Name,
                    description: product.Description,
                    price: product.Price,
                    id: product.Id))
                : (new(
                    name: product.Name,
                    description: product.Description,
                    price: product.Price));
        }
    }
}
