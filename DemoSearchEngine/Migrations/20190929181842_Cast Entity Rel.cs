using Microsoft.EntityFrameworkCore.Migrations;

namespace DemoSearchEngine.Migrations
{
    public partial class CastEntityRel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CastCrewID",
                table: "Movies",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Street",
                table: "Locations",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ZipCode",
                table: "Locations",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "CastCrews",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Rating = table.Column<float>(nullable: false),
                    MovieID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CastCrews", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CastCrews_Movies_MovieID",
                        column: x => x.MovieID,
                        principalTable: "Movies",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MovieCasts",
                columns: table => new
                {
                    MovieId = table.Column<int>(nullable: false),
                    CastCrewId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieCasts", x => new { x.CastCrewId, x.MovieId });
                    table.ForeignKey(
                        name: "FK_MovieCasts_CastCrews_CastCrewId",
                        column: x => x.CastCrewId,
                        principalTable: "CastCrews",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovieCasts_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Movies_CastCrewID",
                table: "Movies",
                column: "CastCrewID");

            migrationBuilder.CreateIndex(
                name: "IX_CastCrews_MovieID",
                table: "CastCrews",
                column: "MovieID");

            migrationBuilder.CreateIndex(
                name: "IX_MovieCasts_MovieId",
                table: "MovieCasts",
                column: "MovieId");

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_CastCrews_CastCrewID",
                table: "Movies",
                column: "CastCrewID",
                principalTable: "CastCrews",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movies_CastCrews_CastCrewID",
                table: "Movies");

            migrationBuilder.DropTable(
                name: "MovieCasts");

            migrationBuilder.DropTable(
                name: "CastCrews");

            migrationBuilder.DropIndex(
                name: "IX_Movies_CastCrewID",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "CastCrewID",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "Street",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "ZipCode",
                table: "Locations");
        }
    }
}
