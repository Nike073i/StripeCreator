using StripeCreator.Business.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StripeCreator.WPF
{
    public class GetAllProductsCommand : BaseCommand
    {
        private readonly IProductRepository _productRepository;

        public Action<IEnumerable<IEntityViewModel>>? DataLoaded { get; set; }
        public Action<string>? DataLoadingError { get; set; }

        public GetAllProductsCommand(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public override bool CanExecute(object? parameter) => true;

        public override void Execute(object? parameter)
        {
            Task.Run(async () =>
            {
                try
                {
                    var data = await _productRepository.GetAllAsync();
                    DataLoaded?.Invoke(data.Select(product => new ProductViewModel(product)));
                }
                catch (Exception ex)
                {
                    DataLoadingError?.Invoke(ex.Message);
                }
            });
        }
    }
}
