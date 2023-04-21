namespace StripeCreator.Stripe.Services
{
    public class SchemeDescription : ISchemeDescription
    {
        private readonly IEnumerable<string> _data;
        public IEnumerable<string> GetData() => _data;

        public SchemeDescription(IEnumerable<string> data)
        {
            _data = data;
        }
    }
}
