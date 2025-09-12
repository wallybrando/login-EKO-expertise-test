using LoginEKO.FileProcessingService.Domain.Interfaces;
using LoginEKO.FileProcessingService.Domain.Interfaces.Services;
using LoginEKO.FileProcessingService.Domain.Services;
using LoginEKO.FileProcessingService.Domain.Services.DataTransformators;
using LoginEKO.FileProcessingService.Domain.Services.FileExtractors;
using Microsoft.Extensions.DependencyInjection;

namespace LoginEKO.FileProcessingService.CompositionRoot.Extensions
{
    public static class DomainServiceCollectionExtensions
    {
        public static IServiceCollection AddDomain(this IServiceCollection services)
        {
            services.AddScoped<IVehicleDataTransformator, TractorDataTransformator>();
            services.AddScoped<IVehicleDataTransformator, CombineDataTransformator>();
            services.AddScoped<ITextFileExtractor, CsvFileExtractor>();
            services.AddScoped<IVehicleService, VehicleService>();
            return services;
        }
    }
}
