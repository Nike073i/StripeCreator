namespace StripeCreator.VK.Models
{
    /// <summary>Данные для взаимодействия с социальной сетью "Вконтакте"</summary>
    public class VkParameters
    {
        #region Public properties

        /// <summary>Токен доступа пользователя</summary>
        public string AccessToken { get; }

        /// <summary>Идентификатор сообщества</summary>
        public long GroupId { get; }

        #endregion

        #region Constructors

        /// <summary>Конструктор с полной инициализацией</summary>
        /// <param name="accessToken">Токен доступа пользователя</param>
        /// <param name="groupId">Идентификатор сообщества</param>
        public VkParameters(string accessToken, long groupId)
        {
            AccessToken = accessToken;
            GroupId = groupId;
        }

        #endregion
    }
}