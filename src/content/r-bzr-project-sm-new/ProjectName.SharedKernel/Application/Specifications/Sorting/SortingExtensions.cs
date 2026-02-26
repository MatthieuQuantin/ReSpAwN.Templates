using Ardalis.Specification;
using System.Linq.Expressions;

namespace ProjectName.SharedKernel.Application.Specifications.Sorting;

public static class SortingExtensions
{
    public static ISpecificationBuilder<T> ApplySorting<T>(this ISpecificationBuilder<T> specBuilder, SortingFilter? sort, IEnumerable<string> allowedFields)
        where T : class
    {
        if (sort is null)
            return specBuilder;

        if (!allowedFields.Contains(sort.Field, StringComparer.OrdinalIgnoreCase))
            return specBuilder;

        var param = Expression.Parameter(typeof(T), "e");
        var member = Expression.PropertyOrField(param, sort.Field);
        var body = Expression.Convert(member, typeof(object));
        var keySelector = Expression.Lambda<Func<T, object>>(body, param);

        return sort.Direction == SortDirection.Asc
            ? specBuilder.OrderBy(keySelector)
            : specBuilder.OrderByDescending(keySelector);
    }
}
