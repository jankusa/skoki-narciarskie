using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MvcSkoki.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sezon",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Rok = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sezon", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Skoczek",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Imie = table.Column<string>(type: "TEXT", nullable: false),
                    Nazwisko = table.Column<string>(type: "TEXT", nullable: false),
                    Rokurodzenia = table.Column<int>(name: "Rok_urodzenia", type: "INTEGER", nullable: false),
                    Wzrost = table.Column<int>(type: "INTEGER", nullable: false),
                    Narodowosc = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skoczek", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Uzytkownicy",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Login = table.Column<string>(type: "TEXT", nullable: false),
                    HashPassword = table.Column<string>(type: "TEXT", nullable: false),
                    Token = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Uzytkownicy", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Skocznia",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nazwa = table.Column<string>(type: "TEXT", nullable: false),
                    Miejscowosc = table.Column<string>(type: "TEXT", nullable: false),
                    Panstwo = table.Column<string>(type: "TEXT", nullable: false),
                    K = table.Column<int>(type: "INTEGER", nullable: false),
                    HS = table.Column<int>(type: "INTEGER", nullable: false),
                    Rekord = table.Column<decimal>(type: "TEXT", nullable: false),
                    SkoczekID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skocznia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Skocznia_Skoczek_SkoczekID",
                        column: x => x.SkoczekID,
                        principalTable: "Skoczek",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Konkurs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SezonID = table.Column<int>(type: "INTEGER", nullable: false),
                    SkoczniaID = table.Column<int>(type: "INTEGER", nullable: false),
                    Data = table.Column<string>(type: "TEXT", nullable: false),
                    Opis = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Konkurs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Konkurs_Sezon_SezonID",
                        column: x => x.SezonID,
                        principalTable: "Sezon",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Konkurs_Skocznia_SkoczniaID",
                        column: x => x.SkoczniaID,
                        principalTable: "Skocznia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Punktacja",
                columns: table => new
                {
                    PunktacjaID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SkoczekID = table.Column<int>(type: "INTEGER", nullable: false),
                    KonkursID = table.Column<int>(type: "INTEGER", nullable: false),
                    Wynik = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Punktacja", x => x.PunktacjaID);
                    table.ForeignKey(
                        name: "FK_Punktacja_Konkurs_KonkursID",
                        column: x => x.KonkursID,
                        principalTable: "Konkurs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Punktacja_Skoczek_SkoczekID",
                        column: x => x.SkoczekID,
                        principalTable: "Skoczek",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Konkurs_SezonID",
                table: "Konkurs",
                column: "SezonID");

            migrationBuilder.CreateIndex(
                name: "IX_Konkurs_SkoczniaID",
                table: "Konkurs",
                column: "SkoczniaID");

            migrationBuilder.CreateIndex(
                name: "IX_Punktacja_KonkursID",
                table: "Punktacja",
                column: "KonkursID");

            migrationBuilder.CreateIndex(
                name: "IX_Punktacja_SkoczekID",
                table: "Punktacja",
                column: "SkoczekID");

            migrationBuilder.CreateIndex(
                name: "IX_Skocznia_SkoczekID",
                table: "Skocznia",
                column: "SkoczekID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Punktacja");

            migrationBuilder.DropTable(
                name: "Uzytkownicy");

            migrationBuilder.DropTable(
                name: "Konkurs");

            migrationBuilder.DropTable(
                name: "Sezon");

            migrationBuilder.DropTable(
                name: "Skocznia");

            migrationBuilder.DropTable(
                name: "Skoczek");
        }
    }
}
