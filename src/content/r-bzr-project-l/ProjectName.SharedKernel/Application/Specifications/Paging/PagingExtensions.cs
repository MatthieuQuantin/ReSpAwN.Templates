using Ardalis.Specification;

namespace ProjectName.SharedKernel.Application.Specifications.Paging;

public static class PagingExtensions
{
    public static ISpecificationBuilder<T> ApplyPaging<T>(this ISpecificationBuilder<T> specBuilder, PagingFilter? page)
        where T : class
    {
        if (page is null)
            return specBuilder;

        return specBuilder
            .Skip(page.Skip, page.Skip > 0)
            .Take(page.Take, page.Take > 0);
    }
}
