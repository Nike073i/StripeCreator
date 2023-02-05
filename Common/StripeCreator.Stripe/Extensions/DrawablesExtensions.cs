using ImageMagick;
using StripeCreator.Stripe.Models;

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

        /// <summary>
        /// Отрисовка линии по точкам <see cref="PointPosition"/>
        /// </summary>
        /// <param name="drawables">объект <see cref="Drawables"/> для расширения</param>
        /// <param name="startPoint">Точка начала отрисовки</param>
        /// <param name="endPoint">Точка окончания отрисовки</param>
        /// <returns>объект <see cref="Drawable"/></returns>
        public static Drawables LineByPoints(this Drawables drawables, PointPosition startPoint, PointPosition endPoint)
        {
            drawables.Line(startPoint.X, startPoint.Y, endPoint.X, endPoint.Y);
            return drawables;
        }

        #endregion
    }
}