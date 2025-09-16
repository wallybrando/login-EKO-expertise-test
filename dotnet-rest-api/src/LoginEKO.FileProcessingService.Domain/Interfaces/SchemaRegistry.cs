using LoginEKO.FileProcessingService.Domain.Models.Enums;
using LoginEKO.FileProcessingService.Domain.Utils;

namespace LoginEKO.FileProcessingService.Domain.Interfaces
{
    public abstract class SchemaRegistry<T>
    {
        protected virtual IDictionary<string, Type> FieldRegistry { get; init; } = new Dictionary<string, Type>();
        protected virtual IDictionary<Type, FilterOperation> OperationRegistry { get; init; } = new Dictionary<Type, FilterOperation>();
        protected virtual IDictionary<string, Func<string, Enum>> EnumRegistry { get; init; } = new Dictionary<string, Func<string, Enum>>();
        protected virtual IDictionary<string, Type> EnumTypeRegistry { get; init; } = new Dictionary<string, Type>();

        protected SchemaRegistry()
        {
            OperationRegistry = new Dictionary<Type, FilterOperation>
            {
                { typeof(string), FilterOperation.EQUALS | FilterOperation.CONTAINS },
                { typeof(DateTime), FilterOperation.EQUALS | FilterOperation.LESSTHAN | FilterOperation.GreaterThan },
                { typeof(double), FilterOperation.EQUALS | FilterOperation.LESSTHAN | FilterOperation.GreaterThan },
                { typeof(double?), FilterOperation.EQUALS | FilterOperation.LESSTHAN | FilterOperation.GreaterThan },
                { typeof(int), FilterOperation.EQUALS | FilterOperation.LESSTHAN | FilterOperation.GreaterThan },
                { typeof(int?), FilterOperation.EQUALS | FilterOperation.LESSTHAN | FilterOperation.GreaterThan },
                { typeof(short), FilterOperation.EQUALS | FilterOperation.LESSTHAN | FilterOperation.GreaterThan },
                { typeof(short?), FilterOperation.EQUALS | FilterOperation.LESSTHAN | FilterOperation.GreaterThan },
                { typeof(bool), FilterOperation.EQUALS },
                { typeof(bool?), FilterOperation.EQUALS }
            };
        }

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

        public virtual bool TryGetEnumValue(string fieldName, object? value, out Enum result)
        {
            result = MockEnum.NULL;
            if (string.IsNullOrEmpty(fieldName) || !EnumRegistry.TryGetValue(fieldName, out var func))
                return false;

            if (value == null)
                return false;

            result = func.Invoke(value.ToString());
            return true;
        }

        public virtual bool TryGetEnumType(string fieldName, out Type result)
        {
            result = typeof(MockType);
            if (string.IsNullOrEmpty(fieldName) || !EnumTypeRegistry.TryGetValue(fieldName, out var type))
            {
                return false;
            }

            result = type;
            return true;
        }
    }

    public class MockType() { }
    public enum MockEnum { NULL }
}
