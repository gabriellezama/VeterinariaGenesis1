using Microsoft.AspNetCore.Mvc;
using VeterinariaGenesis.Application.Interfaces;
using VeterinariaGenesis.Domain.Entities;
using VeterinariaGenesis.Application.DTOs;
using VeterinariaGenesis.Domain.Enums;

namespace VeterinariaGenesis.Api.Controllers
{
    [ApiController]
    [Route("api/GestionMedica")]
    public class GestionMedicaController : ControllerBase
    {
        private readonly IHistorialMedicoRepository _repo;

        public GestionMedicaController(IHistorialMedicoRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("historial/{mascotaId}")]
        public async Task<IActionResult> GetHistorial(Guid mascotaId)
        {
            var eventos = await _repo.ObtenerHistorialPorMascotaAsync(mascotaId);
            
            // Mapeo simple a DTO para la línea de tiempo
            var dtos = eventos.Select(e => new LineaTiempoItemDto
            {
                Id = e.Id,
                Fecha = e.Fecha,
                TipoEvento = e.Tipo.ToString(),
                Titulo = ObtenerTitulo(e),
                Descripcion = e.Descripcion,
                MedicoResponsable = e.MedicoResponsable,
                Icono = ObtenerIcono(e.Tipo),
                ColorClase = ObtenerColor(e.Tipo),
                DetallesExtras = ObtenerDetalles(e),
                Costo = e.Costo
            }).ToList();

            return Ok(dtos);
        }

        [HttpPost("evento")]
        public async Task<IActionResult> AgregarEvento([FromBody] EventoMedicoRequest request)
        {
            try 
            {
                EventoMedico evento;

                switch (request.Tipo)
                {
                    case TipoEventoMedico.Consulta:
                        evento = new Consulta();
                        break;
                    case TipoEventoMedico.Vacuna:
                        DateTime? nextDose = null;
                        if (!string.IsNullOrEmpty(request.InfoExtra2) && DateTime.TryParse(request.InfoExtra2, out var parsedDate)) 
                            nextDose = parsedDate;
                        evento = new Vacuna { ProductoAplicado = request.InfoExtra1, ProximaDosis = nextDose };
                        break;
                    case TipoEventoMedico.Cirugia:
                        evento = new Cirugia { TipoAnestesia = request.InfoExtra1, ReportePostOperatorio = request.InfoExtra2 };
                        break;
                    case TipoEventoMedico.Grooming:
                        evento = new Grooming();
                        break;
                    default:
                        evento = new Consulta(); // Fallback seguro
                        break;
                }

                evento.MascotaId = request.MascotaId;
                evento.Fecha = request.Fecha;
                evento.Descripcion = request.Descripcion;
                evento.MedicoResponsable = request.MedicoResponsable;
                evento.Tipo = request.Tipo;
                evento.Costo = request.Costo;
                
                // Usar el título personalizado si se proporciona
                if (!string.IsNullOrEmpty(request.Titulo))
                {
                    evento.Descripcion = $"[{request.Titulo}] {evento.Descripcion}";
                }

                await _repo.AgregarEventoAsync(evento);
                return Ok(evento);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al guardar evento: {ex.Message}");
            }
        }

        private string ObtenerTitulo(EventoMedico e)
        {
            // Intentar extraer el título del formato [Titulo]
            if (e.Descripcion.StartsWith("[") && e.Descripcion.Contains("]"))
            {
                int end = e.Descripcion.IndexOf("]");
                return e.Descripcion.Substring(1, end - 1);
            }

            if (e is Vacuna v) return $"Vacuna: {v.ProductoAplicado}";
            if (e is Cirugia c) return $"Cirugía: {e.Descripcion}";
            if (e is ExamenLaboratorio ex) return $"Laboratorio: {ex.TipoExamen}";
            return "Atención Médica";
        }

        private string ObtenerIcono(TipoEventoMedico tipo) => tipo switch
        {
            TipoEventoMedico.Vacuna => "fa-syringe",
            TipoEventoMedico.Cirugia => "fa-cut",
            TipoEventoMedico.ExamenLaboratorio => "fa-microscope",
            TipoEventoMedico.Grooming => "fa-shower",
            _ => "fa-stethoscope"
        };

        private string ObtenerColor(TipoEventoMedico tipo) => tipo switch
        {
            TipoEventoMedico.Vacuna => "bg-emerald-500",
            TipoEventoMedico.Cirugia => "bg-rose-500",
            TipoEventoMedico.ExamenLaboratorio => "bg-amber-500",
            TipoEventoMedico.Grooming => "bg-sky-500",
            _ => "bg-indigo-500"
        };

        private string ObtenerDetalles(EventoMedico e)
        {
            if (e is Vacuna v) return $"Lote: {v.Lote} | Próxima: {v.ProximaDosis?.ToShortDateString()}";
            if (e is Cirugia c) return $"Anestesia: {c.TipoAnestesia}";
            return "";
        }
    }

    public class EventoMedicoRequest
    {
        public Guid MascotaId { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
        public TipoEventoMedico Tipo { get; set; }
        public string Titulo { get; set; } = string.Empty; // Nuevo campo
        public string Descripcion { get; set; } = string.Empty;
        public string MedicoResponsable { get; set; } = string.Empty;
        public decimal Costo { get; set; }
        public string InfoExtra1 { get; set; } = string.Empty;
        public string InfoExtra2 { get; set; } = string.Empty;
    }
}
