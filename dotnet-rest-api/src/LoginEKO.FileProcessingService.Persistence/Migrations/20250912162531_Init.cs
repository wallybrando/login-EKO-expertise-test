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
                .Annotation("Npgsql:Enum:crop_type", "maize,sunflowers")
                .Annotation("Npgsql:Enum:cruise_pilot_status_type", "status_0")
                .Annotation("Npgsql:Enum:parking_break_status_type", "status_3")
                .Annotation("Npgsql:Enum:transverse_differential_lock_status_type", "status_0")
                .Annotation("Npgsql:Enum:wheel_drive_status_type", "inactive,active,status_2");

            migrationBuilder.CreateTable(
                name: "combine_telemetry",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    ground_speed_in_kmh = table.Column<double>(type: "DOUBLE PRECISION", nullable: false),
                    engine_load_in_percentage = table.Column<double>(type: "DOUBLE PRECISION", nullable: false),
                    drum_speed_in_rpm = table.Column<int>(type: "INT", nullable: false),
                    fan_speed_in_rpm = table.Column<int>(type: "INT", nullable: false),
                    rotor_straw_walker_speed_in_rpm = table.Column<int>(type: "INT", nullable: false),
                    separation_losses_in_percentage = table.Column<double>(type: "DOUBLE PRECISION", nullable: true),
                    sieve_losses_in_percentage = table.Column<double>(type: "DOUBLE PRECISION", nullable: true),
                    chopper = table.Column<bool>(type: "BOOLEAN", nullable: false),
                    diesel_tank_level_in_percentage = table.Column<double>(type: "DOUBLE PRECISION", nullable: false),
                    number_of_partial_widths = table.Column<short>(type: "SMALLINT", nullable: false),
                    front_attachment = table.Column<bool>(type: "BOOLEAN", nullable: false),
                    Max_number_of_partial_widths = table.Column<short>(type: "SMALLINT", nullable: false),
                    feed_rake_speed_in_rpm = table.Column<int>(type: "INT", nullable: false),
                    working_position = table.Column<bool>(type: "BOOLEAN", nullable: false),
                    grain_tank_unloading = table.Column<bool>(type: "BOOLEAN", nullable: false),
                    main_drive_status = table.Column<bool>(type: "BOOLEAN", nullable: false),
                    concave_position_in_mm = table.Column<short>(type: "SMALLINT", nullable: false),
                    upper_sieve_position_in_mm = table.Column<short>(type: "SMALLINT", nullable: false),
                    lower_sieve_position_in_mm = table.Column<short>(type: "SMALLINT", nullable: false),
                    grain_tank_70 = table.Column<bool>(type: "BOOLEAN", nullable: false),
                    grain_tank_100 = table.Column<bool>(type: "BOOLEAN", nullable: false),
                    grain_moisture_content_in_percentage = table.Column<double>(type: "DOUBLE PRECISION", nullable: true),
                    throughput_tons_per_hour = table.Column<double>(type: "DOUBLE PRECISION", nullable: false),
                    radial_spreader_speed_in_rpm = table.Column<int>(type: "INT", nullable: true),
                    grain_in_returns_in_percentage = table.Column<double>(type: "DOUBLE PRECISION", nullable: false),
                    channel_position_in_percentage = table.Column<double>(type: "DOUBLE PRECISION", nullable: false),
                    yield_measurement = table.Column<bool>(type: "BOOLEAN", nullable: false),
                    returns_aufer_measurement_in_percentage = table.Column<double>(type: "DOUBLE PRECISION", nullable: false),
                    moisture_measurement = table.Column<bool>(type: "BOOLEAN", nullable: false),
                    type_of_crop = table.Column<CropType>(type: "crop_type", nullable: false),
                    special_crop_weight_in_grams = table.Column<int>(type: "INT", nullable: false),
                    auto_pilot_status = table.Column<bool>(type: "BOOLEAN", nullable: false),
                    cruise_pilot_status = table.Column<CruisePilotStatus>(type: "cruise_pilot_status_type", nullable: false),
                    rate_of_work_in_ha_per_hour = table.Column<double>(type: "DOUBLE PRECISION", nullable: false),
                    yield_in_tons_per_hour = table.Column<double>(type: "DOUBLE PRECISION", nullable: true),
                    quantimeter_calibration_factor = table.Column<double>(type: "DOUBLE PRECISION", nullable: false),
                    separation_sensitivity_in_percentage = table.Column<double>(type: "DOUBLE PRECISION", nullable: false),
                    sieve_sensitivity_in_percentage = table.Column<double>(type: "DOUBLE PRECISION", nullable: false),
                    serial_number = table.Column<string>(type: "TEXT", nullable: false),
                    date = table.Column<DateTime>(type: "TIMESTAMP", nullable: false),
                    gps_longitude = table.Column<double>(type: "DOUBLE PRECISION", nullable: false),
                    gps_latitude = table.Column<double>(type: "DOUBLE PRECISION", nullable: false),
                    total_working_hours = table.Column<double>(type: "DOUBLE PRECISION", nullable: false),
                    engine_speed_in_rpm = table.Column<int>(type: "INT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_combine_telemetry", x => x.id);
                });

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
                    actual_status_of_creeper = table.Column<bool>(type: "BOOLEAN", nullable: true),
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
                name: "combine_telemetry");

            migrationBuilder.DropTable(
                name: "file_metadata");

            migrationBuilder.DropTable(
                name: "tractor_telemetry");
        }
    }
}
