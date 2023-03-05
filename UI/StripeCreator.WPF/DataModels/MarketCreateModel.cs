using StripeCreator.VK.Models;

namespace StripeCreator.WPF
{
    /// <summary>Модель для создания нового товара</summary>
    public class MarketCreateModel
    {
        #region Public properties

        /// <summary>Модель нового товара</summary>
        public Market Market { get; }

        /// <summary>Абсолютный путь к изображению</summary>
        public string PhotoPath { get; }

        #endregion

        #region Constructors

        /// <summary>Конструктор с полной инициализацией</summary>
        /// <param name="market">Модель товара</param>
        /// <param name="photoPath">Абсолютный путь к изображению</param>
        public MarketCreateModel(Market market, string photoPath)
        {
            Market = market;
            PhotoPath = photoPath;
        }

        #endregion
    }
}
