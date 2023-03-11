using NUnit.Framework;
using StripeCreator.Core.Models;

namespace StripeCreator.Core.Tests.Models.ValueObjects
{
    internal class SizeTests
    {
        [Test]
        [TestCase(100, 100)]
        [TestCase(1, 100)]
        [TestCase(1, 1)]
        [TestCase(100, 1)]
        public void CreateSize_ByWidthAndHeight_Correct(int width, int height)
        {
            var newSize = new Size(width, height);
            Assert.Multiple(() =>
            {
                Assert.That(newSize.Width, Is.EqualTo(width));
                Assert.That(newSize.Height, Is.EqualTo(height));
            });
        }

        [Test]
        [TestCase(0, 0)]
        [TestCase(-1, -1)]
        [TestCase(0, 100)]
        [TestCase(-1, 100)]
        [TestCase(100, 0)]
        [TestCase(100, -1)]
        public void CreateSize_ByWidthAndHeight_NonPositiveParams(int width, int height)
        {
            void constructSize() => new Size(width, height);
            Assert.Throws<ArgumentOutOfRangeException>(constructSize);
        }
    }
}
