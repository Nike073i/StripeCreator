namespace StripeCreator.Stripe.Services
{
    public class IndentDecorator : SchemeDescriptionDecorator
    {
        private readonly int SchemeWidth;
        private readonly int IndentSize;
        private readonly int IndentColorIndex;
        private readonly string SchemeCellFormat;

        public IndentDecorator(ISchemeDescription schemeDescription,
            int schemeWidth,
            int indentSize,
            int indentColorIndex,
            string schemeCellFormat)
            : base(schemeDescription)
        {
            SchemeWidth = schemeWidth;
            IndentSize = indentSize;
            IndentColorIndex = indentColorIndex;
            SchemeCellFormat = schemeCellFormat;
        }

        public override IEnumerable<string> GetData()
        {
            var schemeData = base.GetData();
            var newWidth = SchemeWidth + IndentSize * 2;
            // Формируем боковые отступы
            var sidePadding = string.Concat(Enumerable.Repeat(string.Format(SchemeCellFormat, IndentColorIndex), IndentSize));

            // Формируем полную строку 
            var rowIndent = string.Concat(Enumerable.Repeat(string.Format(SchemeCellFormat, IndentColorIndex), newWidth));

            var mainIndent = Enumerable.Repeat(rowIndent, IndentSize).ToArray();

            var data = new List<string>();

            // Добавляем верхний отступ
            data.AddRange(mainIndent);

            // Добавляем боковые отступы 
            foreach (var line in schemeData)
            {
                var newLine = string.Concat(sidePadding, line, sidePadding);
                data.Add(newLine);
            }

            // Добавляем нижний отступ
            data.AddRange(mainIndent);
            return data;
        }
    }
}
