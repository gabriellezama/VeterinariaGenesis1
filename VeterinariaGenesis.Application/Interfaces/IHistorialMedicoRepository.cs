using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VeterinariaGenesis.Domain.Entities;

namespace VeterinariaGenesis.Application.Interfaces
{
    public interface IHistorialMedicoRepository
    {
        Task<List<EventoMedico>> ObtenerHistorialPorMascotaAsync(Guid mascotaId);
        Task<EventoMedico?> ObtenerEventoPorIdAsync(Guid id);
        Task AgregarEventoAsync(EventoMedico evento);
        Task ActualizarEventoAsync(Guid id, EventoMedico evento);
        Task EliminarEventoAsync(Guid id);
    }
}
