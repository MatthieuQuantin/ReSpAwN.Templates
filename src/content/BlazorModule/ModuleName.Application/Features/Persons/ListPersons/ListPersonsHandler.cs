using Microsoft.Extensions.Logging;
using ModuleName.Domain.PersonAggregate;

namespace ModuleName.Application.Features.Persons.ListPersons;

internal sealed class ListPersonsHandler(IReadRepository<Person> repository, IValidator<ListPersonsQuery> validator, ILogger<ListPersonsHandler> logger) : IQueryHandler<ListPersonsQuery, Result<List<PersonResult>>>
{
    private readonly IReadRepository<Person> _repository = repository;
    private readonly IValidator<ListPersonsQuery> _validator = validator;
    private readonly ILogger<ListPersonsHandler> _logger = logger;

    public async Task<Result<List<PersonResult>>> Handle(ListPersonsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            if (_validator.Validate(request) is { IsValid: false } validationResult)
                return Result<List<PersonResult>>.Invalid(validationResult.AsErrors());

            var persons = await _repository.ListAsync(cancellationToken);

            return persons.Select(p => new PersonResult(p.Id.Value, p.FirstName.Value, p.LastName.Value)).ToList();
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Une erreur est survenue lors de la récupération de la liste des personnes.");
            throw;
        }
    }
}