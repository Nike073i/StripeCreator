using StripeCreator.Core.Models;
using StripeCreator.Stripe.Models;
using Thread = StripeCreator.Stripe.Models.Thread;

namespace StripeCreator.Stripe.Tests.Infrastructure.Helpers.Models
{
    internal class ThreadHelper
    {
        public const string TestName = "Ткань";
        public const decimal TestPrice = 150m;
        public const string TestManufacturer = "Производитель";
        public const ThreadType TestType = ThreadType.Muline;
        public static readonly Color TestColor = new();
        public const int TestWeight = 40;
        public static readonly Guid TestId = new Guid("d4fe42a5-74e7-4aea-b0ab-8b02b1f5115b");

        public static Thread CreateThread(string? name = null, decimal? price = null, string? manufacturer = null,
            Color? color = null, ThreadType? threadType = null, int? weight = null, Guid? id = null) =>
            new(name ?? TestName, price ?? TestPrice, manufacturer ?? TestManufacturer, color ?? TestColor, threadType ?? TestType, weight ?? TestWeight, id ?? TestId);
    }
}
