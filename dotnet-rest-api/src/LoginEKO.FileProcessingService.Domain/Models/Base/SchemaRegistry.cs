using LoginEKO.FileProcessingService.Domain.Models.Enums;
using LoginEKO.FileProcessingService.Domain.Utils;

namespace LoginEKO.FileProcessingService.Domain.Models.Base
{
    public abstract class SchemaRegistry
    {
        protected virtual IDictionary<string, Type> FieldRegistry { get; init; } = new Dictionary<string, Type>();
        protected virtual IDictionary<Type, FilterOperation> OperationRegistry { get; init; } = new Dictionary<Type, FilterOperation>();

        public virtual bool TryGetFieldType(string fieldName, out Type type)
        {
            type = typeof(MockType);
            fieldName = fieldName.ToLower();
            if (string.IsNullOrEmpty(fieldName) || !FieldRegistry.TryGetValue(fieldName.ToLower(), out type))
                return false;

            type = FieldRegistry[fieldName];
            return true;
        }

        public virtual bool TryGetOperation(Type type, string suppliedOperation, out FilterOperation operation)
        {
            operation = FilterOperation.UNKNOWN;

            if (string.IsNullOrEmpty(suppliedOperation) || !OperationRegistry.TryGetValue(type, out FilterOperation allowedOperations))
                return false;

            try
            {
                operation = DataConverter.ToEnum<FilterOperation>(suppliedOperation);
            }
            catch
            {
                return false;
            }

            if (operation == FilterOperation.UNKNOWN)
                return false;

            if (!allowedOperations.HasFlag(operation))
            {
                operation = FilterOperation.UNKNOWN;
                return false;
            }

            return true;
        }
    }

    public class MockType() { }
}
