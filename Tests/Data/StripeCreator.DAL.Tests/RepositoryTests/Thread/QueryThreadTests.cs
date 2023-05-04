using Shouldly;
using StripeCreator.DAL.Repositories;
using StripeCreator.DAL.Tests.Fixtures;
using Xunit;

namespace StripeCreator.DAL.Tests.RepositoryTests.Thread
{
    [Collection("ThreadCollection")]
    public class QueryThreadTests
    {
        private readonly DbThreadRepository _threadRepository;

        public QueryThreadTests(ThreadQueryTextFixture fixture)
        {
            _threadRepository = fixture.ThreadRepository;
        }

        [Fact]
        public async Task GetAllThreads_Success()
        {
            var threads = await _threadRepository.GetAllAsync();

            threads.ShouldNotBeNull();
            threads.Count().ShouldBe(ThreadQueryTextFixture.Threads.Length);
        }

        [Fact]
        public async Task GetThreadById_Success()
        {
            var storedThread = ThreadQueryTextFixture.Threads[0];
            var receivedThread = await _threadRepository.GetByIdAsync(storedThread.Id!.Value);

            receivedThread.ShouldNotBeNull();
            receivedThread.Id.ShouldBe(storedThread.Id);
            receivedThread.Name.ShouldBe(storedThread.Name);
            receivedThread.Manufacturer.ShouldBe(storedThread.Manufacturer);
            receivedThread.Price.ShouldBe(storedThread.Price);
            receivedThread.Type.ShouldBe(storedThread.Type);
            receivedThread.Weight.ShouldBe(storedThread.Weight);
            receivedThread.Color.HexValue.ShouldBe(storedThread.ColorHex);
        }

        [Fact]
        public async Task GetThreadById_NotFound()
        {
            var receivedThread = await _threadRepository.GetByIdAsync(Guid.NewGuid());
            receivedThread.ShouldBeNull();
        }


        [Fact]
        public void GetThreadsSkipNegative_Fail()
        {
            int skipCount = -1;
            int takeCount = 5;

            Should.ThrowAsync<ArgumentOutOfRangeException>(_threadRepository.GetAsync(skipCount, takeCount));
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void GetThreadsTakeIncorrect_Fail(int takeCount)
        {
            int skipCount = 5;

            Should.ThrowAsync<ArgumentOutOfRangeException>(_threadRepository.GetAsync(skipCount, takeCount));
        }

        [Fact]
        public async Task GetThreads_Success()
        {
            var skip = 1;
            var take = 2;
            var threads = await _threadRepository.GetAsync(skip, take);

            threads.ShouldNotBeNull();
            threads.Count().ShouldBeLessThanOrEqualTo(take);
            threads.ShouldNotContain(thread => thread.Id == ThreadQueryTextFixture.Threads[0].Id);
        }
    }
}
