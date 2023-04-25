using StripeCreator.Business.Models;
using StripeCreator.Business.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StripeCreator.WPF
{
    /// <summary>
    /// Фасад для работы с сущностью <see cref="Client"/>
    /// </summary>
    public class ClientFacade : EntityFacade<Client>
    {
        #region Private fields 

        /// <summary>
        /// Сервис работы с клиентами
        /// </summary>
        private readonly ClientService _clientService;

        /// <summary>
        /// Менеджер интерактивного взаимодействия
        /// </summary>
        private readonly IUiManager _uiManager;

        #endregion

        #region Constructors

        public ClientFacade(ClientService clientService, IUiManager uiManager)
        {
            _clientService = clientService;
            _uiManager = uiManager;
        }

        #endregion

        #region Implementing abstract methods

        protected override async Task<IEnumerable<Client>> GetAllEntities() => await _clientService.GetAllAsync();

        protected override async Task<Guid> RemoveAsync(Guid entityId) => await _clientService.RemoveAsync(entityId);

        protected override async Task<Client> CreateEntityAsync(IEntityViewModel entityViewModel)
        {
            if (entityViewModel is not ClientViewModel clientViewModel)
                throw new ArgumentException("Получена модель, отличная от модели клиента", nameof(entityViewModel));
            return await _clientService.SaveAsync(clientViewModel.Entity);
        }

        protected override Task<Client> EditEntityAsync(IEntityViewModel entityViewModel) => CreateEntityAsync(entityViewModel);

        protected override IEntityViewModel CreateViewModel(Client entity) => new ClientViewModel(entity);

        protected override async Task<IEntityViewModel?> FormData(IEntityViewModel? entityViewModel = null)
        {
            EntityFormationViewModel? formationViewModel;
            if (entityViewModel == null)
                formationViewModel = new ClientFormationViewModel();
            else
            {
                if (entityViewModel is not ClientViewModel clientViewModel)
                    throw new ArgumentException("Получена модель, отличная от модели клиента", nameof(entityViewModel));
                formationViewModel = new ClientFormationViewModel(clientViewModel.Entity);
            }
            return await _uiManager.FormationEntity(formationViewModel);
        }

        #endregion
    }
}
