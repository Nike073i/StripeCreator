using System.ComponentModel.DataAnnotations;

namespace StripeCreator.DAL.Models
{
    /// <summary>
    /// Хранимая сущность клиента
    /// </summary>
    public class DbClient : DbEntity
    {
        #region Public properties

        #region PersonData

        /// <summary>
        /// Персональные данные клиента. Имя
        /// </summary>
        [Required]
        public string FirstName { get; set; } = "Имя";

        /// <summary>
        /// Персональные данные клиента. Фамилия
        /// </summary>
        [Required]
        public string SecondName { get; set; } = "Фамилия";

        #endregion

        #region ContactData

        /// <summary>
        /// Контактные данные клиента. Номер телефона
        /// </summary>
        [Required]
        public string ContactNumber { get; set; } = "+79020067089";

        /// <summary>
        ///  Контактные данные клиента. Электронный почтовый адрес
        /// </summary>
        [Required]
        public string Email { get; set; } = "ulstu@mail.ru";

        /// <summary>
        ///  Контактные данные клиента. Иные контактные сведения. Например адрес страницы в соц. сетях
        /// </summary>
        public string? Other { get; set; }

        #endregion

        #endregion

        #region Constructors

        /// <summary>
        /// Конструктор по умолчанию для EFC
        /// </summary>
        protected DbClient() { }

        /// <summary>
        /// Конструктор с полной инициализацией
        /// </summary>
        /// <param name="firstName">Персональные данные. Имя</param>
        /// <param name="secondName">Персональные данные. Фамилия</param>
        /// <param name="contactNumber">Контактные данные. Номер телефона</param>
        /// <param name="email">Контактные данные. Электронная почта</param>
        /// <param name="other">Контактные данные. Иные контактные сведения</param>
        /// <param name="id">Идентификатор сущности</param>
        public DbClient(string firstName, string secondName, string contactNumber, string email, string? other, Guid? id = null) : base(id)
        {
            FirstName = firstName;
            SecondName = secondName;
            ContactNumber = contactNumber;
            Email = email;
            Other = other;
        }

        #endregion
    }
}