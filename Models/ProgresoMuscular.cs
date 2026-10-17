using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace stoiko3.Models
{
    [Table("progreso_muscular")]
    public class ProgresoMuscular : BaseModel
    {
        [PrimaryKey("id_pm")]
        public int IdPm { get; set; }

        [Column("score_pm")]
        public double ScorePm { get; set; }

        [Column("id_muscular")]
        public int IdMuscular { get; set; }

        [Column("id_division")]
        public int IdDivision { get; set; }

        [Column("id_perfil")]
        public int IdPerfil { get; set; }
    }
}