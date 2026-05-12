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
    }
}
