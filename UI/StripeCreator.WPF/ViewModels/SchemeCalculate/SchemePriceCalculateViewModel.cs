using StripeCreator.Stripe.Models;
using StripeCreator.Stripe.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace StripeCreator.WPF
{
    /// <summary>
    /// Класс view-model для представления расчета стоимости
    /// </summary>
    public class SchemePriceCalculateViewModel : BaseViewModel
    {
        #region Private fields

        /// <summary>
        /// Сервис расчета количества материалов
        /// </summary>
        private readonly SchemePriceCalculator _priceCalculator;

        /// <summary>
        /// Менеджер интерактивного взаимодействия
        /// </summary>
        private readonly IUiManager _uiManager;

        /// <summary>
        /// Схема вышивки
        /// </summary>
        private readonly Scheme _scheme;

        #endregion

        #region Public properties

        /// <summary>
        /// Типы вышивки
        /// </summary>
        public static IEnumerable EmbroideryTypes => Enum.GetValues(typeof(EmbroideryType));

        /// <summary>
        /// Выбранный тип вышивки
        /// </summary>
        public EmbroideryType? SelectedЕmbroideryType { get; set; }

        /// <summary>
        /// Методы вышивки
        /// </summary>
        public static IEnumerable EmbroideryMethods => Enum.GetValues(typeof(EmbroideryMethod));

        /// <summary>
        /// Выбранный метод вышивки
        /// </summary>
        public EmbroideryMethod? SelectedЕmbroideryMethod { get; set; }

        /// <summary>
        /// Список доступных тканей
        /// </summary>
        public IEnumerable<Cloth> Cloths { get; set; }

        /// <summary>
        /// Выбранная ткань
        /// </summary>
        public Cloth? SelectedCloth { get; set; }

        /// <summary>
        /// Список доступных нитей
        /// </summary>
        public IEnumerable<Thread> Threads { get; set; }

        /// <summary>
        /// Словарь нитей для цветов
        /// </summary>
        public ObservableCollection<ColorThreadViewModel> ColorThreads { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
#nullable disable
        public SchemePriceCalculateViewModel() { }
#nullable enable

        /// <summary>
        /// Конструктор с полной инициализацией
        /// </summary>
        /// <param name="uiManager">Менеджер интерактивного взаимодействия</param>
        /// <param name="priceCalculator">Сервис расчета материалов</param>
        public SchemePriceCalculateViewModel(IUiManager uiManager, SchemePriceCalculator priceCalculator, Scheme scheme,
            IEnumerable<Cloth> cloths, IEnumerable<Thread> threads)
        {
            if (!cloths.Any()) throw new ArgumentNullException(nameof(cloths));
            if (!threads.Any()) throw new ArgumentNullException(nameof(threads));

            _uiManager = uiManager;
            _priceCalculator = priceCalculator;
            _scheme = scheme;
            Cloths = cloths;
            Threads = threads;
            var colorStatistic = scheme.GetColorsStatistic();
            ColorThreads = new(colorStatistic.Select(c => new ColorThreadViewModel(c.Key, threads.First())));
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Рассчитать стоимость
        /// </summary>
        public void CalculatePrice()
        {
            if (!SelectedЕmbroideryMethod.HasValue || !SelectedЕmbroideryType.HasValue)
            {
                _uiManager.ShowError(new("Ошибка заполнения полей", "Выберите метод и тип вышивки"));
                return;
            }
            var threadsCosts = ColorThreads.ToDictionary(ct => ct.Color, ct => ct.Thread.Price);
            var price = _priceCalculator.Calculate(SelectedЕmbroideryMethod.Value, SelectedЕmbroideryType.Value, _scheme, threadsCosts, SelectedCloth!.Price);
            _uiManager.ShowInfo(new("Стоимость", $"Стоимость вышивки по указанным параметрам равна = {price:N2} рублей"));
        }

        #endregion
    }
}
