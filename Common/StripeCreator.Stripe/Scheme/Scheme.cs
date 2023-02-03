using System.Runtime.Serialization;
using StripeCreator.Core.Extensions;
using StripeCreator.Core.Models;

namespace StripeCreator.Stripe.Scheme
{
    /// <summary>
    /// Cхема вышивки
    /// </summary>
    [Serializable]
    public class Scheme : ISerializable
    {
        #region Private fields

        /// <summary>
        /// Ширина схемы в клетках
        /// </summary>
        public int _width;

        /// <summary>
        /// Высота схемы в клетках
        /// </summary>
        public int _height;

        /// <summary>
        /// Клетки схемы. Многомерный массив размером <see cref="Width"/> на <see cref="Height"/>
        /// </summary>
        private Cell[,] _cells;

        #endregion

        #region Public properties 

        /// <summary>
        /// Ширина схемы в клетках
        /// </summary>
        public int Width
        {
            get => _width;
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException(nameof(Height), "Ширина схемы не может быть <= 0");
                _width = value;
            }
        }

        /// <summary>
        /// Высота схемы в клетках
        /// </summary>
        public int Height
        {
            get => _height;
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException(nameof(Height), "Высота схемы не может быть <= 0");
                _height = value;
            }
        }

        /// <summary>
        /// Последовательность клеток схемы
        /// </summary>
        public IEnumerable<Cell> Cells => _cells.ToEnumerable();

        /// <summary>
        /// Сетка для схемы. 
        /// Null-значение означает, что сетка не применяется
        /// </summary>
        public Grid? Grid { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Конструктор с полной инициализацией
        /// </summary>
        /// <param name="width">Ширина схемы в клетках</param>
        /// <param name="height">Высота схемы в клетках</param>
        public Scheme(int width, int height)
        {
            Width = width;
            Height = height;
            _cells = new Cell[width, height];
        }

        /// <summary>
        /// Приватный конструктор для десериализации объекта
        /// </summary>
        /// <param name="info">Данные сериализованного объекта</param>
        /// <param name="context">Источник потока сериализованного объекта</param>
        /// <exception cref="SerializationException">Возникает, если нет возможности десериализовать массив клеток</exception>
        private Scheme(SerializationInfo info, StreamingContext context)
        {
            Width = info.GetInt32(nameof(Width));
            Height = info.GetInt32(nameof(Height));
            var cellsObject = info.GetValue(nameof(_cells), typeof(Cell[,]));
            if (cellsObject is not Cell[,] cells)
                throw new SerializationException("Ошибка сериализации клеток схемы");
            _cells = cells;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Установка новой клетки по позиции
        /// </summary>
        /// <param name="color">Цвет клетки</param>
        /// <param name="position">Позиция клетки в схеме</param>
        /// <exception cref="ArgumentOutOfRangeException">Возникает, если указанная позиция выходит за границы схемы</exception>
        public void SetCell(Color color, PointPosition position)
        {
            if (position.X >= Width || position.Y >= Height)
                throw new ArgumentOutOfRangeException(nameof(position));
            _cells[position.X, position.Y] = new Cell(color, position);
        }

        /// <summary>
        /// Последовательность всех использованных цветов в схеме
        /// </summary>
        public IEnumerable<Color> GetColors() => Cells.Select(cell => cell.Color)
                                                      .DistinctBy(color => color.HexValue);

        #endregion

        #region Interface implementations

        /// <summary>
        /// Получение данных объекта для сериализации. Реализация <see cref="ISerializable"/>
        /// </summary>
        /// <param name="info">Данные сериализованного объекта</param>
        /// <param name="context">Источник потока сериализованного объекта</param>
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(Width), Width);
            info.AddValue(nameof(Height), Height);
            info.AddValue(nameof(_cells), _cells);
        }

        #endregion
    }
}
