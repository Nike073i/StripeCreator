namespace StripeCreator.WPF
{
    public partial class ImageProcessingPage
    {
        public ImageProcessingPage(ImageProcessingPageViewModel viewModel) : base(viewModel) => Initialize();

        private void Initialize()
        {
            InitializeComponent();
            App.SetWindowSize(1200, 800);
        }
    }
}
