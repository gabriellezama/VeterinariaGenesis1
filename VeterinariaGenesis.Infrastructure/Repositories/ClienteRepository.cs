using MongoDB.Driver;
using VeterinariaGenesis.Domain.Entities;
using VeterinariaGenesis.Infrastructure.Data;

namespace VeterinariaGenesis.Infrastructure.Repositories
{
    public class ClienteRepository
    {
        private readonly IMongoCollection<Cliente> _collection;

        public ClienteRepository(MongoDbContext context)
        {
            _collection = context.Clientes;
        }

        public async Task<List<Cliente>> GetAllAsync()
            => await _collection.Find(_ => true).ToListAsync();

        public async Task CreateAsync(Cliente cliente)
            => await _collection.InsertOneAsync(cliente);

        public async Task UpdateAsync(Guid id, Cliente cliente)
            => await _collection.ReplaceOneAsync(c => c.Id == id, cliente);

        public async Task DeleteAsync(Guid id)
            => await _collection.DeleteOneAsync(c => c.Id == id);
    }
}
