using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace stellarCinema.Migrations
{
    /// <inheritdoc />
    public partial class showtimeTableUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ShowtimeDate",
                table: "Showtimes",
                newName: "ShowtimeDateStart");

            migrationBuilder.AddColumn<DateTime>(
                name: "ShowtimeDateEnd",
                table: "Showtimes",
                type: "datetime(0)",
                precision: 0,
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShowtimeDateEnd",
                table: "Showtimes");

            migrationBuilder.RenameColumn(
                name: "ShowtimeDateStart",
                table: "Showtimes",
                newName: "ShowtimeDate");
        }
    }
}
