using Shouldly;
using StripeCreator.DAL.Repositories;
using StripeCreator.DAL.Tests.Fixtures;
using Xunit;

namespace StripeCreator.DAL.Tests.RepositoryTests.Product
{
    [Collection("ProductCollection")]
    public class QueryProductTests
    {
        private readonly DbProductRepository _productRepository;

        public QueryProductTests(ProductQueryTextFixture fixture)
        {
            _productRepository = fixture.ProductRepository;
        }

        [Fact]
        public async Task GetAllProducts_Success()
        {
            var products = await _productRepository.GetAllAsync();

            products.ShouldNotBeNull();
            products.Count().ShouldBe(ProductQueryTextFixture.Products.Length);
        }

        [Fact]
        public async Task GetProductById_Success()
        {
            var storedProduct = ProductQueryTextFixture.Products[0];
            var receivedProduct = await _productRepository.GetByIdAsync(storedProduct.Id!.Value);

            receivedProduct.ShouldNotBeNull();
            receivedProduct.Id.ShouldBe(storedProduct.Id);
            receivedProduct.Name.ShouldBe(storedProduct.Name);
            receivedProduct.Price.ShouldBe(storedProduct.Price);
            receivedProduct.Description.ShouldBe(storedProduct.Description);
        }

        [Fact]
        public async Task GetProductById_NotFound()
        {
            var receivedProduct = await _productRepository.GetByIdAsync(Guid.NewGuid());
            receivedProduct.ShouldBeNull();
        }


        [Fact]
        public void GetProductsSkipNegative_Fail()
        {
            int skipCount = -1;
            int takeCount = 5;

            Should.ThrowAsync<ArgumentOutOfRangeException>(_productRepository.GetAsync(skipCount, takeCount));
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void GetProductsTakeIncorrect_Fail(int takeCount)
        {
            int skipCount = 5;

            Should.ThrowAsync<ArgumentOutOfRangeException>(_productRepository.GetAsync(skipCount, takeCount));
        }

        [Fact]
        public async Task GetProducts_Success()
        {
            var skip = 1;
            var take = 2;
            var products = await _productRepository.GetAsync(skip, take);

            products.ShouldNotBeNull();
            products.Count().ShouldBeLessThanOrEqualTo(take);
            products.ShouldNotContain(product => product.Id == ProductQueryTextFixture.Products[0].Id);
        }
    }
}
