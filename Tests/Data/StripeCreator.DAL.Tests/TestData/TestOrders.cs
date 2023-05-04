using StripeCreator.Business.Enums;
using StripeCreator.DAL.Models;

namespace StripeCreator.DAL.Tests.TestData
{
    public static class TestOrders
    {
        public static readonly Guid Order1Id = Guid.Parse("36187E29-9537-445D-831A-5D6A20A37FE6");
        public static readonly Guid Order2Id = Guid.Parse("76183777-D13A-4A1C-86FE-FD903A120957");
        public static readonly Guid Order3Id = Guid.Parse("159F1E28-A9EC-4769-BA0A-CB6290A80032");
        public static readonly Guid Order4Id = Guid.Parse("B276B3B8-74CF-438A-9D7F-1A70EB4C8820");
        public static readonly Guid Order5Id = Guid.Parse("5DB35F43-7ADD-4D0A-A882-B1B78031B867");
        public static readonly Guid Order6Id = Guid.Parse("18CBF713-7124-4FBC-9B14-0639FC58D49E");

        public static readonly DbClient Order1Client = TestClients.Client1;
        public static readonly DbClient Order2Client = TestClients.Client2;
        public static readonly DbClient Order3Client = TestClients.Client4;

        public static DbOrder Order1 => new(
            id: Order1Id,
            clientId: Order1Client.Id!.Value,
            products: TestOrderProducts.Order1Products,
            price: 27900m,
            contactNumber: "+79879654521",
            email: "filippov@mail.ru",
            other: "vk.com/id242866105",
            status: OrderStatus.Processed,
            dateCreated: DateTime.Parse("2023-03-19 14:41:32.8263236")
        );

        public static DbOrder Order2 => new(
            id: Order2Id,
            clientId: Order2Client.Id!.Value,
            products: TestOrderProducts.Order2Products,
            price: 36000m,
            contactNumber: "+79878965458",
            email: "Ivanov@mail.ru",
            other: "ok.ru/id62362623",
            status: OrderStatus.Paid,
            dateCreated: DateTime.Parse("2023-03-20 14:41:42.32329")
        );

        public static DbOrder Order3 => new(
            id: Order3Id,
            clientId: Order3Client.Id!.Value,
            products: TestOrderProducts.Order3Products,
            price: 27600m,
            contactNumber: "+78454965458",
            email: "karandash@mail.ru",
            other: null,
            status: OrderStatus.Sent,
            dateCreated: DateTime.Parse("2023-03-20 14:41:50.9706188")
        );

        public static DbOrder Order4 => new(
            id: Order4Id,
            clientId: TestClients.Client3Id,
            products: TestOrderProducts.Order4Products,
            price: 50700m,
            contactNumber: "+79854589654",
            email: "kiril@mail.ru",
            other: null,
            status: OrderStatus.Paid,
            dateCreated: DateTime.Parse("2023-03-21 14:42:07.4279375")
        );

        public static DbOrder Order5 => new(
            id: Order5Id,
            clientId: TestClients.Client2Id,
            products: TestOrderProducts.Order5Products,
            price: 39000m,
            contactNumber: "+79287899999",
            email: "Ivanov@mail.ru",
            other: "ok.ru/id62362623",
            status: OrderStatus.Paid,
            dateCreated: DateTime.Parse("2023-03-22 14:43:22.2680802")
        );

        public static DbOrder Order6 => new(
            id: Order6Id,
            clientId: TestClients.Client1Id,
            products: TestOrderProducts.Order6Products,
            price: 19500m,
            contactNumber: "+79879654521",
            email: "filippov@mail.ru",
            other: "vk.com/id242866105",
            status: OrderStatus.Paid,
            dateCreated: DateTime.Parse("2023-03-23 10:13:05.5940295")
        );
    }
}
