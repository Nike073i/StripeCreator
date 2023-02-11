using StripeCreator.Business.Models;
using StripeCreator.Business.Repositories;
using System;

namespace StripeCreator.WPF
{
    /// <summary>
    /// Сервис для работы с ViewModel сущности <see cref="Client"/>
    /// </summary>
    public class ClientService : EntityService<Client>
    {
        #region Constructors

        /// <summary>
        /// Конструктор с полной инициализацией
        /// </summary>
        /// <param name="repository">Репозиторий сущности</param>
        public ClientService(IClientRepository repository) : base(repository) { }

        #endregion

        #region Implementing Abstract Methods

        protected override IEntityViewModel CreateViewModel(Client entity) => new ClientViewModel(entity);

        protected override Client GetEntityFromViewModel(IEntityViewModel viewModel)
        {
            if (viewModel is not ClientViewModel clientViewModel)
                throw new ArgumentException($"Указанная ViewModel не является ViewModel сущности клиента");
            return clientViewModel.Entity;
        }

        #endregion
    }
}
