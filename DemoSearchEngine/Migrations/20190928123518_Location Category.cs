﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace DemoSearchEngine.Migrations
{
    public partial class LocationCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Category",
                table: "Locations",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Locations");
        }
    }
}
