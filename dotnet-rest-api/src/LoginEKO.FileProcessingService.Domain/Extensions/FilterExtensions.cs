using LoginEKO.FileProcessingService.Domain.Models;
using LoginEKO.FileProcessingService.Domain.Models.Base;
using LoginEKO.FileProcessingService.Domain.Models.Enums;
using LoginEKO.FileProcessingService.Domain.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginEKO.FileProcessingService.Domain.Extensions
{
    public static class FilterExtensions
    {
        public static bool FieldApplicable(this Filter filter, HashSet<string> fieldPlaceholder)
        {
            return fieldPlaceholder.Contains(filter.Field);
        }

        public static bool OperationApplicable(this Filter filter)
        {
            var operation = DataHelper.ConvertToEnum<FilterOperation>(filter.Operation);

            return operation != FilterOperation.UNKNOWN;
        }
    }
}
