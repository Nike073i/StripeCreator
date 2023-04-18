using StripeCreator.Stripe.Models;

namespace StripeCreator.Stripe.Interfaces
{
    public interface ISchemeDescriptor
    {
        bool SaveDescription(string path, Scheme scheme);
    }
}
