using StripeCreator.Business.Models;
using StripeCreator.Stripe.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StripeCreator.WPF
{
    /// <summary>
    /// Менеджер для интерактивного взаимодействия с пользователем
    /// </summary>
    public interface IUiManager
    {
        /// <summary>
        /// Отобразить информацию для пользователя
        /// </summary>
        /// <param name="viewModel">ViewModel сообщения</param>
        Task ShowInfo(MessageBoxDialogViewModel viewModel);

        /// <summary>
        /// Отобразить ошибку для пользователя
        /// </summary>
        /// <param name="viewModel">ViewModel сообщения</param>
        Task ShowError(MessageBoxDialogViewModel viewModel);

        /// <summary>
        /// Отобразить окно подтверждения для пользователя
        /// </summary>
        /// <param name="viewModel">ViewModel сообщения</param>
        /// <returns>Выбор пользователя</returns>
        Task<bool> ShowConfirm(MessageBoxDialogViewModel viewModel);

        /// <summary>
        /// Отобразить окно формирования сущности
        /// </summary>
        /// <param name="entity">ViewModel сформированной сущности, если формирование прошло успешно
        /// null - если формирование отмененно</param>
        Task<IEntityViewModel?> FormationEntity(EntityFormationViewModel entityFormationViewModel);

        /// <summary>
        /// Отобразить окно создания заказа
        /// </summary>
        /// <param name="products">Список продукции</param>
        /// <param name="clients">Список клиентов</param>
        /// <returns></returns>
        Task<OrderCreateModel?> CreateOrder(IEnumerable<Product> products, IEnumerable<Client> clients);

        /// <summary>
        /// Отобразить окно просмотра информации по заказу
        /// </summary>
        /// <param name="orderDetailViewModel">ViewModel информации по заказу</param>
        Task ShowOrderDetail(OrderDetailViewModel orderDetailViewModel);

        /// <summary>
        /// Отобразить окно расчета материалов
        /// </summary>
        /// <param name="scheme">Схема</param>
        Task CalculateMaterial(Scheme scheme);

        /// <summary>
        /// Отобразить окно расчета стоимости
        /// </summary>
        /// <param name="scheme">Схема</param>
        /// <param name="threads">Хранимые нити</param>
        /// <param name="cloths">Хранимые ткани</param>
        Task CalculatePrice(Scheme scheme, IEnumerable<Thread> threads, IEnumerable<Cloth> cloths);
    }
}
