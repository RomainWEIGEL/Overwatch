using Microsoft.EntityFrameworkCore;

namespace OverwatchAPI.Models.EntityFramework
{
    public partial class MapDBContext : DbContext
    {
        public MapDBContext() { }
        public MapDBContext(DbContextOptions<MapDBContext> options)
            : base(options) { }

        public DbSet<Jouabilite> Jouabilite { get; set; }
        public DbSet<Map> Maps { get; set; }
        public DbSet<Personnage> Personnages { get; set; }

               protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
               {
                   if (!optionsBuilder.IsConfigured)
                    {
                       optionsBuilder.UseNpgsql("Server=localhost;port=5432;Database=CreationBDMapRatings; uid=postgres; password=386974;");
                   }
                }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Map>(entity =>
            {
                entity.HasKey(e => e.MapId)
                    .HasName("pk_flm");
            });

            modelBuilder.Entity<Personnage>(entity =>
            {
                entity.HasKey(e => e.PersonnageId)
                    .HasName("pk_utl");


                entity.Property(e => e.Pays).HasDefaultValue("On sait pas");

                entity.Property(e => e.DateCreation).HasDefaultValueSql("now()");

            });

            modelBuilder.Entity<Jouabilite>(entity =>
            {
                entity.HasKey(e => new { e.PersonnageId, e.MapId })
                    .HasName("pk_not");

                entity.HasCheckConstraint("ck_not_note", "not_note between 0 and 5");


                entity.HasOne(d => d.MapJouable)
                    .WithMany(p => p.JouabiliteMap)
                    .HasForeignKey(d => d.MapId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_jouable_map");

                entity.HasOne(d => d.PersonnageJouable)
                    .WithMany(p => p.JouablePerso)
                    .HasForeignKey(d => d.PersonnageId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_jouable_perso");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}


