using StripeCreator.Stripe.Models;

namespace StripeCreator.Stripe.Extensions
{
    public static class MaterialExtensions
    {
        public static string ConvertToString(this ClothType clothType)
        {
            return clothType switch
            {
                ClothType.Aida => "Аида",
                ClothType.Uniform => "Универсальная",
                ClothType.Stramin => "Страмин",
                ClothType.Plastic => "Пластиковая",
                ClothType.False => "Накладная",
                ClothType.Soluble => "Растворимая",
                _ => throw new NotImplementedException(),
            };
        }

        public static string ConverToString(this ThreadType type)
        {
            return type switch
            {
                ThreadType.Muline => "Мулине",
                ThreadType.Tapestry => "Гобелен",
                ThreadType.Cotton => "Хлопок",
                ThreadType.Silk => "Шелк",
                ThreadType.Universal => "Универсальная",
                ThreadType.Metallic => "Металлическая",
                _ => throw new NotImplementedException(),
            };
        }
    }
}
