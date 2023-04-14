using StripeCreator.Stripe.Models;

namespace StripeCreator.WPF
{
    public class ReductiveMethodViewModel : BaseViewModel
    {
        public ReductiveMethod Method { get; }
        public string Name { get; }
        public string Description { get; }

        public ReductiveMethodViewModel(ReductiveMethod method, string name, string description)
        {
            Method = method;
            Name = name;
            Description = description;
        }
    }
}
