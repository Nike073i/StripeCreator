using StripeCreator.Business.Models;
using StripeCreator.Stripe.Models;
using StripeCreator.VK.Models;
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
        /// <param name="entityFormationViewModel">ViewModel формируемой сущности</param>
        /// <returns>
        /// ViewModel сформированной сущности, если формирование прошло успешно
        /// null - если формирование отмененно
        /// </returns>>
        Task<IEntityViewModel?> FormationEntity(EntityFormationViewModel entityFormationViewModel);

        /// <summary>
        /// Отобразить окно создания заказа
        /// </summary>
        /// <param name="products">Список продукции</param>
        /// <param name="clients">Список клиентов</param>
        /// <returns>Модель создания заказа</returns>
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

        /// <summary>
        /// Отобразить окно создания товара
        /// </summary>
        /// <returns>Модель создания товара</returns>
        Task<MarketCreateModel?> CreateMarket();

        /// <summary>
        /// Отобразить окно редактирования товара
        /// </summary>
        /// <param name="viewModel">ViewModel редактируемого товара</param>
        /// <returns>Модель редактирования товара</returns>
        Task<Market?> EditMarket(MarketViewModel viewModel);

        /// <summary>
        /// Отобразить окно публикации записи
        /// </summary>
        /// <param name="publishMessageViewModel">ViewModel сообщения для публикации</param>
        /// <returns>Модель публикации записи</returns>
        Task<PublishMessageModel?> PublishMessage(PublishMessageViewModel? publishMessageViewModel = null);
    }
}
