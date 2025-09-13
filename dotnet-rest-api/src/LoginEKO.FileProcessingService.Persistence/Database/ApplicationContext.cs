using LoginEKO.FileProcessingService.Domain.Models;
using LoginEKO.FileProcessingService.Domain.Models.Enums;
using LoginEKO.FileProcessingService.Persistence.Database.Configuration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LoginEKO.FileProcessingService.Persistence.Database
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions options) : base(options) { }

        public DbSet<FileMetadata> FileMetadata { get; set; }
        public DbSet<TractorTelemetry> TractorTelemetries { get; set; }
        public DbSet<CombineTelemetry> CombineTelemetries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresEnum<ParkingBreakStatus>(name: "parking_break_status_type");
            modelBuilder.HasPostgresEnum<TransverseDifferentialLockStatus>(name: "transverse_differential_lock_status_type");
            modelBuilder.HasPostgresEnum<WheelDriveStatus>(name: "wheel_drive_status_type");

            modelBuilder.HasPostgresEnum<CropType>(name: "crop_type");
            modelBuilder.HasPostgresEnum<CruisePilotStatus>(name: "cruise_pilot_status_type");

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(FileMetadataEntityConfiguration).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TractorTelemetryEntityConfiguration).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CombineTelemetryEntityConfiguration).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
