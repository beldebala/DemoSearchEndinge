using Microsoft.EntityFrameworkCore.Migrations;

namespace DemoSearchEngine.Migrations
{
    public partial class Locationrelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LocationID",
                table: "Theaters",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Theaters_LocationID",
                table: "Theaters",
                column: "LocationID");

            migrationBuilder.AddForeignKey(
                name: "FK_Theaters_Locations_LocationID",
                table: "Theaters",
                column: "LocationID",
                principalTable: "Locations",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Theaters_Locations_LocationID",
                table: "Theaters");

            migrationBuilder.DropIndex(
                name: "IX_Theaters_LocationID",
                table: "Theaters");

            migrationBuilder.DropColumn(
                name: "LocationID",
                table: "Theaters");
        }
    }
}
