using NUnit.Framework;
using StripeCreator.Core.Models;
using StripeCreator.Stripe.Models;
using StripeCreator.Stripe.Tests.Infrastructure.Helpers.Models;
using Thread = StripeCreator.Stripe.Models.Thread;

namespace StripeCreator.Stripe.Tests.Models
{
    internal class ThreadTests
    {
        [Test]
        [TestCaseSource(nameof(Create_Thread_Correct_Cases))]
        public void Create_Thread_Correct(string name, decimal price, string manufacturer,
            Color color, ThreadType threadType, int weight, Guid? id = null)
        {
            var thread = new Thread(name, price, manufacturer, color, threadType, weight, id);
            Assert.Multiple(() =>
            {
                Assert.That(thread.Name, Is.EqualTo(name));
                Assert.That(thread.Price, Is.EqualTo(price));
                Assert.That(thread.Manufacturer, Is.EqualTo(manufacturer));
                Assert.That(thread.Color, Is.EqualTo(color));
                Assert.That(thread.Type, Is.EqualTo(threadType));
                Assert.That(thread.Weight, Is.EqualTo(weight));
                Assert.That(thread.Id, Is.EqualTo(id));
            });
        }

        public static object[] Create_Thread_Correct_Cases =
        {
            new object?[] { ThreadHelper.TestName, ThreadHelper.TestPrice, ThreadHelper.TestManufacturer, ThreadHelper.TestColor, ThreadHelper.TestType, ThreadHelper.TestWeight, null },
            new object?[] { ThreadHelper.TestName, 1m, ThreadHelper.TestManufacturer, ThreadHelper.TestColor, ThreadHelper.TestType, ThreadHelper.TestWeight, null },
            new object?[] { ThreadHelper.TestName, ThreadHelper.TestPrice, ThreadHelper.TestManufacturer, ThreadHelper.TestColor, ThreadHelper.TestType, ThreadHelper.TestWeight, ThreadHelper.TestId },
        };

        [Test]
        [TestCase("")]
        [TestCase("       ")]
        public void Create_Thread_IncorrectName(string name)
        {
            void construct() => ThreadHelper.CreateThread(name: name);
            Assert.Throws<ArgumentNullException>(construct);
        }

        [Test]
        [TestCase("")]
        [TestCase("       ")]
        public void Create_Thread_IncorrectManufacturer(string manufacturer)
        {
            void construct() => ThreadHelper.CreateThread(manufacturer: manufacturer);
            Assert.Throws<ArgumentNullException>(construct);
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(-550)]
        public void Create_Thread_IncorrectPrice(decimal price)
        {
            void construct() => ThreadHelper.CreateThread(price: price);
            Assert.Throws<ArgumentOutOfRangeException>(construct);
        }

        [Test]
        [TestCase(0)]
        [TestCase(-10)]
        public void Create_Thread_IncorrectWeight(int weight)
        {
            void construct() => ThreadHelper.CreateThread(weight: weight);
            Assert.Throws<ArgumentOutOfRangeException>(construct);
        }
    }
}
