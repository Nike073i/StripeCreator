using StripeCreator.Core.Models;
using StripeCreator.Stripe.Models;

namespace StripeCreator.Stripe.Tests.Infrastructure.Helpers.Models
{
    internal class ClothHelper
    {
        public const string TestName = "Ткань";
        public const decimal TestPrice = 150m;
        public const string TestManufacturer = "Производитель";
        public const ClothType TestType = ClothType.Aida;
        public static readonly Color TestColor = new();
        public const int TestCount = 71;
        public static readonly Guid TestId = new Guid("d4fe42a5-74e7-4aea-b0ab-8b02b1f5115b");

        public static Cloth CreateCloth(string? name = null, decimal? price = null, string? manufacturer = null,
            Color? color = null, ClothType? clothType = null, int? count = null, Guid? id = null) =>
            new(name ?? TestName, price ?? TestPrice, manufacturer ?? TestManufacturer, color ?? TestColor, clothType ?? TestType, count ?? TestCount, id ?? TestId);
    }
}
