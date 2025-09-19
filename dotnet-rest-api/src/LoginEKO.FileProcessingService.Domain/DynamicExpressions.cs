using LoginEKO.FileProcessingService.Domain.Exceptions;
using LoginEKO.FileProcessingService.Domain.Models.Enums;
using LoginEKO.FileProcessingService.Domain.Utils;
using System.Linq.Expressions;
using System.Reflection;


namespace LoginEKO.FileProcessingService.Domain
{
    public class DynamicExpressions
    {
        private static readonly Type _stringType = typeof(string);
        private static readonly MethodInfo? _toStringMethod = typeof(object).GetMethod("ToString");
        private static readonly MethodInfo? _stringContainsMethod = typeof(string).GetMethod("Contains", new Type[] { typeof(string) });

        public static Expression GetFilter(ParameterExpression param, string property, FilterOperation op, object? value, Type? valueType)
        {
            object? filterValue = null;
            if (valueType != null && valueType.IsEnum)
            {
                if (value == null || !Enum.TryParse(valueType, value.ToString(), true, out var enumValue) || !Enum.IsDefined(valueType, enumValue))
                {
                    throw new DataConversionException($"Unable to convert filter value type to enum type");
                }

                filterValue = enumValue;
            }
            // Handle non-nullable values f all types
            else if (value != null)
            {

                try
                {
                    filterValue = TypeValidator.IsNullableType(valueType)
                        ? TypeValidator.ChangeTypeNullable(value.ToString(), valueType) : Convert.ChangeType(value.ToString(), valueType);
                }
                catch (Exception ex)
                {
                    throw new DataConversionException($"Unable to convert filter value type to '{valueType}'");
                }
            }

            value = filterValue;

            var constant = Expression.Constant(value, valueType);
            var prop = Expression.Property(param, property);
            return CreateFilter(prop, op, constant);
        }

        private static Expression CreateFilter(MemberExpression prop, FilterOperation op, ConstantExpression constant)
        {
            return op switch
            {
                FilterOperation.EQUALS => RobustEquals(prop, constant),
                FilterOperation.GREATERTHAN => Expression.GreaterThan(prop, constant),
                FilterOperation.LESSTHAN => Expression.LessThan(prop, constant),
                FilterOperation.CONTAINS => GetContainsMethodCallExpression(prop, constant),
                _ => throw new NotImplementedException()
            };
        }

        private static Expression RobustEquals(MemberExpression prop, ConstantExpression constant)
        {
            if (prop.Type == typeof(bool) && bool.TryParse(constant.Value.ToString(), out var val))
            {
                return Expression.Equal(prop, Expression.Constant(val));
            }
            return Expression.Equal(prop, constant);
        }

        private static Expression GetContainsMethodCallExpression(MemberExpression prop, ConstantExpression constant)
        {
            if (prop.Type == _stringType)
                return Expression.Call(prop, _stringContainsMethod, PrepareConstant(constant));

            throw new NotImplementedException($"{prop.Type} contains is not implemented.");
        }

        private static Expression PrepareConstant(ConstantExpression constant)
        {
            if (constant.Type == _stringType)
                return constant;

            var convertedExpr = Expression.Convert(constant, typeof(object));
            return Expression.Call(convertedExpr, _toStringMethod);
        }
    }
}
