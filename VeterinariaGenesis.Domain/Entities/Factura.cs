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
        [BsonElement("Fecha")] // Compatibilidad
        public DateTime FechaEmision { get; set; } = DateTime.Now;
        [BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public Guid ClienteId { get; set; }
        [BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public Guid TrabajadorId { get; set; }
        
        public List<DetalleFactura> Detalles { get; set; } = new List<DetalleFactura>();

        public decimal Subtotal => Detalles.Sum(d => d.Subtotal);
        public decimal Impuestos => Subtotal * 0.15m; // 15% IVA
        public decimal Total => Subtotal + Impuestos;
    }

    public class DetalleFactura
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public Guid Id { get; set; } = Guid.NewGuid();
        [BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public Guid FacturaId { get; set; }
        [BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public Guid ProductoId { get; set; }
        public string DescripcionItem { get; set; } = string.Empty;
        public int Cantidad { get; set; } = 1;
        public decimal PrecioUnitario { get; set; }
        
        public decimal Subtotal => Cantidad * PrecioUnitario;
    }
}
