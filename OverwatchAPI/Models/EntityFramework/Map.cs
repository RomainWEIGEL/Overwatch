
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OverwatchAPI.Models.EntityFramework
{
    [Table("map")]
    public class Map
    {

        public Map()
        {
            JouabiliteMap = new HashSet<Jouabilite>();
        }


        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("map_id")]
        public int MapId { get; set; }

        [Column("map_ville")]
        [StringLength(50)]
        public string? Ville { get; set; }
        [Column("map_pays")]
        [StringLength(50)]
        public string? Pays { get; set; }


        [InverseProperty(nameof(Jouabilite.MapJouable))]
        public virtual ICollection<Jouabilite> JouabiliteMap { get; set; }
    }
}
