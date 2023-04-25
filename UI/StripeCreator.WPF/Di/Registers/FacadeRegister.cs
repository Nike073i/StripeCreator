using Microsoft.Extensions.DependencyInjection;

namespace StripeCreator.WPF
{
    /// <summary>
    /// Расширения для фасадов в IoC 
    /// </summary>
    public static class FacadeRegister
    {
        /// <summary>
        /// Метод расширения для регистрации сервисов в IoC
        /// </summary>
        /// <param name="services">Коллекция сервисов</param>
        /// <returns>Коллекция сервисов</returns>
        public static IServiceCollection AddFacades(this IServiceCollection services)
        {
            services.AddTransient<ClientFacade>();
            services.AddTransient<ClothFacade>();
            services.AddTransient<ThreadFacade>();
            services.AddTransient<ProductFacade>();
            services.AddTransient<OrderFacade>();

            return services;
        }
    }
}
