using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson.Serialization.Attributes;

namespace VeterinariaGenesis.Domain.Entities
{
    public class Pedido
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public Guid Id { get; set; } = Guid.NewGuid();
        
        public string NumeroPedido { get; set; } = $"WEB-{DateTime.Now.ToString("yyyyMMdd")}-{new Random().Next(1000, 9999)}";
        public DateTime FechaPedido { get; set; } = DateTime.Now;
        
        // Datos del Cliente (Guest Checkout)
        public string NombreCliente { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        
        // Datos de Envío y Pago
        public string DireccionEnvio { get; set; } = string.Empty;
        public string MetodoPago { get; set; } = "Efectivo"; // Efectivo, Transferencia, Tarjeta
        public string MetodoEntrega { get; set; } = "Retiro en Tienda"; // Retiro en Tienda, Delivery
        
        // Estado del pedido: Pendiente, Procesando, Enviado, Entregado, Cancelado
        public string Estado { get; set; } = "Pendiente";
        
        public List<DetallePedido> Detalles { get; set; } = new List<DetallePedido>();

        [BsonIgnore]
        public decimal Subtotal => Detalles != null ? Detalles.Sum(d => d.Subtotal) : 0;
        
        [BsonIgnore]
        public decimal Impuestos => Subtotal * 0.15m; // 15% IVA
        
        [BsonIgnore]
        public decimal Total => Subtotal + Impuestos;
    }

    public class DetallePedido
    {
        [BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public Guid ProductoId { get; set; }
        public string NombreProducto { get; set; } = string.Empty;
        public decimal Cantidad { get; set; } = 1;
        public decimal PrecioUnitario { get; set; }
        
        [BsonIgnore]
        public decimal Subtotal => Cantidad * PrecioUnitario;
    }
}
