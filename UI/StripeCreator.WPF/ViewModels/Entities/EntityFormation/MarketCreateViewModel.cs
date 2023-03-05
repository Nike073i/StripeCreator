using Microsoft.Win32;
using StripeCreator.VK.Models;
using System;
using System.Windows.Input;

namespace StripeCreator.WPF
{
    /// <summary>ViewModel представления для создания товара</summary>
    public class MarketCreateViewModel : BaseViewModel
    {
        #region Public properties

        ///<summary>Сообщение с ошибкой заполнения данных</summary>
        public string ErrorString { get; private set; } = string.Empty;

        ///<summary>Название товара</summary>
        public string? Name { get; set; }

        ///<summary>Описание товара</summary>
        public string? Description { get; set; }

        ///<summary>Стоимость товара</summary>
        public decimal? Price { get; set; }

        ///<summary>Абсолютный путь изображения</summary>
        public string? PhotoPath { get; set; }

        /// <summary>Команда выбора изображения</summary>
        public ICommand ChooseCommand { get; }

        #endregion

        #region Constructors

        /// <summary>Конструктор по умолчанию</summary>
        public MarketCreateViewModel()
        {
            //Инициализация комманд
            ChooseCommand = new RelayCommand(OnExecutedChooseCommand);
        }

        #endregion

        #region Public methods

        public MarketCreateModel? GetData() => ValidateData() ? TryCreateOrderModel() : null;

        #endregion

        #region Private methods

        /// <summary>
        /// Валидация введенных данных
        /// </summary>
        /// <returns></returns>
        private bool ValidateData()
        {
            if (string.IsNullOrWhiteSpace(Name) || !Price.HasValue
               || string.IsNullOrWhiteSpace(Description) || string.IsNullOrWhiteSpace(PhotoPath))
            {
                ErrorString = "Заполните обязательные поля";
                return false;
            }
            return true;
        }

        /// <summary>
        /// Попытка создания модели по заполненным данным
        /// </summary>
        /// <returns>MarketCreateModel - если модель создана успешно
        /// null - если указанны некорректные данные</returns>
        private MarketCreateModel? TryCreateOrderModel()
        {
            try
            {
                var market = new Market(Name!, Description!, Price!.Value, Categories.Needlework);
                var createModel = new MarketCreateModel(market, PhotoPath!);
                return createModel;
            }
            catch (Exception ex)
            {
                ErrorString = ex.Message;
                return null;
            }
        }

        /// <summary>
        /// Действие при команде выбора изображения
        /// </summary>
        /// <param name="parameter">Параметр команды</param>
        private void OnExecutedChooseCommand(object? parameter)
        {
            var dialog = new OpenFileDialog
            {
                Filter = "Файлы рисунков (*.jpg, *.png)|*.jpg;*.png"
            };
            if (dialog.ShowDialog() == false) return;
            PhotoPath = dialog.FileName;
        }

        #endregion
    }
}