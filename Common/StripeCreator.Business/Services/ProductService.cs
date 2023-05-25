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

        public Task<Product> SaveAsync(Product entity) => _productRepository.SaveAsync(entity);

        public async Task<Guid> RemoveAsync(Guid id)
        {
            try
            {
                return await _productRepository.RemoveAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "Возможно вы пытаетесь удалить данные продукции, которая уже используется в одном из заказов",
                    ex.InnerException);
            }
        }

        public Task<IEnumerable<Product>> GetAllAsync() => _productRepository.GetAllAsync();

        public Task<Product?> GetByIdAsync(Guid id) => _productRepository.GetByIdAsync(id);
    }
}
