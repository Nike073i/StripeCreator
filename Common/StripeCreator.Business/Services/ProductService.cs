using StripeCreator.Business.Models;
using StripeCreator.Business.Repositories;

namespace StripeCreator.Business.Services
{
    public class ProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Product> SaveAsync(Product entity) => await _productRepository.SaveAsync(entity);

        public async Task<Guid> RemoveAsync(Guid id) => await _productRepository.RemoveAsync(id);

        public async Task<IEnumerable<Product>> GetAllAsync() => await _productRepository.GetAllAsync();

        public async Task<Product?> GetByIdAsync(Guid id) => await _productRepository.GetByIdAsync(id);
    }
}
