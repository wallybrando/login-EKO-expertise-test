namespace LoginEKO.FileProcessingService.Domain.Models
{
    public class Filter
    {
        public required string Field { get; set; }
        public required string Operation { get; set; }
        //public required string? Value { get; set; }
        public required object? Value { get; set; }
    }
}
