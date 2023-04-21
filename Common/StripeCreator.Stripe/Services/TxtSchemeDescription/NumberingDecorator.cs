using System.Text;

namespace StripeCreator.Stripe.Services
{
    public class NumberingDecorator : SchemeDescriptionDecorator
    {
        private const int CellNumberingLength = 5;
        private readonly string CellNumberingFormat = $"{{0,-{CellNumberingLength}}}";
        private readonly int SchemeWidth;

        public NumberingDecorator(ISchemeDescription schemeDescription,
            int schemeWidth)
            : base(schemeDescription)
        {
            SchemeWidth = schemeWidth;
        }

        public override IEnumerable<string> GetData()
        {
            var schemeData = base.GetData();
            var stringBuilder = new StringBuilder();
            stringBuilder.Append(new string(' ', CellNumberingLength));
            for (int i = 0; i < SchemeWidth; i++)
                stringBuilder.Append(string.Format(CellNumberingFormat, i));
            var upperNumbering = stringBuilder.ToString();
            var data = new List<string> { upperNumbering };

            foreach (var (item, index) in schemeData.Select((item, index) => (item, index)))
            {
                var newLine = string.Concat(string.Format(CellNumberingFormat, index), item);
                data.Add(newLine);
            }
            return data;
        }
    }
}
