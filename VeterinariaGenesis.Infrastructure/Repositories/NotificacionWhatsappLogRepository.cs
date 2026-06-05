using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using VeterinariaGenesis.Domain.Entities;
using VeterinariaGenesis.Infrastructure.Data;

namespace VeterinariaGenesis.Infrastructure.Repositories
{
    public class NotificacionWhatsappLogRepository
    {
        private readonly IMongoCollection<NotificacionWhatsappLog> _collection;

        public NotificacionWhatsappLogRepository(MongoDbContext context)
        {
            _collection = context.NotificacionWhatsappLogs;
        }

        public async Task<List<NotificacionWhatsappLog>> GetAllAsync()
            => await _collection.Find(_ => true).SortByDescending(log => log.FechaEnvio).ToListAsync();

        public async Task CreateAsync(NotificacionWhatsappLog log)
            => await _collection.InsertOneAsync(log);

        public async Task DeleteAsync(Guid id)
            => await _collection.DeleteOneAsync(log => log.Id == id);

        public async Task DeleteAllAsync()
            => await _collection.DeleteManyAsync(_ => true);
    }
}
