using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace stoiko3.Models
{
    [Table("ejercicio")]
    public class Ejercicio : BaseModel
    {
        [PrimaryKey("id_ejercicio")]
        public int IdEjercicio { get; set; }

        [Column("nom_ejercicio")]
        public string? NomEjercicio { get; set; }

        [Column("descripcion")]
        public string? Descripcion { get; set; }

        [Column("tipo_ejercicio")]
        public string? TipoEjercicio { get; set; }

        [Column("puntos_ejercicio")]
        public double PuntosEjercicio { get; set; }
    }
}