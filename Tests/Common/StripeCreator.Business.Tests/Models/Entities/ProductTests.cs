using NUnit.Framework;
using StripeCreator.Business.Models;
using StripeCreator.Business.Tests.Infrastructure.Helpers.Models;

namespace StripeCreator.Business.Tests.Models.Entities
{
    internal class ProductTests
    {
        [Test]
        [TestCaseSource(nameof(Create_Product_Correct_Cases))]
        public void Create_Product_Correct(string name, decimal price, string description, Guid? id = null)
        {
            var product = new Product(name, price, description, id);
            Assert.Multiple(() =>
            {
                Assert.That(product.Name, Is.EqualTo(name));
                Assert.That(product.Price, Is.EqualTo(price));
                Assert.That(product.Description, Is.EqualTo(description));
                Assert.That(product.Id, Is.EqualTo(id));
            });
        }

        static readonly object?[] Create_Product_Correct_Cases =
        {
            new object?[] { ProductHelper.TestName, ProductHelper.TestPrice, ProductHelper.TestDescription, null },
            new object?[] { ProductHelper.TestName, 1m, ProductHelper.TestDescription, null },
            new object?[] { ProductHelper.TestName, 1m, ProductHelper.TestDescription, ProductHelper.TestId },
        };

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(-1000)]
        public void Create_Product_IncorrectPrice(decimal price)
        {
            void construct() => ProductHelper.CreateProduct(price: price);
            Assert.Throws<ArgumentOutOfRangeException>(construct);
        }

        [Test]
        [TestCase("")]
        [TestCase("         ")]
        public void Create_Product_IncorrectName(string name)
        {
            void construct() => ProductHelper.CreateProduct(name: name);
            Assert.Throws<ArgumentNullException>(construct);
        }

        [Test]
        [TestCase("")]
        [TestCase("         ")]
        public void Create_Product_IncorrectDescription(string description)
        {
            void construct() => ProductHelper.CreateProduct(description: description);
            Assert.Throws<ArgumentNullException>(construct);
        }
    }
}
