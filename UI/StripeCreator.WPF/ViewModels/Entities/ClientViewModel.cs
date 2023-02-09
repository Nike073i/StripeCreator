using StripeCreator.Business.Models;

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
                // TODO : РЕАЛИЗОВАТЬ МЕТОД
                throw new System.Exception();
            }
        }

        #endregion
    }
}
