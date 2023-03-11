using StripeCreator.Business.Models;

namespace StripeCreator.Business.Tests.Infrastructure.Helpers.Models
{
    internal static class ContactDataHelper
    {
        public const string TestContactNumber = "+79475912545";
        public const string TestEmail = "kit013i@mipl.ry";
        public static ContactData CreateContactData(string contactNumber = TestContactNumber, string email = TestEmail) =>
            new(contactNumber, email);
    }
}
