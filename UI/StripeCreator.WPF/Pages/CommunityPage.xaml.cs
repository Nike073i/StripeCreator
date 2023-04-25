namespace StripeCreator.WPF
{
    public partial class CommunityPage
    {
        public CommunityPage(CommunityPageViewModel viewModel) : base(viewModel) => Initialize();

        private void Initialize()
        {
            InitializeComponent();
            App.SetWindowSize(1200, 870);
        }
    }
}
