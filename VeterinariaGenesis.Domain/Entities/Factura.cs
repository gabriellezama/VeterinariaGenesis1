using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson.Serialization.Attributes;

namespace VeterinariaGenesis.Domain.Entities
{
    public class Factura
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string NumeroFactura { get; set; } = $"FAC-{DateTime.Now.ToString("yyyyMMdd")}-{new Random().Next(1000, 9999)}";
        
        public DateTime FechaEmision { get; set; } = DateTime.Now;

        [BsonElement("Fecha")]
        private DateTime? FechaVieja { set { if (value.HasValue) FechaEmision = value.Value; } }
        [BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public Guid ClienteId { get; set; }
        [BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public Guid TrabajadorId { get; set; }
        
        public List<DetalleFactura> Detalles { get; set; } = new List<DetalleFactura>();

        [BsonIgnore]
        public decimal Subtotal => Detalles != null ? Detalles.Sum(d => d.Subtotal) : 0;
        
        [BsonIgnore]
        public decimal Impuestos => Subtotal * 0.15m; 
        
        [BsonIgnore]
        public decimal Total => Subtotal + Impuestos;
    }

    public class DetalleFactura
    {
        [BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public Guid Id { get; set; } = Guid.NewGuid();
        [BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public Guid FacturaId { get; set; }
        [BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public Guid ProductoId { get; set; }
        public string DescripcionItem { get; set; } = string.Empty;
        public decimal Cantidad { get; set; } = 1;
        public decimal PrecioUnitario { get; set; }
        
        [BsonIgnore]
        public decimal Subtotal => Cantidad * PrecioUnitario;
    }
}
