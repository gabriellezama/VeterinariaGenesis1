using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using VeterinariaGenesis.Domain.Entities;
using VeterinariaGenesis.Infrastructure.Data;

namespace VeterinariaGenesis.Infrastructure.Repositories
{
    public class CitaRepository
    {
        private readonly IMongoCollection<Cita> _collection;

        public CitaRepository(MongoDbContext context)
        {
            _collection = context.Citas;
        }

        public async Task<List<Cita>> GetAllAsync()
            => await _collection.Find(_ => true).ToListAsync();

        public async Task<Cita?> GetByIdAsync(Guid id)
            => await _collection.Find(c => c.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Cita cita)
            => await _collection.InsertOneAsync(cita);

        public async Task UpdateAsync(Guid id, Cita cita)
            => await _collection.ReplaceOneAsync(c => c.Id == id, cita);

        public async Task DeleteAsync(Guid id)
            => await _collection.DeleteOneAsync(c => c.Id == id);
    }
}
