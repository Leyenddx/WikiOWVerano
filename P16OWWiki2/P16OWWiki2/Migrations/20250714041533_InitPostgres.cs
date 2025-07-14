using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace P16OWWiki2.Migrations
{
    /// <inheritdoc />
    public partial class InitPostgres : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HeroeSet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    RNombre = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Edad = table.Column<int>(type: "integer", nullable: false),
                    Ocupa = table.Column<string>(type: "text", nullable: false),
                    Nacionalidad = table.Column<string>(type: "text", nullable: false),
                    Afiliacion1 = table.Column<string>(type: "text", nullable: false),
                    Afiliacion2 = table.Column<string>(type: "text", nullable: false),
                    Rol = table.Column<string>(type: "text", nullable: false),
                    Salud = table.Column<int>(type: "integer", nullable: false),
                    Nomfoto = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HeroeSet", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HeroeSet");
        }
    }
}
