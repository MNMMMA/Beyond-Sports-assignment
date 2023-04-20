using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeyondSportsAssignment.Migrations
{
    /// <inheritdoc />
    public partial class BeyondUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TeamId",
                table: "Players",
                newName: "PreviousTeamId");

            migrationBuilder.AddColumn<int>(
                name: "CurrentTeamId",
                table: "Players",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastTransferDate",
                table: "Players",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentTeamId",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "LastTransferDate",
                table: "Players");

            migrationBuilder.RenameColumn(
                name: "PreviousTeamId",
                table: "Players",
                newName: "TeamId");
        }
    }
}
