using StripeCreator.DAL.Models;

namespace StripeCreator.DAL.Tests.TestData
{
    public static class TestOrderProducts
    {
        public static DbOrderProduct DbOrder1Product1 => new(
            id: Guid.Parse("6F893331-A663-4C90-908B-8A2C1C7F96D0"),
            quantity: 1,
            productId: TestProducts.Product1Id,
            orderId: TestOrders.Order1Id
        );

        public static DbOrderProduct DbOrder1Product2 => new(
            id: Guid.Parse("C78FB71B-7879-4175-8E48-A74ADCFCBDAE"),
            quantity: 2,
            productId: TestProducts.Product2Id,
            orderId: TestOrders.Order1Id
        );

        public static DbOrderProduct DbOrder2Product2 => new(
            id: Guid.Parse("CE47D3FD-A302-4C9C-84E1-D3CC73280A43"),
            quantity: 3,
            productId: TestProducts.Product2Id,
            orderId: TestOrders.Order2Id
        );

        public static DbOrderProduct DbOrder3Product3 => new(
            id: Guid.Parse("58F03EA7-C476-484E-BF80-0CD573D5D294"),
            quantity: 4,
            productId: TestProducts.Product3Id,
            orderId: TestOrders.Order3Id
        );

        public static DbOrderProduct DbOrder4Product3 => new(
            id: Guid.Parse("64C75A3C-76BE-4C20-80BA-BB7A64C55989"),
            quantity: 1,
            productId: TestProducts.Product3Id,
            orderId: TestOrders.Order4Id
        );

        public static DbOrderProduct DbOrder4Product1 => new(
            id: Guid.Parse("A8D75B37-A784-4276-BC89-371DB6FCF68A"),
            quantity: 2,
            productId: TestProducts.Product1Id,
            orderId: TestOrders.Order4Id
        );

        public static DbOrderProduct DbOrder4Product2 => new(
            id: Guid.Parse("C77B233D-41AA-4553-AE3C-BB95D368E6C6"),
            quantity: 3,
            productId: TestProducts.Product2Id,
            orderId: TestOrders.Order4Id
        );

        public static DbOrderProduct DbOrder5Product1 => new(
            id: Guid.Parse("9F8DC0FE-4E42-446E-BC96-978F2F5B4A13"),
            quantity: 10,
            productId: TestProducts.Product1Id,
            orderId: TestOrders.Order5Id
        );

        public static DbOrderProduct DbOrder6Product1 => new(
            id: Guid.Parse("829B8A11-1F09-4D15-9801-6906B9F0133D"),
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
