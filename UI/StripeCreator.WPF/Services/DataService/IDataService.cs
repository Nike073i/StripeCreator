using System;
using System.Threading.Tasks;

namespace StripeCreator.WPF
{
    /// <summary>
    /// Интерфейс взаимодействия с сущностями в виде ViewModel
    /// </summary>
    public interface IDataService
    {
        /// <summary>
        /// Запрос на сохранение сущности в виде ViewModel
        /// </summary>
        /// <param name="entity">ViewModel сохраняемой сущности</param>
        /// <returns>ViewModel сохраненной сущности</returns>
        Task<IEntityViewModel> SaveAsync(IEntityViewModel entity);

        /// <summary>
        /// Запрос на удаление сущности по ее ViewModel
        /// </summary>
        /// <param name="entity">ViewMOdel удаляемой сущности</param>
        /// <returns>Идентификатор удаленной сущности</returns>
        Task<Guid> RemoveAsync(IEntityViewModel entity);

        /// <summary>
        /// Создание модели формирования сущности
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>Модель формирования сущности</returns>
        EntityFormationViewModel CreateFormationViewModel(IEntityViewModel? entity = null);
    }
}
