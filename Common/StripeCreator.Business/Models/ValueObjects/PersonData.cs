namespace StripeCreator.Business.Models
{
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
        public PersonData(string firstName, string secondName)
        {
            FirstName = firstName;
            SecondName = secondName;
        }

        #endregion

        #region Override object methods

        public override bool Equals(object? obj) => (obj is PersonData other) && Equals(other);

        public bool Equals(PersonData other) => other != null && 
                                                FirstName == other.FirstName &&
                                                SecondName == other.SecondName;

        public override int GetHashCode() => HashCode.Combine(FirstName, SecondName);

        public override string ToString() => $"{FirstName} {SecondName}";

        #endregion
    }
}