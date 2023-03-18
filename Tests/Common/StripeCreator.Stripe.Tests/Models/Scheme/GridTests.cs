using NUnit.Framework;
using StripeCreator.Core.Models;
using StripeCreator.Stripe.Models;

namespace StripeCreator.Stripe.Tests.Models.Scheme
{
    internal class GridTests
    {
        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(-10)]
        public void Create_Grid_IncorrectSize(int size)
        {
            void construct() => new Grid(size);
            Assert.Throws<ArgumentOutOfRangeException>(construct);
        }

        [Test]
        [TestCase(1)]
        [TestCase(10)]
        public void CreateGrid_OnlySize_Correct(int size)
        {
            var grid = new Grid(size);
            Assert.Multiple(() =>
            {
                Assert.That(grid.Size, Is.EqualTo(size));
                Assert.That(grid.Color, Is.EqualTo(new Color()));
            });
        }

        [Test]
        [TestCaseSource(nameof(CreateGrid_WithColorAndSize_Correct_Cases))]
        public void CreateGrid_WithColorAndSize_Correct(int size, Color color)
        {
            var grid = new Grid(size, color);
            Assert.Multiple(() =>
            {
                Assert.That(grid.Size, Is.EqualTo(size));
                Assert.That(grid.Color, Is.EqualTo(color));
            });
        }

        public static object[] CreateGrid_WithColorAndSize_Correct_Cases =
        {
            new object[] { 1, new Color()}
        };
    }
}
