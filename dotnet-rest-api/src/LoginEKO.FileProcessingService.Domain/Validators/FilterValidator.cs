using LoginEKO.FileProcessingService.Domain.Interfaces;
using LoginEKO.FileProcessingService.Domain.Models.Enums;
using LoginEKO.FileProcessingService.Domain.Utils;

namespace LoginEKO.FileProcessingService.Domain.Validators
{
    public class FilterValidator<T>
    {
        private readonly SchemaRegistry<T> _schemaRegistry;

        public FilterValidator(SchemaRegistry<T> schemaRegistry)
        {
            _schemaRegistry = schemaRegistry;
        }

        public bool ValidateFilterFieldName(string fieldName)
        {
            return _schemaRegistry.FieldExists(fieldName);
        }

        public bool ValidFilterType(string fieldName)
        {
            if (!_schemaRegistry.TryGetFieldType(fieldName, out var outputType))
                return false;

            if (outputType == typeof(Enum) && !_schemaRegistry.TryGetEnumType(fieldName, out _))
            {
                return false;
            }

            return true;
        }

        public bool ValidateOperation(string fieldName, FilterOperation filterOperation)
        {
            if (!_schemaRegistry.TryGetFieldType(fieldName, out var outputType))
                return false;

            if (outputType == typeof(Enum) && !_schemaRegistry.TryGetEnumType(fieldName, out outputType))
            {
                return false;
            }

            if (!_schemaRegistry.TypeHasAllowedOperation(outputType, filterOperation))
                return false;

            return true;
        }

        public bool ValidateNullAssignment(string fieldName, object? value, FilterOperation filterOperation)
        {
            if (value == null)
            {
                if (!_schemaRegistry.TryGetFieldType(fieldName, out var outputType))
                    return false;

                if (!TypeValidator.IsNullableType(outputType))
                    return false;

                if (filterOperation != FilterOperation.EQUALS)
                    return false;
            }

            return true;
        }
    }
}
