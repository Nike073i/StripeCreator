using StripeCreator.Business.Models;
using System.Collections.Generic;

namespace StripeCreator.WPF
{
    /// <summary>
    /// ViewModel доменной сущности <see cref="Client"/>
    /// </summary>
    public class ClientViewModel : EntityViewModel<Client>
    {
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
                var data = new List<EntityInfoValueViewModel>();

                var personData = Entity.PersonData;
                data.Add(new("Имя", personData.FirstName));
                data.Add(new("Фамилия", personData.SecondName));

                var contactData = Entity.ContactData;
                data.Add(new("Н.тел.", contactData.ContactNumber));
                data.Add(new("Почта", contactData.Email));
                data.Add(new("Иное", contactData.Other ?? "Отсутствует"));

                return new EntityInfoViewModel(data);
            }
        }

        #endregion
    }
}
