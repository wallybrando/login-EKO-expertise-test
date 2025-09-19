using LoginEKO.FileProcessingService.Domain.Models.Enums;

namespace LoginEKO.FileProcessingService.Domain.Models
{
    public class Filter
    {
        public required string Field { get; set; }
        public required FilterOperation Operation { get; set; }
        public required object? Value { get; set; }
    }
}
