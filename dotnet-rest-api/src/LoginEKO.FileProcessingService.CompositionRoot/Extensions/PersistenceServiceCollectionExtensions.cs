using LoginEKO.FileProcessingService.Domain.Interfaces.Repositories;
using LoginEKO.FileProcessingService.Domain.Models.Enums;
using LoginEKO.FileProcessingService.Persistence.Database;
using LoginEKO.FileProcessingService.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace LoginEKO.FileProcessingService.CompositionRoot.Extensions
{
    public static class PersistenceServiceCollectionExtensions
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, string connectionString)
        {
            services.AddEFDatabaseConfiguration(connectionString);

            services.AddScoped<IFileMetadataRepository, FileMetadataRepository>();
            services.AddScoped<ITractorTelemetryRepository, TractorTelemetryRepository>();
            services.AddScoped<ICombineTelemetryRepository, CombineTelemetryRepository>();
            return services;
        }

        private static IServiceCollection AddEFDatabaseConfiguration(this IServiceCollection services, string connectionString)
        {
            var dataSourceBuilder = new NpgsqlDataSourceBuilder(connectionString);

            dataSourceBuilder.MapEnum<ParkingBreakStatus>("parking_break_status_type");
            dataSourceBuilder.MapEnum<TransverseDifferentialLockStatus>("transverse_differential_lock_status_type");
            dataSourceBuilder.MapEnum<WheelDriveStatus>("wheel_drive_status_type");
            dataSourceBuilder.MapEnum<CropType>("crop_type");
            dataSourceBuilder.MapEnum<CruisePilotStatus>("cruise_pilot_status_type");

            var dataSource = dataSourceBuilder.Build();
            services.AddDbContext<ApplicationContext>(options => options.UseNpgsql(dataSource));

            return services;
        }
    }
}
