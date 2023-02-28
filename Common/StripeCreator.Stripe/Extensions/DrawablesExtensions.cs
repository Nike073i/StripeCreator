using ImageMagick;

namespace StripeCreator.Stripe.Extensions
{
    /// <summary>
    /// Расширения для элементов отрисовки <see cref="Drawables"/>
    /// </summary>
    public static class DrawablesExtensions
    {
        #region Public methods

        /// <summary>
        /// Отрисовка закрашенной точки
        /// </summary>
        /// <param name="drawables">объект <see cref="Drawables"/> для расширения</param>
        /// <param name="x">X координата</param>
        /// <param name="y">Y координата</param>
        /// <param name="color">Цвет заливки</param>
        /// <returns>объект <see cref="Drawable"/></returns>
        public static Drawables FilledPoint(this Drawables drawables, int x, int y, MagickColor color)
        {
            drawables.FillColor(color)
                     .Point(x, y);
            return drawables;
        }

        #endregion
    }
}