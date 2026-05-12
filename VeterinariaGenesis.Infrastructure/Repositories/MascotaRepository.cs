using MongoDB.Driver;
using VeterinariaGenesis.Domain.Entities;
using VeterinariaGenesis.Infrastructure.Data;

namespace VeterinariaGenesis.Infrastructure.Repositories
{
    public class MascotaRepository
    {
        private readonly IMongoCollection<Mascota> _collection;

        public MascotaRepository(MongoDbContext context)
        {
            _collection = context.Mascotas;
        }

        public async Task<List<Mascota>> GetAllAsync()
            => await _collection.Find(_ => true).ToListAsync();

        public async Task<List<Mascota>> GetByClienteAsync(Guid clienteId)
            => await _collection.Find(m => m.ClienteId == clienteId).ToListAsync();

        public async Task CreateAsync(Mascota mascota)
            => await _collection.InsertOneAsync(mascota);
    }
}
