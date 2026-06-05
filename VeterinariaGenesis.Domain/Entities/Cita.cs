using System;
using MongoDB.Bson.Serialization.Attributes;

namespace VeterinariaGenesis.Domain.Entities
{
    [BsonIgnoreExtraElements]
    public class Cita
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public Guid Id { get; set; } = Guid.NewGuid();

        [BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public Guid ClienteId { get; set; }

        [BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public Guid MascotaId { get; set; }

        [BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public Guid TrabajadorId { get; set; }

        public DateTime FechaHora { get; set; } = DateTime.Now;
        public string Motivo { get; set; } = string.Empty;
        public string Notas { get; set; } = string.Empty;
        public string Estado { get; set; } = "Programada"; // Programada, Completada, Cancelada
        public bool NotificadoWhatsapp { get; set; } = false;
        public DateTime? FechaNotificacion { get; set; }
    }
}
