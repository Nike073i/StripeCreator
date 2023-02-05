using Newtonsoft.Json;
using StripeCreator.Stripe.Models;

namespace StripeCreator.Stripe.Services
{
    /// <summary>
    /// Хранитель данных <see cref="Scheme"/> в JSON
    /// </summary>
    public class JsonSchemeKeeper : IDataKeeper<Scheme>
    {
        #region Public properties

        /// <summary>
        /// Запись данных с отступами и переносами строк
        /// False - запись сплошным текстом
        /// True - запись в удобочитаемом виде
        /// </summary>
        public bool WriteIndented { get; set; } = false;

        #endregion

        #region Interface implementations

        /// <summary>
        /// Загрузка схемы из json файла
        /// </summary>
        /// <param name="path">Абсолютный путь к файлу сохранения в json формате</param>
        /// <returns><see cref="Scheme"/> - загруженные данные схемы</returns>
        /// <exception cref="FileNotFoundException">Возникает, если файл со схемой по указанному пути отсутствует</exception>
        /// <exception cref="ArgumentException">Возникает, если указанный файл не имеет расширение "json"</exception>
        /// <exception cref="JsonSerializationException">Возникает, если произошла ошибка при десериализации схемы из файла</exception>
        public async Task<Scheme> LoadAsync(string path)
        {
            var fileInfo = new FileInfo(path);
            if (!fileInfo.Exists)
                throw new FileNotFoundException($"Схема по пути {path} не найдена");
            if (fileInfo.Extension != ".json")
                throw new ArgumentException($"Указан файл с неподходящим расширением - {fileInfo.Extension}");

            var data = await File.ReadAllTextAsync(path);
            var scheme = JsonConvert.DeserializeObject<Scheme>(data);

            return scheme ?? throw new JsonSerializationException($"Ошибка десериализации схемы по пути {path}");
        }

        /// 
        /// <summary>
        /// Сохранения схемы в файл
        /// </summary>
        /// <param name="path">Абсолютный путь сохранения схемы</param>
        /// <param name="scheme">Данные схемы для сохранения</param>
        /// <param name="writeIndented">/></param>
        /// <returns></returns>
        public async Task SaveAsync(string path, Scheme scheme)
        {
            var formating = WriteIndented ? Formatting.Indented : Formatting.None;
            var json = JsonConvert.SerializeObject(scheme, formating);
            await File.WriteAllTextAsync(path, json);
        }

        #endregion
    }
}