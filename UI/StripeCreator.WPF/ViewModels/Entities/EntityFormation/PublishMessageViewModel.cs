using Microsoft.Win32;
using System;
using System.Windows.Input;

namespace StripeCreator.WPF
{
    /// <summary>ViewModel представления для публикации записи</summary>
    public class PublishMessageViewModel : BaseViewModel
    {
        #region Public properties

        ///<summary>Сообщение с ошибкой заполнения данных</summary>
        public string ErrorString { get; private set; } = string.Empty;

        ///<summary>Сообщение</summary>
        public string? Message { get; set; }

        ///<summary>Абсолютный путь изображения</summary>
        public string? PhotoPath { get; set; }

        /// <summary>Команда выбора изображения</summary>
        public ICommand ChooseCommand { get; }

        #endregion

        #region Constructors

        /// <summary>Конструктор по умолчанию</summary>
        public PublishMessageViewModel()
        {
            //Инициализация комманд
            ChooseCommand = new RelayCommand(OnExecutedChooseCommand);
        }

        #endregion

        #region Public methods

        public PublishMessageModel? GetData() => ValidateData() ? TryCreateOrderModel() : null;

        #endregion

        #region Private methods

        /// <summary>
        /// Валидация введенных данных
        /// </summary>
        /// <returns></returns>
        private bool ValidateData()
        {
            if (string.IsNullOrWhiteSpace(Message))
            {
                ErrorString = "Заполните обязательные поля";
                return false;
            }
            return true;
        }

        /// <summary>
        /// Попытка создания модели по заполненным данным
        /// </summary>
        /// <returns>PublishMessageModel - если модель создана успешно
        /// null - если указанны некорректные данные
        /// </returns>
        private PublishMessageModel? TryCreateOrderModel()
        {
            try
            {
                return new(Message!, PhotoPath);
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