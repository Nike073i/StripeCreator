using StripeCreator.Business.Models;
using StripeCreator.DAL.Models;

namespace StripeCreator.DAL.Mappers
{
    /// <summary>
    /// преобразователя хранимых <see cref="DbProduct"/> и доменных <see cref="Product"/> сущностей 
    /// </summary>
    public class ProductMapper : IDbMapper<DbProduct, Product>
    {
        #region Interface implementation

        public DbProduct CreateDbModel(Product domainModel)
        {
            string name = domainModel.Name;
            decimal price = domainModel.Price;
            string description = domainModel.Description;
            return new DbProduct(name, price, description);
        }

        public Product MapToDomainModel(DbProduct dbModel)
        {
            string name = dbModel.Name;
            decimal price = dbModel.Price;
            string description = dbModel.Description;
            var id = dbModel.Id;
            return new Product(name, price, description, id);
        }

        public void UpdateDbModel(Product domainModel, ref DbProduct dbModel)
        {
            dbModel.Name = domainModel.Name;
            dbModel.Price = domainModel.Price;
            dbModel.Description = domainModel.Description;
        }

        #endregion
    }
}