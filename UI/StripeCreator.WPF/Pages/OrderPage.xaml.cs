namespace StripeCreator.WPF
{
    public partial class OrderPage
    {
        public OrderPage(OrderPageViewModel viewModel) : base(viewModel) => Initialize();

        private void Initialize()
        {
            InitializeComponent();
            App.SetWindowSize(1180, 700);
        }
    }
}
