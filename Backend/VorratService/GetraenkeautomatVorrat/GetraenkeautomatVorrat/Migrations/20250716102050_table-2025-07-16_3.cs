using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GetraenkeautomatVorrat.Migrations
{
    /// <inheritdoc />
    public partial class table20250716_3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Vorrat");

            migrationBuilder.CreateTable(
                name: "Vorraete",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Preis = table.Column<double>(type: "REAL", nullable: false),
                    Groesse = table.Column<double>(type: "REAL", nullable: false),
                    Anzahl = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vorraete", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Vorraete");

            migrationBuilder.CreateTable(
                name: "Vorrat",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Anzahl = table.Column<int>(type: "INTEGER", nullable: false),
                    Groesse = table.Column<double>(type: "REAL", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Preis = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vorrat", x => x.Id);
                });
        }
    }
}
