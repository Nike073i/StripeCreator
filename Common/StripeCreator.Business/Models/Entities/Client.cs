using StripeCreator.Core.Models;

namespace StripeCreator.Business.Models
{
    /// <summary>
    /// Класс сущности клиента
    /// </summary>
    public class Client : Entity
    {
        #region Public properties

        /// <summary>
        /// Персональные данные клиента
        /// </summary>
        public PersonData PersonData { get; set; }

        /// <summary>
        /// Контактные данные клиента
        /// </summary>
        public ContactData ContactData { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Конструктор с полной инициализацией
        /// </summary>
        /// <param name="personData">Персональные данные</param>
        /// <param name="contactData">Контактные данные</param>
        /// <param name="id">Идентификатор сущности</param>
        public Client(PersonData personData, ContactData contactData, Guid? id = null) : base(id)
        {
            PersonData = personData;
            ContactData = contactData;
        }

        #endregion
    }
}