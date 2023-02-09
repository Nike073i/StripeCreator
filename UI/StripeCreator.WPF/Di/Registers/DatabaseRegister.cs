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
        /// Название проекта с миграциями для SqlServer
        /// </summary>
        private const string SqlServerMigrationAssembly = "StripeCreator.DAL.SqlServer";

        /// <summary>
        /// Метод расширения для регистрации MSSQL IoC
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddSqlServer(this IServiceCollection services, IConfiguration сonfig)
        {
            services.AddDbContext<StripeCreatorDb>(opt =>
            {
                var type = сonfig["Type"];
                if (string.IsNullOrEmpty(type))
                    throw new InvalidOperationException($"Тип подключения не указан");
                if (!type.Equals("MSSQL"))
                    throw new InvalidOperationException($"Тип подключения {type} не поддерживается");
                opt.UseSqlServer(сonfig.GetConnectionString(type), o => o.MigrationsAssembly(SqlServerMigrationAssembly));
            });
            return services;
        }
    }
}
