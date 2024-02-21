using Microsoft.EntityFrameworkCore.Infrastructure;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OverwatchAPI.Models.EntityFramework
{
    [Table("personnage")]
    public class Personnage
    {
        public Personnage()
        {
            JouablePerso = new HashSet<Jouabilite>();
        }

        public Personnage(int personnageId, string? nom, string? prenom, string? pays, int note)
        {
            this.PersonnageId = personnageId;
            this.Nom = nom;
            this.Prenom = prenom;
            this.Pays = pays;
            this.Note = note;
        }


        public Personnage(string? nom, string? prenom, string? pays, int note)
        {
            this.Nom = nom;
            this.Prenom = prenom;
            this.Pays = pays;
            this.Note = note;
        }

        [Key]
        [Column("perso_id")]
        public int PersonnageId { get; set; }
        [Column("perso_nom")]
        [StringLength(50)]
        public string? Nom { get; set; }
        [Column("perso_prenom")]
        [StringLength(50)]
        public string? Prenom { get; set; }
        [Column("perso_pays")]
        [StringLength(50)]
        public string? Pays { get; set; }
        [Column("perso_datecreation", TypeName = "date")]
        public DateTime DateCreation { get; set; }
        [Column("perso_notediff")]
        [StringLength(5)]
        public int Note { get; set; }

        [InverseProperty(nameof(Jouabilite.PersonnageJouable))]
        public virtual ICollection<Jouabilite> JouablePerso { get; set; }
    }
}
