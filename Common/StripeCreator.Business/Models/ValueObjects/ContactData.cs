using System.Text.RegularExpressions;

namespace StripeCreator.Business.Models
{
    public class ContactData
    {
        #region Constants

        public static readonly string ContactNumberPattern = @"^\+7\d{10}$";
        public static readonly string EmailPattern = @"^[a-zA-Z0-9]+[@][a-z]{4,}[.][a-z]{2,}";

        #endregion

        #region Public properties

        /// <summary>
        /// Контактный номер телефона
        /// </summary>
        public string ContactNumber { get; }

        /// <summary>
        /// Электронный почтовый адрес
        /// </summary>
        public string Email { get; }

        /// <summary>
        /// Иные контактные сведения. Например адрес страницы в соц. сетях
        /// </summary>
        public string? Other { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Конструктор с полной инициализацией
        /// </summary>
        /// <param name="number">Контактный номер телефона</param>
        /// <param name="email">Электронный почтовый адрес</param>
        /// <param name="other">Иные контактные сведения</param>
        /// <exception cref="ArgumentException">Возникает, если контактный номер или адрес электронной почты указан неверно</exception>
        public ContactData(string number, string email, string? other = null)
        {
            if (!Regex.IsMatch(number, ContactNumberPattern)) throw new ArgumentException("Контактный номер телефона указан неверно");
            if (!Regex.IsMatch(email, EmailPattern)) throw new ArgumentException("E-mail указан неверно");

            ContactNumber = number;
            Email = email;
            Other = other;
        }

        #endregion
    }
}