using LoginEKO.FileProcessingService.Domain.Extensions;
using LoginEKO.FileProcessingService.Domain.Interfaces;
using LoginEKO.FileProcessingService.Domain.Interfaces.Services;
using LoginEKO.FileProcessingService.Domain.Models.Entities;
using LoginEKO.FileProcessingService.Domain.Models.Enums;
using LoginEKO.FileProcessingService.Domain.SchemaRegistries;
using LoginEKO.FileProcessingService.Domain.Services;
using LoginEKO.FileProcessingService.Domain.Services.FileExtractors;
using LoginEKO.FileProcessingService.Domain.Services.TelemetryParsers;
using LoginEKO.FileProcessingService.Domain.Validators;
using Microsoft.Extensions.DependencyInjection;

namespace LoginEKO.FileProcessingService.CompositionRoot.Extensions
{
    public static class DomainServiceCollectionExtensions
    {
        public static IServiceCollection AddDomain(this IServiceCollection services)
        {
            services.AddKeyedScoped<IVehicleTelemetryParser, TractorTelemetryParser>(VehicleType.TRACTOR.GetDescription());
            services.AddKeyedScoped<IVehicleTelemetryParser, CombineTelemetryParser>(VehicleType.COMBINE.GetDescription());

            services.AddSingleton<SchemaRegistry<TractorTelemetry>, TractorTelemetrySchemaRegistry<TractorTelemetry>>();
            services.AddSingleton<SchemaRegistry<CombineTelemetry>, CombineTelemetrySchemaRegistry<CombineTelemetry>>();

            services.AddSingleton<FilterValidator<TractorTelemetry>>();
            services.AddSingleton<FilterValidator<CombineTelemetry>>();

            services.AddKeyedScoped<IFileExtractor, CsvFileExtractor>(FileType.CSV.GetDescription());

            services.AddScoped<IFileService, FileService>();
            services.AddScoped<ITelemetryService, TelemetryService>();

            return services;
        }
    }
}
