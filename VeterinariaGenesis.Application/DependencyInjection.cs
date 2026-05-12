using Microsoft.Extensions.DependencyInjection;
using VeterinariaGenesis.Application.Services;

namespace VeterinariaGenesis.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IHistorialMedicoService, HistorialMedicoService>();
            return services;
        }
    }
}
