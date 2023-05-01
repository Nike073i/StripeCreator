namespace StripeCreator.Stripe.Interfaces
{
    /// <summary>
    /// Интерфейс хранителя данных
    /// </summary>
    /// <typeparam name="TData">Тип хранимых данных</typeparam>
    public interface IDataKeeper<TData>
    {
        /// <summary>
        /// Загрузка данных по абсолютному пути
        /// </summary>
        /// <param name="path">Абсолютный путь к данным</param>
        /// <returns>Данные</returns>
        Task<TData> LoadAsync(string path);

        /// <summary>
        /// Сохранение данных по абсолютному пути
        /// </summary>
        /// <param name="path">Абсолютный путь сохранения данных</param>
        /// <param name="data">Сохраняемые данные</param>
        /// <returns></returns>
        Task SaveAsync(string path, TData data);
    }
}