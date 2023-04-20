using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StripeCreator.VK.Models;
using StripeCreator.VK.Repositories;
using StripeCreator.VK.Services;
using System;

namespace StripeCreator.WPF
{
    /// <summary>
    /// Расширения для взаимодействия с VK в IoC 
    /// </summary>
    public static class VkRegister
    {
        /// <summary>
        /// Метод расширения для регистрации сервисов в IoC
        /// </summary>
        /// <param name="services">Коллекция сервисов</param>
        /// <returns>Коллекция сервисов</returns>
        public static IServiceCollection AddVkServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(provider =>
            {
                var accessToken = configuration["AccessToken"];
                if (string.IsNullOrWhiteSpace(accessToken))
                    throw new InvalidOperationException("Токен не указан");
                var groupId = configuration.GetValue<long>("GroupId");
                if (groupId == 0)
                    throw new InvalidOperationException("Идентификатор сообщества не указан");
                return new VkParameters(accessToken, groupId);
            });
            services.AddTransient<CommunityService>();
            return services;
        }

        /// <summary>
        /// Метод расширения для регистрации репозиториев в IoC
        /// </summary>
        /// <param name="services">Коллекция сервисов</param>
        /// <returns>Коллекция сервисов</returns>
        public static IServiceCollection AddVkRepositories(this IServiceCollection services)
        {
            services.AddTransient<VkWallRepository>();
            services.AddTransient<MarketRepository>();
            return services;
        }
    }
}
