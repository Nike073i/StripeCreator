using System.Collections.Generic;

namespace StripeCreator.WPF
{
    public class EntityInfoViewModel : BaseViewModel
    {
        #region Public properties

        public List<EntityInfoValueViewModel> Data { get; protected set; }

        #endregion

        #region Constructors 

        public EntityInfoViewModel(List<EntityInfoValueViewModel> data)
        {
            Data = data;
        }

        /// <summary>
        /// Дефолтный конструктор
        /// </summary>
        public EntityInfoViewModel()
        {
            Data = new List<EntityInfoValueViewModel>();
        }

        #endregion
    }
}
