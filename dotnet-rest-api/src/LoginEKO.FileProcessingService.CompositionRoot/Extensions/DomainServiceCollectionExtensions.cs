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
            services.AddScoped<IVehicleDataParser, TractorDataParser>();
            services.AddScoped<IVehicleDataParser, CombineDataParser>();
            services.AddScoped<IFileExtractor, CsvFileExtractor>();
            services.AddScoped<IFileService, FileService>();
            return services;
        }
    }
}
