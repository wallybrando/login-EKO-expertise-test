using LoginEKO.FileProcessingService.Domain.Exceptions;
using LoginEKO.FileProcessingService.Domain.Extensions;
using LoginEKO.FileProcessingService.Domain.Interfaces;
using LoginEKO.FileProcessingService.Domain.Models;
using LoginEKO.FileProcessingService.Domain.Models.Enums;
using LoginEKO.FileProcessingService.Domain.Utils;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace LoginEKO.FileProcessingService.Persistence
{
    public class FilterExpressionBuilder<T> : IFilterExpressionBuilder<T>
    {
        private readonly SchemaRegistry<T> _schemaRegistry;
        private readonly ILogger<FilterExpressionBuilder<T>> _logger;

        public FilterExpressionBuilder(SchemaRegistry<T> schemaRegistry, ILogger<FilterExpressionBuilder<T>> logger)
        {
            _schemaRegistry = schemaRegistry;
            _logger = logger;
        }

        public IEnumerable<Expression<Func<T, bool>>> ApplyFilters(IEnumerable<Filter> filters)
        {
            var queryFilterStatements = new List<Expression<Func<T, bool>>>();

            foreach (var filter in filters)
            {
                if (TryCreateFilterExpression(filter, out var filterStatement))
                {
                    queryFilterStatements.Add(filterStatement!);
                }
                else
                {
                    _logger.LogWarning("Filter rejected: Field={Field} Operation={Operation} Value={Value}", filter.Field, filter.Operation, filter.Value);
                }
            }

            return queryFilterStatements;
        }


        private bool TryCreateFilterExpression(Filter filter, out Expression<Func<T, bool>>? filterStatement)
        {
            filterStatement = null;
            if (!_schemaRegistry.TryGetFieldType(filter.Field, out var fieldType))
            {
                return false;
            }

            object? value = null;
            if (filter.Value != null)
            {
                if (fieldType == typeof(Enum))
                {
                    if (!_schemaRegistry.TryGetEnumType(filter.Field, out fieldType))
                    {
                        _logger.LogError("Provided enum type does not exists");
                        throw new FilterValidationException("Provided enum type does not exists");
                        //return false;
                    }

                    if (!_schemaRegistry.TryGetEnumValue(filter.Field, filter.Value, out var val))
                    {
                        _logger.LogError($"Unknown value '{filter.Value}' for enum type '{fieldType}'");
                        throw new FilterValidationException("Unknown enum value");
                        //return false;
                    }

                    value = val;
                }
                else
                {
                    try
                    {
                        value = TypeValidator.IsNullableType(fieldType) ? TypeValidator.ChangeTypeNullable(filter.Value.ToString(), fieldType) :
                                Convert.ChangeType(filter.Value.ToString(), fieldType);
                    }
                    catch (Exception)
                    {
                        _logger.LogError("Invalid value for field {Field}", filter.Field);
                        throw new FilterValidationException($"Invalid value for field {filter.Field}");
                        //return false;
                    }
                }
            }

            if (!_schemaRegistry.TryGetOperation(fieldType, filter.Operation, out var operation))
            {
                _logger.LogError("Applied operation is not supported");
                throw new FilterValidationException($"Operation '{filter.Operation}' is not supported for field '{filter.Field}'");
                //return false;
            }

            if (value == null && operation != FilterOperation.EQUALS)
            {
                _logger.LogError("Applied operation cannot be applied on NULL value");
                throw new FilterValidationException($"Operation '{operation.GetDescription()}' cannot be used with NULL");
               // return false;
            }

            if (value == null && !TypeValidator.IsNullableType(fieldType))
            {
                _logger.LogError("NULL check cannot be applied to non-nullable field");
                throw new FilterValidationException("NULL check cannot be applied to non-nullable field");
                //return false;
            }

            var parameter = Expression.Parameter(typeof(T), "x");
            var property = Expression.PropertyOrField(parameter, filter.Field);
            var constant = Expression.Constant(value, fieldType);

            Expression? comparison = operation switch
            {
                FilterOperation.EQUALS
                    when constant.Value != null || IsFilterNullable(fieldType) => Expression.Equal(property, constant),
                FilterOperation.EQUALS
                    when constant.Value == null || !IsFilterNullable(fieldType) => Expression.Equal(Expression.Constant(1), Expression.Constant(2)),

                FilterOperation.GreaterThan
                    when (TypeValidator.FilterIsNumber(fieldType) || TypeValidator.FilterIsDate(fieldType)) && (constant.Value != null || IsFilterNullable(fieldType)) => Expression.GreaterThan(property, constant),
                //FilterOperation.GreaterThan
                //    when (IsNumber(fieldType) || IsDate(fieldType)) && (constant.Value == null || !NullableTypes(fieldType)) => Expression.Equal(Expression.Constant(1), Expression.Constant(2)),

                FilterOperation.LESSTHAN
                    when (TypeValidator.FilterIsNumber(fieldType) || TypeValidator.FilterIsDate(fieldType)) && (constant.Value != null || IsFilterNullable(fieldType)) => Expression.LessThan(property, constant),
                //FilterOperation.LESSTHAN
                //    when (IsNumber(fieldType) || IsDate(fieldType)) && (constant.Value == null || !NullableTypes(fieldType)) => Expression.Equal(Expression.Constant(1), Expression.Constant(2)),

                FilterOperation.CONTAINS
                    when !FilterValueIsEmptyString(fieldType, constant) && constant.Value != null => Expression.Call(property, nameof(string.Contains), null, constant),
                FilterOperation.CONTAINS
                    when FilterValueIsEmptyString(fieldType, constant) || constant.Value == null => Expression.Equal(property, constant),
                _ => null
            };

            if (comparison != null)
            {
                filterStatement = Expression.Lambda<Func<T, bool>>(comparison, parameter);
                return true;
            }

            return false;
        }

        private static bool FilterValueIsEmptyString(Type type, ConstantExpression expression)
        {
            if (!TypeValidator.FilterIsString(type, expression))
                return false;

            if (expression.Value is not string)
                return false;

            return (string)expression.Value! == string.Empty;
        }

        private static bool IsFilterNullable(Type type)
        {
            return type == typeof(bool?) || type == typeof(short?) || type == typeof(int?) || type == typeof(double?);
        }
    }
}
