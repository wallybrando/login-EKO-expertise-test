namespace LoginEKO.FileProcessingService.Contracts.Requests.V1
{
    public class FilterRequest
    {
        public required string Field { get; init; }
        public string? Operation { get; init; }
        public required object? Value { get; init; }
    }
}
