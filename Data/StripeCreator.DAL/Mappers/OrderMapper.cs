using StripeCreator.Business.Models;
using StripeCreator.DAL.Models;

namespace StripeCreator.DAL.Mappers
{
    /// <summary>
    /// преобразователя хранимых <see cref="DbOrder"/> и доменных <see cref="Order"/> сущностей 
    /// </summary>
    public class OrderMapper : IDbMapper<DbOrder, Order>
    {
        #region Interface implementation

        public DbOrder CreateDbModel(Order domainModel)
        {
            var clientId = domainModel.ClientId;
            decimal price = domainModel.Price;
            var products = CreateDbOrderProductModel(domainModel.Products);
            var contactData = domainModel.ContactData;
            string contactNumber = contactData.ContactNumber;
            string email = contactData.Email;
            string? other = contactData.Other;
            var status = domainModel.Status;
            var date = domainModel.DateCreated;
            return new DbOrder(clientId, price, products, contactNumber, email, other, status, date);
        }

        public Order MapToDomainModel(DbOrder dbModel)
        {
            var clientId = dbModel.ClientId;
            decimal price = dbModel.Price;
            var products = MapDbOrderProductToDomainModel(dbModel.Products);
            var contactData = new ContactData(dbModel.ContactNumber, dbModel.Email, dbModel.Other);
            var status = dbModel.Status;
            var date = dbModel.DateCreated;
            var id = dbModel.Id;
            return new Order(clientId, price, products, contactData, status, date, id);
        }

        public void UpdateDbModel(Order domainModel, ref DbOrder dbModel)
        {
            // Обновлению подлежит только статус заказа. 
            // Все остальные поля обновлять запрещено!
            dbModel.Status = domainModel.Status;
        }

        #endregion

        #region Private helper methods

        private IEnumerable<DbOrderProduct> CreateDbOrderProductModel(IEnumerable<OrderProduct> domainModel) =>
            domainModel.Select(orderProduct => new DbOrderProduct(orderProduct.ProductId, orderProduct.Quantity)).ToList();

        private IEnumerable<OrderProduct> MapDbOrderProductToDomainModel(IEnumerable<DbOrderProduct> dbModel) =>
            dbModel.Select(orderProduct => new OrderProduct(orderProduct.ProductId, orderProduct.Quantity)).ToList();

        #endregion
    }
}