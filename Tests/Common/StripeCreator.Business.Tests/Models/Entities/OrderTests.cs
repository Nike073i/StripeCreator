using NUnit.Framework;
using StripeCreator.Business.Enums;
using StripeCreator.Business.Models;
using StripeCreator.Business.Tests.Infrastructure.Helpers.Models;

namespace StripeCreator.Business.Tests.Models.Entities
{
    internal class OrderTests
    {
        [Test]
        [TestCaseSource(nameof(Create_NewOrder_Correct_Cases))]
        public void Create_NewOrder_Correct(Guid clientId, decimal price, IEnumerable<OrderProduct> products, ContactData contactData)
        {
            var order = new Order(clientId, price, products, contactData);
            Assert.Multiple(() =>
            {
                Assert.That(order.Status, Is.EqualTo(OrderStatus.Created));
                Assert.That(order.Products.SequenceEqual(products));
                Assert.That(order.Price, Is.EqualTo(price));
                Assert.That(order.ClientId, Is.EqualTo(clientId));
                Assert.That(order.ContactData, Is.EqualTo(contactData));
            });
        }

        static readonly object[] Create_NewOrder_Correct_Cases =
        {
            new object[] { OrderHelper.TestClientId, OrderHelper.TestPrice, OrderHelper.TestProducts, OrderHelper.TestContactData },
        };

        [Test]
        [TestCaseSource(nameof(Create_Order_IncorrectClientId_Cases))]
        public void Create_Order_IncorrectClientId(Guid clientId)
        {
            void construct() => OrderHelper.CreateNewOrder(clientId: clientId);
            Assert.Throws<ArgumentNullException>(construct);
        }

        static readonly object[] Create_Order_IncorrectClientId_Cases =
        {
            new object[] { Guid.Empty },
            new object[] { new Guid("00000000-0000-0000-0000-000000000000") },
        };

        [Test]
        [TestCase(-100)]
        [TestCase(-1)]
        public void Create_NewOrder_IncorrectPrice(decimal price)
        {
            void construct() => OrderHelper.CreateNewOrder(price: price);
            Assert.Throws<ArgumentOutOfRangeException>(construct);
        }

        [Test]
        [TestCaseSource(nameof(Create_NewOrder_IncorrectProducts_Cases))]
        public void Create_NewOrder_IncorrectProducts(IEnumerable<OrderProduct> products)
        {
            void construct() => OrderHelper.CreateNewOrder(products: products);
            Assert.Throws<ArgumentNullException>(construct);
        }

        static readonly object[] Create_NewOrder_IncorrectProducts_Cases =
        {
            new object[] { Enumerable.Empty<OrderProduct>() },
        };

        [Test]
        [TestCaseSource(nameof(Create_ExistOrder_Correct_Cases))]
        public void Create_ExistOrder_Correct(Guid clientId, decimal price, IEnumerable<OrderProduct> products, ContactData contactData, OrderStatus orderStatus, DateTime dateCreated, Guid id)
        {
            var order = new Order(clientId, price, products, contactData, orderStatus, dateCreated, id);
            Assert.Multiple(() =>
            {
                Assert.That(order.Id, Is.EqualTo(id));
                Assert.That(order.Status, Is.EqualTo(orderStatus));
                Assert.That(order.DateCreated, Is.EqualTo(dateCreated));
                Assert.That(order.Products.SequenceEqual(products));
                Assert.That(order.Price, Is.EqualTo(price));
                Assert.That(order.ClientId, Is.EqualTo(clientId));
                Assert.That(order.ContactData, Is.EqualTo(contactData));
            });
        }

        static readonly object?[] Create_ExistOrder_Correct_Cases =
        {
            new object[] { OrderHelper.TestClientId, OrderHelper.TestPrice, OrderHelper.TestProducts, OrderHelper.TestContactData, OrderHelper.TestOrderStatus, OrderHelper.TestDateCreated, OrderHelper.TestId },
        };

        [Test]
        [TestCaseSource(nameof(Change_Status_CancelActiveOrder_Cases))]
        public void Change_Status_CancelActiveOrder(Order order)
        {
            order.ChangeStatus(OrderStatus.Canceled);
            Assert.That(order.Status, Is.EqualTo(OrderStatus.Canceled));
        }

        static readonly object[] Change_Status_CancelActiveOrder_Cases =
        {
            new object[] { OrderHelper.CreateExistOrder(orderStatus: OrderStatus.Created) },
            new object[] { OrderHelper.CreateExistOrder(orderStatus: OrderStatus.Paid) },
            new object[] { OrderHelper.CreateExistOrder(orderStatus: OrderStatus.Processed) },
        };

        [Test]
        [TestCaseSource(nameof(Change_Status_CancelFinishedOrder_Cases))]
        public void Change_Status_CancelFinishedOrder(Order order)
        {
            void cancelOrder() => order.ChangeStatus(OrderStatus.Canceled);
            Assert.Throws<InvalidOperationException>(cancelOrder);
        }

        static readonly object[] Change_Status_CancelFinishedOrder_Cases =
        {
            new object[] { OrderHelper.CreateExistOrder(orderStatus: OrderStatus.Canceled) },
            new object[] { OrderHelper.CreateExistOrder(orderStatus: OrderStatus.Sent) },
        };

        [Test]
        [TestCaseSource(nameof(Change_Status_SequentialChange_Cases))]
        public void Change_Status_SequentialChange(Order order, OrderStatus newStatus)
        {
            order.ChangeStatus(newStatus);
            Assert.That(order.Status, Is.EqualTo(newStatus));
        }

        static readonly object[] Change_Status_SequentialChange_Cases =
        {
            new object[] { OrderHelper.CreateNewOrder(), OrderStatus.Paid, },
            new object[] { OrderHelper.CreateExistOrder(orderStatus : OrderStatus.Paid), OrderStatus.Processed, },
            new object[] { OrderHelper.CreateExistOrder(orderStatus : OrderStatus.Processed), OrderStatus.Sent, },
        };

        [Test]
        [TestCaseSource(nameof(Change_Status_InconsistentChange_Cases))]
        public void Change_Status_InconsistentChange(Order order, OrderStatus newStatus)
        {
            void changeStatus() => order.ChangeStatus(newStatus);
            Assert.Throws<InvalidOperationException>(changeStatus);
        }

        static readonly object[] Change_Status_InconsistentChange_Cases =
        {
            new object[] { OrderHelper.CreateNewOrder(), OrderStatus.Processed, },
            new object[] { OrderHelper.CreateNewOrder(), OrderStatus.Sent, },
            new object[] { OrderHelper.CreateExistOrder(orderStatus : OrderStatus.Paid), OrderStatus.Sent, },
        };
    }
}
