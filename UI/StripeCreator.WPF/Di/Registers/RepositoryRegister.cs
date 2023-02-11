using Microsoft.Extensions.DependencyInjection;
using StripeCreator.Business.Repositories;
using StripeCreator.DAL.Repositories;
using StripeCreator.Stripe.Repositories;

namespace StripeCreator.WPF
{
    /// <summary>
    /// Расширения для репозиториев в IoC 
    /// </summary>
    public static class RepositoryRegister
    {
        /// <summary>
        /// Метод расширения для регистрации репозиториев БД в IoC
        /// </summary>
        /// <param name="services">Коллекция сервисов</param>
        /// <returns>Коллекция сервисов</returns>
        public static IServiceCollection AddDbRepository(this IServiceCollection services)
        {
            services.AddTransient<IClientRepository, DbClientRepository>();
            services.AddTransient<IThreadRepository, DbThreadRepository>();
            services.AddTransient<IClothRepository, DbClothRepository>();
            return services;
        }
    }
}
