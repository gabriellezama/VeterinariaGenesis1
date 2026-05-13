using MongoDB.Driver;
using VeterinariaGenesis.Domain.Entities;
using VeterinariaGenesis.Infrastructure.Data;
using VeterinariaGenesis.Application.Interfaces;

namespace VeterinariaGenesis.Infrastructure.Repositories
{
    public class HistorialMedicoRepository : IHistorialMedicoRepository
    {
        private readonly IMongoCollection<EventoMedico> _collection;

        public HistorialMedicoRepository(MongoDbContext context)
        {
            _collection = context.EventosMedicos;
        }

        public async Task AgregarEventoAsync(EventoMedico evento)
        {
            if (evento.Id == Guid.Empty) evento.Id = Guid.NewGuid();
            await _collection.InsertOneAsync(evento);
        }

        public async Task<EventoMedico?> ObtenerEventoPorIdAsync(Guid id)
        {
            return await _collection.Find(e => e.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<EventoMedico>> ObtenerHistorialPorMascotaAsync(Guid mascotaId)
        {
            return await _collection.Find(e => e.MascotaId == mascotaId)
                                    .SortByDescending(e => e.Fecha)
                                    .ToListAsync();
        }

        public async Task ActualizarEventoAsync(Guid id, EventoMedico evento)
        {
            await _collection.ReplaceOneAsync(e => e.Id == id, evento);
        }

        public async Task EliminarEventoAsync(Guid id)
        {
            await _collection.DeleteOneAsync(e => e.Id == id);
        }
    }
}
