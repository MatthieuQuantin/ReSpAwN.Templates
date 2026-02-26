using Ardalis.Specification;
using System.Linq.Expressions;

namespace ProjectName.SharedKernel.Application.Specifications.Searching;

public static class WhereAnyExtensions
{
    public static ISpecificationBuilder<T> WhereAny<T>(this ISpecificationBuilder<T> specBuilder, IEnumerable<Expression<Func<T, bool>>> predicates)
    {
        var list = predicates.ToList();
        if (list.Count == 0)
            return specBuilder;

        var param = Expression.Parameter(typeof(T), "e");
        Expression? body = null;

        foreach (var p in list)
        {
            var replaced = new ReplaceParamVisitor(p.Parameters[0], param).Visit(p.Body)!;
            body = body is null ? replaced : Expression.OrElse(body, replaced);
        }

        return specBuilder.Where(Expression.Lambda<Func<T, bool>>(body!, param));
    }

    private sealed class ReplaceParamVisitor : ExpressionVisitor
    {
        private readonly ParameterExpression _from, _to;

        public ReplaceParamVisitor(ParameterExpression from, ParameterExpression to)
        {
            _from = from;
            _to = to;
        }

        protected override Expression VisitParameter(ParameterExpression node)
            => node == _from ? _to : base.VisitParameter(node);
    }
}
