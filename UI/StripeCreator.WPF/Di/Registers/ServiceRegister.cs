using Microsoft.Extensions.DependencyInjection;
using StripeCreator.Business.Services;

namespace StripeCreator.WPF
{
    /// <summary>
    /// Расширения для сервисов в IoC 
    /// </summary>
    public static class ServiceRegister
    {
        /// <summary>
        /// Метод расширения для регистрации сервисов в IoC
        /// </summary>
        /// <param name="services">Коллекция сервисов</param>
        /// <returns>Коллекция сервисов</returns>
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<IUiManager, DialogUiManager>();
            services.AddTransient<ClientService>();
            services.AddTransient<ProductService>();
            services.AddTransient<ThreadService>();
            services.AddTransient<ClothService>();
            services.AddTransient<OrderPriceCalculator>();
            services.AddTransient<SaleService>();
            services.AddTransient<OrderService>();
            return services;
        }
    }
}
