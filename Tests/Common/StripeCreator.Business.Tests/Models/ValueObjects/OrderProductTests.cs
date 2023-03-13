using NUnit.Framework;
using StripeCreator.Business.Models;
using StripeCreator.Business.Tests.Infrastructure.Helpers.Models;

namespace StripeCreator.Business.Tests.Models.ValueObjects
{
    internal class OrderProductTests
    {
        [Test]
        [TestCaseSource(nameof(Create_OrderProduct_Correct_Cases))]
        public void Create_OrderProduct_Correct(Guid productId, int quantity)
        {
            var orderProduct = new OrderProduct(productId, quantity);
            Assert.Multiple(() =>
            {
                Assert.That(orderProduct.ProductId, Is.EqualTo(productId));
                Assert.That(orderProduct.Quantity, Is.EqualTo(quantity));
            });
        }

        static readonly object[] Create_OrderProduct_Correct_Cases =
        {
            new object[] { OrderProductHelper.TestProductId, 10},
            new object[] { OrderProductHelper.TestProductId, 1},
        };

        [Test]
        [TestCaseSource(nameof(Create_OrderProduct_IncorrectGuid_Cases))]
        public void Create_OrderProduct_IncorrectGuid(Guid productId)
        {
            void construct() => new OrderProduct(productId, OrderProductHelper.TestQuantity);
            Assert.Throws<ArgumentNullException>(construct);
        }

        static readonly object[] Create_OrderProduct_IncorrectGuid_Cases =
        {
            new object[] { Guid.Empty },
        };

        [Test]
        [TestCaseSource(nameof(Create_OrderProduct_IncorrectQuantity_Cases))]
        public void Create_OrderProduct_IncorrectQuantity(int quantity)
        {
            void construct() => new OrderProduct(OrderProductHelper.TestProductId, quantity);
            Assert.Throws<ArgumentOutOfRangeException>(construct);
        }

        static readonly object[] Create_OrderProduct_IncorrectQuantity_Cases =
        {
            new object[] { 0 },
            new object[] { -1 },
            new object[] { -100 },
        };


        [Test]
        [TestCaseSource(nameof(Equals_OrderProduct_ReturnTrue_Cases))]
        public void Equals_OrderProducts_ReturnTrue(OrderProduct a, OrderProduct b) => Assert.That(b, Is.EqualTo(a));

        static readonly object[] Equals_OrderProduct_ReturnTrue_Cases =
        {
            new object[] { OrderProductHelper.CreateOrderProduct(), OrderProductHelper.CreateOrderProduct() },
        };

        [Test]
        [TestCaseSource(nameof(Equals_OrderProduct_ReturnFalse_Cases))]
        public void Equals_OrderProducts_ReturnFalse(OrderProduct a, OrderProduct b) => Assert.That(b, Is.Not.EqualTo(a));

        static readonly object[] Equals_OrderProduct_ReturnFalse_Cases =
        {
            new object[] { OrderProductHelper.CreateOrderProduct(productId: new Guid("1c9c32a0-9e83-4896-a2e5-eb87ae86ac3d")), OrderProductHelper.CreateOrderProduct() },
            new object[] { OrderProductHelper.CreateOrderProduct(quantity: 10), OrderProductHelper.CreateOrderProduct(quantity: 20) },

        };
    }
}
