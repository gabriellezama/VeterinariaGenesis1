using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace VeterinariaGenesis.Domain.Entities
{
    public class Cliente
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public Guid Id { get; set; } = Guid.NewGuid();

        // Propiedad principal que usa el sistema
        public string Nombres { get; set; } = string.Empty;

        // Propiedad de "respaldo" para leer datos antiguos de la DB
        [BsonElement("Nombre")]
        private string? NombreViejo { set { if (string.IsNullOrEmpty(Nombres)) Nombres = value ?? ""; } }

        public string Apellidos { get; set; } = string.Empty;

        // Propiedad principal
        public string Identificacion { get; set; } = string.Empty;

        // Propiedad de "respaldo" para leer datos antiguos
        [BsonElement("Cedula")]
        private string? CedulaVieja { set { if (string.IsNullOrEmpty(Identificacion)) Identificacion = value ?? ""; } }

        public string Telefono { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public DateTime FechaRegistro { get; set; } = DateTime.Now;
        
        public ICollection<Mascota> Mascotas { get; set; } = new List<Mascota>();
    }
}
