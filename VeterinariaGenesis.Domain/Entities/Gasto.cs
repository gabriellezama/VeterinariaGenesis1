using System;
using MongoDB.Bson.Serialization.Attributes;

namespace VeterinariaGenesis.Domain.Entities
{
    public class Gasto
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public Guid Id { get; set; } = Guid.NewGuid();
        
        public string Descripcion { get; set; } = string.Empty;
        
        [BsonRepresentation(MongoDB.Bson.BsonType.Decimal128)]
        public decimal Monto { get; set; }
        
        public DateTime Fecha { get; set; } = DateTime.Now;
        
        public string Categoria { get; set; } = string.Empty;
    }
}
