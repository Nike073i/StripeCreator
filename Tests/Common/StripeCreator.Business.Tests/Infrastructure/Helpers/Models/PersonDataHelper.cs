using StripeCreator.Business.Models;

namespace StripeCreator.Business.Tests.Infrastructure.Helpers.Models
{
    internal static class PersonDataHelper
    {
        public const string TestFirstName = "Никита";
        public const string TestSecondName = "Филиппов";
        public static PersonData CreatePersonData(string firstName = TestFirstName, string secondName = TestSecondName) =>
            new(firstName, secondName);
    }
}
