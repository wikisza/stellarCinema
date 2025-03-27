using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace stellarCinema.Migrations
{
    /// <inheritdoc />
    public partial class alteredConfigurationTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SeatPrice",
                table: "Configurations",
                newName: "KeyValue");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "KeyValue",
                table: "Configurations",
                newName: "SeatPrice");
        }
    }
}
