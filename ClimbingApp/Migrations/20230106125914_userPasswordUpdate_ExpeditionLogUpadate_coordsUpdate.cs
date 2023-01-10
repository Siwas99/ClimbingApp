using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClimbingApp.Migrations
{
    /// <inheritdoc />
    public partial class userPasswordUpdateExpeditionLogUpadatecoordsUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Coordinates",
                table: "Rocks");

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordHash",
                table: "Users",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordSalt",
                table: "Users",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AlterColumn<string>(
                name: "Difficulty",
                table: "Routes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "positionLatitude",
                table: "Rocks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "positionLogitude",
                table: "Rocks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ClimbStyleId",
                table: "ExpeditionLogs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ClimbStyle",
                columns: table => new
                {
                    ClimbStyleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClimbStyle", x => x.ClimbStyleId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExpeditionLogs_ClimbStyleId",
                table: "ExpeditionLogs",
                column: "ClimbStyleId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExpeditionLogs_ClimbStyle_ClimbStyleId",
                table: "ExpeditionLogs",
                column: "ClimbStyleId",
                principalTable: "ClimbStyle",
                principalColumn: "ClimbStyleId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExpeditionLogs_ClimbStyle_ClimbStyleId",
                table: "ExpeditionLogs");

            migrationBuilder.DropTable(
                name: "ClimbStyle");

            migrationBuilder.DropIndex(
                name: "IX_ExpeditionLogs_ClimbStyleId",
                table: "ExpeditionLogs");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PasswordSalt",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "positionLatitude",
                table: "Rocks");

            migrationBuilder.DropColumn(
                name: "positionLogitude",
                table: "Rocks");

            migrationBuilder.DropColumn(
                name: "ClimbStyleId",
                table: "ExpeditionLogs");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Difficulty",
                table: "Routes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Coordinates",
                table: "Rocks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
