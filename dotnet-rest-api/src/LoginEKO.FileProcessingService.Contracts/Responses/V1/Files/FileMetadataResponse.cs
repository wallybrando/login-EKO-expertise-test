namespace LoginEKO.FileProcessingService.Contracts.Responses.V1.Files
{
    public class FileMetadataResponse
    {
        public Guid Id { get; init; }
        public required string Filename { get; init; }
        public long SizeInBytes { get; init; }
        public int NumberOfImports { get; set; }
        public DateTime CreatedDate { get; init; }
    }
}
