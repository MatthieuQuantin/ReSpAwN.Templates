namespace ProjectName.SharedKernel.Application.Specifications.Sorting;

public sealed record SortingFilter(string Field, SortDirection Direction = SortDirection.Asc);
