using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;
namespace stoiko3.Models
{
    [Table("ejercicio_musculo")]
    public class EjercicioMusculo : BaseModel
    {
        [PrimaryKey("id_em")]
        public int IdEm { get; set; }

        [Column("id_ejercicio")]
        public int IdEjercicio { get; set; }

        [Column("id_musculo")]
        public int IdMusculo { get; set; }

        [Column("tipo_participacion")]
        public string TipPartici { get; set; } = string.Empty;
    }
}
