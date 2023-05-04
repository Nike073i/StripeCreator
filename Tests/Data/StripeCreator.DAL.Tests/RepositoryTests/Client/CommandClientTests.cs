using Shouldly;
using StripeCreator.Business.Models;
using StripeCreator.DAL.Exceptions;
using StripeCreator.DAL.Models;
using StripeCreator.DAL.Repositories;
using StripeCreator.DAL.Tests.Base;
using StripeCreator.DAL.Tests.TestData;
using Xunit;
using ClientModel = StripeCreator.Business.Models.Client;

namespace StripeCreator.DAL.Tests.RepositoryTests.Client
{
    public class CommandClientTests : BaseTest
    {
        private readonly DbClientRepository _repository;

        public CommandClientTests()
        {
            _repository = new DbClientRepository(Context);
        }

        [Fact]
        public async Task CreateClient_Success()
        {
            var newClientModel = MapFrom(TestClients.Client1);
            var savedClient = await _repository.SaveAsync(newClientModel);

            savedClient.ShouldNotBeNull();
            savedClient.Id.ShouldNotBeNull();
            savedClient.Id.ShouldNotBe(Guid.Empty);
        }

        [Fact]
        public async Task EditClient_Success()
        {
            Context.Add(TestClients.Client2);
            Context.SaveChanges();
            var storedClient = MapFrom(TestClients.Client2, true);
            var newContactData = new ContactData(
                "+78987898789",
                "text@mail.ru"
            );
            var newPersonData = new PersonData(
                "Тестовое имя",
                "Тестовая фамилия"
            );

            storedClient.ContactData = newContactData;
            storedClient.PersonData = newPersonData;
            var changedClient = await _repository.SaveAsync(storedClient);

            changedClient.ShouldNotBeNull();
            changedClient.Id.ShouldBe(storedClient.Id);
            changedClient.PersonData.ShouldBe(newPersonData);
            changedClient.ContactData.ShouldBe(newContactData);
        }

        [Fact]
        public async Task RemoveClient_Success()
        {
            var testClient = TestClients.Client3;
            Context.Add(testClient);
            Context.SaveChanges();

            var removeId = await _repository.RemoveAsync(testClient.Id!.Value);
            var clientExists = Context.Find<DbClient>(testClient.Id);

            removeId.ShouldBe(testClient.Id.Value);
            clientExists.ShouldBeNull();
        }

        [Fact]
        public void RemoveClientNonExists_ThrowException()
        {
            var newGuid = Guid.NewGuid();
            Should.ThrowAsync<EntityNotFoundException>(_repository.RemoveAsync(newGuid));
        }

        private static ClientModel MapFrom(DbClient client, bool mapId = false)
        {
            var contactData = new ContactData(
                email: client.Email,
                other: client.Other,
                number: client.ContactNumber
            );
            var personData = new PersonData(
                firstName: client.FirstName,
                secondName: client.SecondName
            );
            return mapId
                ? (new(personData, contactData, client.Id))
                : (new(personData, contactData));
        }
    }
}
