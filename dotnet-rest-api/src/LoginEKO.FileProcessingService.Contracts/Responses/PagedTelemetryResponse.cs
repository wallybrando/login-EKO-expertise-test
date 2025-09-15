namespace LoginEKO.FileProcessingService.Contracts.Responses
{
    public class PagedTelemetryResponse
    {
        public int? Page { get; init; }
        public int? PageSize { get; init; }
        public int TotalCombineItems { get; init; }
        public int TotalTractorItems { get; init; }
        public int TotalItems { get; init; }
        public IDictionary<string, IEnumerable<object>> Telemetry { get; init; }
    }
}
