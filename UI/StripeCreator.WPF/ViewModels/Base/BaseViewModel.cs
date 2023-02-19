using System.ComponentModel;

namespace StripeCreator.WPF
{
    /// <summary>
    /// ������� ����� ��� ViewModel
    /// </summary>
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        #region Interface implementations

        public event PropertyChangedEventHandler? PropertyChanged = (sender, e) => { };

        #endregion
    }
}