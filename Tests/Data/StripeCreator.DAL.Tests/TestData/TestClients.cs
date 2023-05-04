using StripeCreator.DAL.Models;

namespace StripeCreator.DAL.Tests.TestData
{
    public static class TestClients
    {
        public static readonly Guid Client1Id = Guid.Parse("59DA4920-5326-4662-AC71-591B25E4B5C7");
        public static readonly Guid Client2Id = Guid.Parse("654417EE-37B9-4CCC-9F59-5128ADCDBE9E");
        public static readonly Guid Client3Id = Guid.Parse("2EA4468D-5242-40CA-B0D5-379C52EC7DF4");
        public static readonly Guid Client4Id = Guid.Parse("FE55FAFC-B6EC-466A-BB23-136262FA045C");

        public static DbClient Client1 => new(
            firstName: "Никита",
            secondName: "Филиппов",
            contactNumber: "+79879654521",
            email: "filippov@mail.ru",
            other: "vk.com/id242866105",
            id: Client1Id
        );

        public static DbClient Client2 => new(
            firstName: "Иван",
            secondName: "Иванов",
            contactNumber: "+79878965458",
            email: "Ivanov@mail.ru",
            other: "ok.ru/id62362623",
            id: Client2Id
        );

        public static DbClient Client3 => new(
            firstName: "Кирилл",
            secondName: "Кириллов",
            contactNumber: "+79854589654",
            email: "kiril@mail.ru",
            other: null,
            id: Client3Id
        );

        public static DbClient Client4 => new(
            firstName: "Денис",
            secondName: "Григорьев",
            contactNumber: "+78454965458",
            email: "karandash@mail.ru",
            other: null,
            id: Client4Id
        );
    }
}
