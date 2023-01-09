using System.ComponentModel;

namespace StripeCreator.WPF
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
    }
}