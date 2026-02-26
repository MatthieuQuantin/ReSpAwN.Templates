namespace ProjectName.SharedKernel.Application.Specifications.Paging;

public sealed record PagingFilter(int Page = default, int Size = default)
{
    public int Skip => Page <= 1 ? 0 : (Page - 1) * (Size <= 0 ? 0 : Size);
    public int Take => Size <= 0 ? 0 : Size;
}
