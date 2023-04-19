using StripeCreator.Core.Models;
using StripeCreator.Stripe.Extensions;
using StripeCreator.Stripe.Interfaces;
using StripeCreator.Stripe.Models;
using System.Text;

namespace StripeCreator.Stripe.Services
{
    public class TxtSchemeDescriptor : ISchemeDescriptor
    {
        public void SaveDescription(string path, Scheme scheme)
        {
            var data = CreateDescription(scheme);
            SaveToFile(path, data);
        }

        private string[] CreateDescription(Scheme scheme)
        {
            var colors = IndexColors(scheme.GetColors());
            var schemeDescription = GetSchemeDescription(scheme, colors);
            var descriptionOfColors = GetDescriptionOfColors(colors);
            var description = schemeDescription.Concat(descriptionOfColors);
            return description.ToArray();
        }

        private string[] GetSchemeDescription(Scheme scheme, Dictionary<Color, int> colors)
        {
            var template = scheme.SchemeTemplate;
            var indent = scheme.Indent;
            if (indent != null)
            {
                // Добавляем цвет отступа, если его нет в основной палитре
                colors.TryAdd(indent.Color, colors.Count + 1);
            }

            using var magickImage = MagickImageExtensions.CreateMagickImage(template);
            using var pixels = magickImage.GetPixels();
            var data = new List<string>();
            var stringBuilder = new StringBuilder();
            // Мб тут не foreach, а типа while dowhile.
            foreach (var pixel in pixels)
            {
                if (pixel.X == 0 && pixel.Y != 0)
                {
                    data.Add(stringBuilder.ToString());
                    stringBuilder.Clear();
                }
                var color = new Color(pixel.ToColor()!.ToHexString());
                var colorIndex = colors[color];
                stringBuilder.Append(string.Format("[ {0,-3} ]", colorIndex));
            }
            data.Add(stringBuilder.ToString());
            // Нету данных по отступу
            return data.ToArray();
        }

        private string[] GetDescriptionOfColors(Dictionary<Color, int> colors)
        {
            var data = new List<string>();
            // Добавляем строку заголовков
            data.Add("| Индекс |    Код    |");
            // Вносим данные по цветам
            var pairs = colors.OrderBy(pair => pair.Value).ToList();
            for (int i = 0; i < pairs.Count; i++)
            {
                var color = pairs[i].Key;
                var index = string.Format("| {0,-6} |", i);
                var code = string.Format(" {0,-9} |", color.HexValue);
                data.Add(index + code);
            }
            return data.ToArray();
        }

        private Dictionary<Color, int> IndexColors(IEnumerable<Color> colors)
        {
            var dictionary = new Dictionary<Color, int>();
            var index = 1;
            foreach (var color in colors)
                dictionary.Add(color, index++);
            return dictionary;
        }

        private void SaveToFile(string path, string[] data) => File.WriteAllLines(path, data);
    }
}
