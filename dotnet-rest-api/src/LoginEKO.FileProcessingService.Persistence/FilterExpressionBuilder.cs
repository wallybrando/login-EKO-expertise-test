using LoginEKO.FileProcessingService.Domain.Extensions;
using LoginEKO.FileProcessingService.Domain.Interfaces;
using LoginEKO.FileProcessingService.Domain.Models;
using LoginEKO.FileProcessingService.Domain.Models.Base;
using LoginEKO.FileProcessingService.Domain.Models.Enums;
using System.Linq.Expressions;

namespace LoginEKO.FileProcessingService.Persistence
{
    public class FilterExpressionBuilder<T> : IFilterExpressionBuilder<T>
    {
        private readonly SchemaRegistry _schemaRegistry;
        public FilterExpressionBuilder(SchemaRegistry schemaRegistry)
        {
            _schemaRegistry = schemaRegistry;
        }

        public IEnumerable<Expression<Func<T, bool>>> ApplyFilters(IEnumerable<Filter> filters)
        {
            var result = new List<Expression<Func<T, bool>>>();

            foreach (var filter in filters)
            {
                if (!_schemaRegistry.TryGetFieldType(filter.Field, out var fieldType))
                {
                    continue;
                }

                object? value;
                if (filter.Value == null)
                {
                    value = null;
                }
                else
                {
                    try
                    {
                        if (ConvertExtensions.IsNullableType(fieldType))
                        {
                            value = ConvertExtensions.ChangeTypeNullable(filter.Value.ToString(), fieldType);
                        }
                        else
                        {
                            value = Convert.ChangeType(filter.Value.ToString(), fieldType);
                        }
                    }
                    catch (Exception)
                    {
                        throw new ArgumentException("Invalid filter value");
                    }
                }

                if (!_schemaRegistry.TryGetOperation(fieldType, filter.Operation, out var operation) || (value == null && operation != FilterOperation.EQUALS))
                    throw new ArgumentException("Invalid filter operation");

                var parameter = Expression.Parameter(typeof(T), "x");
                var property = Expression.PropertyOrField(parameter, filter.Field);
                var constant = Expression.Constant(value, fieldType);


                Expression? comparison = operation switch
                {
                    FilterOperation.EQUALS 
                        when constant.Value != null || NullableTypes(fieldType) => Expression.Equal(property, constant),
                    FilterOperation.EQUALS
                        when constant.Value == null || !NullableTypes(fieldType) => Expression.Equal(Expression.Constant(1), Expression.Constant(2)),

                    FilterOperation.GreaterThan
                        when (IsNumber(fieldType) || IsDate(fieldType)) && (constant.Value != null || NullableTypes(fieldType)) => Expression.GreaterThan(property, constant),
                    //FilterOperation.GreaterThan
                    //    when (IsNumber(fieldType) || IsDate(fieldType)) && (constant.Value == null || !NullableTypes(fieldType)) => Expression.Equal(Expression.Constant(1), Expression.Constant(2)),

                    FilterOperation.LESSTHAN
                        when (IsNumber(fieldType) || IsDate(fieldType)) && (constant.Value != null || NullableTypes(fieldType)) => Expression.LessThan(property, constant),
                    //FilterOperation.LESSTHAN
                    //    when (IsNumber(fieldType) || IsDate(fieldType)) && (constant.Value == null || !NullableTypes(fieldType)) => Expression.Equal(Expression.Constant(1), Expression.Constant(2)),

                    FilterOperation.CONTAINS
                        when !IsEmptyString(fieldType, constant) && constant.Value != null => Expression.Call(property, nameof(string.Contains), null, constant),
                    FilterOperation.CONTAINS
                        when IsEmptyString(fieldType, constant) || constant.Value == null => Expression.Equal(property, constant),
                    _ => null
                };

                if (comparison != null)
                {
                    var lambda = Expression.Lambda<Func<T, bool>>(comparison, parameter);
                    result.Add(lambda);
                }
            }

            return result;
        }

        private static bool IsNumber(Type type)
        {
            return type == typeof(int) || type == typeof(double) || type == typeof(short) ||
                type == typeof(short?) || type == typeof(int?) || type == typeof(double?);
        }

        private static bool IsDate(Type type)
        {
            return type == typeof(DateTime);
        }

        private static bool IsEmptyString(Type type, ConstantExpression expression)
        {
            if (!IsString(type, expression))
                return false;

            return (string)expression.Value! == "";
        }

        private static bool IsString(Type type, ConstantExpression expression)
        {
            return type == typeof(string) && expression.Value is string;
        }

        private static bool NullableTypes(Type type)
        {
            return type == typeof(bool?) || type == typeof(short?) || type == typeof(int?) || type == typeof(double?);
        }
    }
}
