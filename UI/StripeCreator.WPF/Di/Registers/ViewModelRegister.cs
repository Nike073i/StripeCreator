using Microsoft.Extensions.DependencyInjection;

namespace StripeCreator.WPF
{
    /// <summary>
    /// Расширения для регистрации ViewModel в IoC 
    /// </summary>
    public static class ViewModelRegister
    {
        /// <summary>
        /// Метод расширения для регистрации view-model в IoC
        /// </summary>
        /// <param name="services">Коллекция сервисов</param>
        /// <returns>Коллекция сервисов</returns>
        public static IServiceCollection AddViewModel(this IServiceCollection services)
        {
            services.AddSingleton<ApplicationViewModel>();
            services.AddTransient<WelcomePageViewModel>();
            services.AddTransient<DataStorePageViewModel>();
            services.AddTransient<OrderPageViewModel>();
            return services;
        }
    }
}
