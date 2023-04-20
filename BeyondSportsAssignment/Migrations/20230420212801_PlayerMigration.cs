using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeyondSportsAssignment.Migrations
{
    /// <inheritdoc />
    public partial class PlayerMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PreviousTeamId",
                table: "Players");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PreviousTeamId",
                table: "Players",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
