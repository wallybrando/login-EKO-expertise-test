using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginEKO.FileProcessingService.Persistence.DML
{
    public static class TractorTelemetryQueries
    {
        public const string BulkInsertTelemetrySql = """
            
            INSERT INTO tractor_telemetry
            (id, serial_number, date, gps_longitude, gps_latitude, total_working_hours, engine_speed_in_rpm, engine_load_in_percentage, fuel_consumption_per_hour, ground_speed_gearbox_in_kmh, ground_speed_radar_in_kmh,
            coolant_temperature_in_celsius, speed_front_pto_in_rpm, speed_rear_pto_in_rpm, current_gear_shift, ambient_temperature_in_celsius, parking_break_status, transverse_differential_lock_status, all_wheel_drive_status, actual_status_of_creeper)
            VALUES (uuid_generate_v4(), @SerialNumber, @Date, @GPSLongitude, @GPSLatitude, @TotalWorkingHours, @EngineSpeedInRpm, @EngineLoadInPercentage, @FuelConsumptionPerHour, @GroundSpeedGearboxInKmh, @GroundSpeedRadarInKmh,
            @CoolantTemperatureInCelsius, @SpeedFrontPtoInRpm, @SpeedRearPtoInRpm, @CurrentGearShift, @AmbientTemperatureInCelsius, @ParkingBreakStatus, @TransverseDifferentialLockStatus, @AllWheelDriveStatus, @ActualStatusOfCreeper);
            """;
    }
}
