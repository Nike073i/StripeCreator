using StripeCreator.Core.Models;
using StripeCreator.DAL.Models;

namespace StripeCreator.DAL.Mappers
{
    /// <summary>
    /// Базовый интерфейс преобразователя хранимых и доменных сущностей
    /// </summary>
    /// <typeparam name="TDbEntity"></typeparam>
    /// <typeparam name="TDomainEntity"></typeparam>
    public interface IDbMapper<TDbEntity, TDomainEntity>
    where TDbEntity : DbEntity
    where TDomainEntity : Entity
    {
        /// <summary>
        /// Создать хранимую сущность по доменной модели
        /// </summary>
        /// <param name="domainModel">Доменная модель сущности</param>
        /// <returns>Новая хранимая сущность</returns>
        TDbEntity CreateDbModel(TDomainEntity domainModel);

        /// <summary>
        /// Обновить хранимую сущность данными доменной модели
        /// </summary>
        /// <param name="domainModel">Доменная модель</param>
        /// <param name="dbModel">Хранимая сущность</param>
        void UpdateDbModel(TDomainEntity domainModel, ref TDbEntity dbModel);

        /// <summary>
        /// Преобразовать хранимую сущность в доменную модель
        /// </summary>
        /// <param name="dbModel">Хранимая сущность</param>
        /// <returns>Новая доменная модель</returns>
        TDomainEntity MapToDomainModel(TDbEntity dbModel);
    }
}