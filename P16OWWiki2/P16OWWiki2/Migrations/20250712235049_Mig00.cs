using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace P16OWWiki2.Migrations
{
    /// <inheritdoc />
    public partial class Mig00 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HeroeSet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    RNombre = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Edad = table.Column<int>(type: "int", nullable: false),
                    Ocupa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nacionalidad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Afiliacion1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Afiliacion2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rol = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Salud = table.Column<int>(type: "int", nullable: false),
                    Nomfoto = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
