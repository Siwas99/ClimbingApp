using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClimbingApp.Migrations
{
    /// <inheritdoc />
    public partial class extendedRockTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Town",
                table: "Regions");

            migrationBuilder.AlterColumn<int>(
                name: "Year",
                table: "Routes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "Number",
                table: "Routes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Distance",
                table: "Rocks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Height",
                table: "Rocks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Populatiry",
                table: "Rocks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RockFaceExposure",
                table: "Rocks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "isLoose",
                table: "Rocks",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isRecommended",
                table: "Rocks",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isShadedFromTrees",
                table: "Rocks",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "DominantRockFormations",
                columns: table => new
                {
                    DominantRockFormationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RockFormation = table.Column<int>(type: "int", nullable: false),
                    RockId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DominantRockFormations", x => x.DominantRockFormationId);
                    table.ForeignKey(
                        name: "FK_DominantRockFormations_Rocks_RockId",
                        column: x => x.RockId,
                        principalTable: "Rocks",
                        principalColumn: "RockId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DominantRockFormations_RockId",
                table: "DominantRockFormations",
                column: "RockId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DominantRockFormations");

            migrationBuilder.DropColumn(
                name: "Number",
                table: "Routes");

            migrationBuilder.DropColumn(
                name: "Distance",
                table: "Rocks");

            migrationBuilder.DropColumn(
                name: "Height",
                table: "Rocks");

            migrationBuilder.DropColumn(
                name: "Populatiry",
                table: "Rocks");

            migrationBuilder.DropColumn(
                name: "RockFaceExposure",
                table: "Rocks");

            migrationBuilder.DropColumn(
                name: "isLoose",
                table: "Rocks");

            migrationBuilder.DropColumn(
                name: "isRecommended",
                table: "Rocks");

            migrationBuilder.DropColumn(
                name: "isShadedFromTrees",
                table: "Rocks");

            migrationBuilder.AlterColumn<int>(
                name: "Year",
                table: "Routes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Town",
                table: "Regions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
