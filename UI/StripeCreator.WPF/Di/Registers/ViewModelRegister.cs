using Microsoft.Extensions.DependencyInjection;

namespace StripeCreator.WPF
{
    public static class ViewModelRegister
    {
        /// <summary>
        /// Метод расширения для регистрации view-model в IoC
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddViewModel(this IServiceCollection services)
        {
            services.AddSingleton<ApplicationViewModel>();
            services.AddTransient<WelcomePageViewModel>();
            return services;
        }
    }
}
