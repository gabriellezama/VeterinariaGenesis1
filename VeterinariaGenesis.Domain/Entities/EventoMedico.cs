using System;
using VeterinariaGenesis.Domain.Enums;

namespace VeterinariaGenesis.Domain.Entities
{
    public abstract class EventoMedico
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid MascotaId { get; set; }
        public DateTime Fecha { get; set; } = DateTime.UtcNow;
        public TipoEventoMedico Tipo { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public string MedicoResponsable { get; set; } = string.Empty;
    }
}
