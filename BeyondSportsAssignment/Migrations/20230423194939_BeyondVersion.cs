using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeyondSportsAssignment.Migrations
{
    /// <inheritdoc />
    public partial class BeyondVersion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AssistsCurrentSeason",
                table: "Players",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GamesPlayedCurrentSeason",
                table: "Players",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GoalsCurrentSeason",
                table: "Players",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssistsCurrentSeason",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "GamesPlayedCurrentSeason",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "GoalsCurrentSeason",
                table: "Players");
        }
    }
}
