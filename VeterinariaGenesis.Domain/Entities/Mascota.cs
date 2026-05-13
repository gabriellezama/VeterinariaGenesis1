using System;
using MongoDB.Bson.Serialization.Attributes;

namespace VeterinariaGenesis.Domain.Entities
{
    public class Mascota
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Nombre { get; set; } = string.Empty;
        public string Especie { get; set; } = string.Empty;
        public string Raza { get; set; } = string.Empty;
        public int Edad { get; set; }
        public string Sexo { get; set; } = string.Empty;
        public decimal Peso { get; set; }
        public string Color { get; set; } = string.Empty;
        [BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public Guid ClienteId { get; set; }
    }
}
