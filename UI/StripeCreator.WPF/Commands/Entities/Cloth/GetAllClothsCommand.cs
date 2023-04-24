using StripeCreator.Stripe.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StripeCreator.WPF
{
    public class GetAllClothsCommand : BaseCommand
    {
        private readonly IClothRepository _clothRepository;

        public Action<IEnumerable<IEntityViewModel>>? DataLoaded { get; set; }
        public Action<string>? DataLoadingError { get; set; }

        public GetAllClothsCommand(IClothRepository clothRepository)
        {
            _clothRepository = clothRepository;
        }

        public override bool CanExecute(object? parameter) => true;

        public override void Execute(object? parameter)
        {
            Task.Run(async () =>
            {
                try
                {
                    var data = await _clothRepository.GetAllAsync();
                    DataLoaded?.Invoke(data.Select(cloth => new ClothViewModel(cloth)));
                }
                catch (Exception ex)
                {
                    DataLoadingError?.Invoke(ex.Message);
                }
            });
        }
    }
}
