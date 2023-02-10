using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ClimbingApp.Migrations
{
    /// <inheritdoc />
    public partial class addedSeeds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ClimbStyle",
                columns: new[] { "ClimbStyleId", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Czyste przejście drogi, tzn. bez odpadnięcia i obciążania liny, bez jej znajomości. Oznacza to, że niedozwolone są podpowiedzi lub obserwacja innych wspinaczy.", "On Sight" },
                    { 2, "Czyste przejście drogii, tzn. bez odpadnięcia i obciążania liny, ze znajomością drogi. Oznacza to, że podpowiedzi oraz oglądanie innych wspinaczy jest dozwolone.", "Flash" },
                    { 3, "Przejście całej drogi od początku do końca bez odpadnięć i odpoczynków. Dozwolone jest wcześniejsze ćwiczenie drogi i opracowanie sekwencji przechwytów. Styl ten uważa się za normalny w przypadku trudniejszych dróg.", "Red Point" },
                    { 4, "Lina asekurująca wspinacza biegnie na górę, przechodzi przez stanowisko i wraca do stojącego na dole partnera. Przejście drogi w tym stylu nie jest obecnie uznawane za klasyczne, jednak z uwagi na najmniejsze ryzyko urazów wspinaczka na wędkę ma znaczenie w treningu, patentowaniu drogi oraz we wspinaczkowej rekreacji, szczególnie u osób początkujących.", "Top Rope" }
                });

            migrationBuilder.InsertData(
                table: "RockFaceExposures",
                columns: new[] { "RockFaceExposureId", "Name" },
                values: new object[,]
                {
                    { 1, "North" },
                    { 2, "East" },
                    { 3, "South" },
                    { 4, "West" }
                });

            migrationBuilder.InsertData(
                table: "RockFormations",
                columns: new[] { "RockFormationId", "Name" },
                values: new object[,]
                {
                    { 1, "Slabs" },
                    { 2, "Vertical" },
                    { 3, "Overhang" },
                    { 4, "Roof" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ClimbStyle",
                keyColumn: "ClimbStyleId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ClimbStyle",
                keyColumn: "ClimbStyleId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ClimbStyle",
                keyColumn: "ClimbStyleId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ClimbStyle",
                keyColumn: "ClimbStyleId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "RockFaceExposures",
                keyColumn: "RockFaceExposureId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "RockFaceExposures",
                keyColumn: "RockFaceExposureId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "RockFaceExposures",
                keyColumn: "RockFaceExposureId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "RockFaceExposures",
                keyColumn: "RockFaceExposureId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "RockFormations",
                keyColumn: "RockFormationId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "RockFormations",
                keyColumn: "RockFormationId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "RockFormations",
                keyColumn: "RockFormationId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "RockFormations",
                keyColumn: "RockFormationId",
                keyValue: 4);
        }
    }
}
