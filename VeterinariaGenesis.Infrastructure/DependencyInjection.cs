using Microsoft.Extensions.DependencyInjection;
using VeterinariaGenesis.Application.Interfaces;
using VeterinariaGenesis.Infrastructure.Data;
using VeterinariaGenesis.Infrastructure.Repositories;

namespace VeterinariaGenesis.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<IHistorialMedicoRepository, HistorialMedicoRepository>();
            
            // MongoDB Context (Singleton - una sola conexión)
            services.AddSingleton<MongoDbContext>();
            
            // Repositories (Singleton para reutilizar la misma instancia)
            services.AddSingleton<ClienteRepository>();
            services.AddSingleton<MascotaRepository>();
            services.AddSingleton<TrabajadorRepository>();
            services.AddSingleton<ProveedorRepository>();
            services.AddSingleton<ProductoRepository>();
            services.AddSingleton<FacturaRepository>();
            services.AddSingleton<CitaRepository>();
            services.AddSingleton<NotificacionWhatsappLogRepository>();
            services.AddSingleton<GastoRepository>();
            services.AddSingleton<PedidoRepository>();
            
            return services;
        }
    }
}
