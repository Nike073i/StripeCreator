using System.Collections.Generic;

namespace StripeCreator.WPF
{
    /// <summary>
    /// Информация сущности
    /// </summary>
    public class EntityInfoViewModel : BaseViewModel
    {
        #region Private fields 

        /// <summary>
        /// Список данных сущностей
        /// </summary>
        private List<EntityInfoValueViewModel> _data;

        #endregion

        #region Public properties

        /// <summary>
        /// Список данных сущностей
        /// </summary>
        public IEnumerable<EntityInfoValueViewModel> Data => _data;

        #endregion

        #region Constructors 

        /// <summary>
        /// Конструктор с полной инициализацией
        /// </summary>
        /// <param name="data">Данные сущности</param>
        public EntityInfoViewModel(List<EntityInfoValueViewModel> data)
        {
            _data = data;
        }

        #endregion
    }
}
