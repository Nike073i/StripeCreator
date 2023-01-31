using FontAwesome5;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace StripeCreator.WPF
{
    /// <summary>
    /// Класс view-model для страницы работы со справочниками
    /// </summary>
    public class DataStorePageViewModel : BaseViewModel
    {
        #region Constants

        private readonly string _header = "Справочники";

        #endregion

        #region Public Properties 

        public ActionMenuViewModel ActionMenuViewModel { get; private set; }
        public ObservableCollection<IEntityViewModel>? Entities { get; protected set; }
        public IEntityViewModel? SelectedEntity { get; set; }

        #region Commands

        public ICommand AddCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand RemoveCommand { get; }
        public ICommand RefreshCommand { get; }

        #endregion

        #endregion

        #region Constructors

        public DataStorePageViewModel()
        {
            ActionMenuViewModel = new(_header, GetSideMenuItems());

            // Инициализация команд
            AddCommand = new RelayCommand(OnExecutedAddCommand);
            EditCommand = new RelayCommand(OnExecutedEditCommand);
            RemoveCommand = new RelayCommand(OnExecutedRemoveCommand);
            RefreshCommand = new RelayCommand(OnExecutedRefreshCommand);
        }

        #endregion

        #region Private helper methods

        private List<ActionMenuItemViewModel> GetSideMenuItems()
        {
            return new List<ActionMenuItemViewModel>
            {
                new(EFontAwesomeIcon.Solid_Bars, "Нитки", ShowThreadStore),
                new(EFontAwesomeIcon.Solid_CropAlt, "Ткани",ShowClothStore),
            };
        }

        private void ShowThreadStore(object? parameter) { }
        private void ShowClothStore(object? parameter) { }

        private void OnExecutedAddCommand(object? parameter) { }
        private void OnExecutedEditCommand(object? parameter) { }
        private void OnExecutedRemoveCommand(object? parameter) { }
        private void OnExecutedRefreshCommand(object? parameter) { }

        #endregion
    }
}
