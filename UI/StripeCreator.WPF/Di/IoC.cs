using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Threading.Tasks;

namespace StripeCreator.WPF
{
    /// <summary>
    /// Хост IoC
    /// </summary>
    public static class IoC
    {
        #region Private fields

        /// <summary>
        /// Сообщение об ошибке использования хоста
        /// </summary>
        private static readonly string HostNotCreatedError = "Ошибка взаимодействия с хостом. Хост не был создан";

        /// <summary>
        /// Контейнер
        /// </summary>
        private static IHost? _container;

        #endregion

        #region Public properties

        /// <summary>
        /// Контейнер
        /// </summary>
        public static IHost Container
        {
            get
            {
                if (_container == null) throw new Exception(HostNotCreatedError);
                return _container;
            }
            private set => _container = value;
        }

        #endregion

        #region Host building

        /// <summary>
        /// Создание хоста IoC
        /// </summary>
        /// <param name="args">Аргументы запуска</param>
        /// <param name="contentPath">Стартовая директория</param>
        /// <param name="settingsFile">Путь к файлу-конфигурации</param>
        public static void CreateHost(string[] args, string contentPath, string settingsFile)
        {
            if (!File.Exists(settingsFile)) 
                throw new ArgumentNullException(nameof(settingsFile), $"Отсутствует файл конфигурации {settingsFile}");
            Container = Host.CreateDefaultBuilder(args)
                        .UseContentRoot(contentPath)
                        .ConfigureAppConfiguration((host, cfg) =>
                            cfg.SetBasePath(contentPath)
                                .AddJsonFile(settingsFile, true, true))
                        .ConfigureServices(ConfigureServices)
                        .Build();
        }

        /// <summary>
        /// Конфигурация сервисов
        /// </summary>
        /// <param name="host">Хост контейнера</param>
        /// <param name="services">Сервисы</param>
        private static void ConfigureServices(HostBuilderContext host, IServiceCollection services)
        {
            services.AddSqlServer(host.Configuration.GetSection("Database"))
                .AddDbRepository()
                .AddServices()
                .AddVkServices(host.Configuration.GetSection("VK"))
                .AddVkRepositories()
                .AddPagesViewModel();
        }

        #endregion

        #region Host management

        /// <summary>
        /// Запуск контейнера
        /// </summary>
        /// <returns></returns>
        public static async Task StartAsync() => await Container.StartAsync();

        /// <summary>
        /// Остановка контейнера
        /// </summary>
        /// <returns></returns>
        public static async Task StopAsync()
        {
            await Container.StopAsync();
            Container.Dispose();
        }

        #endregion

        #region Access methods

        /// <summary>
        /// Запрос обязательного сервиса
        /// </summary>
        /// <typeparam name="T">Тип запрашиваемого сервиса</typeparam>
        /// <returns>Запрашиваемый сервис</returns>
        public static T GetRequiredService<T>() where T : notnull => Container.Services.GetRequiredService<T>();

        /// <summary>
        /// Запрос необязательного сервиса
        /// </summary>
        /// <typeparam name="T">Тип запрашиваемого сервиса</typeparam>
        /// <returns>Запрашиваемый сервис</returns>
        public static T? GetService<T>() => Container.Services.GetService<T>();

        #endregion
    }
}
