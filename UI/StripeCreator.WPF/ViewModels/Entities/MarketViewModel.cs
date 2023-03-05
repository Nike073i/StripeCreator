using StripeCreator.VK.Models;
using System;

namespace StripeCreator.WPF
{
    /// <summary>ViewModel сущности товара <see cref="VK.Models.Market"/></summary>
    public class MarketViewModel : BaseViewModel
    {
        #region Public properties

        /// <summary>Модель товара</summary>
        public Market Market { get; }

        /// <summary>Название товара</summary>
        public string Title => Market.Title;

        /// <summary>Категория товара</summary>
        public string CategoryName => Market.Category.Name;

        /// <summary>Текст описания товара</summary>
        public string Description => Market.Description;

        /// <summary>Цена</summary>
        public decimal Price => Market.Price;

        /// <summary>Дата создания товара</summary>
        public DateTime? Date => Market.Date;

        /// <summary>Адрес изображения товара</summary>
        public Uri? PhotoUri => Market.MainPhoto?.Uri;

        #endregion

        #region Constructors

#nullable disable
        /// <summary>Конструктор по умолчанию</summary>
        public MarketViewModel() { }
#nullable enable

        /// <summary>Конструктор с полной инициализацией</summary>
        /// <param name="model">Модель товара</param>
        public MarketViewModel(Market model)
        {
            Market = model;
        }

        #endregion
    }
}
