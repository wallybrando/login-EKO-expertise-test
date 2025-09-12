using System;
using LoginEKO.FileProcessingService.Domain.Models;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoginEKO.FileProcessingService.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:parking_break_status_type", "status_3")
                .Annotation("Npgsql:Enum:transverse_differential_lock_status_type", "status_0")
                .Annotation("Npgsql:Enum:wheel_drive_status_type", "inactive,active,status_2");

            migrationBuilder.CreateTable(
                name: "file_metadata",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    filename = table.Column<string>(type: "TEXT", nullable: false),
                    extension = table.Column<string>(type: "TEXT", nullable: false),
                    size_in_bytes = table.Column<long>(type: "BIGINT", nullable: false),
                    binary_object = table.Column<byte[]>(type: "BYTEA", nullable: false),
                    md5_hash = table.Column<string>(type: "TEXT", nullable: false),
                    created_date = table.Column<DateTime>(type: "TIMESTAMP", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_file_metadata", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tractor_telemetry",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    engine_load_in_percentage = table.Column<double>(type: "DOUBLE PRECISION", nullable: false),
                    fuel_consumption_per_hour = table.Column<double>(type: "DOUBLE PRECISION", nullable: true),
                    ground_speed_gearbox_in_kmh = table.Column<double>(type: "DOUBLE PRECISION", nullable: false),
                    ground_speed_radar_in_kmh = table.Column<int>(type: "INT", nullable: true),
                    coolant_temperature_in_celsius = table.Column<int>(type: "INT", nullable: false),
                    speed_front_pto_in_rpm = table.Column<int>(type: "INT", nullable: false),
                    speed_rear_pto_in_rpm = table.Column<int>(type: "INT", nullable: false),
                    current_gear_shift = table.Column<short>(type: "SMALLINT", nullable: true),
                    ambient_temperature_in_celsius = table.Column<double>(type: "DOUBLE PRECISION", nullable: false),
                    parking_break_status = table.Column<ParkingBreakStatus>(type: "parking_break_status_type", nullable: false),
                    transverse_differential_lock_status = table.Column<TransverseDifferentialLockStatus>(type: "transverse_differential_lock_status_type", nullable: false),
                    all_wheel_drive_status = table.Column<WheelDriveStatus>(type: "wheel_drive_status_type", nullable: false),
                    actual_status_of_creeper = table.Column<bool>(type: "BOOLEAN", nullable: false),
                    serial_number = table.Column<string>(type: "TEXT", nullable: false),
                    date = table.Column<DateTime>(type: "TIMESTAMP", nullable: false),
                    gps_longitude = table.Column<double>(type: "DOUBLE PRECISION", nullable: false),
                    gps_latitude = table.Column<double>(type: "DOUBLE PRECISION", nullable: false),
                    total_working_hours = table.Column<double>(type: "DOUBLE PRECISION", nullable: false),
                    engine_speed_in_rpm = table.Column<int>(type: "INT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tractor_telemetry", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "file_metadata");

            migrationBuilder.DropTable(
                name: "tractor_telemetry");
        }
    }
}
