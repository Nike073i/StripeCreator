using StripeCreator.Core.Models;

namespace StripeCreator.Stripe.Scheme
{
    /// <summary>
    /// Клетка схемы
    /// </summary>
    public class Cell : IComparable<Cell>
    {
        #region Public properties 

        /// <summary>
        /// Цвет клетки
        /// </summary>
        public Color Color { get; set; }

        /// <summary>
        /// Позиция клетки
        /// </summary>
        public PointPosition Position { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Конструктор с полной инициализацией
        /// </summary>
        /// <param name="color">Цвет клетки</param>
        /// <param name="position">Позиция клетки</param>
        public Cell(Color color, PointPosition position)
        {
            Color = color;
            Position = position;
        }

        #endregion

        #region Interface implementations

        /// <summary>
        /// Сравнение клеток по координатам. Реализация интерфейса <see cref="IComparable{Cell}"/>
        /// </summary>
        /// <param name="other">Другая клетка</param>
        /// <returns>Результат сравнения клеток по координатам</returns>
        /// <exception cref="ArgumentNullException">Возникает, если в качестве объекта клетки получен null</exception>
        public int CompareTo(Cell? other)
        {
            if (other is null)
                throw new ArgumentNullException(nameof(other), "Значение клетки не может быть null");
            return Position.CompareTo(other.Position);
        }

        #endregion
    }
}
