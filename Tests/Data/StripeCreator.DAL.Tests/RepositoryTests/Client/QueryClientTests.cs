using Shouldly;
using StripeCreator.DAL.Repositories;
using StripeCreator.DAL.Tests.Fixtures;
using Xunit;

namespace StripeCreator.DAL.Tests.RepositoryTests.Client
{
    [Collection("ClientCollection")]
    public class QueryClientTests
    {
        private readonly DbClientRepository _clientRepository;

        public QueryClientTests(ClientQueryTextFixture fixture)
        {
            _clientRepository = fixture.ClientRepository;
        }

        [Fact]
        public async Task GetAllClients_Success()
        {
            var clients = await _clientRepository.GetAllAsync();

            clients.ShouldNotBeNull();
            clients.Count().ShouldBe(ClientQueryTextFixture.Clients.Length);
        }

        [Fact]
        public async Task GetClientById_Success()
        {
            var storedClient = ClientQueryTextFixture.Clients[0];
            var receivedClient = await _clientRepository.GetByIdAsync(storedClient.Id!.Value);

            receivedClient.ShouldNotBeNull();
            receivedClient.Id.ShouldBe(storedClient.Id);
            receivedClient.ContactData.ContactNumber.ShouldBe(storedClient.ContactNumber);
            receivedClient.ContactData.Email.ShouldBe(storedClient.Email);
            receivedClient.ContactData.Other.ShouldBe(storedClient.Other);
            receivedClient.PersonData.FirstName.ShouldBe(storedClient.FirstName);
            receivedClient.PersonData.SecondName.ShouldBe(storedClient.SecondName);
        }

        [Fact]
        public async Task GetClientById_NotFound()
        {
            var receivedClient = await _clientRepository.GetByIdAsync(Guid.NewGuid());
            receivedClient.ShouldBeNull();
        }


        [Fact]
        public void GetClientsSkipNegative_Fail()
        {
            int skipCount = -1;
            int takeCount = 5;

            Should.ThrowAsync<ArgumentOutOfRangeException>(_clientRepository.GetAsync(skipCount, takeCount));
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void GetClientsTakeIncorrect_Fail(int takeCount)
        {
            int skipCount = 5;

            Should.ThrowAsync<ArgumentOutOfRangeException>(_clientRepository.GetAsync(skipCount, takeCount));
        }

        [Fact]
        public async Task GetClients_Success()
        {
            var skip = 1;
            var take = 2;
            var clients = await _clientRepository.GetAsync(skip, take);

            clients.ShouldNotBeNull();
            clients.Count().ShouldBeLessThanOrEqualTo(take);
            clients.ShouldNotContain(client => client.Id == ClientQueryTextFixture.Clients[0].Id);
        }
    }
}
