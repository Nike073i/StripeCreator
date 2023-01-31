namespace StripeCreator.WPF
{
    public partial class DataStorePage
    {
        public DataStorePage(DataStorePageViewModel viewModel) : base(viewModel) => Initialize();

        private void Initialize()
        {
            InitializeComponent();
            App.SetWindowSize(1200, 700);
        }
    }
}
