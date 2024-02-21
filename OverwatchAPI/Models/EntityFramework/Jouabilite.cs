using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OverwatchAPI.Models.EntityFramework
{
    [Table("jouabilite")]
    public class Jouabilite
    {

        [Key]
        [Column("perso_id")]
        public int PersonnageId { get; set; }

        [Key]
        [Column("flm_id")]
        public int MapId { get; set; }

        [Column("not_note")]
        public int Note { get; set; }

        [ForeignKey(nameof(MapId))]
        [InverseProperty(nameof(Map.JouabiliteMap))]
        public virtual Map MapJouable { get; set; } = null!;

        [ForeignKey(nameof(PersonnageId))]
        [InverseProperty(nameof(Personnage.JouablePerso))]
        public virtual Personnage PersonnageJouable { get; set; } = null!;
    }
}
