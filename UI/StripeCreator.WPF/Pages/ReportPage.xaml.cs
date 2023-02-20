namespace StripeCreator.WPF
{
    public partial class ReportPage
    {
        public ReportPage(ReportPageViewModel viewModel) : base(viewModel) => Initialize();

        private void Initialize()
        {
            InitializeComponent();
            App.SetWindowSize(1200, 700);
        }
    }
}
