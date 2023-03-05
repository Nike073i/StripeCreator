using ImageMagick;
using StripeCreator.VK.Models;
using StripeCreator.VK.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace StripeCreator.WPF.Services
{
    /// <summary>Сервис для работы с сообществом во "ВКонтакте"</summary>
    public class CommunityService
    {
        #region Private fields

        /// <summary>Репозиторий записей сообщества</summary>
        private readonly WallRepository _wallRepository;

        /// <summary>Репозиторий товаров сообщества</summary>
        private readonly MarketRepository _marketRepository;

        #endregion

        #region Constructors

        /// <summary>Конструктор с полной инициализацией</summary>
        /// <param name="wallRepository">Репозиторий записей</param>
        /// <param name="marketRepository">Репозиторий товаров</param>
        public CommunityService(WallRepository wallRepository, MarketRepository marketRepository)
        {
            _wallRepository = wallRepository;
            _marketRepository = marketRepository;
        }

        #endregion

        #region Public methods

        /// <summary>Запрос на получение всех товаров в виде ViewModel</summary>
        /// <returns>Список ViewModel сущностей</returns>
        public async Task<IEnumerable<MarketViewModel>> GetAllAsync()
        {
            var markets = await _marketRepository.GetAllAsync();
            return markets.Select(market => new MarketViewModel(market));
        }

        /// <summary>Запрос на добавление товара</summary>
        /// <param name="createModel">ViewModel сохраняемого товара</param>
        public Task AddAsync(MarketCreateModel createModel)
        {
            var photoPath = createModel.PhotoPath;
            if (!File.Exists(photoPath))
                throw new ArgumentException($"Изображение по пути {photoPath} не найдено");
            var imageInfo = new MagickImageInfo(photoPath);
            if (imageInfo.Width < Photo.ImageMinWidth || imageInfo.Width > Photo.ImageMaxWidth)
                throw new ArgumentException($"Ширина изображения должна быть от {Photo.ImageMinWidth} до {Photo.ImageMaxWidth}");
            if (imageInfo.Height < Photo.ImageMinHeight || imageInfo.Height > Photo.ImageMaxHeight)
                throw new ArgumentException($"Высота изображения должна быть от {Photo.ImageMinHeight} до {Photo.ImageMaxHeight}");

            return _marketRepository.AddAsync(createModel.Market, photoPath);
        }

        /// <summary>Запрос на редактирование товара</summary>
        /// <param name="market">Измененный товар</param>
        public Task<bool> EditAsync(Market market) => _marketRepository.EditAsync(market);

        /// <summary>Запрос на удаление товара</summary>
        /// <param name="market">Удаляемый товар</param>
        public Task<bool> RemoveAsync(Market market)
        {
            var marketId = market.Id ??
                throw new ArgumentNullException(nameof(market), "Отсутствует идентификатор у удаляемого товара");
            return _marketRepository.RemoveAsync(marketId);
        }

        /// <summary>Запрос на публикацию записи</summary>
        /// <param name="publishModel">ViewModel новой записи</param>
        public Task PostMessageAsync(PublishMessageModel publishModel)
        {
            var photoPath = publishModel.PhotoPath;
            if (photoPath != null)
            {
                if (!File.Exists(photoPath))
                    throw new ArgumentException($"Изображение по пути {photoPath} не найдено");
                var imageInfo = new MagickImageInfo(photoPath);
                if (imageInfo.Width < Photo.ImageMinWidth || imageInfo.Width > Photo.ImageMaxWidth)
                    throw new ArgumentException($"Ширина изображения должна быть от {Photo.ImageMinWidth} до {Photo.ImageMaxWidth}");
                if (imageInfo.Height < Photo.ImageMinHeight || imageInfo.Height > Photo.ImageMaxHeight)
                    throw new ArgumentException($"Высота изображения должна быть от {Photo.ImageMinHeight} до {Photo.ImageMaxHeight}");
            }
            return _wallRepository.PostAsync(publishModel.Message, photoPath);
        }

        #endregion
    }
}
