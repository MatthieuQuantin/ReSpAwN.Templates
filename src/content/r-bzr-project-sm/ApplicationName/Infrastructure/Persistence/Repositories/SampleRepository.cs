using ApplicationName.Application.Persistence.Repositories;
using ApplicationName.Domain.SampleAggregate;
using ApplicationName.Infrastructure.Persistence.Repositories.Base;

namespace ApplicationName.Infrastructure.Persistence.Repositories;

internal sealed class SampleRepository(ApplicationNameDbContext dbContext) : EfRepository<Sample>(dbContext), ISampleRepository
{
    /// <inheritdoc />
    public Task<List<Sample>> GetSamplesWithIdStartWith(string start, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}