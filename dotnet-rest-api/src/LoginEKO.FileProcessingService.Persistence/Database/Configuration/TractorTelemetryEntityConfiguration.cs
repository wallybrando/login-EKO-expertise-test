using LoginEKO.FileProcessingService.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace LoginEKO.FileProcessingService.Persistence.Database.Configuration
{
    public class TractorTelemetryEntityConfiguration : IEntityTypeConfiguration<TractorTelemetry>
    {
        public void Configure(EntityTypeBuilder<TractorTelemetry> builder)
        {
            builder.ToTable("tractor_telemetry");

            builder.Property(t => t.Id)
                .HasColumnName("id")
                .HasValueGenerator((Id, type) => new SequentialGuidValueGenerator())
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Throw);

            builder.Property(p => p.SerialNumber)
                 .HasColumnName("serial_number")
                 .HasColumnType("TEXT")
                 .IsRequired();

            builder.Property(p => p.Date)
                 .HasColumnName("date")
                 .HasColumnType("TIMESTAMP")
                 .IsRequired();

            builder.Property(p => p.GPSLongitude)
                 .HasColumnName("gps_longitude")
                 .HasColumnType("DOUBLE PRECISION")
                 .IsRequired();

            builder.Property(p => p.GPSLatitude)
                 .HasColumnName("gps_latitude")
                 .HasColumnType("DOUBLE PRECISION")
                 .IsRequired();

            builder.Property(p => p.TotalWorkingHours)
                 .HasColumnName("total_working_hours")
                 .HasColumnType("DOUBLE PRECISION")
                 .IsRequired();

            builder.Property(p => p.EngineSpeedInRpm)
                 .HasColumnName("engine_speed_in_rpm")
                 .HasColumnType("INT");

            builder.Property(p => p.EngineLoadInPercentage)
                 .HasColumnName("engine_load_in_percentage")
                 .HasColumnType("DOUBLE PRECISION")
                 .IsRequired();

            builder.Property(p => p.FuelConsumptionPerHour)
                 .HasColumnName("fuel_consumption_per_hour")
                 .HasColumnType("DOUBLE PRECISION");

            builder.Property(p => p.GroundSpeedGearboxInKmh)
                 .HasColumnName("ground_speed_gearbox_in_kmh")
                 .HasColumnType("DOUBLE PRECISION")
                 .IsRequired();

            builder.Property(p => p.GroundSpeedRadarInKmh)
                 .HasColumnName("ground_speed_radar_in_kmh")
                 .HasColumnType("INT");

            builder.Property(p => p.CoolantTemperatureInCelsius)
                 .HasColumnName("coolant_temperature_in_celsius")
                 .HasColumnType("INT")
                 .IsRequired();

            builder.Property(p => p.SpeedFrontPtoInRpm)
                 .HasColumnName("speed_front_pto_in_rpm")
                 .HasColumnType("INT")
                 .IsRequired();

            builder.Property(p => p.SpeedRearPtoInRpm)
                 .HasColumnName("speed_rear_pto_in_rpm")
                 .HasColumnType("INT")
                 .IsRequired();

            builder.Property(p => p.CurrentGearShift)
                 .HasColumnName("current_gear_shift")
                 .HasColumnType("SMALLINT");

            builder.Property(p => p.AmbientTemperatureInCelsius)
                 .HasColumnName("ambient_temperature_in_celsius")
                 .HasColumnType("DOUBLE PRECISION")
                 .IsRequired();

            builder.Property(p => p.ParkingBreakStatus)
                 .HasColumnName("parking_break_status")
                 .HasColumnType("parking_break_status_type")
                 .IsRequired();

            builder.Property(p => p.TransverseDifferentialLockStatus)
                 .HasColumnName("transverse_differential_lock_status")
                 .HasColumnType("transverse_differential_lock_status_type")
                 .IsRequired();

            builder.Property(p => p.AllWheelDriveStatus)
                 .HasColumnName("all_wheel_drive_status")
                 .HasColumnType("wheel_drive_status_type")
                 .IsRequired();

            builder.Property(p => p.ActualStatusOfCreeper)
                 .HasColumnName("actual_status_of_creeper")
                 .HasColumnType("BOOLEAN")
                 .IsRequired();
        }
    }
}
