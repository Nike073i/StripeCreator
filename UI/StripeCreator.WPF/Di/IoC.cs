using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;

namespace StripeCreator.WPF
{
    public static class IoC
    {
        #region Private fields

        private static readonly string HostNotCreatedError = "Ошибка взаимодействия с хостом. Хост не был создан";
        private static IHost? _container;

        #endregion

        #region Public properties

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

        public static void CreateHost(string[] args, string contentPath, string settingsFile)
        {
            Container = Host.CreateDefaultBuilder(args)
                        .UseContentRoot(contentPath)
                        .ConfigureAppConfiguration((host, cfg) =>
                            cfg.SetBasePath(contentPath)
                                .AddJsonFile(settingsFile, true, true))
                        .ConfigureServices(ConfigureServices)
                        .Build();
        }

        private static void ConfigureServices(HostBuilderContext host, IServiceCollection services)
        {

        }

        #endregion

        #region Host management

        public static Task StartAsync() => Container.StartAsync();

        public static Task StopAsync() => Container.StopAsync();

        #endregion

        #region Access methods

        public static T GetRequiredService<T>() => Container.Services.GetRequiredService<T>();

        public static T GetService<T>() => Container.Services.GetService<T>();

        #endregion
    }
}
