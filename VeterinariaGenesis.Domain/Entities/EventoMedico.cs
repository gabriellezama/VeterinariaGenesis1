using System;
using VeterinariaGenesis.Domain.Enums;
using MongoDB.Bson.Serialization.Attributes;

namespace VeterinariaGenesis.Domain.Entities
{
    [BsonDiscriminator(RootClass = true)]
    [BsonKnownTypes(typeof(Vacuna), typeof(Cirugia), typeof(ExamenLaboratorio), typeof(Consulta), typeof(Grooming))]
    public abstract class EventoMedico
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public Guid Id { get; set; } = Guid.NewGuid();

        [BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public Guid MascotaId { get; set; }
        public DateTime Fecha { get; set; } = DateTime.UtcNow;
        public TipoEventoMedico Tipo { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public string MedicoResponsable { get; set; } = string.Empty;
        public decimal Costo { get; set; }
    }
}
