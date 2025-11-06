using ModuleName.Application.Interfaces.Persistence.Repositories;
using ModuleName.Domain.PersonAggregate;

namespace ModuleName.Application.Features.Persons.UpdateContact;

internal sealed class UpdateContactHandler(IModuleNameRepository<Person> repository, IValidator<UpdateContactCommand> validator, ILogger<UpdateContactHandler> logger) : ICommandHandler<UpdateContactCommand, Result>
{
    public async Task<Result> Handle(UpdateContactCommand request, CancellationToken cancellationToken)
    {
        try
        {
            if (validator.Validate(request) is { IsValid: false } validationResult)
                return Result.Invalid(validationResult.AsErrors());

            var person = await repository.GetByIdAsync(PersonId.From(request.PersonId), cancellationToken);
            if (person is null)
                return Result.NotFound($"La personne '{request.PersonId}' n'a pas été trouvée");

            var personUpdateContactResult = person.UpdateContact(ContactId.From(request.ContactId), request.Email);
            if (personUpdateContactResult.IsNotFound())
                return Result.NotFound([.. personUpdateContactResult.Errors]);
            if (personUpdateContactResult.IsInvalid())
                return Result.Invalid(personUpdateContactResult.ValidationErrors);

            await repository.UpdateAsync(person, cancellationToken);
            return Result.Success();
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Une erreur est survenue lors de la mise à jour du contact avec l'Id {ContactId} pour la personne avec l'Id {PersonId}", request.ContactId, request.PersonId);
            return Result.Error($"Une erreur est survenue lors de la mise à jour du contact avec l'Id {request.ContactId} pour la personne avec l'Id {request.PersonId}");
        }
    }
}