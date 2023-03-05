using StripeCreator.VK.Models;
using VkNet;
using VkNet.Model.RequestParams;

namespace StripeCreator.VK.Repositories
{
    /// <summary>Репозиторий записей сообщества</summary>
    public class WallRepository : VkRepository
    {
        #region Constants

        /// <summary>Минимальная длина сообщения для публикации</summary>
        private const int WallPostMinLength = 10;

        #endregion

        #region Constructors

        /// <summary>Конструктор с полной инициализацией</summary>
        /// <param name="vkParameters">Параметры взаимодействия</param>
        public WallRepository(VkParameters vkParameters) : base(vkParameters) { }

        #endregion

        #region Public methods

        /// <summary>Добавить запись в сообщество</summary>
        /// <param name="message">Сообщение к публикации</param>
        /// <param name="photoPath">Абсолютный путь к изображению</param>
        /// <exception cref="ArgumentNullException">Возникает, если указано сообщение с некорректной длиной</exception>
        /// <exception cref="ArgumentException">Возникает, если изображение по указанному пути не найдено</exception>
        /// <exception cref="InvalidOperationException">Возникает, если произошла ошибка запроса к API</exception>
        public async Task PostAsync(string message, string? photoPath = null)
        {
            if (string.IsNullOrWhiteSpace(message) || message.Length < WallPostMinLength)
                throw new ArgumentNullException(nameof(message),
                    $"Указано сообщение с длиной меньше < {WallPostMinLength}");
            var postParams = new WallPostParams
            {
                OwnerId = -GroupId,
                Message = message,
                FromGroup = true
            };
            if (photoPath != null)
            {
                var photoInfo = new FileInfo(photoPath);
                if (!photoInfo.Exists)
                    throw new ArgumentException($"Изображение по пути {photoPath} не найдено");
                var uploadServer = await VkApi.Photo.GetWallUploadServerAsync(GroupId);
                using var httpClient = new HttpClient();
                var imageBytes = await File.ReadAllBytesAsync(photoPath);
                var requestContent = new MultipartFormDataContent
                {
                    { new ByteArrayContent(imageBytes), "file", photoInfo.Name },
                };
                var uploadResponse = await httpClient.PostAsync(uploadServer.UploadUrl, requestContent);
                if (!uploadResponse.IsSuccessStatusCode)
                    throw new InvalidOperationException("Ошибка запроса загрузки изображения");
                var responseContent = await uploadResponse.Content.ReadAsStringAsync();
                var photo = await VkApi.Photo.SaveWallPhotoAsync(responseContent, (ulong?)VkApi.UserId, (ulong)GroupId);
                var mainPhoto = photo.First() ??
                                throw new InvalidOperationException("Ошибка загрузки изображения. Идентификатор не присвоен");
                postParams.Attachments = new[] { mainPhoto };
            }
            await VkApi.Wall.PostAsync(postParams);
        }

        #endregion
    }
}