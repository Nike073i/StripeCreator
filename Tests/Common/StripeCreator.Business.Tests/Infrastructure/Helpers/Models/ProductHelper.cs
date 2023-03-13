using StripeCreator.Business.Models;

namespace StripeCreator.Business.Tests.Infrastructure.Helpers.Models
{
    internal static class ProductHelper
    {
        public const string TestName = "Продукт";
        public const decimal TestPrice = 150m;
        public const string TestDescription = "Описание";
        public static readonly Guid TestId = new("e9e43c3f-cb55-4877-854f-b92263948506");
        public static Product CreateProduct(string name = TestName, decimal price = TestPrice, string description = TestDescription, Guid? id = null) =>
            new(name, price, description, id ?? TestId);
    }
}
