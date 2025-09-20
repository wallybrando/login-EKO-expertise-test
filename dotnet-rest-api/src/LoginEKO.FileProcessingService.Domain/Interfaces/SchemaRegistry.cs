using LoginEKO.FileProcessingService.Domain.Models.Entities;
using LoginEKO.FileProcessingService.Domain.Models.Enums;

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
            FieldRegistry = new Dictionary<string, Type>()
            {
                {  nameof(TractorTelemetry.SerialNumber).ToLower(), typeof(string) },
                {  nameof(TractorTelemetry.Date).ToLower(), typeof(DateTime) },
                {  nameof(TractorTelemetry.GPSLongitude).ToLower(), typeof(double) },
                {  nameof(TractorTelemetry.GPSLatitude).ToLower(), typeof(double) },
                {  nameof(TractorTelemetry.TotalWorkingHours).ToLower(), typeof(double) },
                {  nameof(TractorTelemetry.EngineSpeedInRpm).ToLower(), typeof(int) }
            };

            OperationRegistry = new Dictionary<Type, FilterOperation>
            {
                { typeof(string), FilterOperation.EQUALS | FilterOperation.CONTAINS },
                { typeof(DateTime), FilterOperation.EQUALS | FilterOperation.LESSTHAN | FilterOperation.GREATERTHAN },
                { typeof(double), FilterOperation.EQUALS | FilterOperation.LESSTHAN | FilterOperation.GREATERTHAN },
                { typeof(double?), FilterOperation.EQUALS | FilterOperation.LESSTHAN | FilterOperation.GREATERTHAN },
                { typeof(int), FilterOperation.EQUALS | FilterOperation.LESSTHAN | FilterOperation.GREATERTHAN },
                { typeof(int?), FilterOperation.EQUALS | FilterOperation.LESSTHAN | FilterOperation.GREATERTHAN },
                { typeof(short), FilterOperation.EQUALS | FilterOperation.LESSTHAN | FilterOperation.GREATERTHAN },
                { typeof(short?), FilterOperation.EQUALS | FilterOperation.LESSTHAN | FilterOperation.GREATERTHAN },
                { typeof(bool), FilterOperation.EQUALS },
                { typeof(bool?), FilterOperation.EQUALS }
            };
        }

        public virtual bool FieldExists(string fieldName)
        {
            fieldName = fieldName.ToLower();
            if (string.IsNullOrEmpty(fieldName) || !FieldRegistry.ContainsKey(fieldName))
                return false;

            return true;
        }

        public virtual bool TryGetFieldType(string fieldName, out Type? type)
        {
            type = null;

            fieldName = fieldName.ToLower();
            if (!FieldExists(fieldName))
                return false;

            type = FieldRegistry[fieldName];
            return true;
        }

        public virtual bool TypeHasAllowedOperation(Type type, FilterOperation suppliedOperation)
        {
            if (suppliedOperation == FilterOperation.UNKNOWN)
                return false;

            if (!OperationRegistry.TryGetValue(type, out FilterOperation allowedOperations))
                return false;

            if (!allowedOperations.HasFlag(suppliedOperation))
            {
                return false;
            }

            return true;
        }

        public virtual bool TryGetEnumValue(string fieldName, object? value, out Enum result)
        {
            result = MockEnum.NULL;
            fieldName = fieldName.ToLower();
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
            fieldName = fieldName.ToLower();

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
