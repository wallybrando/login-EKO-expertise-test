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
            var queryFilterStatements = new List<Expression<Func<T, bool>>>();

            foreach (var filter in filters)
            {
                if (TryCreateFilterExpression(filter, out var filterStatement))
                {
                    queryFilterStatements.Add(filterStatement!);
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

            object? value;
            if (filter.Value == null)
            {
                value = null;

            }
            else
            {
                try
                {
                    value = ConvertExtensions.IsNullableType(fieldType) ?
                        ConvertExtensions.ChangeTypeNullable(filter.Value.ToString(), fieldType) :
                        Convert.ChangeType(filter.Value.ToString(), fieldType);


                    //if (ConvertExtensions.IsNullableType(fieldType))
                    //{
                    //    value = ConvertExtensions.ChangeTypeNullable(filter.Value.ToString(), fieldType);
                    //}
                    //else
                    //{
                    //    value = Convert.ChangeType(filter.Value.ToString(), fieldType);
                    //}
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
                    when constant.Value != null || IsFilterNullable(fieldType) => Expression.Equal(property, constant),
                FilterOperation.EQUALS
                    when constant.Value == null || !IsFilterNullable(fieldType) => Expression.Equal(Expression.Constant(1), Expression.Constant(2)),

                FilterOperation.GreaterThan
                    when (FilterIsNumber(fieldType) || FilterIsDate(fieldType)) && (constant.Value != null || IsFilterNullable(fieldType)) => Expression.GreaterThan(property, constant),
                //FilterOperation.GreaterThan
                //    when (IsNumber(fieldType) || IsDate(fieldType)) && (constant.Value == null || !NullableTypes(fieldType)) => Expression.Equal(Expression.Constant(1), Expression.Constant(2)),

                FilterOperation.LESSTHAN
                    when (FilterIsNumber(fieldType) || FilterIsDate(fieldType)) && (constant.Value != null || IsFilterNullable(fieldType)) => Expression.LessThan(property, constant),
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

        private static bool FilterIsNumber(Type type)
        {
            return type == typeof(int) || type == typeof(double) || type == typeof(short) ||
                type == typeof(short?) || type == typeof(int?) || type == typeof(double?);
        }

        private static bool FilterIsDate(Type type)
        {
            return type == typeof(DateTime);
        }

        private static bool FilterValueIsEmptyString(Type type, ConstantExpression expression)
        {
            if (!FilterIsString(type, expression))
                return false;

            return (string)expression.Value! == string.Empty;
        }

        private static bool FilterIsString(Type type, ConstantExpression expression)
        {
            return type == typeof(string) && expression.Value is string;
        }

        private static bool IsFilterNullable(Type type)
        {
            return type == typeof(bool?) || type == typeof(short?) || type == typeof(int?) || type == typeof(double?);
        }
    }
}
