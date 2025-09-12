using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoginEKO.FileProcessingService.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CreeperStatusIsNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "actual_status_of_creeper",
                table: "tractor_telemetry",
                type: "BOOLEAN",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "BOOLEAN");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "actual_status_of_creeper",
                table: "tractor_telemetry",
                type: "BOOLEAN",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "BOOLEAN",
                oldNullable: true);
        }
    }
}
