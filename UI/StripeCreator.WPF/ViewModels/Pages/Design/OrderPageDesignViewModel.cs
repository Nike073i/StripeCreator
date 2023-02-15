using FontAwesome5;
using StripeCreator.Business.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace StripeCreator.WPF
{
    /// <summary>
    /// Класс view-model для страницы взаимодействия с заказами в design-mode
    /// </summary>
    public class OrderPageDesignViewModel : OrderPageViewModel
    {
        #region Singleton

        /// <summary>
        /// Экземпляр для показа в Design-mode
        /// </summary>
        public static OrderPageDesignViewModel Instance => new();

        #endregion

        #region Design data
        private static List<OrderProduct> orderLines = new()
            {
                new(Guid.NewGuid(), 15),
            };
        private static List<OrderViewModel> _designOrders = new() 
        {
                new(new(Guid.NewGuid(), 150m, orderLines, new("+79176306258", "nike073i@mail.ru"))),
                new(new(Guid.NewGuid(), 150m, orderLines, new("+79176306258", "nike073i@mail.ru"))),
                new(new(Guid.NewGuid(), 150m, orderLines, new("+79176306258", "nike073i@mail.ru"))),
                new(new(Guid.NewGuid(), 150m, orderLines, new("+79176306258", "nike073i@mail.ru"))),
        };

        private static Action<object?> _emptyAction = (_) => { };
        private static string _designHeader = "Доступные действия";
        private static List<ActionMenuItemViewModel> _designActions =>
             new()
             {
                new (EFontAwesomeIcon.Solid_Plus, "Создать заказ", _emptyAction),
                new (EFontAwesomeIcon.Solid_Info, "Детали заказа", _emptyAction),
                new (EFontAwesomeIcon.Solid_AngleDoubleUp, "Продвинуть статус", _emptyAction),
                new (EFontAwesomeIcon.Solid_Ban, "Отменить заказ", _emptyAction),
                new (EFontAwesomeIcon.Solid_SyncAlt, "Обновить список", _emptyAction),
                new (EFontAwesomeIcon.Solid_ArrowLeft, "В меню", _emptyAction),
            };

        #endregion

        #region Constructors

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public OrderPageDesignViewModel()
        {
            Orders = new ObservableCollection<OrderViewModel>(_designOrders);
            ActionMenuViewModel = new(_designHeader, _designActions);
            SelectedOrder = Orders.FirstOrDefault();
        }

        #endregion
    }
}
