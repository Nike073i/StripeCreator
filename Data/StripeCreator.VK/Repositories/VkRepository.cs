using StripeCreator.VK.Models;
using VkNet;
using VkNet.Model;

namespace StripeCreator.VK.Repositories
{
    /// <summary>
    /// Базовый класс репозитория работы с сущностями "ВКонтакте"
    /// </summary>
    public abstract class VkRepository
    {
        #region Protected properties

        /// <summary>API для работы с VK</summary>
        protected VkApi VkApi { get; }

        /// <summary>Идентификатор сообщества</summary>
        protected long GroupId { get; }

        #endregion

        #region Constructors

        /// <summary>Конструктор с полной инициализацией</summary>
        /// <param name="vkParameters">Параметры взаимодействия</param>
        /// <exception cref="InvalidOperationException">Возникает, если не пройдена авторизация по параметрам <paramref name="vkParameters"/></exception>
        protected VkRepository(VkParameters vkParameters)
        {
            VkApi = new VkApi();
            VkApi.Authorize(new ApiAuthParams
            {
                AccessToken = vkParameters.AccessToken
            });
            if (!VkApi.IsAuthorized)
                throw new InvalidOperationException("Авторизация не пройдена");
            GroupId = vkParameters.GroupId;
        }

        #endregion
    }
}