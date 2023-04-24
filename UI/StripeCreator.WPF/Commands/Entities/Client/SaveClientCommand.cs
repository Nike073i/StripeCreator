using StripeCreator.Business.Repositories;
using System;
using System.Threading.Tasks;

namespace StripeCreator.WPF
{
    public class SaveClientCommand : BaseCommand
    {
        private readonly IClientRepository _clientRepository;

        public Action<IEntityViewModel>? DataSaved { get; set; }
        public Action<string>? DataSavingError { get; set; }

        public SaveClientCommand(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public override bool CanExecute(object? parameter) => parameter is ClientViewModel;

        public override void Execute(object? parameter)
        {
            if (parameter is not ClientViewModel clientViewModel)
                throw new InvalidOperationException();
            Task.Run(async () =>
            {
                try
                {
                    var savedEntity = await _clientRepository.SaveAsync(clientViewModel.Entity);
                    DataSaved?.Invoke(new ClientViewModel(savedEntity));
                }
                catch (Exception ex)
                {
                    DataSavingError?.Invoke(ex.Message);
                }
            });
        }
    }
}
