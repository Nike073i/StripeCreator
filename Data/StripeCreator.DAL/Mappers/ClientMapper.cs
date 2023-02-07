using StripeCreator.Business.Models;
using StripeCreator.DAL.Models;

namespace StripeCreator.DAL.Mappers
{
    /// <summary>
    /// преобразователя хранимых <see cref="DbClient"/> и доменных <see cref="Client"/> сущностей 
    /// </summary>
    public class ClientMapper : IDbMapper<DbClient, Client>
    {
        #region Interface implementation

        public DbClient CreateDbModel(Client domainModel)
        {
            var personData = domainModel.PersonData;
            string firstName = personData.FirstName;
            string secondName = personData.SecondName;
            var contactData = domainModel.ContactData;
            string contactNumber = contactData.ContactNumber;
            string email = contactData.Email;
            string? other = contactData.Other;
            return new DbClient(firstName, secondName, contactNumber, email, other);
        }

        public Client MapToDomainModel(DbClient dbModel)
        {
            var personData = new PersonData(dbModel.FirstName, dbModel.SecondName);
            var contactData = new ContactData(dbModel.ContactNumber, dbModel.Email, dbModel.Other);
            var id = dbModel.Id;
            return new Client(personData, contactData, id);
        }

        public void UpdateDbModel(Client domainModel, ref DbClient dbModel)
        {
            var personData = domainModel.PersonData;
            dbModel.FirstName = personData.FirstName;
            dbModel.SecondName = personData.SecondName;
            var contactData = domainModel.ContactData;
            dbModel.ContactNumber = contactData.ContactNumber;
            dbModel.Email = contactData.Email;
            dbModel.Other = contactData.Other;
        }

        #endregion
    }
}