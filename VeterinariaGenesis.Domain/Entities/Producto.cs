using System;
using MongoDB.Bson.Serialization.Attributes;

namespace VeterinariaGenesis.Domain.Entities
{
    public class Producto
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public decimal PrecioCompra { get; set; }
        public decimal PrecioVenta { get; set; }
        [BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public Guid? ProveedorId { get; set; }
    }
}
