using LoginEKO.FileProcessingService.Domain.Models;
using System.Linq.Expressions;

namespace LoginEKO.FileProcessingService.Domain.Interfaces
{
    public interface IFilterExpressionBuilder<T>
    {
        IEnumerable<Expression<Func<T, bool>>> ApplyFilters(IEnumerable<Filter> filters);
    }
}
