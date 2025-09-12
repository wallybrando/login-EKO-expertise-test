using LoginEKO.FileProcessingService.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginEKO.FileProcessingService.Persistence.Database.Configuration
{
    public class CombineTelemetryEntityConfiguration : IEntityTypeConfiguration<CombineTelemetry>
    {
        public void Configure(EntityTypeBuilder<CombineTelemetry> builder)
        {
            builder.ToTable("combine_telemetry");

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

            builder.Property(p => p.GroundSpeedInKmh)
                 .HasColumnName("ground_speed_in_kmh")
                 .HasColumnType("DOUBLE PRECISION")
                 .IsRequired();

            builder.Property(p => p.EngineSpeedInRpm)
                 .HasColumnName("engine_speed_in_rpm")
                 .HasColumnType("INT");

            builder.Property(p => p.EngineLoadInPercentage)
                 .HasColumnName("engine_load_in_percentage")
                 .HasColumnType("DOUBLE PRECISION")
                 .IsRequired();

            builder.Property(p => p.DrumSpeedInRpm)
                 .HasColumnName("drum_speed_in_rpm")
                 .HasColumnType("INT")
                 .IsRequired();

            builder.Property(p => p.FanSpeedInRpm)
                 .HasColumnName("fan_speed_in_rpm")
                 .HasColumnType("INT")
                 .IsRequired();

            builder.Property(p => p.RotorStrawWalkerSpeedInRpm)
                 .HasColumnName("rotor_straw_walker_speed_in_rpm")
                 .HasColumnType("INT")
                 .IsRequired();

            builder.Property(p => p.SeparationLossesInPercentage)
                 .HasColumnName("separation_losses_in_percentage")
                 .HasColumnType("DOUBLE PRECISION");

            builder.Property(p => p.SieveLossesInPercentage)
                 .HasColumnName("sieve_losses_in_percentage")
                 .HasColumnType("DOUBLE PRECISION");

            builder.Property(p => p.Chopper)
                 .HasColumnName("chopper")
                 .HasColumnType("BOOLEAN")
                 .IsRequired();

            builder.Property(p => p.DieselTankLevelInPercentage)
                 .HasColumnName("diesel_tank_level_in_percentage")
                 .HasColumnType("DOUBLE PRECISION")
                 .IsRequired();

            builder.Property(p => p.NumberOfPartialWidths)
                 .HasColumnName("number_of_partial_widths")
                 .HasColumnType("SMALLINT")
                 .IsRequired();

            builder.Property(p => p.FrontAttachment)
                 .HasColumnName("front_attachment")
                 .HasColumnType("BOOLEAN")
                 .IsRequired();

            builder.Property(p => p.MaxNumberOfPartialWidths)
                 .HasColumnName("Max_number_of_partial_widths")
                 .HasColumnType("SMALLINT")
                 .IsRequired();

            builder.Property(p => p.FeedRakeSpeedInRpm)
                 .HasColumnName("feed_rake_speed_in_rpm")
                 .HasColumnType("INT")
                 .IsRequired();

            builder.Property(p => p.WorkingPosition)
                 .HasColumnName("working_position")
                 .HasColumnType("BOOLEAN")
                 .IsRequired();

            builder.Property(p => p.GrainTankUnloading)
                 .HasColumnName("grain_tank_unloading")
                 .HasColumnType("BOOLEAN")
                 .IsRequired();

            builder.Property(p => p.MainDriveStatus)
                 .HasColumnName("main_drive_status")
                 .HasColumnType("BOOLEAN")
                 .IsRequired();

            builder.Property(p => p.ConcavePositionInMM)
                 .HasColumnName("concave_position_in_mm")
                 .HasColumnType("SMALLINT")
                 .IsRequired();

            builder.Property(p => p.UpperSievePositionInMM)
                 .HasColumnName("upper_sieve_position_in_mm")
                 .HasColumnType("SMALLINT")
                 .IsRequired();

            builder.Property(p => p.LowerSievePositionInMM)
                 .HasColumnName("lower_sieve_position_in_mm")
                 .HasColumnType("SMALLINT")
                 .IsRequired();

            builder.Property(p => p.GrainTank70)
                 .HasColumnName("grain_tank_70")
                 .HasColumnType("BOOLEAN")
                 .IsRequired();

            builder.Property(p => p.GrainTank100)
                 .HasColumnName("grain_tank_100")
                 .HasColumnType("BOOLEAN")
                 .IsRequired();

            builder.Property(p => p.GrainMoistureContentInPercentage)
                 .HasColumnName("grain_moisture_content_in_percentage")
                 .HasColumnType("DOUBLE PRECISION");

            builder.Property(p => p.ThroughputTonsPerHour)
                 .HasColumnName("throughput_tons_per_hour")
                 .HasColumnType("DOUBLE PRECISION")
                 .IsRequired();

            builder.Property(p => p.RadialSpreaderSpeedInRpm)
                 .HasColumnName("radial_spreader_speed_in_rpm")
                 .HasColumnType("INT");

            builder.Property(p => p.GrainInReturnsInPercentage)
                 .HasColumnName("grain_in_returns_in_percentage")
                 .HasColumnType("DOUBLE PRECISION")
                 .IsRequired();

            builder.Property(p => p.ChannelPositionInPercentage)
                 .HasColumnName("channel_position_in_percentage")
                 .HasColumnType("DOUBLE PRECISION")
                 .IsRequired();

            builder.Property(p => p.YieldMeasurement)
                 .HasColumnName("yield_measurement")
                 .HasColumnType("BOOLEAN")
                 .IsRequired();

            builder.Property(p => p.ReturnsAuferMeasurementInPercentage)
                 .HasColumnName("returns_aufer_measurement_in_percentage")
                 .HasColumnType("DOUBLE PRECISION")
                 .IsRequired();

            builder.Property(p => p.MoistureMeasurement)
                 .HasColumnName("moisture_measurement")
                 .HasColumnType("BOOLEAN")
                 .IsRequired();

            builder.Property(p => p.TypeOfCrop)
                 .HasColumnName("type_of_crop")
                 .HasColumnType("crop_type")
                 .IsRequired();

            builder.Property(p => p.SpecialCropWeightInGrams)
                 .HasColumnName("special_crop_weight_in_grams")
                 .HasColumnType("INT")
                 .IsRequired();

            builder.Property(p => p.AutoPilotStatus)
                 .HasColumnName("auto_pilot_status")
                 .HasColumnType("BOOLEAN")
                 .IsRequired();

            builder.Property(p => p.CruisePilotStatus)
                 .HasColumnName("cruise_pilot_status")
                 .HasColumnType("cruise_pilot_status_type")
                 .IsRequired();

            builder.Property(p => p.RateOfWorkInHaPerHour)
                 .HasColumnName("rate_of_work_in_ha_per_hour")
                 .HasColumnType("DOUBLE PRECISION")
                 .IsRequired();

            builder.Property(p => p.YieldInTonsPerHour)
                 .HasColumnName("yield_in_tons_per_hour")
                 .HasColumnType("DOUBLE PRECISION");

            builder.Property(p => p.QuantimeterCalibrationFactor)
                 .HasColumnName("quantimeter_calibration_factor")
                 .HasColumnType("DOUBLE PRECISION")
                 .IsRequired();

            builder.Property(p => p.SeparationSensitivityInPercentage)
                 .HasColumnName("separation_sensitivity_in_percentage")
                 .HasColumnType("DOUBLE PRECISION")
                 .IsRequired();

            builder.Property(p => p.SieveSensitivityInPercentage)
                 .HasColumnName("sieve_sensitivity_in_percentage")
                 .HasColumnType("DOUBLE PRECISION")
                 .IsRequired();
        }
    }
}
