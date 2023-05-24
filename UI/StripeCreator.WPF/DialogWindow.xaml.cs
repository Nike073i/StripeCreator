namespace StripeCreator.WPF
{
    /// <summary>
    /// Внутренняя логика окна <see cref="DialogWindow"/>
    /// </summary>
    public partial class DialogWindow
    {
        /// <summary>
        /// Конструктор с полной инициализацией
        /// </summary>
        public DialogWindow()
        {
            Owner = App.Current.MainWindow;
            InitializeComponent();
        }
    }
}
