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
        public string Apellidos { get; set; } = string.Empty;
        public string Identificacion { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public DateTime FechaRegistro { get; set; } = DateTime.Now;
        
        [BsonIgnore]
        public ICollection<Mascota> Mascotas { get; set; } = new List<Mascota>();

        // Este diccionario captura cualquier campo con nombre diferente (nombres, cedula, etc.)
        // permitiendo que la aplicación lea datos antiguos sin fallar al guardar nuevos.
        [BsonExtraElements]
        public IDictionary<string, object> ExtraElements { get; set; } = new Dictionary<string, object>();

        // Propiedades de conveniencia para mapear datos antiguos al cargar
        [BsonIgnore]
        public string DisplayNombres => string.IsNullOrEmpty(Nombres) ? GetFromExtra("nombres", "Nombre", "nombre") : Nombres;

        [BsonIgnore]
        public string DisplayIdentificacion => string.IsNullOrEmpty(Identificacion) ? GetFromExtra("identificacion", "Cedula", "cedula") : Identificacion;

        private string GetFromExtra(params string[] keys)
        {
            foreach (var key in keys)
            {
                if (ExtraElements.TryGetValue(key, out var value))
                    return value?.ToString() ?? "";
            }
            return "";
        }
    }
}
