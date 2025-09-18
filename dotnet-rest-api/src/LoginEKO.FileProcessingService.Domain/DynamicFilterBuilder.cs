using LoginEKO.FileProcessingService.Domain.Models.Enums;
using System.Linq.Expressions;

namespace LoginEKO.FileProcessingService.Domain
{
    public class DynamicFilterBuilder<TEntity>
    {
        private Expression Expression { get; set; }
        private readonly ParameterExpression _param;

        public DynamicFilterBuilder() : this(Expression.Parameter(typeof(TEntity), "x")) { }

        DynamicFilterBuilder(ParameterExpression param)
        {
            _param = param;
        }

        public DynamicFilterBuilder<TEntity> And(string property, FilterOperation op, object value, Type valueType)
        {
            var newExpr = DynamicExpressions.GetFilter(_param, property, op, value, valueType);
            Expression = Expression == null ? newExpr : Expression.AndAlso(Expression, newExpr);
            return this;
        }

        public DynamicFilterBuilder<TEntity> And(Action<DynamicFilterBuilder<TEntity>> action)
        {
            var builder = new DynamicFilterBuilder<TEntity>(_param);
            action(builder);

            if (builder.Expression == null)
                throw new Exception("Empty builder");

            Expression = Expression == null ? builder.Expression : Expression.AndAlso(Expression, builder.Expression);
            return this;
        }

        public DynamicFilterBuilder<TEntity> Or(IEnumerable<(string property, FilterOperation op, object? value, Type? valueType)> items)
        {
            foreach (var i in items)
            {
                Or(i.property, i.op, i.value, i.valueType);
            }
            return this;
        }

        public DynamicFilterBuilder<TEntity> Or(string property, FilterOperation op, object? value, Type? valueType)
        {
            var newExpr = DynamicExpressions.GetFilter(_param, property, op, value, valueType);
            Expression = Expression == null ? newExpr : Expression.OrElse(Expression, newExpr);
            return this;
        }

        public DynamicFilterBuilder<TEntity> Or(Action<DynamicFilterBuilder<TEntity>> action)
        {
            var builder = new DynamicFilterBuilder<TEntity>(_param);
            action(builder);

            if (builder.Expression == null)
                throw new Exception("Empty builder");

            Expression = Expression == null ? builder.Expression : Expression.OrElse(Expression, builder.Expression);
            return this;
        }

        public Expression<Func<TEntity, bool>> Build()
        {
            return Expression.Lambda<Func<TEntity, bool>>(Expression, _param);
        }

        public Func<TEntity, bool> Compile() => Build().Compile();
    }
}
