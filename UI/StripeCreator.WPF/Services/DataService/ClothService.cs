using StripeCreator.Stripe.Models;
using StripeCreator.Stripe.Repositories;
using System;

namespace StripeCreator.WPF
{
    /// <summary>
    /// Сервис для работы с ViewModel сущности <see cref="Cloth"/>
    /// </summary>
    public class ClothService : EntityService<Cloth>
    {
        #region Constructors

        /// <summary>
        /// Конструктор с полной инициализацией
        /// </summary>
        /// <param name="repository">Репозиторий сущности</param>
        public ClothService(IClothRepository repository) : base(repository) { }

        #endregion

        #region Implementing Abstract Methods

        protected override IEntityViewModel CreateViewModel(Cloth entity) => new ClothViewModel(entity);

        protected override Cloth GetEntityFromViewModel(IEntityViewModel viewModel)
        {
            if (viewModel is not ClothViewModel clothViewModel)
                throw new ArgumentException($"Указанная ViewModel не является ViewModel сущности ткани");
            return clothViewModel.Entity;
        }

        public override EntityFormationViewModel CreateFormationViewModel(IEntityViewModel? entity = null) =>
            entity == null ? new ClothFormationViewModel() : new ClothFormationViewModel(GetEntityFromViewModel(entity));

        #endregion
    }
}
