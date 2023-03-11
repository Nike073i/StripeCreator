namespace StripeCreator.Business.Models
{
    /// <summary>
    /// Класс персональных данных
    /// </summary>
    public class PersonData
    {
        #region Public properties

        /// <summary>
        /// Имя
        /// </summary>
        public string FirstName { get; }

        /// <summary>
        /// Фамилия
        /// </summary>
        public string SecondName { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Конструктор с полной инициализацией
        /// </summary>
        /// <param name="firstName">Имя</param>
        /// <param name="secondName">Фамилия</param>
        /// <exception cref="ArgumentNullException">Возникает, если <paramref name="firstName"/> или <paramref name="secondName"/> имеют пустое значение</exception>
        public PersonData(string firstName, string secondName)
        {
            if (string.IsNullOrWhiteSpace(firstName)) throw new ArgumentNullException(nameof(firstName));
            if (string.IsNullOrWhiteSpace(secondName)) throw new ArgumentNullException(nameof(secondName));
            FirstName = firstName;
            SecondName = secondName;
        }

        #endregion

        #region Override object methods

        public override bool Equals(object? obj) => (obj is PersonData other) && Equals(other);

        public bool Equals(PersonData other) => other != null &&
                                                FirstName.Equals(other.FirstName, StringComparison.OrdinalIgnoreCase) &&
                                                SecondName.Equals(other.SecondName, StringComparison.OrdinalIgnoreCase);

        public override int GetHashCode() => HashCode.Combine(FirstName, SecondName);

        public override string ToString() => $"{FirstName} {SecondName}";

        #endregion
    }
}