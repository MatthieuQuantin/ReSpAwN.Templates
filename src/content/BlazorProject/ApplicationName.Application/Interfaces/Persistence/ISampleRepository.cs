using ApplicationName.Domain.SampleAggregate;

namespace ApplicationName.Application.Interfaces.Persistence;

public interface ISampleRepository : IRepository<Sample>, IReadRepository<Sample>
{
    /// <summary>
    /// Sample method to demonstrate a custom query.
    /// </summary>
    /// <param name="start"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<List<Sample>> GetSamplesWithIdStartWith(string start, CancellationToken cancellationToken = default);
}