using StripeCreator.Business.Models;
using System.Collections.Generic;

namespace StripeCreator.WPF
{
    /// <summary>
    /// ViewModel доменной сущности <see cref="Client"/>
    /// </summary>
    public class ClientViewModel : EntityViewModel<Client>
    {
        #region Public properties

        /// <summary>
        /// Имя 
        /// </summary>
        public string FirstName => Entity.PersonData.FirstName;

        /// <summary>
        /// Фамилия 
        /// </summary>
        public string SecondName => Entity.PersonData.SecondName;

        /// <summary>
        /// Номер телефона
        /// </summary>
        public string ContactNumber => Entity.ContactData.ContactNumber;

        /// <summary>
        /// Адрес электронной почты
        /// </summary>
        public string Email => Entity.ContactData.Email;

        /// <summary>
        /// Иные контактные сведения
        /// </summary>
        public string? Other => Entity.ContactData.Other;

        #endregion

        #region Constructors

        /// <summary>
        /// Конструктор с полной инициализацией
        /// </summary>
        /// <param name="client">Доменная модель клиента</param>
        public ClientViewModel(Client client) : base(client) { }

        #endregion

        #region Interface implementations 

        public override EntityInfoViewModel GetData
        {
            get
            {
                var data = new List<EntityInfoValueViewModel>
                {
                    new("Имя", FirstName),
                    new("Фамилия", SecondName),
                    new("Н.тел.", ContactNumber),
                    new("Почта", Email),
                    new("Иное", Other ?? "Отсутствует")
                };
                return new EntityInfoViewModel(data);
            }
        }

        #endregion
    }
}
