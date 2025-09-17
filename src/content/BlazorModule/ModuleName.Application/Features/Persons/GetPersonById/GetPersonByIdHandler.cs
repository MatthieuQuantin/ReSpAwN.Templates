using ModuleName.Application.Interfaces.Persistence.Repositories;
using ModuleName.Domain.PersonAggregate;

namespace ModuleName.Application.Features.Persons.GetPersonById;

internal sealed class GetPersonByIdHandler(IModuleNameReadRepository<Person> repository, IValidator<GetPersonByIdQuery> validator, ILogger<GetPersonByIdHandler> logger)
    : IQueryHandler<GetPersonByIdQuery, Result<PersonResult>>
{
    private readonly IModuleNameReadRepository<Person> _repository = repository;
    private readonly IValidator<GetPersonByIdQuery> _validator = validator;
    private readonly ILogger<GetPersonByIdHandler> _logger = logger;

    public async Task<Result<PersonResult>> Handle(GetPersonByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            if (_validator.Validate(request) is { IsValid: false } validationResult)
                return Result<PersonResult>.Invalid(validationResult.AsErrors());

            var person = await _repository.GetByIdAsync(PersonId.From(request.Id), cancellationToken);
            if (person is null)
                return Result.NotFound($"La personne '{request.Id}' n'a pas été trouvée");

            return new PersonResult(
                person.Id.Value,
                person.FirstName.Value,
                person.LastName.Value,
                [.. person.Contacts.Select(c => new ContactResult(c.Id.Value, c.Email.Value))]);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Une erreur est survenue lors de la récupération de la personne avec l'Id {Id}", request.Id);
            throw;
        }
    }
}