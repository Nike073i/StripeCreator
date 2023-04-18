using StripeCreator.Core.Models;
using StripeCreator.Stripe.Interfaces;
using StripeCreator.Stripe.Models;

namespace StripeCreator.Stripe.Services
{
    public class TxtSchemeDescriptor : ISchemeDescriptor
    {
        public bool SaveDescription(string path, Scheme scheme)
        {
            var data = CreateDescription(scheme);
            return SaveToFile(path, data);
        }

        private string[] CreateDescription(Scheme scheme)
        {
            var colors = IndexColors(scheme.GetColors());
            var descriptionOfColors = GetDescriptionOfColors(colors);
            var schemeDescription = GetSchemeDescription(scheme);
            var description = schemeDescription.Concat(descriptionOfColors);
            return description.ToArray();
        }

        private string[] GetSchemeDescription(Scheme scheme)
        {
            throw new NotImplementedException();
        }

        private string[] GetDescriptionOfColors(List<Color> colors)
        {
            throw new NotImplementedException();
        }

        private List<Color> IndexColors(IEnumerable<Color> enumerable)
        {
            throw new NotImplementedException();
        }

        private bool SaveToFile(string path, string[] data)
        {
            throw new NotImplementedException();
        }
    }
}
