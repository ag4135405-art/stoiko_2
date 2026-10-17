using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;
namespace stoiko3.Models
{
    [Table("registro_ejercicio")]
    public class RegistrarEjercicio : BaseModel
    {
        [PrimaryKey("id_registro")]
        public int IdRegistro { get; set; }

        [Column("id_perfil")]
        public int IdPerfil { get; set; }

        [Column("id_ejercicio")]
        public int IdEjercicio { get; set; }

        [Column("repeticiones")]
        public int Repeticiones { get;set; }

        [Column("fecha")]
        public DateTime Fecha { get; set; }

        [Column("puntos_obtenidos")]
        public double PuntosObtenidos { get; set; }
    }
}
