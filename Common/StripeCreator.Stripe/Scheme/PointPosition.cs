namespace StripeCreator.Stripe.Scheme
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

        public int CompareTo(PointPosition other) => Y != other.Y ? Y - other.Y : X - other.X;

        #endregion
    }
}
