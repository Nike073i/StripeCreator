using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StripeCreator.WPF
{
    /// <summary>
    /// Интерфейс взаимодействия с сущностями в виде ViewModel
    /// </summary>
    public interface IDataFacade
    {
        /// <summary>
        /// Запрос на получение всех сущностей в виде ViewModel
        /// </summary>
        /// <returns>Список ViewModel сущностей</returns>
        Task<IEnumerable<IEntityViewModel>> GetAllAsync();

        /// <summary>
        /// Запрос на создание сущности
        /// </summary>
        /// <returns>ViewModel сохраненной сущности</returns>
        Task<IEntityViewModel?> CreateAsync();

        /// <summary>
        /// Запрос на изменение сущности
        /// </summary>
        /// <param name="entity">ViewModel изменяемой сущности</param>
        /// <returns>ViewModel сохраненной сущности</returns>
        Task<IEntityViewModel?> EditAsync(IEntityViewModel entity);

        /// <summary>
        /// Запрос на удаление сущности по ее ViewModel
        /// </summary>
        /// <param name="entity">ViewMOdel удаляемой сущности</param>
        /// <returns>Идентификатор удаленной сущности</returns>
        Task<Guid> RemoveAsync(IEntityViewModel entity);
    }
}
