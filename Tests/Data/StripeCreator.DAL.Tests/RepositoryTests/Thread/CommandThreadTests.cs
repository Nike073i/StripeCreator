using Shouldly;
using StripeCreator.Core.Models;
using StripeCreator.DAL.Exceptions;
using StripeCreator.DAL.Models;
using StripeCreator.DAL.Repositories;
using StripeCreator.DAL.Tests.Base;
using StripeCreator.DAL.Tests.TestData;
using StripeCreator.Stripe.Models;
using Xunit;
using ThreadModel = StripeCreator.Stripe.Models.Thread;

namespace StripeCreator.DAL.Tests.RepositoryTests.Thread
{
    public class CommandThreadTests : BaseTest
    {
        private readonly DbThreadRepository _repository;

        public CommandThreadTests()
        {
            _repository = new DbThreadRepository(Context);
        }

        [Fact]
        public async Task CreateThread_Success()
        {
            var newThreadModel = MapFrom(TestThreads.Thread1);
            var savedThread = await _repository.SaveAsync(newThreadModel);

            savedThread.ShouldNotBeNull();
            savedThread.Id.ShouldNotBeNull();
            savedThread.Id.ShouldNotBe(Guid.Empty);
        }

        [Fact]
        public async Task EditThread_Success()
        {
            Context.Add(TestThreads.Thread2);
            Context.SaveChanges();
            var storedThread = MapFrom(TestThreads.Thread2, true);
            int newWeight = 15;
            string newManufacter = "Тестовый производитель";
            string newName = "Тестовое название";
            decimal newPrice = 500m;
            var newType = ThreadType.Tapestry;

            storedThread.Weight = newWeight;
            storedThread.Manufacturer = newManufacter;
            storedThread.Name = newName;
            storedThread.Price = newPrice;
            storedThread.Type = newType;
            var changedThread = await _repository.SaveAsync(storedThread);

            changedThread.ShouldNotBeNull();
            changedThread.Id.ShouldBe(storedThread.Id);
            changedThread.Weight.ShouldBe(newWeight);
            changedThread.Manufacturer.ShouldBe(newManufacter);
            changedThread.Name.ShouldBe(newName);
            changedThread.Price.ShouldBe(newPrice);
            changedThread.Type.ShouldBe(newType);
        }

        [Fact]
        public async Task RemoveThread_Success()
        {
            var testThread = TestThreads.Thread3;
            Context.Add(testThread);
            Context.SaveChanges();

            var removeId = await _repository.RemoveAsync(testThread.Id!.Value);
            var threadExists = Context.Find<DbThread>(testThread.Id);

            removeId.ShouldBe(testThread.Id.Value);
            threadExists.ShouldBeNull();
        }

        [Fact]
        public void RemoveThreadNonExists_ThrowException()
        {
            var newGuid = Guid.NewGuid();
            Should.ThrowAsync<EntityNotFoundException>(_repository.RemoveAsync(newGuid));
        }

        private static ThreadModel MapFrom(DbThread thread, bool mapId = false)
        {
            return mapId
                ? (new(
                    name: thread.Name,
                    manufacturer: thread.Manufacturer,
                    price: thread.Price,
                    type: thread.Type,
                    weight: thread.Weight,
                    color: new Color(thread.ColorHex),
                    id: thread.Id))
                : (new(
                    name: thread.Name,
                    manufacturer: thread.Manufacturer,
                    price: thread.Price,
                    type: thread.Type,
                    color: new Color(thread.ColorHex),
                    weight: thread.Weight));
        }
    }
}
