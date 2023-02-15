namespace StripeCreator.WPF
{
    public partial class WelcomePage
    {
        public WelcomePage(WelcomePageViewModel viewModel) : base(viewModel) => Initialize();

        private void Initialize()
        {
            InitializeComponent();
            App.SetWindowSize(600, 450);
        }
    }
}
