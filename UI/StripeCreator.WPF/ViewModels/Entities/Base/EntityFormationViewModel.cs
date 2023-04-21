using System;

namespace StripeCreator.WPF
{
    /// <summary>
    /// Базовый класс ViewModel для формирования сущности
    /// </summary>
    public abstract class EntityFormationViewModel : BaseViewModel
    {
        #region Public properties

        /// <summary>
        /// Идентификатор формируемой сущности
        /// </summary>
        public Guid? Id { get; protected set; }

        /// <summary>
        /// Сообщение с ошибкой заполнения данных
        /// </summary>
        public string ErrorString { get; protected set; } = string.Empty;

        #endregion

        #region Public methods

        /// <summary>
        /// Получить сформированные данные
        /// </summary>
        /// <returns>ViewModel - если все поля заполнены корректно.
        /// null - если допущена ошибка при заполнении</returns>
        public IEntityViewModel? GetData() => !ValidateData() ? null : TryCreateEntity();

        #endregion

        #region Abstract methods

        /// <summary>
        /// Валидация заполненных полей
        /// </summary>
        /// <returns>Результат валидации</returns>
        protected abstract bool ValidateData();

        /// <summary>
        /// Попытка создания ViewModel по заполненным данным
        /// </summary>
        /// <returns>ViewModel - если сущность создана успешно
        /// null - если указанны некорректные данные</returns>
        protected abstract IEntityViewModel? TryCreateEntity();

        #endregion
    }
}
