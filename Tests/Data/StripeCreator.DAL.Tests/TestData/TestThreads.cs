using StripeCreator.DAL.Models;
using StripeCreator.Stripe.Models;

namespace StripeCreator.DAL.Tests.TestData
{
    public static class TestThreads
    {
        public static DbThread Thread1 => new(
            id: Guid.Parse("6385B852-6AC7-4BD5-BDD3-1854E60B536F"),
            weight: 30,
            type: ThreadType.Universal,
            name: "Нитки EURON №120 Красные",
            price: 0.4m,
            manufacturer: "КНР",
            colorHex: "#FFFF0000"
        );

        public static DbThread Thread2 => new(
            id: Guid.Parse("BCC1AB25-FBD2-402E-8647-3832A38C33F7"),
            weight: 27,
            type: ThreadType.Tapestry,
            name: "Нитки Belfil S 120 Зеленые",
            price: 0.6m,
            manufacturer: "Belfis",
            colorHex: "#FF008000"
        );

        public static DbThread Thread3 => new(
            id: Guid.Parse("E0A0595D-E846-428A-A2E2-3B433686CA84"),
            weight: 39,
            type: ThreadType.Muline,
            name: "Нитки Мулине 102 Синие",
            price: 1.5m,
            manufacturer: "DMC",
            colorHex: "#FF0000FF"
        );

        public static DbThread Thread4 => new(
            id: Guid.Parse("81C4B86C-D207-4026-BBF2-BE659EF43195"),
            weight: 28,
            type: ThreadType.Silk,
            name: "Нитки Safira 120 Желтые",
            price: 0.06m,
            manufacturer: "Silko",
            colorHex: "#FFFFFF00"
        );
    }
}
