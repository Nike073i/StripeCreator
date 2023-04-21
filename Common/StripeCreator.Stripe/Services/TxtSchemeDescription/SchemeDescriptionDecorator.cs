namespace StripeCreator.Stripe.Services
{
    public abstract class SchemeDescriptionDecorator : ISchemeDescription
    {
        private readonly ISchemeDescription SchemeDescription;
        public virtual IEnumerable<string> GetData() => SchemeDescription.GetData();
        public SchemeDescriptionDecorator(ISchemeDescription schemeDescription)
        {
            SchemeDescription = schemeDescription;
        }
    }
}
