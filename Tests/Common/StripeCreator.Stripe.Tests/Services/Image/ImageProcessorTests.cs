using NUnit.Framework;
using StripeCreator.Core.Models;
using StripeCreator.Stripe.Extensions;
using StripeCreator.Stripe.Services;

namespace StripeCreator.Stripe.Tests.Services
{
    internal class ImageProcessorTests
    {
        private ImageProcessor _imageProcessor;

        [SetUp]
        public void SetUp()
        {
            int width = 5;
            int height = 5;
            var size = new Size(width, height);
            var emptyPicture = MagickImageExtensions.CreateMagickImage(size);
            _imageProcessor = new ImageProcessor(emptyPicture.CreateImage());
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(-10)]
        public void ColorQuantize_IncorrectColors_ThrowException(int colors)
        {
            void quantize() => _imageProcessor.Quantize(colors);
            Assert.Throws<ArgumentOutOfRangeException>(quantize);
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(-10)]
        [TestCase(257)]
        [TestCase(1000)]
        public void Posterize_IncorrectLevels_ThrowException(int levels)
        {
            void quantize() => _imageProcessor.Posterize(levels);
            Assert.Throws<ArgumentOutOfRangeException>(quantize);
        }
    }
}
