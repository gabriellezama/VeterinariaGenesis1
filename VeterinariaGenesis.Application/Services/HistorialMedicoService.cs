using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VeterinariaGenesis.Application.DTOs;
using VeterinariaGenesis.Application.Interfaces;
using VeterinariaGenesis.Domain.Entities;

namespace VeterinariaGenesis.Application.Services
{
    public interface IHistorialMedicoService
    {
        Task<List<LineaTiempoItemDto>> ObtenerLineaTiempoMascotaAsync(Guid mascotaId);
    }

    public class HistorialMedicoService : IHistorialMedicoService
    {
        private readonly IHistorialMedicoRepository _repository;

        public HistorialMedicoService(IHistorialMedicoRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<LineaTiempoItemDto>> ObtenerLineaTiempoMascotaAsync(Guid mascotaId)
        {
            var eventos = await _repository.ObtenerHistorialPorMascotaAsync(mascotaId);

            var lineaTiempo = eventos.Select(MapearAItemLineaTiempo).OrderByDescending(e => e.Fecha).ToList();

            return lineaTiempo;
        }

        private LineaTiempoItemDto MapearAItemLineaTiempo(EventoMedico evento)
        {
            var item = new LineaTiempoItemDto
            {
                Id = evento.Id,
                Fecha = evento.Fecha,
                Descripcion = evento.Descripcion,
                MedicoResponsable = evento.MedicoResponsable
            };

            switch (evento)
            {
                case Vacuna vacuna:
                    item.TipoEvento = "Vacunación";
                    item.Titulo = $"Vacuna: {vacuna.ProductoAplicado}";
                    item.DetallesExtras = $"Lote: {vacuna.Lote} | Próxima dosis: {(vacuna.ProximaDosis.HasValue ? vacuna.ProximaDosis.Value.ToShortDateString() : "No especificada")}";
                    item.Icono = "fa-syringe";
                    item.ColorClase = "bg-blue-500";
                    break;
                case Cirugia cirugia:
                    item.TipoEvento = "Cirugía";
                    item.Titulo = "Intervención Quirúrgica";
                    item.DetallesExtras = $"Anestesia: {cirugia.TipoAnestesia} | Reporte: {cirugia.ReportePostOperatorio}";
                    item.Icono = "fa-scalpel";
                    item.ColorClase = "bg-red-500";
                    break;
                case ExamenLaboratorio examen:
                    item.TipoEvento = "Laboratorio";
                    item.Titulo = $"Examen: {examen.TipoExamen}";
                    item.DetallesExtras = $"Resultados: {examen.Resultados}";
                    item.Icono = "fa-flask";
                    item.ColorClase = "bg-green-500";
                    break;
                default:
                    item.TipoEvento = "Consulta General";
                    item.Titulo = "Consulta Médica";
                    item.DetallesExtras = "";
                    item.Icono = "fa-stethoscope";
                    item.ColorClase = "bg-gray-500";
                    break;
            }

            return item;
        }
    }
}
