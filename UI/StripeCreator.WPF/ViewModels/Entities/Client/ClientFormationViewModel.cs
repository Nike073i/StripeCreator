using StripeCreator.Business.Models;
using System;

namespace StripeCreator.WPF
{
    /// <summary>
    /// ViewModel формирования сущности клиента
    /// </summary>
    public class ClientFormationViewModel : EntityFormationViewModel
    {
        #region Public properties

        #region PersonData

        /// <summary>
        /// Персональные данные клиента. Имя
        /// </summary>
        public string? FirstName { get; set; }

        /// <summary>
        /// Персональные данные клиента. Фамилия
        /// </summary>
        public string? SecondName { get; set; }

        #endregion

        #region ContactData

        /// <summary>
        /// Контактные данные клиента. Номер телефона
        /// </summary>
        public string? ContactNumber { get; set; }

        /// <summary>
        ///  Контактные данные клиента. Электронный почтовый адрес
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        ///  Контактные данные клиента. Иные контактные сведения. Например адрес страницы в соц. сетях
        /// </summary>
        public string? Other { get; set; }

        #endregion

        #endregion

        #region Constructors

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public ClientFormationViewModel() { }

        /// <summary>
        /// Конструктор с инициализацией полей по ViewModel сущности клиента
        /// </summary>
        /// <param name="viewModel">ViewModel сущности клиента</param>
        public ClientFormationViewModel(Client entity)
        {
            Id = entity.Id;
            var personData = entity.PersonData;
            FirstName = personData.FirstName;
            SecondName = personData.SecondName;
            var contactData = entity.ContactData;
            ContactNumber = contactData.ContactNumber;
            Email = contactData.Email;
            Other = contactData.Other;
        }

        #endregion

        #region Overrides

        protected override bool ValidateData()
        {
            if (string.IsNullOrWhiteSpace(FirstName) || string.IsNullOrWhiteSpace(SecondName)
                || string.IsNullOrWhiteSpace(ContactNumber) || string.IsNullOrWhiteSpace(Email))
            {
                ErrorString = "Заполните обязательные поля";
                return false;
            }
            return true;
        }

        protected override IEntityViewModel? TryCreateEntity()
        {
            try
            {
                var personData = new PersonData(FirstName!, SecondName!);
                var contactData = new ContactData(ContactNumber!, Email!, Other);
                var client = new Client(personData, contactData, Id);
                return new ClientViewModel(client);
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
