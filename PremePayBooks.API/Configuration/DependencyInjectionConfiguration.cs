using Microsoft.Extensions.DependencyInjection;
using PremePayBooks.API.Data;

namespace PremePayBooks.API.Configuration
{
    public static class DependencyInjectionConfiguration
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();

            services.AddScoped<DataContext>();
        }
    }
}