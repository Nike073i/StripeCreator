using StripeCreator.Business.Models;
using StripeCreator.Business.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StripeCreator.WPF
{
    /// <summary>
    /// Сервис для работы с ViewModel сущности <see cref="Product"/>
    /// </summary>
    public class ProductFacade : EntityFacade<Product>
    {
        #region Private fields 

        /// <summary>
        /// Сервис работы с продукцией
        /// </summary>
        private readonly ProductService _productService;

        /// <summary>
        /// Менеджер интерактивного взаимодействия
        /// </summary>
        private readonly IUiManager _uiManager;

        #endregion

        #region Constructors

        public ProductFacade(ProductService productService, IUiManager uiManager)
        {
            _productService = productService;
            _uiManager = uiManager;
        }

        #endregion

        #region Implementing abstract methods

        protected override async Task<IEnumerable<Product>> GetAllEntities() => await _productService.GetAllAsync();

        protected override async Task<Guid> RemoveAsync(Guid entityId) => await _productService.RemoveAsync(entityId);

        protected override async Task<Product> CreateEntityAsync(IEntityViewModel entityViewModel)
        {
            if (entityViewModel is not ProductViewModel productViewModel)
                throw new ArgumentException("Получена модель, отличная от модели продукции", nameof(entityViewModel));
            return await _productService.SaveAsync(productViewModel.Entity);
        }

        protected override Task<Product> EditEntityAsync(IEntityViewModel entityViewModel) => CreateEntityAsync(entityViewModel);

        protected override IEntityViewModel CreateViewModel(Product entity) => new ProductViewModel(entity);

        protected override async Task<IEntityViewModel?> FormData(IEntityViewModel? entityViewModel = null)
        {
            EntityFormationViewModel? formationViewModel;
            if (entityViewModel == null)
                formationViewModel = new ProductFormationViewModel();
            else
            {
                if (entityViewModel is not ProductViewModel productViewModel)
                    throw new ArgumentException("Получена модель, отличная от модели продукции", nameof(entityViewModel));
                formationViewModel = new ProductFormationViewModel(productViewModel.Entity);
            }
            return await _uiManager.FormationEntity(formationViewModel);
        }

        #endregion
    }
}
