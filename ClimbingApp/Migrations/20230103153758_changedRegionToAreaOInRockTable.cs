using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClimbingApp.Migrations
{
    /// <inheritdoc />
    public partial class changedRegionToAreaOInRockTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rocks_Regions_RegionId",
                table: "Rocks");

            migrationBuilder.RenameColumn(
                name: "RegionId",
                table: "Rocks",
                newName: "AreaId");

            migrationBuilder.RenameIndex(
                name: "IX_Rocks_RegionId",
                table: "Rocks",
                newName: "IX_Rocks_AreaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rocks_Areas_AreaId",
                table: "Rocks",
                column: "AreaId",
                principalTable: "Areas",
                principalColumn: "AreaId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rocks_Areas_AreaId",
                table: "Rocks");

            migrationBuilder.RenameColumn(
                name: "AreaId",
                table: "Rocks",
                newName: "RegionId");

            migrationBuilder.RenameIndex(
                name: "IX_Rocks_AreaId",
                table: "Rocks",
                newName: "IX_Rocks_RegionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rocks_Regions_RegionId",
                table: "Rocks",
                column: "RegionId",
                principalTable: "Regions",
                principalColumn: "RegionId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
