using NUnit.Framework;
using StripeCreator.Business.Models;

namespace StripeCreator.Business.Tests.Models.ValueObjects
{
    internal class ContactDataTests
    {
        [Test]
        [TestCase("+79999999999", "example@mail.ru")]
        [TestCase("+79999999999", "example@mail.ru", "Пример описания")]
        public void Create_ContactData_Correct(string contactNumber, string email, string? other = null)
        {
            var contactData = new ContactData(contactNumber, email, other);
            Assert.Multiple(() =>
            {
                Assert.That(contactData.ContactNumber, Is.EqualTo(contactNumber));
                Assert.That(contactData.Email, Is.EqualTo(email));
                Assert.That(contactData.Other, Is.EqualTo(other));
            });
        }

        [Test]
        [TestCase("79999999999", "example@mail.ru")]
        [TestCase("+89999999999", "example@mail.ru")]
        [TestCase("89999999999", "example@mail.ru")]
        [TestCase("+799999999999", "example@mail.ru")]
        [TestCase("+79999999999", "@mail.ru")]
        [TestCase("+79999999999", "examplemail.ru")]
        [TestCase("+79999999999", "examplemailru")]
        [TestCase("+79999999999", "example@m.ru")]
        [TestCase("+79999999999", "example@Mail.ru")]
        public void Create_ContactData_IncorrectParams(string contactNumber, string email)
        {
            void construct() => new ContactData(contactNumber, email);
            Assert.Throws<ArgumentException>(construct);
        }

        [Test]
        [TestCaseSource(nameof(Equals_ContactData_ReturnTrue_Cases))]
        public void Equals_ContactData_ReturnTrue(ContactData a, ContactData b) => Assert.That(b, Is.EqualTo(a));

        static readonly object[] Equals_ContactData_ReturnTrue_Cases =
        {
            new object[] { new ContactData("+79999999999", "example@mail.ru"), new ContactData("+79999999999", "example@mail.ru") },
            new object[] { new ContactData("+79999999999", "Example@mail.ru"), new ContactData("+79999999999", "example@mail.ru") },
            new object[] { new ContactData("+79999999999", "example@mail.ru","Описание"), new ContactData("+79999999999", "example@mail.ru", "Описание") },
        };

        [Test]
        [TestCaseSource(nameof(Equals_ContactData_ReturnFalse_Cases))]
        public void Equals_ContactData_ReturnFalse(ContactData a, ContactData b) => Assert.That(b, Is.Not.EqualTo(a));

        static readonly object[] Equals_ContactData_ReturnFalse_Cases =
        {
            new object[] { new ContactData("+79999999999", "example@mail.ru"), new ContactData("+79999999999", "other@mail.ru") },
            new object[] { new ContactData("+79999999999", "example@mail.ru"), new ContactData("+78888888888", "example@mail.ru") },
            new object[] { new ContactData("+79999999999", "example@mail.ru", "Описание"), new ContactData("+79999999999", "example@mail.ru") },
            new object[] { new ContactData("+79999999999", "example@mail.ru", "Описание"), new ContactData("+79999999999", "example@mail.ru", "Описание2") },
        };
    }
}
