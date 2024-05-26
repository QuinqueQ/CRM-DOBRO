using Domain.Abstractions.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrasctucture.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrasctucture(
            this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CRMDBContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ISaleRepository, SaleRepository>();
            services.AddScoped<ILeadRepository, LeadRepository>();
            services.AddScoped<IContactRepository, ContactRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
