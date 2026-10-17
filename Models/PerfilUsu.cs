using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;
namespace stoiko3.Models
{
     [Table("perfil_usu")]
    public class PerfilUsu : BaseModel
    {
        [PrimaryKey("id_perfil")]
        public int IdPerfil { get; set; }

         [Column("nom_perfil")]
        public string NomPerfil { get; set; } = string.Empty;

         [Column("score_total")]
        public double ScoreTotal { get; set; }

         [Column("id_division")]
        public int IdDivision { get; set; }

        [Column("numero_rachas")]
        public int NumeroRachas { get; set;}
    }
}