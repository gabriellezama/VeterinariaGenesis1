using MongoDB.Driver;
using VeterinariaGenesis.Domain.Entities;
using VeterinariaGenesis.Infrastructure.Data;

namespace VeterinariaGenesis.Infrastructure.Repositories
{
    public class TrabajadorRepository
    {
        private readonly IMongoCollection<Trabajador> _collection;

        public TrabajadorRepository(MongoDbContext context)
        {
            _collection = context.Trabajadores;
        }

        public async Task<List<Trabajador>> GetAllAsync()
            => await _collection.Find(_ => true).ToListAsync();

        public async Task CreateAsync(Trabajador trabajador)
            => await _collection.InsertOneAsync(trabajador);

        public async Task UpdateAsync(Guid id, Trabajador trabajador)
            => await _collection.ReplaceOneAsync(t => t.Id == id, trabajador);

        public async Task DeleteAsync(Guid id)
            => await _collection.DeleteOneAsync(t => t.Id == id);
    }
}
