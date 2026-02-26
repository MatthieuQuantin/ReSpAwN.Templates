using Ardalis.Specification;
using System.Linq.Expressions;
using System.Reflection;

namespace ProjectName.SharedKernel.Application.Specifications.Searching;

public static class SearchingExtensions
{
    public static ISpecificationBuilder<T> ApplyFilters<T>(
        this ISpecificationBuilder<T> specBuilder,
        IEnumerable<SearchingFilter>? filters,
        IEnumerable<string> allowedFields,
        Func<SearchingFilter, IEnumerable<Expression<Func<T, bool>>>>? extraPredicates = null,
        bool caseInsensitive = true)
    {
        if (filters is null || !filters.Any() || filters.All(f => string.IsNullOrWhiteSpace(f.Value)))
            return specBuilder;

        foreach (var filter in filters)
        {
            var allowed = allowedFields.FirstOrDefault(allowedField => allowedField.Equals(filter.Field, StringComparison.OrdinalIgnoreCase));

            if (allowed == null)
                continue;

            List<Expression<Func<T, bool>>> predicates = [];

            if (!ExistMember<T>(allowed))
            {
                if (extraPredicates is not null)
                    predicates.AddRange(extraPredicates(filter));
            }
            else
                predicates.Add(BuildPredicate<T>(allowed, filter.Op, filter.Value, caseInsensitive));

            specBuilder.WhereAny(predicates);
        }

        return specBuilder;
    }

    private static bool ExistMember<T>(string fieldName)
    {
        var type = typeof(T);
        var param = Expression.Parameter(type, "e");

        return TryGetPropertyOrField(param, fieldName, out _);
    }

    static bool TryGetPropertyOrField(ParameterExpression param, string fieldName, out MemberExpression? memberExpression)
    {
        var propertyInfo = param.Type.GetProperty(fieldName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
        if (propertyInfo is not null)
        {
            memberExpression = Expression.Property(param, propertyInfo);
            return true;
        }

        var fieldInfo = param.Type.GetField(fieldName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);

        if (fieldInfo is not null)
        {
            memberExpression = Expression.Field(param, fieldInfo);
            return true;
        }

        memberExpression = null;
        return false;
    }

    private static Expression<Func<T, bool>> BuildPredicate<T>(string field, FilterOp op, string value, bool caseInsensitive)
    {
        var param = Expression.Parameter(typeof(T), "e");
        if (!TryGetPropertyOrField(param, field, out var member))
            throw new InvalidOperationException($"Le champ '{field}' n'existe pas sur le type '{typeof(T).Name}'");

        Expression left = member!;
        Expression right = Expression.Constant(value, typeof(string));

        if (caseInsensitive)
        {
            left = Expression.Call(left, nameof(string.ToLower), Type.EmptyTypes);
            right = Expression.Call(right, nameof(string.ToLower), Type.EmptyTypes);
        }

        Expression body = op switch
        {
            FilterOp.Contains => Expression.Call(left, nameof(string.Contains), Type.EmptyTypes, right),
            FilterOp.StartsWith => Expression.Call(left, nameof(string.StartsWith), Type.EmptyTypes, right),
            FilterOp.EndsWith => Expression.Call(left, nameof(string.EndsWith), Type.EmptyTypes, right),
            FilterOp.Equal => Expression.Equal(left, right),
            FilterOp.NotEqual => Expression.NotEqual(left, right),
            _ => Expression.Constant(true) // no-op pour op non géré ici
        };

        return Expression.Lambda<Func<T, bool>>(body, param);
    }
}
