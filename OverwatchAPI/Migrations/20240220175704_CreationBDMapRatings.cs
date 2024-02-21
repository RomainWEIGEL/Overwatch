using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace OverwatchAPI.Migrations
{
    public partial class CreationBDMapRatings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "map",
                columns: table => new
                {
                    map_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    map_ville = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    map_pays = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_flm", x => x.map_id);
                });

            migrationBuilder.CreateTable(
                name: "personnage",
                columns: table => new
                {
                    perso_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    perso_nom = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    perso_prenom = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    perso_pays = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true, defaultValue: "On sait pas"),
                    perso_datecreation = table.Column<DateTime>(type: "date", nullable: false, defaultValueSql: "now()"),
                    perso_notediff = table.Column<int>(type: "integer", maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_utl", x => x.perso_id);
                });

            migrationBuilder.CreateTable(
                name: "jouabilite",
                columns: table => new
                {
                    perso_id = table.Column<int>(type: "integer", nullable: false),
                    flm_id = table.Column<int>(type: "integer", nullable: false),
                    not_note = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_not", x => new { x.perso_id, x.flm_id });
                    table.CheckConstraint("ck_not_note", "not_note between 0 and 5");
                    table.ForeignKey(
                        name: "fk_jouable_map",
                        column: x => x.flm_id,
                        principalTable: "map",
                        principalColumn: "map_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_jouable_perso",
                        column: x => x.perso_id,
                        principalTable: "personnage",
                        principalColumn: "perso_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_jouabilite_flm_id",
                table: "jouabilite",
                column: "flm_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "jouabilite");

            migrationBuilder.DropTable(
                name: "map");

            migrationBuilder.DropTable(
                name: "personnage");
        }
    }
}
