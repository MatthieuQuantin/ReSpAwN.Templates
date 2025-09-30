using ModuleName.Application.Interfaces.Persistence.Repositories;
using ModuleName.Domain.Commons;
using ModuleName.Domain.PersonAggregate;

namespace ModuleName.Application.Features.Persons.UpdatePersonContact;

internal sealed class UpdatePersonContactHandler(IModuleNameRepository<Person> repository, IValidator<UpdatePersonContactCommand> validator, ILogger<UpdatePersonContactHandler> logger) : ICommandHandler<UpdatePersonContactCommand, Result>
{
    private readonly IModuleNameRepository<Person> _repository = repository;
    private readonly IValidator<UpdatePersonContactCommand> _validator = validator;
    private readonly ILogger<UpdatePersonContactHandler> _logger = logger;

    public async Task<Result> Handle(UpdatePersonContactCommand request, CancellationToken cancellationToken)
    {
        try
        {
            if (_validator.Validate(request) is { IsValid: false } validationResult)
                return Result.Invalid(validationResult.AsErrors());

            var person = await _repository.GetByIdAsync(PersonId.From(request.PersonId), cancellationToken);
            if (person is null)
                return Result.NotFound($"La personne '{request.PersonId}' n'a pas été trouvée");

            var emailCreateResult = Email.From(request.Email);
            if (emailCreateResult.IsInvalid())
                return Result.Invalid(emailCreateResult.ValidationErrors);

            var personUpdateContactResult = person.UpdateContact(ContactId.From(request.Id), emailCreateResult.Value);
            if (personUpdateContactResult.IsNotFound())
                return Result.NotFound(personUpdateContactResult.Errors.ToArray());
            if (personUpdateContactResult.IsInvalid())
                return Result.Invalid(personUpdateContactResult.ValidationErrors);

            await _repository.UpdateAsync(person, cancellationToken);
            return Result.Success();
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Une erreur est survenue lors de la mise à jour du contact avec l'Id {Id} pour la personne avec l'Id {PersonId}", request.Id, request.PersonId);
            throw;
        }
    }
}