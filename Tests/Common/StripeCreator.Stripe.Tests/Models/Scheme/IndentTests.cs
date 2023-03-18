using NUnit.Framework;
using StripeCreator.Core.Models;
using StripeCreator.Stripe.Models;

namespace StripeCreator.Stripe.Tests.Models.Scheme
{
    internal class IndentTests
    {
        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(-10)]
        public void Create_Indent_IncorrectSize(int size)
        {
            void construct() => new Indent(size);
            Assert.Throws<ArgumentOutOfRangeException>(construct);
        }

        [Test]
        [TestCase(1)]
        [TestCase(10)]
        public void CreateIndent_OnlySize_Correct(int size)
        {
            var indent = new Indent(size);
            Assert.Multiple(() =>
            {
                Assert.That(indent.Size, Is.EqualTo(size));
                Assert.That(indent.Color, Is.EqualTo(new Color()));
            });
        }

        [Test]
        [TestCaseSource(nameof(CreateIndent_WithColorAndSize_Correct_Cases))]
        public void CreateIndent_WithColorAndSize_Correct(int size, Color color)
        {
            var indent = new Indent(size, color);
            Assert.Multiple(() =>
            {
                Assert.That(indent.Size, Is.EqualTo(size));
                Assert.That(indent.Color, Is.EqualTo(color));
            });
        }

        public static object[] CreateIndent_WithColorAndSize_Correct_Cases =
        {
            new object[] { 1, new Color()}
        };
    }
}
