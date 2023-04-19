using StripeCreator.Stripe.Models;

namespace StripeCreator.Stripe.Interfaces
{
    public interface ISchemeDescriptor
    {
        void SaveDescription(string path, Scheme scheme);
    }
}
