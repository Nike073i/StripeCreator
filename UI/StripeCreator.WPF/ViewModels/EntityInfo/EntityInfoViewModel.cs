using System.Collections.Generic;

namespace StripeCreator.WPF
{
    public class EntityInfoViewModel : BaseViewModel
    {
        #region Private fields 

        private List<EntityInfoValueViewModel> _data;

        #endregion

        #region Public properties

        public IEnumerable<EntityInfoValueViewModel> Data => _data;

        #endregion

        #region Constructors 

        public EntityInfoViewModel(List<EntityInfoValueViewModel> data)
        {
            _data = data;
        }

        #endregion
    }
}
