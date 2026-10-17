using System;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace stoiko3.Models
{
    [Table("usuario")]
    public class Usuario : BaseModel
    {
        // 1. Clave Primaria (id_usu)
        [PrimaryKey("id_usu", false)]
        public int Id { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public int IdUsu
        {
            get => Id;
            set => Id = value;
        }

        // 2. Correo Electrónico (email_usu)
        [Column("email_usu")]
        public string Email { get; set; } = string.Empty;

        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public string EmailUsu
        {
            get => Email;
            set => Email = value;
        }

        // 3. Contraseña (pass_usu)
        [Column("pass_usu")]
        public string? PassUsu { get; set; } = string.Empty;

        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public string? PasswordUsu
        {
            get => PassUsu;
            set => PassUsu = value;
        }

        // 4. Fechas de control
        [Column("fecha_registro")]
        public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;

        [Column("fecha_iniciosecion")]
        public DateTime FechaInicioSecion { get; set; } = DateTime.UtcNow;
    }
}