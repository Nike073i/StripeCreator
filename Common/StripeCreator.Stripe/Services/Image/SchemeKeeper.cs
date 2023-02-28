using StripeCreator.Stripe.Models;
using System.Runtime.Serialization.Formatters.Binary;

namespace StripeCreator.Stripe.Services
{
    /// <summary>
    /// Хранитель данных <see cref="Scheme"/>
    /// </summary>
    public class SchemeKeeper : IDataKeeper<Scheme>
    {
        #region Interface implementations

        /// <summary>
        /// Загрузка схемы из файла
        /// </summary>
        /// <param name="path">Абсолютный путь к схеме</param>
        /// <returns><see cref="Scheme"/> - Схема вышивки</returns>
        /// <exception cref="FileNotFoundException">Возникает, если файл со схемой по указанному пути отсутствует</exception>
        public Task<Scheme> LoadAsync(string path)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException("Схема по указаному пути не найдена");
            var formatter = new BinaryFormatter();
            using var stream = new FileStream(path, FileMode.Open, FileAccess.Read);
            var scheme = (Scheme)formatter.Deserialize(stream);
            return Task.FromResult(scheme);
        }

        /// <summary>
        /// Сохранение схемы в файл
        /// </summary>
        /// <param name="path">Абсолютный путь сохранения схемы</param>
        /// <param name="scheme">Схема для сохранения</param>
        public Task SaveAsync(string path, Scheme scheme)
        {
            var formatter = new BinaryFormatter();
            using var stream = new FileStream(path, FileMode.OpenOrCreate);
            formatter.Serialize(stream, scheme);
            return Task.CompletedTask;
        }

        #endregion
    }
}