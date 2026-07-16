using MongoDB.Driver;
using VeterinariaGenesis.Domain.Entities;
using Microsoft.Extensions.Configuration;

namespace VeterinariaGenesis.Infrastructure.Data
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(IConfiguration configuration)
        {
            var connectionString = configuration["MongoDB:ConnectionString"] ?? "mongodb://localhost:27017";
            var databaseName = configuration["MongoDB:DatabaseName"] ?? "VeterinariaGenesis";

            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(databaseName);
        }

        public IMongoCollection<Cliente> Clientes => _database.GetCollection<Cliente>("Clientes");
        public IMongoCollection<Mascota> Mascotas => _database.GetCollection<Mascota>("Mascotas");
        public IMongoCollection<Trabajador> Trabajadores => _database.GetCollection<Trabajador>("Trabajadores");
        public IMongoCollection<Proveedor> Proveedores => _database.GetCollection<Proveedor>("Proveedores");
        public IMongoCollection<Producto> Productos => _database.GetCollection<Producto>("Productos");
        public IMongoCollection<Factura> Facturas => _database.GetCollection<Factura>("Facturas");
        public IMongoCollection<EventoMedico> EventosMedicos => _database.GetCollection<EventoMedico>("EventosMedicos");
        public IMongoCollection<Cita> Citas => _database.GetCollection<Cita>("Citas");
        public IMongoCollection<NotificacionWhatsappLog> NotificacionWhatsappLogs => _database.GetCollection<NotificacionWhatsappLog>("NotificacionWhatsappLogs");
        public IMongoCollection<Gasto> Gastos => _database.GetCollection<Gasto>("Gastos");
        public IMongoCollection<Pedido> Pedidos => _database.GetCollection<Pedido>("Pedidos");
    }
}
