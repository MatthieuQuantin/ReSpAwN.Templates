using ProjectName.Application.Features.Persons.GetPersonByIdWithContacts.Specifications;
using ProjectName.Application.Persistence.Repositories;
using ProjectName.Domain.PersonAggregate;

namespace ProjectName.Application.Features.Persons.GetPersonByIdWithContacts;

internal sealed class GetPersonByIdWithContactsHandler(IProjectNameReadRepository<Person> repository, IValidator<GetPersonByIdWithContactsQuery> validator, ILogger<GetPersonByIdWithContactsHandler> logger)
    : IQueryHandler<GetPersonByIdWithContactsQuery, Result<PersonResult>>
{
    public async Task<Result<PersonResult>> Handle(GetPersonByIdWithContactsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            if (validator.Validate(request) is { IsValid: false } validationResult)
                return Result.Invalid(validationResult.AsErrors());

            var person = await repository.SingleOrDefaultAsync(new PersonByIdWithContactsSpecification(request.PersonId), cancellationToken);
            if (person is null)
            {
                logger.LogWarning("La personne '{PersonId}' n'a pas été trouvée", request.PersonId);
                return Result.NotFound($"La personne '{request.PersonId}' n'a pas été trouvée");
            }

            return person;
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Une erreur est survenue lors de la récupération de la personne avec l'Id {PersonId}", request.PersonId);
            return Result.Error($"Une erreur est survenue lors de la récupération de la personne avec l'Id {request.PersonId}");
        }
    }
}