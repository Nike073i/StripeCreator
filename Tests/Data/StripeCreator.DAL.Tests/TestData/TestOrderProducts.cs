using StripeCreator.DAL.Models;

namespace StripeCreator.DAL.Tests.TestData
{
    public static class TestOrderProducts
    {
        public static DbOrderProduct DbOrder1Product1 => new(
            quantity: 1,
            productId: TestProducts.Product1Id,
            orderId: TestOrders.Order1Id
        );

        public static DbOrderProduct DbOrder1Product2 => new(
            quantity: 2,
            productId: TestProducts.Product2Id,
            orderId: TestOrders.Order1Id
        );

        public static DbOrderProduct DbOrder2Product2 => new(
            quantity: 3,
            productId: TestProducts.Product2Id,
            orderId: TestOrders.Order2Id
        );

        public static DbOrderProduct DbOrder3Product3 => new(
            quantity: 4,
            productId: TestProducts.Product3Id,
            orderId: TestOrders.Order3Id
        );

        public static DbOrderProduct DbOrder4Product3 => new(
            quantity: 1,
            productId: TestProducts.Product3Id,
            orderId: TestOrders.Order4Id
        );

        public static DbOrderProduct DbOrder4Product1 => new(
            quantity: 2,
            productId: TestProducts.Product1Id,
            orderId: TestOrders.Order4Id
        );

        public static DbOrderProduct DbOrder4Product2 => new(
            quantity: 3,
            productId: TestProducts.Product2Id,
            orderId: TestOrders.Order4Id
        );

        public static DbOrderProduct DbOrder5Product1 => new(
            quantity: 10,
            productId: TestProducts.Product1Id,
            orderId: TestOrders.Order5Id
        );

        public static DbOrderProduct DbOrder6Product1 => new(
            quantity: 5,
            productId: TestProducts.Product1Id,
            orderId: TestOrders.Order6Id
        );

        public static readonly DbOrderProduct[] Order1Products = { DbOrder1Product1, DbOrder1Product2, };
        public static readonly DbOrderProduct[] Order2Products = { DbOrder2Product2 };
        public static readonly DbOrderProduct[] Order3Products = { DbOrder3Product3 };
        public static readonly DbOrderProduct[] Order4Products = { DbOrder4Product3, DbOrder4Product1, DbOrder4Product2 };
        public static readonly DbOrderProduct[] Order5Products = { DbOrder5Product1 };
        public static readonly DbOrderProduct[] Order6Products = { DbOrder6Product1 };
    }
}
