using System;
using MongoDB.Bson.Serialization.Attributes;

namespace VeterinariaGenesis.Domain.Entities
{
    [BsonIgnoreExtraElements]
    public class NotificacionWhatsappLog
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public Guid Id { get; set; } = Guid.NewGuid();

        [BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public Guid CitaId { get; set; }

        public string ClienteNombre { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Mensaje { get; set; } = string.Empty;
        public DateTime FechaEnvio { get; set; } = DateTime.Now;
        public string EstadoEnvio { get; set; } = "Enviado"; // Enviado, Fallido
    }
}
