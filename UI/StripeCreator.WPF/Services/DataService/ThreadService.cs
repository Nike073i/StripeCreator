using StripeCreator.Stripe.Repositories;
using System;
using Thread = StripeCreator.Stripe.Models.Thread;

namespace StripeCreator.WPF
{
    /// <summary>
    /// Сервис для работы с ViewModel сущности <see cref="Thread"/>
    /// </summary>
    public class ThreadService : EntityService<Thread>
    {
        #region Constructors

        /// <summary>
        /// Конструктор с полной инициализацией
        /// </summary>
        /// <param name="repository">Репозиторий сущности</param>
        public ThreadService(IThreadRepository repository) : base(repository) { }

        #endregion

        #region Implementing Abstract Methods

        protected override IEntityViewModel CreateViewModel(Thread entity) => new ThreadViewModel(entity);

        protected override Thread GetEntityFromViewModel(IEntityViewModel viewModel)
        {
            if (viewModel is not ThreadViewModel threadViewModel)
                throw new ArgumentException($"Указанная ViewModel не является ViewModel сущности нити");
            return threadViewModel.Entity;
        }

        public override EntityFormationViewModel CreateFormationViewModel(IEntityViewModel? entity = null) =>
            entity == null ? new ThreadFormationViewModel() : new ThreadFormationViewModel(GetEntityFromViewModel(entity));

        #endregion
    }
}
