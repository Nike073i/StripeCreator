using ImageMagick;
using StripeCreator.Core.Models;
using StripeCreator.Stripe.Models;
using StripeCreator.Stripe.Services;

namespace StripeCreator.TestConsole
{
    public class Program
    {
        private static string TestImageInputPath = @"Images/Вольные.bmp";
        //private static string TestImageInputPath = @"Images/ДДТ.jpg";
        private static string TestImageOutputPath = @"Images/OutputImage.png";

        public static async Task Main(string[] args)
        {
            //Тест ImageProcessor
            var imageKeeper = new ImageKeeper();
            var image = await imageKeeper.LoadAsync(TestImageInputPath);
            using var imageProcessor = new ImageProccesor(image);
            imageProcessor.Scale(250, 250);
            imageProcessor.Quantize(3);
            await imageKeeper.SaveAsync(TestImageOutputPath, imageProcessor.Image);

            // Тест отрисовки схемы
            // var imageKeeper = new ImageKeeper();
            // var image = await imageKeeper.LoadImageAsync(TestImageInputPath);
            // var converter = new SchemeConverter();
            // var scheme = converter.CreateScheme(image);

            // var viz = new SchemeVisualizer(scheme);
            // scheme.Grid = new Grid(1, new Color("#AAAAAA"));
            // //var newImage = viz.CreateCellScheme(5);
            // var newImage = viz.CreatePrototypeScheme(ЕmbroideryType.SmoothVertical, EmbroideryMethod.In2Thread, 5);

            // await imageKeeper.SaveImageAsync(TestImageOutputPath, newImage);
            //var cells = scheme.Cells;

            //int count = 0;
            //foreach (var cell in cells)
            //{
            //    if (count == 5)
            //        break;
            //    mi.Draw(viz.DrawableFilledCell(cell));
            //    //mi.Draw(viz.DrawableSymbolCell(cell, 'М'));
            //    //mi.Draw(viz.DrawableTypeCell(cell,ЕmbroideryType.Cross, cellSize:10));

            //    count++;
            //}

            //await mi.WriteAsync(TestImageOutputPath);

            //var newImage = converter.CreateImageFromScheme(scheme);
            //await imageKeeper.SaveImageAsync(TestImageOutputPath, newImage);

            // Тест работы с изображением
            //var imageKeeper = new ImageKeeper();
            //var image = await imageKeeper.LoadImageAsync(TestImageInputPath);
            //var converter = new SchemeConverter();
            //var scheme = converter.CreateScheme(image);
            //var newImage = converter.CreateImageFromScheme(scheme);
            //await imageKeeper.SaveImageAsync(TestImageOutputPath, newImage);
            //var schemeKeeper = new SchemeKeeper();
            //await schemeKeeper.SaveSchemeAsync("scheme1.json", scheme);
            //var schemeRestore = await schemeKeeper.LoadSchemeFromJsonAsync("scheme1.json");
            //var imageProccesor = new ImageProccesor(image);
            //imageProccesor.Trim();

            //Task.Run(() => imageKeeper.SaveImageAsync(TestImageOutputPath, imageProccesor.Image)).Wait();

            // Тест работы схемы
            //var scheme = new Scheme(10, 10);
            //var position = new CellPosition(9, 9);
            //var color = new CellColor("#000000");
            //scheme.SetCell(color, position);
            //var cells = scheme.Cells;
        }
    }
}