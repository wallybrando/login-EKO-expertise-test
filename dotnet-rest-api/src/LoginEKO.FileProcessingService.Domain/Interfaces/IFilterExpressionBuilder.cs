using LoginEKO.FileProcessingService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LoginEKO.FileProcessingService.Domain.Interfaces
{
    public interface IFilterExpressionBuilder<T>
    {
        IEnumerable<Expression<Func<T, bool>>> ApplyFilters(IEnumerable<Filter> filters);
    }
}
