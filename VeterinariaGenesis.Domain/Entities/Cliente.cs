using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace VeterinariaGenesis.Domain.Entities
{
    [BsonIgnoreExtraElements]
    public class Cliente
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Nombres { get; set; } = string.Empty;

        [BsonElement("nombres")]
        private string? NombresLower { set { if (string.IsNullOrEmpty(Nombres)) Nombres = value ?? ""; } }

        [BsonElement("Nombre")]
        private string? NombreLegacy { set { if (string.IsNullOrEmpty(Nombres)) Nombres = value ?? ""; } }

        [BsonElement("nombre")]
        private string? NombreLower { set { if (string.IsNullOrEmpty(Nombres)) Nombres = value ?? ""; } }

        public string Apellidos { get; set; } = string.Empty;

        [BsonElement("apellidos")]
        private string? ApellidosLower { set { if (string.IsNullOrEmpty(Apellidos)) Apellidos = value ?? ""; } }

        public string Identificacion { get; set; } = string.Empty;

        [BsonElement("identificacion")]
        private string? IdentificacionLower { set { if (string.IsNullOrEmpty(Identificacion)) Identificacion = value ?? ""; } }

        [BsonElement("Cedula")]
        private string? CedulaLegacy { set { if (string.IsNullOrEmpty(Identificacion)) Identificacion = value ?? ""; } }

        [BsonElement("cedula")]
        private string? CedulaLower { set { if (string.IsNullOrEmpty(Identificacion)) Identificacion = value ?? ""; } }
        public string Telefono { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public DateTime FechaRegistro { get; set; } = DateTime.Now;
        
        [BsonIgnore]
        public ICollection<Mascota> Mascotas { get; set; } = new List<Mascota>();
    }
}
