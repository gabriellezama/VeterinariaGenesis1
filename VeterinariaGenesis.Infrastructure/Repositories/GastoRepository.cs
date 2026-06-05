using MongoDB.Driver;
using VeterinariaGenesis.Domain.Entities;
using VeterinariaGenesis.Infrastructure.Data;

namespace VeterinariaGenesis.Infrastructure.Repositories
{
    public class GastoRepository
    {
        private readonly IMongoCollection<Gasto> _collection;

        public GastoRepository(MongoDbContext context)
        {
            _collection = context.Gastos;
        }

        public async Task<List<Gasto>> GetAllAsync()
            => await _collection.Find(_ => true).ToListAsync();

        public async Task CreateAsync(Gasto gasto)
            => await _collection.InsertOneAsync(gasto);

        public async Task UpdateAsync(Guid id, Gasto gasto)
            => await _collection.ReplaceOneAsync(g => g.Id == id, gasto);

        public async Task DeleteAsync(Guid id)
            => await _collection.DeleteOneAsync(g => g.Id == id);
    }
}
