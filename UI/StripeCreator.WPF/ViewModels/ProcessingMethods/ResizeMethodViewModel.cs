using StripeCreator.Stripe.Models;

namespace StripeCreator.WPF
{
    public class ResizeMethodViewModel : BaseViewModel
    {
        public ResizeMethod Method { get; }
        public string Name { get; }
        public string Description { get; }

        public ResizeMethodViewModel(ResizeMethod method, string name, string description)
        {
            Method = method;
            Name = name;
            Description = description;
        }
    }
}
