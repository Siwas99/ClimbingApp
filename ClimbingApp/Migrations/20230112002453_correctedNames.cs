using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClimbingApp.Migrations
{
    /// <inheritdoc />
    public partial class correctedNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "positionLogitude",
                table: "Rocks",
                newName: "Longitude");

            migrationBuilder.RenameColumn(
                name: "positionLatitude",
                table: "Rocks",
                newName: "Latitude");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Longitude",
                table: "Rocks",
                newName: "positionLogitude");

            migrationBuilder.RenameColumn(
                name: "Latitude",
                table: "Rocks",
                newName: "positionLatitude");
        }
    }
}
