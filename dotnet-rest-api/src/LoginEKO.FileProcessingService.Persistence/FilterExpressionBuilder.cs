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


        private bool TryCreateFilterExpression(Filter filter, out Expression<Func<T, bool>>? filterExpression)
        {
            /*** Reject non-applicable filter **************************/
            filterExpression = null;
            if (!_schemaRegistry.TryGetFieldType(filter.Field, out var fieldType))
            {

                return false;
            }

            /***   *************/
            object? value = null;
            // Handle Enum filters
            if (fieldType == typeof(Enum))
            {
                if (!_schemaRegistry.TryGetEnumType(filter.Field, out fieldType))
                {
                    _logger.LogError("Provided enum type does not exists");
                    throw new FilterValidationException("Provided enum type does not exists");
                }

                if (!_schemaRegistry.TryGetEnumValue(filter.Field, filter.Value, out var enumValue))
                {
                    _logger.LogError($"Unknown value '{filter.Value}' for enum type '{fieldType}'");
                    throw new FilterValidationException("Unknown enum value");
                }

                if (filter.Value != null)
                {
                    value = enumValue;
                }
            }
            // Handle non-nullable values for all types
            else if (filter.Value != null)
            {
                try
                {
                    value = TypeValidator.IsNullableType(fieldType)
                        ? TypeValidator.ChangeTypeNullable(filter.Value.ToString(), fieldType) : Convert.ChangeType(filter.Value.ToString(), fieldType);
                }
                catch (Exception)
                {
                    _logger.LogError("Invalid value for field {Field}", filter.Field);
                    throw new FilterValidationException($"Invalid value for field {filter.Field}");
                }
            }

            //if (filter.Value != null)
            //{
            //    if (fieldType == typeof(Enum))
            //    {
            //        if (!_schemaRegistry.TryGetEnumType(filter.Field, out fieldType))
            //        {
            //            _logger.LogError("Provided enum type does not exists");
            //            throw new FilterValidationException("Provided enum type does not exists");
            //            //return false;
            //        }

            //        if (!_schemaRegistry.TryGetEnumValue(filter.Field, filter.Value, out var enumValue))
            //        {
            //            _logger.LogError($"Unknown value '{filter.Value}' for enum type '{fieldType}'");
            //            throw new FilterValidationException("Unknown enum value");
            //            //return false;
            //        }

            //        value = enumValue;
            //    }
            //    else
            //    {
            //        try
            //        {
            //            value = TypeValidator.IsNullableType(fieldType) ? TypeValidator.ChangeTypeNullable(filter.Value.ToString(), fieldType) :
            //                    Convert.ChangeType(filter.Value.ToString(), fieldType);
            //        }
            //        catch (Exception)
            //        {
            //            _logger.LogError("Invalid value for field {Field}", filter.Field);
            //            throw new FilterValidationException($"Invalid value for field {filter.Field}");
            //            //return false;
            //        }
            //    }
            //}

            /*** Validate filter parameters ****************************/
            if (!_schemaRegistry.TryGetOperation(fieldType, filter.Operation, out var operation))
            {
                _logger.LogError("Applied operation is not supported");
                throw new FilterValidationException($"Operation '{filter.Operation}' is not supported for field '{filter.Field}'");
            }

            if (value == null && operation != FilterOperation.EQUALS)
            {
                _logger.LogError("Applied operation cannot be applied on NULL value");
                throw new FilterValidationException($"Operation '{operation.GetDescription()}' cannot be used with NULL");
            }

            if (value == null && !TypeValidator.IsNullableType(fieldType))
            {
                _logger.LogError("NULL check cannot be applied to non-nullable field");
                throw new FilterValidationException("NULL check cannot be applied to non-nullable field");
            }

            /*** Construct filter expression *****************************/
            var parameter = Expression.Parameter(typeof(T), "x");
            var property = Expression.PropertyOrField(parameter, filter.Field);
            var constant = Expression.Constant(value, fieldType);

            Expression? comparison = operation switch
            {
                FilterOperation.EQUALS when constant.Value != null || IsFilterNullable(fieldType)
                    => Expression.Equal(property, constant),
                FilterOperation.EQUALS when constant.Value == null || !IsFilterNullable(fieldType)
                    => Expression.Equal(Expression.Constant(1), Expression.Constant(2)),

                FilterOperation.GreaterThan when (TypeValidator.FilterIsNumber(fieldType) || TypeValidator.FilterIsDate(fieldType)) &&
                                                 (constant.Value != null || IsFilterNullable(fieldType))
                                                    => Expression.GreaterThan(property, constant),
                
                FilterOperation.LESSTHAN when (TypeValidator.FilterIsNumber(fieldType) || TypeValidator.FilterIsDate(fieldType)) &&
                                              (constant.Value != null || IsFilterNullable(fieldType))
                                                    => Expression.LessThan(property, constant),
                
                FilterOperation.CONTAINS when !FilterValueIsEmptyString(fieldType, constant) && constant.Value != null
                    => Expression.Call(property, nameof(string.Contains), null, constant),
                FilterOperation.CONTAINS when FilterValueIsEmptyString(fieldType, constant) || constant.Value == null
                    => Expression.Equal(property, constant),
                _ => null
            };

            if (comparison != null)
            {
                filterExpression = Expression.Lambda<Func<T, bool>>(comparison, parameter);
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
