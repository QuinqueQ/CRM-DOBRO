using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
        services.AddScoped<UserService>();
        services.AddScoped<ContactService>();
        services.AddScoped<LeadService>();
        services.AddScoped<SaleService>();

        services.AddAuthentication().AddCookie("Cookies", opts =>
        {
            opts.ExpireTimeSpan = TimeSpan.FromMinutes(30);
            opts.Events.OnRedirectToLogin = (context) =>
            {
                context.Response.StatusCode = 401;
                return Task.CompletedTask;
            };
            opts.Events.OnRedirectToAccessDenied = (context) =>
            {
                context.Response.StatusCode = 403;
                return Task.CompletedTask;
            };
        });

        return services;
    }
}
