using StripeCreator.Stripe.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Thread = StripeCreator.Stripe.Models.Thread;

namespace StripeCreator.WPF
{
    /// <summary>
    /// Сервис для работы с ViewModel сущности <see cref="Thread"/>
    /// </summary>
    public class ThreadFacade : EntityFacade<Thread>
    {
        #region Private fields 

        /// <summary>
        /// Сервис работы с нитями
        /// </summary>
        private readonly ThreadService _threadService;

        /// <summary>
        /// Менеджер интерактивного взаимодействия
        /// </summary>
        private readonly IUiManager _uiManager;

        #endregion

        #region Constructors

        public ThreadFacade(ThreadService threadService, IUiManager uiManager)
        {
            _threadService = threadService;
            _uiManager = uiManager;
        }

        #endregion

        #region Implementing abstract methods

        protected override async Task<IEnumerable<Thread>> GetAllEntities() => await _threadService.GetAllAsync();

        protected override async Task<Guid> RemoveAsync(Guid entityId) => await _threadService.RemoveAsync(entityId);

        protected override async Task<Thread> CreateEntityAsync(IEntityViewModel entityViewModel)
        {
            if (entityViewModel is not ThreadViewModel threadViewModel)
                throw new ArgumentException("Получена модель, отличная от модели нити", nameof(entityViewModel));
            return await _threadService.SaveAsync(threadViewModel.Entity);
        }

        protected override Task<Thread> EditEntityAsync(IEntityViewModel entityViewModel) => CreateEntityAsync(entityViewModel);

        protected override IEntityViewModel CreateViewModel(Thread entity) => new ThreadViewModel(entity);

        protected override async Task<IEntityViewModel?> FormData(IEntityViewModel? entityViewModel = null)
        {
            EntityFormationViewModel? formationViewModel;
            if (entityViewModel == null)
                formationViewModel = new ThreadFormationViewModel();
            else
            {
                if (entityViewModel is not ThreadViewModel threadViewModel)
                    throw new ArgumentException("Получена модель, отличная от модели нити", nameof(entityViewModel));
                formationViewModel = new ThreadFormationViewModel(threadViewModel.Entity);
            }
            return await _uiManager.FormationEntity(formationViewModel);
        }

        #endregion
    }
}
