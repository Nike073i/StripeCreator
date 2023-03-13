using StripeCreator.Business.Models;

namespace StripeCreator.Business.Tests.Infrastructure.Helpers.Models
{
    internal static class ClientHelper
    {
        public static readonly PersonData TestPersonData = PersonDataHelper.CreatePersonData();
        public static readonly ContactData TestContactData = ContactDataHelper.CreateContactData();
        public static readonly Guid TestId = new("e9e43c3f-cb55-4877-854f-b92263948506");

        public static Client CreateClient(PersonData? personData = null, ContactData? contactData = null, Guid? id = null) =>
            new(personData ?? TestPersonData, TestContactData, id ?? TestId);
    }
}
