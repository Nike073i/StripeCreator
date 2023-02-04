namespace StripeCreator.Stripe.Models
{
    /// <summary>
    /// Вспомогательный класс расчета координат точек клетки <see cref="Cell"/>
    /// </summary>
    public class CellCoordinatesHelper
    {
        #region Private fields

        private readonly int _cellLength;

        #endregion

        #region Public properties

        /// <summary>
        /// Верхняя левая точка клетки
        /// </summary>
        public PointPosition TopLeftPosition { get; }

        /// <summary>
        /// Верхняя правая точка клетки
        /// </summary>
        public PointPosition TopRightPosition => new(TopLeftPosition.X + _cellLength, TopLeftPosition.Y);

        /// <summary>
        /// Нижняя левая точка клетки
        /// </summary>
        public PointPosition BottomLeftPosition => new(TopLeftPosition.X, TopLeftPosition.Y + _cellLength);

        /// <summary>
        /// Нижняя правая точка клетки
        /// </summary>
        public PointPosition BottomRightPosition => new(TopLeftPosition.X + _cellLength, TopLeftPosition.Y + _cellLength);

        /// <summary>
        /// Центральная точка клетки
        /// </summary>
        public PointPosition CenterPosition => new(TopLeftPosition.X + _cellLength / 2, TopLeftPosition.Y + _cellLength / 2);

        /// <summary>
        /// Центральная левая точка клетки
        /// </summary>
        public PointPosition CenterLeftPosition => new(TopLeftPosition.X, CenterPosition.Y);

        /// <summary>
        /// Центральная правая точка клетки
        /// </summary>
        public PointPosition CenterRightPosition => new(TopRightPosition.X, CenterPosition.Y);

        /// <summary>
        /// Верхняя центральная точка клетки
        /// </summary>
        public PointPosition TopCenterPosition => new(CenterPosition.X, TopLeftPosition.Y);

        /// <summary>
        /// Нижняя центральная точка клетки
        /// </summary>
        public PointPosition BottomCenterPosition => new(CenterPosition.X, BottomLeftPosition.Y);

        #endregion

        #region Constructors 

        /// <summary>
        /// Конструктор с инициализацией верхней левой координаты и размера клетки
        /// </summary>
        /// <param name="topLeftPoint">Верхняя левая координата</param>
        /// <param name="cellSize">Размер клетки</param>
        public CellCoordinatesHelper(PointPosition topLeftPoint, int cellSize)
        {
            if (cellSize <= 1)
                throw new ArgumentOutOfRangeException(nameof(cellSize), "Размер клетки не может быть <= 1");
            _cellLength = cellSize - 1;
            TopLeftPosition = topLeftPoint;
        }

        #endregion
    }
}