namespace LoginEKO.FileProcessingService.Domain.Models
{
    public class PaginatedFilter
    {
        public required int PageNumber { get; set; }
        public required int PageSize { get; set; }
        public IEnumerable<Filter> Filters { get; set; } = [];
    }
}
