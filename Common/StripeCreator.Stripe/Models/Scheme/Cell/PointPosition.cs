namespace StripeCreator.Stripe.Models
{
    /// <summary>
    /// Структура данных координат точки
    /// </summary>
    public struct PointPosition : IComparable<PointPosition>
    {
        #region Public properties

        /// <summary>
        /// Х координата
        /// </summary>
        public int X { get; }
        /// <summary>
        /// Y координата
        /// </summary>
        public int Y { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Конструктор с полной инициализацией
        /// </summary>
        /// <param name="x">x координата</param>
        /// <param name="y">y координата</param>
        public PointPosition(int x, int y)
        {
            X = x;
            Y = y;
        }

        #endregion

        #region Interface implementations

        /// <summary>
        /// Сравнение координат. Реализация интерфейса <see cref="IComparable{PointPosition}"/>
        /// Первичная сортировка по Y координатам
        /// Вторичная по X координатам
        /// </summary>
        /// <param name="other">Другая позиция</param>
        /// <returns>Результат сравнения позиций</returns>
        public int CompareTo(PointPosition other) => Y != other.Y ? Y - other.Y : X - other.X;

        #endregion
    }
}
