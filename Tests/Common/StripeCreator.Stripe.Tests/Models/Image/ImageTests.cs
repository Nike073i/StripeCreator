using NUnit.Framework;
using StripeCreator.Core.Models;
using StripeCreator.Stripe.Models;

namespace StripeCreator.Stripe.Tests.Models
{
    internal class ImageTests
    {
        [Test]
        public void Create_Image_EmptyData()
        {
            var data = Array.Empty<byte>();
            var size = new Size(100, 100);
            void construct() => new Image(data, size);
            Assert.Throws<ArgumentException>(construct);
        }

        [Test]
        public void Create_Image_WithSize()
        {
            var width = 5;
            var height = 5;
            var size = new Size(width, height);
            var data = new byte[width * height];
            var newImage = new Image(data, size);
            Assert.Multiple(() =>
            {
                Assert.That(newImage, Is.Not.Null);
                Assert.That(newImage.Width, Is.EqualTo(width));
                Assert.That(newImage.Height, Is.EqualTo(height));
            });
        }

        [Test]
        public void Create_Image_WithWidthAndHeight()
        {
            var width = 5;
            var height = 5;
            var data = new byte[width * height];
            var newImage = new Image(data, width, height);
            Assert.Multiple(() =>
            {
                Assert.That(newImage, Is.Not.Null);
                Assert.That(newImage.Width, Is.EqualTo(width));
                Assert.That(newImage.Height, Is.EqualTo(height));
            });
        }
    }
}
