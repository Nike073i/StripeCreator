using StripeCreator.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StripeCreator.WPF
{
    /// <summary>
    /// Класс view-model представления создания заказа для design-mode
    /// </summary>
    public class OrderCreateDesignViewModel : OrderCreateViewModel
    {
        #region Singleton

        /// <summary>
        /// Экземпляр для показа в Design-mode
        /// </summary>
        public static OrderCreateDesignViewModel Instance => new();

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
        private static Product _designProduct = new("Продукт 1", 150m, "Описание", Guid.NewGuid());
        private static List<Client> _designClients = new() { _designClient };
        private static List<Product> _designProducts = new() { _designProduct };

        #endregion

        #region Constructors 

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public OrderCreateDesignViewModel() : base(null!, null!, _designClients, _designProducts)
        {
            SelectedClient = new ClientViewModel(_designClients.FirstOrDefault()!);
            SelectedProduct = _designProducts.FirstOrDefault();
            Quantity = 5;
        }

        #endregion
    }
}