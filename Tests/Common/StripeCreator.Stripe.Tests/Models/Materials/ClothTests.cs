using NUnit.Framework;
using StripeCreator.Core.Models;
using StripeCreator.Stripe.Models;
using StripeCreator.Stripe.Tests.Infrastructure.Helpers.Models;

namespace StripeCreator.Stripe.Tests.Models
{
    internal class ClothTests
    {
        [Test]
        [TestCaseSource(nameof(Create_Cloth_Correct_Cases))]
        public void Create_Cloth_Correct(string name, decimal price, string manufacturer,
            Color color, ClothType clothType, int count, Guid? id = null)
        {
            var cloth = new Cloth(name, price, manufacturer, color, clothType, count, id);
            Assert.Multiple(() =>
            {
                Assert.That(cloth.Name, Is.EqualTo(name));
                Assert.That(cloth.Price, Is.EqualTo(price));
                Assert.That(cloth.Manufacturer, Is.EqualTo(manufacturer));
                Assert.That(cloth.Color, Is.EqualTo(color));
                Assert.That(cloth.Type, Is.EqualTo(clothType));
                Assert.That(cloth.Count, Is.EqualTo(count));
                Assert.That(cloth.Id, Is.EqualTo(id));
            });
        }

        public static object[] Create_Cloth_Correct_Cases =
        {
            new object?[] { ClothHelper.TestName, ClothHelper.TestPrice, ClothHelper.TestManufacturer, ClothHelper.TestColor, ClothHelper.TestType, ClothHelper.TestCount, null },
            new object?[] { ClothHelper.TestName, 1m, ClothHelper.TestManufacturer, ClothHelper.TestColor, ClothHelper.TestType, ClothHelper.TestCount, null },
            new object?[] { ClothHelper.TestName, ClothHelper.TestPrice, ClothHelper.TestManufacturer, ClothHelper.TestColor, ClothHelper.TestType, ClothHelper.TestCount, ClothHelper.TestId },
        };

        [Test]
        [TestCase("")]
        [TestCase("       ")]
        public void Create_Cloth_IncorrectName(string name)
        {
            void construct() => ClothHelper.CreateCloth(name: name);
            Assert.Throws<ArgumentNullException>(construct);
        }

        [Test]
        [TestCase("")]
        [TestCase("       ")]
        public void Create_Cloth_IncorrectManufacturer(string manufacturer)
        {
            void construct() => ClothHelper.CreateCloth(manufacturer: manufacturer);
            Assert.Throws<ArgumentNullException>(construct);
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(-550)]
        public void Create_Cloth_IncorrectPrice(decimal price)
        {
            void construct() => ClothHelper.CreateCloth(price: price);
            Assert.Throws<ArgumentOutOfRangeException>(construct);
        }

        [Test]
        [TestCase(0)]
        [TestCase(-10)]
        public void Create_Cloth_IncorrectCount(int count)
        {
            void construct() => ClothHelper.CreateCloth(count: count);
            Assert.Throws<ArgumentOutOfRangeException>(construct);
        }
    }
}
