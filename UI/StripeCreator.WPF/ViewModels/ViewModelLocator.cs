namespace StripeCreator.WPF
{
    public class ViewModelLocator
    {
        public static ApplicationViewModel ApplicationViewModel => IoC.GetRequiredService<ApplicationViewModel>();
    }
}
