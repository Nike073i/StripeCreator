using StripeCreator.Business.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StripeCreator.WPF
{
    public class GetAllClientsCommand : BaseCommand
    {
        private readonly IClientRepository _clientRepository;

        public Action<IEnumerable<IEntityViewModel>>? DataLoaded { get; set; }
        public Action<string>? DataLoadingError { get; set; }

        public GetAllClientsCommand(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public override bool CanExecute(object? parameter) => true;

        public override void Execute(object? parameter)
        {
            Task.Run(async () =>
            {
                try
                {
                    var data = await _clientRepository.GetAllAsync();
                    DataLoaded?.Invoke(data.Select(client => new ClientViewModel(client)));
                }
                catch (Exception ex)
                {
                    DataLoadingError?.Invoke(ex.Message);
                }
            });
        }
    }
}
