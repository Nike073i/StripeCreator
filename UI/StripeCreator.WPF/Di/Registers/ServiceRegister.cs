﻿using Microsoft.Extensions.DependencyInjection;
using StripeCreator.Business.Services;
using StripeCreator.Statistic.Services;
using StripeCreator.Stripe.Interfaces;
using StripeCreator.Stripe.Models;
using StripeCreator.Stripe.Services;

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
            services.AddTransient<ClientService>();
            services.AddTransient<ProductService>();
            services.AddTransient<ClothService>();
            services.AddTransient<OrderPriceCalculator>();
            services.AddTransient<OrderService>();
            services.AddTransient<ThreadService>();
            services.AddTransient<StatisticService>();
            services.AddTransient<ReportService>();

            services.AddTransient<IUiManager, DialogUiManager>();

            services.AddTransient<IDataKeeper<Image>, ImageKeeper>();
            services.AddTransient<ImageService>();
            services.AddTransient<SchemeVisualizer>();
            services.AddTransient<IDataKeeper<Scheme>, SchemeKeeper>();
            services.AddTransient<ISchemeDescriptor, TxtSchemeDescriptor>();

            return services;
        }
    }
}
