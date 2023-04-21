using StripeCreator.Stripe.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StripeCreator.WPF
{
    public class GetAllThreadsCommand : BaseCommand
    {
        private readonly IThreadRepository _threadRepository;

        public event EventHandler<IEnumerable<IEntityViewModel>>? DataLoaded;
        public event EventHandler<string>? DataLoadingError;

        public GetAllThreadsCommand(IThreadRepository threadRepository)
        {
            _threadRepository = threadRepository;
        }

        public override bool CanExecute(object? parameter) => true;

        public override void Execute(object? parameter)
        {
            Task.Run(async () =>
            {
                try
                {
                    var data = await _threadRepository.GetAllAsync();
                    DataLoaded?.Invoke(this, data.Select(thread => new ThreadViewModel(thread)));
                }
                catch (Exception ex)
                {
                    DataLoadingError?.Invoke(this, ex.Message);
                }
            });
        }
    }
}
