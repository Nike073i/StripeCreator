using StripeCreator.VK.Models;
using System;

namespace StripeCreator.WPF
{
    /// <summary>ViewModel представления для изменения товара</summary>
    public class MarketEditViewModel : BaseViewModel
    {
        #region Private fields

        /// <summary>Текущая модель товара</summary>
        private readonly Market _currentModel;

        #endregion

        #region Public properties

        ///<summary>Сообщение с ошибкой заполнения данных</summary>
        public string ErrorString { get; private set; } = string.Empty;

        ///<summary>Название товара</summary>
        public string Name { get; set; }

        ///<summary>Описание товара</summary>
        public string Description { get; set; }

        ///<summary>Стоимость товара</summary>
        public decimal Price { get; set; }

        ///<summary>Абсолютный путь изображения</summary>
        public Uri PhotoPath { get; }

        #endregion

        #region Constructors

        /// <summary>Конструктор по умолчанию</summary>
#nullable disable
        public MarketEditViewModel() { }
#nullable enable

        public MarketEditViewModel(Market market)
        {
            _currentModel = market;
            Name = market.Title;
            Description = market.Description;
            Price = market.Price;
            PhotoPath = market.MainPhoto!.Uri;
        }

        #endregion

        #region Public methods

        public Market? GetData() => ValidateData() ? TryApplyChanges() : null;

        #endregion

        #region Private methods

        /// <summary>
        /// Валидация введенных данных
        /// </summary>
        /// <returns></returns>
        private bool ValidateData()
        {
            if (string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(Description))
            {
                ErrorString = "Заполните обязательные поля";
                return false;
            }
            return true;
        }

        /// <summary>
        /// Попытка создания модели по заполненным данным
        /// </summary>
        /// <returns>
        /// Market - если модель создана успешно
        /// null - если указанны некорректные данные
        /// </returns>
        private Market? TryApplyChanges()
        {
            try
            {
                _currentModel.Title = Name;
                _currentModel.Description = Description;
                _currentModel.Price = Price;
                return _currentModel;
            }
            catch (Exception ex)
            {
                ErrorString = ex.Message;
                return null;
            }
        }

        #endregion
    }
}
