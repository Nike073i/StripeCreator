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
        private static IHost? _host;

        #endregion

        #region Host building

        public static void CreateHost(string[] args, string contentPath, string settingsFile)
        {
            _host = Host.CreateDefaultBuilder(args)
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

        public static Task StartAsync()
        {
            if (_host == null) throw new Exception(HostNotCreatedError);
            return _host.StartAsync();
        }

        public static Task StopAsync()
        {
            if (_host == null) throw new Exception(HostNotCreatedError);
            return _host.StopAsync();
        }

        #endregion

        #region Access methods

        public static T GetRequiredService<T>()
        {
            if (_host == null) throw new Exception(HostNotCreatedError);
            return _host.Services.GetRequiredService<T>();
        }

        public static T GetService<T>()
        {
            if (_host == null) throw new Exception(HostNotCreatedError);
            return _host.Services.GetService<T>();
        }

        #endregion
    }
}
