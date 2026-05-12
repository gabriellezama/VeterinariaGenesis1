using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VeterinariaGenesis.Application.Interfaces;
using VeterinariaGenesis.Domain.Entities;

namespace VeterinariaGenesis.Infrastructure.Repositories
{
    public class HistorialMedicoRepository : IHistorialMedicoRepository
    {
        // Simulando una base de datos en memoria (MongoDB / SQL Server)
        private readonly List<EventoMedico> _eventosSimulados = new();

        public HistorialMedicoRepository()
        {
            // Seed data para pruebas
            var mascotaId = Guid.Parse("00000000-0000-0000-0000-000000000001");
            
            _eventosSimulados.Add(new Vacuna 
            { 
                MascotaId = mascotaId,
                Fecha = DateTime.UtcNow.AddMonths(-6),
                MedicoResponsable = "Dr. López",
                Descripcion = "Vacunación anual",
                ProductoAplicado = "Rabia + Quíntuple",
                Lote = "L-48593",
                ProximaDosis = DateTime.UtcNow.AddMonths(6)
            });

            _eventosSimulados.Add(new Cirugia 
            { 
                MascotaId = mascotaId,
                Fecha = DateTime.UtcNow.AddMonths(-2),
                MedicoResponsable = "Dra. Martínez",
                Descripcion = "Esterilización",
                TipoAnestesia = "Inhalada (Isofluorano)",
                ReportePostOperatorio = "Sin complicaciones, recuperación favorable.",
                ConsentimientoInformadoDigital = true
            });

            _eventosSimulados.Add(new ExamenLaboratorio 
            { 
                MascotaId = mascotaId,
                Fecha = DateTime.UtcNow.AddDays(-10),
                MedicoResponsable = "Dr. López",
                Descripcion = "Chequeo de rutina",
                TipoExamen = "Hemograma Completo",
                Resultados = "Glóbulos blancos levemente elevados. Sugerir observación."
            });
        }

        public async Task AgregarEventoAsync(EventoMedico evento)
        {
            _eventosSimulados.Add(evento);
            await Task.CompletedTask;
        }

        public async Task<List<EventoMedico>> ObtenerHistorialPorMascotaAsync(Guid mascotaId)
        {
            // Filtrar eventos por la mascota. Si enviamos un Empty Guid, devolvemos todos para la demo.
            var eventos = mascotaId == Guid.Empty 
                ? _eventosSimulados 
                : _eventosSimulados.Where(e => e.MascotaId == mascotaId).ToList();

            return await Task.FromResult(eventos);
        }
    }
}
