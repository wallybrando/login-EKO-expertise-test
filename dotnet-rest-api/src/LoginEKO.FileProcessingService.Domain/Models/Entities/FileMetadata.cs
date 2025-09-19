using Microsoft.AspNetCore.Http;

namespace LoginEKO.FileProcessingService.Domain.Models.Entities
{
    public class FileMetadata
    {
        public Guid Id { get; set; }
        public required string Filename { get; set; }
        public required string Extension { get; set; }
        public long SizeInBytes { get; set; }
        public required byte[] BinaryObject { get; set; }
        public required IFormFile File { get; set; }
        public string MD5Hash { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual ICollection<TractorTelemetry> TractorTelemetries { get; set; }
        public virtual ICollection<CombineTelemetry> CombineTelemetries { get; set; }
    }
}
