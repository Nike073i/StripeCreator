using StripeCreator.DAL.Models;
using StripeCreator.Stripe.Models;

namespace StripeCreator.DAL.Tests.TestData
{
    public static class TestCloths
    {
        public static DbCloth Cloth1 => new(
            id: Guid.Parse("9A40E904-959C-4FBC-ABA6-177DED940676"),
            count: 55,
            type: ClothType.Plastic,
            name: "Канва KPLA-14 Пластиковая Белая",
            price: 2890m,
            manufacturer: "Gamma",
            colorHex: "#FFFFFF"
        );

        public static DbCloth Cloth2 => new(
            id: Guid.Parse("7A032C4E-83B9-4E5F-8195-1DB89625BF12"),
            count: 55,
            type: ClothType.Aida,
            name: "Канва K14 Хлопок Белая",
            price: 900m,
            manufacturer: "Gamma",
            colorHex: "#FFFFFF"
        );

        public static DbCloth Cloth3 => new(
            id: Guid.Parse("1ED1484C-D1E2-4EF4-B626-6FAB97F24D68"),
            count: 43,
            type: ClothType.Aida,
            name: "Канва K03 Хлопок Белая",
            price: 916m,
            manufacturer: "Gamma",
            colorHex: "#FFFFFF"
        );

        public static DbCloth Cloth4 => new(
            id: Guid.Parse("5975EFFD-D9DA-4F87-8981-F18608BA1727"),
            count: 55,
            type: ClothType.Soluble,
            name: "Канва VRK Растворимая ПВАЛ Черная",
            price: 14725m,
            manufacturer: "VKR",
            colorHex: "#FF000000"
        );
    }
}
