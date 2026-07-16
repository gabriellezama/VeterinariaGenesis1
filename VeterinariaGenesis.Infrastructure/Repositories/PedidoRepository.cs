using MongoDB.Driver;
using VeterinariaGenesis.Domain.Entities;
using VeterinariaGenesis.Infrastructure.Data;

namespace VeterinariaGenesis.Infrastructure.Repositories
{
    public class PedidoRepository
    {
        private readonly IMongoCollection<Pedido> _pedidos;

        public PedidoRepository(MongoDbContext context)
        {
            _pedidos = context.Pedidos;
        }

        public async Task<List<Pedido>> GetAllAsync() =>
            await _pedidos.Find(_ => true).ToListAsync();

        public async Task<Pedido?> GetByIdAsync(Guid id) =>
            await _pedidos.Find(p => p.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Pedido pedido) =>
            await _pedidos.InsertOneAsync(pedido);

        public async Task UpdateAsync(Guid id, Pedido pedido) =>
            await _pedidos.ReplaceOneAsync(p => p.Id == id, pedido);

        public async Task DeleteAsync(Guid id) =>
            await _pedidos.DeleteOneAsync(p => p.Id == id);
            
        public async Task UpdateEstadoAsync(Guid id, string nuevoEstado)
        {
            var update = Builders<Pedido>.Update.Set(p => p.Estado, nuevoEstado);
            await _pedidos.UpdateOneAsync(p => p.Id == id, update);
        }
    }
}
