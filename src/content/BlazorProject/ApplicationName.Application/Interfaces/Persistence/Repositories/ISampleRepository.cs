using ApplicationName.Domain.SampleAggregate;

namespace ApplicationName.Application.Interfaces.Persistence.Repositories;

public interface ISampleRepository : IApplicationNameRepository<Sample>, IApplicationNameReadRepository<Sample>
{
    /// <summary>
    /// Sample method to demonstrate a custom query.
    /// </summary>
    /// <param name="start"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<List<Sample>> GetSamplesWithIdStartWith(string start, CancellationToken cancellationToken = default);
}