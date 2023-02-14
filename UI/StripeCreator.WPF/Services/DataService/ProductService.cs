using StripeCreator.Business.Models;
using StripeCreator.Business.Repositories;
using System;

namespace StripeCreator.WPF
{
    /// <summary>
    /// Сервис для работы с ViewModel сущности <see cref="Product"/>
    /// </summary>
    public class ProductService : EntityService<Product>
    {
        #region Constructors

        /// <summary>
        /// Конструктор с полной инициализацией
        /// </summary>
        /// <param name="repository">Репозиторий сущности</param>
        public ProductService(IProductRepository repository) : base(repository) { }

        #endregion

        #region Implementing Abstract Methods

        protected override IEntityViewModel CreateViewModel(Product entity) => new ProductViewModel(entity);

        protected override Product GetEntityFromViewModel(IEntityViewModel viewModel)
        {
            if (viewModel is not ProductViewModel productViewModel)
                throw new ArgumentException($"Указанная ViewModel не является ViewModel сущности продукта");
            return productViewModel.Entity;
        }

        public override EntityFormationViewModel CreateFormationViewModel(IEntityViewModel? entity = null) =>
            entity == null ? new ProductFormationViewModel() : new ProductFormationViewModel(GetEntityFromViewModel(entity));

        #endregion
    }
}
