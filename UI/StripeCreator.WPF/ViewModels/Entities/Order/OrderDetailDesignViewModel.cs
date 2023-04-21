using StripeCreator.Business.Models;
using System;
using System.Collections.Generic;

namespace StripeCreator.WPF
{
    /// <summary>
    /// Класс view-model представления детальной информации заказа для design-mode
    /// </summary>
    public class OrderDetailDesignViewModel : OrderDetailViewModel
    {
        #region Singleton

        /// <summary>
        /// Экземпляр для показа в Design-mode
        /// </summary>
        public static OrderDetailDesignViewModel Instance => new();

        #endregion

        #region Design Data 

        private static string _designClientFirstName = "Николай";
        private static string _designClientSecondName = "Иванов";
        private static string _designClientContactNumber = "+79156300123";
        private static string _designClientEmail = "example@mail.ru";
        private static string _designClientOther = "Вконтакте - vk.com/id536525252";
        private static PersonData _designClientPersonData = new(_designClientFirstName, _designClientSecondName);
        private static ContactData _designClientContactData = new(_designClientContactNumber, _designClientEmail, _designClientOther);
        private static Client _designClient = new(_designClientPersonData, _designClientContactData, Guid.NewGuid());
        private static Product _designProduct1 = new("Продукт 1", 100m, "Описание 1", Guid.NewGuid());
        private static Product _designProduct2 = new("Продукт 2", 200m, "Описание 2", Guid.NewGuid());
        private static List<OrderProductViewModel> _designOrderProductsViewModel = new() { new(_designProduct1, 1), new(_designProduct2, 2) };
        private static List<OrderProduct> _designOrderProducts = new() { new(_designProduct1.Id!.Value, 1), new(_designProduct2.Id!.Value, 2) };
        private static Order _designOrder = new(_designClient.Id!.Value, 1000m, _designOrderProducts, _designClientContactData, Business.Enums.OrderStatus.Created, DateTime.Now, Guid.NewGuid());

        #endregion

        #region Constructors 

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public OrderDetailDesignViewModel() : base(_designOrder, _designClient, _designOrderProductsViewModel) { }

        #endregion
    }
}