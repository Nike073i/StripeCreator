using System.Collections.Generic;

namespace StripeCreator.WPF
{
    /// <summary>
    /// Класс view-model для данных сущности
    /// </summary>
    public class EntityInfoDesignViewModel : EntityInfoViewModel
    {
        #region Singleton

        /// <summary>
        /// Экземпляр для показа в Design-mode
        /// </summary>
        public static EntityInfoDesignViewModel Instance => new();

        #endregion

        #region Design Data 

        private static readonly List<EntityInfoValueViewModel> _designData = new()
        {
            new("Название", "Aida 16"),
            new("Размерность", "16"),
            new("Плотность", "71"),
            new("Цвет", "Черный"),
        };

        #endregion

        #region Constructors 

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public EntityInfoDesignViewModel() : base(new List<EntityInfoValueViewModel>(_designData)) { }

        #endregion
    }
}
