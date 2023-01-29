namespace StripeCreator.WPF
{
    public class EntityInfoValueViewModel : BaseViewModel
    {
        #region Public properties

        public string Name { get; set; }
        public string Value { get; set; }

        #endregion

        #region Constructors 

        public EntityInfoValueViewModel(string name, string value)
        {
            Name = name;
            Value = value;
        }

        #endregion
    }
}
