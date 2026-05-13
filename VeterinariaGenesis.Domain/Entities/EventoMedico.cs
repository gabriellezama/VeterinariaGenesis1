using System;
using VeterinariaGenesis.Domain.Enums;
using MongoDB.Bson.Serialization.Attributes;

namespace VeterinariaGenesis.Domain.Entities
{
    [BsonDiscriminator(RootClass = true)]
    [BsonKnownTypes(typeof(Vacuna), typeof(Cirugia), typeof(ExamenLaboratorio))]
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
