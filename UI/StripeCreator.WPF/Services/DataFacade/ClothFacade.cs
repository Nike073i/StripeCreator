using StripeCreator.Stripe.Models;
using StripeCreator.Stripe.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StripeCreator.WPF
{
    /// <summary>
    /// Фасад для работы с ViewModel сущности <see cref="Cloth"/>
    /// </summary>
    public class ClothFacade : EntityFacade<Cloth>
    {
        #region Private fields 

        /// <summary>
        /// Сервис работы с тканями
        /// </summary>
        private readonly ClothService _clothService;

        /// <summary>
        /// Менеджер интерактивного взаимодействия
        /// </summary>
        private readonly IUiManager _uiManager;

        #endregion

        #region Constructors

        public ClothFacade(ClothService clothService, IUiManager uiManager)
        {
            _clothService = clothService;
            _uiManager = uiManager;
        }

        #endregion

        #region Implementing abstract methods

        protected override async Task<IEnumerable<Cloth>> GetAllEntities() => await _clothService.GetAllAsync();

        protected override async Task<Guid> RemoveAsync(Guid entityId) => await _clothService.RemoveAsync(entityId);

        protected override async Task<Cloth> CreateEntityAsync(IEntityViewModel entityViewModel)
        {
            if (entityViewModel is not ClothViewModel clothViewModel)
                throw new ArgumentException("Получена модель, отличная от модели ткани", nameof(entityViewModel));
            return await _clothService.SaveAsync(clothViewModel.Entity);
        }

        protected override Task<Cloth> EditEntityAsync(IEntityViewModel entityViewModel) => CreateEntityAsync(entityViewModel);

        protected override IEntityViewModel CreateViewModel(Cloth entity) => new ClothViewModel(entity);

        protected override async Task<IEntityViewModel?> FormData(IEntityViewModel? entityViewModel = null)
        {
            EntityFormationViewModel? formationViewModel;
            if (entityViewModel == null)
                formationViewModel = new ClothFormationViewModel();
            else
            {
                if (entityViewModel is not ClothViewModel clothViewModel)
                    throw new ArgumentException("Получена модель, отличная от модели ткани", nameof(entityViewModel));
                formationViewModel = new ClothFormationViewModel(clothViewModel.Entity);
            }
            return await _uiManager.FormationEntity(formationViewModel);
        }

        #endregion
    }
}
