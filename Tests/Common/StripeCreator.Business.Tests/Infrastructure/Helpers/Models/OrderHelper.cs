using StripeCreator.Business.Enums;
using StripeCreator.Business.Models;

namespace StripeCreator.Business.Tests.Infrastructure.Helpers.Models
{
    internal static class OrderHelper
    {
        public static readonly Guid TestId = new("e9e43c3f-cb55-4877-854f-b92263948506");
        public static readonly Guid TestClientId = new("bdd6e24d-3c4c-451d-8367-d9830758c593");
        public const decimal TestPrice = 1500m;
        public static readonly IEnumerable<OrderProduct> TestProducts = new List<OrderProduct>
        {
            OrderProductHelper.CreateOrderProduct(new Guid("7db5d30d-3c46-4c4d-8606-678306324aa3"), 10),
            OrderProductHelper.CreateOrderProduct(new Guid("e491a723-b08e-4651-89e7-84f41a5cdbd1"), 15),
            OrderProductHelper.CreateOrderProduct(new Guid("fab3a329-8d0e-43e7-a8be-5dc979f26083"), 20),
        };
        public static readonly ContactData TestContactData = ContactDataHelper.CreateContactData();
        public const OrderStatus TestOrderStatus = OrderStatus.Paid;
        public static readonly DateTime TestDateCreated = new(2023, 1, 1);

        public static Order CreateNewOrder(Guid? clientId = null, decimal? price = null, IEnumerable<OrderProduct>? products = null, ContactData? contactData = null) =>
            new(clientId ?? TestClientId, price ?? TestPrice, products ?? TestProducts, contactData ?? TestContactData);

        public static Order CreateExistOrder(Guid? clientId = null, decimal? price = null, IEnumerable<OrderProduct>? products = null, ContactData? contactData = null,
            OrderStatus? orderStatus = null, DateTime? dateCreated = null, Guid? id = null) =>
             new(clientId ?? TestClientId, price ?? TestPrice, products ?? TestProducts, contactData ?? TestContactData, orderStatus ?? TestOrderStatus, dateCreated ?? TestDateCreated, id ?? TestId);
    }
}
