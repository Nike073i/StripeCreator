using StripeCreator.DAL.Models;

namespace StripeCreator.DAL.Tests.TestData
{
    public static class TestProducts
    {
        public static readonly Guid Product1Id = Guid.Parse("262720F5-EF77-4759-B48F-D3DDEAF17613");
        public static readonly Guid Product2Id = Guid.Parse("B0BE1E80-86DD-4A2E-97A2-BA72151252A2");
        public static readonly Guid Product3Id = Guid.Parse("1EB8A249-9E9E-479B-8AA6-E3ED94DE713B");

        public static DbProduct Product1 => new(
            id: Product1Id,
            name: "Вышивка Натюрморт с гранатами",
            price: 3900m,
            description: "Гранат – символ плодородия и изобилия, возрождения и бессмертия, любви и брака.\r\nЦветок граната – знак верной совершенной дружбы, единения.\r\nВышивка ручная картина \"Натюрморт с гранатами\".\r\nРазмер вышитого полотна 35х24 см. Размер с рамой - 44х33 см.\r\nВышивка оформлена в багетной мастерской в пластиковую раму с двойным картонным паспорту без стекла."
        );

        public static DbProduct Product2 => new(
            id: Product2Id,
            name: "Вышитая картина \"Портрет Одри Хепберн\"",
            price: 12000m,
            description: "Портрет очаровательной Одри Хепберн станет прекрасным украшением Вашего дома. Приятные теплые оттенки делают картину похожей на старую фотографию,что непременно оценят любители черно-белых фильмов.\r\n\r\nКартина оформлена в багетной мастерской"
        );

        public static DbProduct Product3 => new(
            id: Product3Id,
            name: "Вышитая бисером картина \"Нежные розы\"",
            price: 6900m,
            description: "Роза не зря зовется королевой цветов. Её волшебная красота всегда привлекала внимание человека. Какими только эпитетами её не награждали - нежная, очаровательная, неповторимая... Без рамки"
        );
    }
}
