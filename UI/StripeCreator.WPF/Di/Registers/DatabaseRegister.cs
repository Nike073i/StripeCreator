using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StripeCreator.DAL;
using System;

namespace StripeCreator.WPF
{
    /// <summary>
    /// Расширения для регистрации базы данных в IoC 
    /// </summary>
    public static class DatabaseRegister
    {
        /// <summary>
        /// Метод расширения для регистрации MSSQL IoC
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration сonfig)
        {
            services.AddDbContext<StripeCreatorDb>(opt =>
            {
                var type = сonfig["Type"];
                if (string.IsNullOrWhiteSpace(type))
                    throw new InvalidOperationException($"Тип подключения не указан");
                var connectionString = сonfig.GetConnectionString(type);
                if (string.IsNullOrWhiteSpace(connectionString))
                    throw new InvalidOperationException($"Строка подключения не указана");
                switch (type)
                {
                    case "MSSQL":
                        opt.UseSqlServer(connectionString);
                        break;
                    case "SQLite":
                        opt.UseSqlite(connectionString);
                        break;
                    default:
                        throw new InvalidOperationException($"Тип подключения {type} не поддерживается");
                }
            });
            return services;
        }
    }
}
