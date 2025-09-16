namespace LoginEKO.FileProcessingService.Contracts.Requests
{
    public class FilterRequest
    {
        public required string Field { get; init; }
        public string? Operation { get; init; }
        //public required string? Value { get; init; }
        public required object? Value { get; init; }
    }
}
