using LoginEKO.FileProcessingService.Domain.Extensions;
using LoginEKO.FileProcessingService.Domain.Models.Enums;
using LoginEKO.FileProcessingService.Domain.Utils;
using System.Collections;
using System.Linq.Expressions;
using System.Reflection;


namespace LoginEKO.FileProcessingService.Domain
{
    public class DynamicExpressions
    {
        private static readonly Type _stringType = typeof(string);
        private static readonly MethodInfo? _toStringMethod = typeof(object).GetMethod("ToString");
        private static readonly MethodInfo? _stringContainsMethod = typeof(string).GetMethod("Contains", new Type[] { typeof(string) });
        private static readonly MethodInfo _dictionaryContainsKeyMethod = typeof(Dictionary<string, string>)
            .GetMethods()
            .Where(x => string.Equals(x.Name, "ContainsKey", StringComparison.OrdinalIgnoreCase))
            .Single();
        private static readonly MethodInfo _dictionaryContainsValueMethod = typeof(Dictionary<string, string>)
            .GetMethods()
            .Where(x => string.Equals(x.Name, "ContainsValue", StringComparison.OrdinalIgnoreCase))
            .Single();
        private static readonly MethodInfo _enumerableContainsMethod = typeof(Enumerable)
            .GetMethods()
            .Where(x => string.Equals(x.Name, "Contains", StringComparison.OrdinalIgnoreCase))
            .Single(x => x.GetParameters().Length == 2).MakeGenericMethod(typeof(string));

        public static Expression GetFilter(ParameterExpression param, string property, FilterOperation op, object? value, Type? valueType)
        {
            object? tempValue = null;
            if (valueType != null && valueType.IsEnum)
            {
                if (value != null && Enum.TryParse(valueType, value.ToString(), true, out var enumValue) && Enum.IsDefined(valueType, enumValue))
                {
                    tempValue = enumValue;
                }
            }
            // Handle non-nullable values f all types
            else if (value != null)
            {
                try
                {
                    tempValue = TypeValidator.IsNullableType(valueType)
                        ? TypeValidator.ChangeTypeNullable(value.ToString(), valueType) : Convert.ChangeType(value.ToString(), valueType);
                }
                catch (Exception)
                {
                    throw;
                    //_logger.LogError("Invalid value for '{Field}' field", filter.Field);
                   // throw new FilterValidationException($"Invalid value for '{filter.Field}' field");
                }
            }

            value = tempValue;

            var constant = Expression.Constant(value, valueType);
            var prop = param.GetNestedProperty(property);
            return CreateFilter(prop, op, constant);
        }

        private static Expression CreateFilter(MemberExpression prop, FilterOperation op, ConstantExpression constant)
        {
            return op switch
            {
                FilterOperation.EQUALS => RobustEquals(prop, constant),
                FilterOperation.GreaterThan => Expression.GreaterThan(prop, constant),
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
            else if (prop.Type.GetInterfaces().Contains(typeof(IDictionary)))
                return Expression.Or(Expression.Call(prop, _dictionaryContainsKeyMethod, PrepareConstant(constant)), Expression.Call(prop, _dictionaryContainsValueMethod, PrepareConstant(constant)));
            else if (prop.Type.GetInterfaces().Contains(typeof(IEnumerable)))
                return Expression.Call(_enumerableContainsMethod, prop, PrepareConstant(constant));

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
