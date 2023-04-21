using Microsoft.Extensions.DependencyInjection;

namespace StripeCreator.WPF
{
    /// <summary>
    /// Расширения для команд в IoC 
    /// </summary>
    public static class CommandRegister
    {
        /// <summary>
        /// Метод расширения для регистрации команд IoC
        /// </summary>
        /// <param name="services">Коллекция сервисов</param>
        /// <returns>Коллекция сервисов</returns>
        public static IServiceCollection AddCommands(this IServiceCollection services)
        {
            services.AddTransient<GetAllThreadsCommand>();
            return services;
        }
    }
}
