using StripeCreator.VK.Models;
using VkNet;
using VkNet.Model.RequestParams.Market;
using Market = StripeCreator.VK.Models.Market;
using MarketDto = VkNet.Model.Market;

namespace StripeCreator.VK.Repositories
{
    /// <summary>Репозиторий товаров сообщества</summary>
    public class MarketRepository : VkRepository
    {
        #region Constructors

        /// <summary>Конструктор с полной инициализацией</summary>
        /// <param name="vkParameters">Параметры взаимодействия</param>
        public MarketRepository(VkParameters vkParameters) : base(vkParameters) { }

        #endregion

        #region Public methods

        /// <summary>Добавить товар в сообщество</summary>
        /// <param name="market">Новый товар</param>
        /// <param name="photoPath">Абсолютный путь к изображению</param>
        /// <exception cref="ArgumentException">Возникает, если изображение по указанному пути не найдено</exception>
        /// <exception cref="InvalidOperationException">Возникает, если произошла ошибка запроса к API</exception>
        public async Task AddAsync(Market market, string photoPath)
        {
            var photoInfo = new FileInfo(photoPath);
            if (!photoInfo.Exists)
                throw new ArgumentException($"Изображение по пути {photoPath} не найдено");
            var uploadServer = await VkApi.Photo.GetMarketUploadServerAsync(GroupId);
            using var httpClient = new HttpClient();
            var imageBytes = await File.ReadAllBytesAsync(photoPath);
            using var requestContent = new MultipartFormDataContent
            {
                { new ByteArrayContent(imageBytes), "file", photoInfo.Name },
            };
            var uploadResponse = await httpClient.PostAsync(uploadServer.UploadUrl, requestContent);
            if (!uploadResponse.IsSuccessStatusCode)
                throw new InvalidOperationException("Ошибка запроса загрузки изображения");
            var responseContent = await uploadResponse.Content.ReadAsStringAsync();
            if (responseContent.Contains("error"))
                throw new InvalidOperationException("Ошибка запроса загрузки изображения");
            var photo = await VkApi.Photo.SaveMarketPhotoAsync(GroupId, responseContent);
            var mainPhotoId = photo.First().Id ??
                              throw new InvalidOperationException(
                                  "Ошибка загрузки изображения. Идентификатор не присвоен");
            await VkApi.Markets.AddAsync(new MarketProductParams
            {
                OwnerId = -GroupId,
                Name = market.Title,
                Description = market.Description,
                Price = market.Price,
                CategoryId = market.Category.Id,
                MainPhotoId = mainPhotoId,
                OldPrice = 1,
            });
        }

        /// <summary>Изменить товар в сообществе</summary>
        /// <param name="market">Существующий товар</param>
        /// <returns>Результат редактирования товара.
        /// true - редактирование прошло успешно,
        /// false - редактирование отменено</returns>
        /// <exception cref="ArgumentException">Возникает, если у товара <paramref name="market"/> отсутствуют необходимые поля</exception>
        public async Task<bool> EditAsync(Market market)
        {
            var itemId = market.Id ?? throw new ArgumentException("Отсутствует идентификатор у товара");
            var photo = market.MainPhoto ?? throw new ArgumentException("Отсутствует изображение у товара");
            return await VkApi.Markets.EditAsync(new MarketProductParams
            {
                OwnerId = -GroupId,
                ItemId = itemId,
                CategoryId = market.Category.Id,
                Name = market.Title,
                Description = market.Description,
                Price = market.Price,
                OldPrice = 1,
                MainPhotoId = photo.Id,
            });
        }

        /// <summary>Удаление товара из сообщества</summary>
        /// <param name="id">Идентификатор товара</param>
        /// <returns>Результат удаления товара.
        /// true - удаление прошло успешно,
        /// false - удаление отменено</returns>
        public async Task<bool> RemoveAsync(long id) =>
            await VkApi.Markets.DeleteAsync(-GroupId, id);

        /// <summary>Получить все товары в сообществе</summary>
        /// <returns>Список товаров сообщества</returns>
        public async Task<IEnumerable<Market>> GetAllAsync()
        {
            var markets = await VkApi.Markets.GetAsync(-GroupId, extended: true);
            return markets.Select(CreateModel);
        }

        #endregion

        #region Private methods

        /// <summary>Создать модель товара из полученного DTO</summary>
        /// <param name="marketDto">Полученное из запроса DTO</param>
        /// <returns>Модель товара</returns>
        /// <exception cref="ArgumentException">Возникает, если у полученного DTO отсутствуют необходимые значения</exception>
        private Market CreateModel(MarketDto marketDto)
        {
            var title = marketDto.Title;
            var description = marketDto.Description;
            var price = marketDto.Price.Amount / 100m ?? throw new ArgumentException("Не указана стоимость товара");
            var categoryId = marketDto.Category.Id ?? throw new ArgumentException("Не указана категория товара");
            var category = new Category(categoryId, marketDto.Category.Name);
            var dtoPhoto = marketDto.Photos.First();
            var photo = new Photo(
                dtoPhoto.Id ?? throw new ArgumentException("Не указан идентификатор изображения товара"), marketDto.ThumbPhoto);
            var id = marketDto.Id ?? throw new ArgumentException("Не указан идентификатор товара");
            var date = marketDto.Date ?? throw new ArgumentException("Не указана дата создания товара");
            return new Market(title, description, price, category, id, photo, date);
        }

        #endregion
    }
}