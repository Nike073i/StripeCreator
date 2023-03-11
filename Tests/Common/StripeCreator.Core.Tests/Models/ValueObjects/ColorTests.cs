using NUnit.Framework;
using StripeCreator.Core.Models;

namespace StripeCreator.Core.Tests.Models.ValueObjects
{
    internal class ColorTests
    {
        [Test]
        [TestCase("#000000")]
        [TestCase("#000000FF")]
        [TestCase("#000000ff")]
        [TestCase("#ffffff")]
        [TestCase("#ffffffff")]
        public void CreateColor_ByHexString_Correct(string colorHex)
        {
            var newColor = new Color(colorHex);
            Assert.That(newColor.HexValue, Is.EqualTo(colorHex));
        }

        [Test]
        public void CreateColor_WithoutParam_Correct()
        {
            var newColor = new Color();
            Assert.That(newColor.HexValue, Is.EqualTo(Color.DefaultColor));
        }

        [Test]
        [TestCase("000000")]
        [TestCase("abcdefg")]
        [TestCase("abcdefghi")]
        [TestCase("#abcdefg")]
        [TestCase("#abcdefghi")]
        [TestCase("#ABCDEFG")]
        [TestCase("#ABCDEFGHI")]
        public void CreateColor_ByHexString_IncorrectParam(string colorHex)
        {
            var newColor = new Color(colorHex);
            Assert.That(newColor.HexValue, Is.EqualTo(Color.DefaultColor));
        }

        [Test]
        [TestCaseSource(nameof(Get_ColorChannels_ReturnByte_Cases))]
        public void Get_ColorChannels_ReturnByte(Color color, byte[] expectedBytes)
        {
            var alpha = color.Alpha;
            var red = color.Red;
            var green = color.Green;
            var blue = color.Blue;
            Assert.Multiple(() =>
            {
                Assert.That(alpha, Is.EqualTo(expectedBytes[0]));
                Assert.That(red, Is.EqualTo(expectedBytes[1]));
                Assert.That(green, Is.EqualTo(expectedBytes[2]));
                Assert.That(blue, Is.EqualTo(expectedBytes[3]));
            });
        }

        static readonly object[] Get_ColorChannels_ReturnByte_Cases =
        {
            new object[] { new Color("#ABCDEE"), new byte[] { 0xFF, 0xAB, 0xCD, 0xEE } },
            new object[] { new Color("#000000"), new byte[] { 0xFF, 0, 0, 0} },
            new object[] { new Color("#00ABCDEE"), new byte[] { 0, 0xAB, 0xCD, 0xEE } },
            new object[] { new Color("#11223344"), new byte[] { 0x11, 0x22, 0x33, 0x44 } },
        };

        [Test]
        [TestCaseSource(nameof(Equals_Colors_ReturnTrue_Cases))]
        public void Equals_Colors_ReturnTrue(Color a, Color b) => Assert.That(b, Is.EqualTo(a));

        static readonly object[] Equals_Colors_ReturnTrue_Cases =
        {
            new object[] { new Color("#ABCDEE"), new Color("#ABCDEE") },
            new object[] { new Color("#abcdee"), new Color("#ABCDEE") },
            new object[] { new Color("#ABCDEEFF"), new Color("#ABCDEEFF") },
            new object[] { new Color("#abcdeeff"), new Color("#ABCDEEff") },
            new object[] { new Color("#aBcDeEfF"), new Color("#ABCDEEff") },
        };

        [Test]
        [TestCaseSource(nameof(Equals_Colors_ReturnFalse_Cases))]
        public void Equals_Colors_ReturnFalse(Color a, Color b) => Assert.That(b, Is.Not.EqualTo(a));

        static readonly object[] Equals_Colors_ReturnFalse_Cases =
        {
            new object[] { new Color("#ABCDEE"), new Color("#000000") },
            new object[] { new Color("#abcdee"), new Color("#000000") },
            new object[] { new Color("#aBcDeE"), new Color("#000000") },
        };
    }
}
