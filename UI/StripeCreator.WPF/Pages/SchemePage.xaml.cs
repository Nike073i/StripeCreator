namespace StripeCreator.WPF
{
    public partial class SchemePage
    {
        public SchemePage(SchemePageViewModel viewModel) : base(viewModel) => Initialize();

        private void Initialize()
        {
            InitializeComponent();
            App.SetWindowSize(1300, 900);
        }
    }
}
