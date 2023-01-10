using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClimbingApp.Migrations
{
    /// <inheritdoc />
    public partial class addedRockFaceExposureANDRockFormations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RockFaceExposure",
                table: "Rocks",
                newName: "RockFaceExposureId");

            migrationBuilder.RenameColumn(
                name: "RockFormation",
                table: "DominantRockFormations",
                newName: "RockFormationId");

            migrationBuilder.CreateTable(
                name: "RockFaceExposures",
                columns: table => new
                {
                    RockFaceExposureId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RockFaceExposures", x => x.RockFaceExposureId);
                });

            migrationBuilder.CreateTable(
                name: "RockFormations",
                columns: table => new
                {
                    RockFormationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RockFormations", x => x.RockFormationId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rocks_RockFaceExposureId",
                table: "Rocks",
                column: "RockFaceExposureId");

            migrationBuilder.CreateIndex(
                name: "IX_DominantRockFormations_RockFormationId",
                table: "DominantRockFormations",
                column: "RockFormationId");

            migrationBuilder.AddForeignKey(
                name: "FK_DominantRockFormations_RockFormations_RockFormationId",
                table: "DominantRockFormations",
                column: "RockFormationId",
                principalTable: "RockFormations",
                principalColumn: "RockFormationId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rocks_RockFaceExposures_RockFaceExposureId",
                table: "Rocks",
                column: "RockFaceExposureId",
                principalTable: "RockFaceExposures",
                principalColumn: "RockFaceExposureId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DominantRockFormations_RockFormations_RockFormationId",
                table: "DominantRockFormations");

            migrationBuilder.DropForeignKey(
                name: "FK_Rocks_RockFaceExposures_RockFaceExposureId",
                table: "Rocks");

            migrationBuilder.DropTable(
                name: "RockFaceExposures");

            migrationBuilder.DropTable(
                name: "RockFormations");

            migrationBuilder.DropIndex(
                name: "IX_Rocks_RockFaceExposureId",
                table: "Rocks");

            migrationBuilder.DropIndex(
                name: "IX_DominantRockFormations_RockFormationId",
                table: "DominantRockFormations");

            migrationBuilder.RenameColumn(
                name: "RockFaceExposureId",
                table: "Rocks",
                newName: "RockFaceExposure");

            migrationBuilder.RenameColumn(
                name: "RockFormationId",
                table: "DominantRockFormations",
                newName: "RockFormation");
        }
    }
}
