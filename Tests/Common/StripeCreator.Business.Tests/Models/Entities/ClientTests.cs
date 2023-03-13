using NUnit.Framework;
using StripeCreator.Business.Models;
using StripeCreator.Business.Tests.Infrastructure.Helpers.Models;

namespace StripeCreator.Business.Tests.Models.Entities
{
    internal class ClientTests
    {
        [Test]
        [TestCaseSource(nameof(Create_Client_Correct_Cases))]
        public void Create_Client_Correct(PersonData personData, ContactData contactData, Guid? id = null)
        {
            var newClient = new Client(personData, contactData, id);
            Assert.Multiple(() =>
            {
                Assert.That(newClient.PersonData, Is.EqualTo(personData));
                Assert.That(newClient.ContactData, Is.EqualTo(contactData));
                Assert.That(newClient.Id, Is.EqualTo(id));
            });
        }

        static readonly object?[] Create_Client_Correct_Cases =
        {
            new object?[] { ClientHelper.TestPersonData, ClientHelper.TestContactData, null },
            new object[] { ClientHelper.TestPersonData, ClientHelper.TestContactData, ClientHelper.TestId },
        };
    }
}
