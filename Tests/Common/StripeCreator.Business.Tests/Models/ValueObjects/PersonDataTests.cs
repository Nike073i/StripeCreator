using NUnit.Framework;
using StripeCreator.Business.Models;

namespace StripeCreator.Business.Tests.Models.ValueObjects
{
    internal class PersonDataTests
    {
        [Test]
        [TestCase("Никита", "Филиппов")]
        [TestCase("Павел", "Иванов")]
        public void Create_PersonData_Correct(string firstName, string secondName)
        {
            var personData = new PersonData(firstName, secondName);
            Assert.Multiple(() =>
            {
                Assert.That(personData.FirstName, Is.EqualTo(firstName));
                Assert.That(personData.SecondName, Is.EqualTo(secondName));
            });
        }

        [Test]
        [TestCase("", "Филиппов")]
        [TestCase("Никита", "")]
        public void Create_PersonData_IncorrectParams(string firstName, string secondName)
        {
            void construct() => new PersonData(firstName, secondName);
            Assert.Throws<ArgumentNullException>(construct);
        }

        [Test]
        [TestCaseSource(nameof(Equals_PersonData_ReturnTrue_Cases))]
        public void Equals_PersonData_ReturnTrue(PersonData a, PersonData b) => Assert.That(b, Is.EqualTo(a));

        static readonly object[] Equals_PersonData_ReturnTrue_Cases =
        {
            new object[] { new PersonData("Никита", "Филиппов"), new PersonData("Никита", "Филиппов") },
            new object[] { new PersonData("никита", "филиппов"), new PersonData("Никита", "Филиппов") },
            new object[] { new PersonData("НиКиТа", "ФиЛиПпоВ"), new PersonData("Никита", "Филиппов") },
        };

        [Test]
        [TestCaseSource(nameof(Equals_PersonData_ReturnFalse_Cases))]
        public void Equals_Colors_ReturnFalse(PersonData a, PersonData b) => Assert.That(b, Is.Not.EqualTo(a));

        static readonly object[] Equals_PersonData_ReturnFalse_Cases =
        {
            new object[] { new PersonData("Никита", "Филиппов"), new PersonData("Павел", "Иванов") },
        };
    }
}
