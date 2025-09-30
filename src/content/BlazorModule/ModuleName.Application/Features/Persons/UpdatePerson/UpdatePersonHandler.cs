using ModuleName.Application.Interfaces.Persistence.Repositories;
using ModuleName.Domain.PersonAggregate;

namespace ModuleName.Application.Features.Persons.UpdatePerson;

internal sealed class UpdatePersonHandler(IModuleNameRepository<Person> repository, IValidator<UpdatePersonCommand> validator, ILogger<UpdatePersonHandler> logger)
    : ICommandHandler<UpdatePersonCommand, Result>
{
    private readonly IModuleNameRepository<Person> _repository = repository;
    private readonly IValidator<UpdatePersonCommand> _validator = validator;
    private readonly ILogger<UpdatePersonHandler> _logger = logger;

    public async Task<Result> Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
    {
        try
        {
            if (_validator.Validate(request) is { IsValid: false } validationResult)
                return Result.Invalid(validationResult.AsErrors());

            var person = await _repository.GetByIdAsync(PersonId.From(request.Id), cancellationToken);
            if (person is null)
                return Result.NotFound($"La personne '{request.Id}' n'a pas été trouvée");

            var personFirstNameResult = PersonFirstName.From(request.LastName);
            if (personFirstNameResult.IsInvalid())
                return Result.Invalid(personFirstNameResult.ValidationErrors);

            var personLastNameResult = PersonLastName.From(request.LastName);
            if (personLastNameResult.IsInvalid())
                return Result.Invalid(personLastNameResult.ValidationErrors);

            var personUpdateResult = person.Update(personFirstNameResult.Value, personLastNameResult.Value);
            if (personUpdateResult.IsInvalid())
                return Result.Invalid(personUpdateResult.ValidationErrors);

            await _repository.UpdateAsync(person, cancellationToken);
            return Result.Success();
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Une erreur est survenue lors de la mise à jour de la personne avec l'Id {Id}", request.Id);
            throw;
        }
    }
}