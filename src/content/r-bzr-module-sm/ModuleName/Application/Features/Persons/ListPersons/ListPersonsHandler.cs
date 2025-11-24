using ModuleName.Application.Persistence.Repositories;
using ModuleName.Domain.PersonAggregate;

namespace ModuleName.Application.Features.Persons.ListPersons;

internal sealed class ListPersonsHandler(IModuleNameReadRepository<Person> repository, IValidator<ListPersonsQuery> validator, ILogger<ListPersonsHandler> logger) : IQueryHandler<ListPersonsQuery, Result<List<PersonResult>>>
{
    public async Task<Result<List<PersonResult>>> Handle(ListPersonsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            if (validator.Validate(request) is { IsValid: false } validationResult)
                return Result.Invalid(validationResult.AsErrors());

            var persons = await repository.ListAsync(cancellationToken);

            return persons.Select(p => new PersonResult(p.Id, p.FirstName.Value, p.LastName.Value)).ToList();
        }
        catch (Exception exception)
        {
            const string message = "Une erreur est survenue lors de la récupération de la liste des personnes.";
            logger.LogError(exception, message);
            return Result.Error(message);
        }
    }
}