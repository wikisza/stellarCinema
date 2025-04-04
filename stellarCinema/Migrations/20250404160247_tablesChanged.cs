using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace stellarCinema.Migrations
{
    /// <inheritdoc />
    public partial class tablesChanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReservationSeats_Showtimes_ShowtimeIdShowtime",
                table: "ReservationSeats");

            migrationBuilder.DropIndex(
                name: "IX_ReservationSeats_ShowtimeIdShowtime",
                table: "ReservationSeats");

            migrationBuilder.DropColumn(
                name: "ShowtimeIdShowtime",
                table: "ReservationSeats");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationSeats_IdShowtime",
                table: "ReservationSeats",
                column: "IdShowtime");

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationSeats_Showtimes_IdShowtime",
                table: "ReservationSeats",
                column: "IdShowtime",
                principalTable: "Showtimes",
                principalColumn: "IdShowtime",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReservationSeats_Showtimes_IdShowtime",
                table: "ReservationSeats");

            migrationBuilder.DropIndex(
                name: "IX_ReservationSeats_IdShowtime",
                table: "ReservationSeats");

            migrationBuilder.AddColumn<int>(
                name: "ShowtimeIdShowtime",
                table: "ReservationSeats",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReservationSeats_ShowtimeIdShowtime",
                table: "ReservationSeats",
                column: "ShowtimeIdShowtime");

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationSeats_Showtimes_ShowtimeIdShowtime",
                table: "ReservationSeats",
                column: "ShowtimeIdShowtime",
                principalTable: "Showtimes",
                principalColumn: "IdShowtime");
        }
    }
}
