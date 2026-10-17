using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace stoiko3.Models
{
    [Table("musculo")]
    public class Musculo : BaseModel
    {
        [PrimaryKey("id_musculo")]
        public int IdMusculo { get; set; }

        [Column("nom_musculo")]
        public string? NomMusculo { get; set; }
    }
}