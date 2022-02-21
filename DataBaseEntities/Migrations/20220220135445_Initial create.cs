using Microsoft.EntityFrameworkCore.Migrations;

namespace DataBaseEntities.Migrations
{
    public partial class Initialcreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    CountryName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.CountryName);
                });

            migrationBuilder.CreateTable(
                name: "Languages",
                columns: table => new
                {
                    LanguageName = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.LanguageName);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    CityName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CountryName = table.Column<string>(type: "nvarchar(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.CityName);
                    table.ForeignKey(
                        name: "FK_Cities_Countries_CountryName",
                        column: x => x.CountryName,
                        principalTable: "Countries",
                        principalColumn: "CountryName",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CityName = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    PhoneNumber = table.Column<int>(type: "int", nullable: false),
                    CountryName = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    LanguageName = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.Id);
                    table.ForeignKey(
                        name: "FK_People_Cities_CityName",
                        column: x => x.CityName,
                        principalTable: "Cities",
                        principalColumn: "CityName",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_People_Countries_CountryName",
                        column: x => x.CountryName,
                        principalTable: "Countries",
                        principalColumn: "CountryName",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_People_Languages_LanguageName",
                        column: x => x.LanguageName,
                        principalTable: "Languages",
                        principalColumn: "LanguageName",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LanguagePerson",
                columns: table => new
                {
                    LanguageName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PersonId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LanguagePerson", x => new { x.LanguageName, x.PersonId });
                    table.ForeignKey(
                        name: "FK_LanguagePerson_Languages_LanguageName",
                        column: x => x.LanguageName,
                        principalTable: "Languages",
                        principalColumn: "LanguageName",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LanguagePerson_People_PersonId",
                        column: x => x.PersonId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cities_CountryName",
                table: "Cities",
                column: "CountryName");

            migrationBuilder.CreateIndex(
                name: "IX_LanguagePerson_PersonId",
                table: "LanguagePerson",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_People_CityName",
                table: "People",
                column: "CityName");

            migrationBuilder.CreateIndex(
                name: "IX_People_CountryName",
                table: "People",
                column: "CountryName");

            migrationBuilder.CreateIndex(
                name: "IX_People_LanguageName",
                table: "People",
                column: "LanguageName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LanguagePerson");

            migrationBuilder.DropTable(
                name: "People");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "Languages");

            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}
