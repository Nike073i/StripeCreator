using StripeCreator.Core.Models;
using StripeCreator.Stripe.Models;
using StripeCreator.Stripe.Services;
using System;
using System.Collections;
using System.Collections.Generic;

namespace StripeCreator.WPF
{
    /// <summary>
    /// Класс view-model для представления расчета материалов
    /// </summary>
    public class SchemeMaterialCalculateViewModel : BaseViewModel
    {
        #region Private fields

        /// <summary>
        /// Сервис расчета количества материалов
        /// </summary>
        private readonly SchemeMaterialCalculator _materialCalculator;

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
        /// Ширина ткани в метрах
        /// </summary>
        public double? ClothWidth { get; set; }

        /// <summary>
        /// Высота ткани в метрах
        /// </summary>
        public double? ClothHeight { get; set; }

        /// <summary>
        /// Длины нитей каждого цвета в метрах
        /// </summary>
        public Dictionary<Color, double>? ColorLengths { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
#nullable disable
        public SchemeMaterialCalculateViewModel() { }
#nullable enable

        /// <summary>
        /// Конструктор с полной инициализацией
        /// </summary>
        /// <param name="uiManager">Менеджер интерактивного взаимодействия</param>
        /// <param name="materialCalculator">Сервис расчета материалов</param>
        public SchemeMaterialCalculateViewModel(IUiManager uiManager, SchemeMaterialCalculator materialCalculator, Scheme scheme)
        {
            _uiManager = uiManager;
            _materialCalculator = materialCalculator;
            _scheme = scheme;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Рассчитать материалы
        /// </summary>
        public void CalculateMaterial()
        {
            if (!SelectedЕmbroideryMethod.HasValue || !SelectedЕmbroideryType.HasValue)
            {
                _uiManager.ShowError(new("Ошибка заполнения полей", "Выберите метод и тип вышивки"));
                return;
            }
            var estimate = _materialCalculator.Calculate(SelectedЕmbroideryMethod.Value, SelectedЕmbroideryType.Value, _scheme);
            ClothWidth = estimate.ClothWidth;
            ClothHeight = estimate.ClothHeight;
            ColorLengths = estimate.ColorLengths;
        }

        #endregion
    }
}
