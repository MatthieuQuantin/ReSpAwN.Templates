using ProjectName.Application.Persistence.Repositories;
using ProjectName.Domain.PersonAggregate;

namespace ProjectName.Application.Features.Persons.UpdateContact;

internal sealed class UpdateContactHandler(IProjectNameRepository<Person> repository, IValidator<UpdateContactCommand> validator, ILogger<UpdateContactHandler> logger) : ICommandHandler<UpdateContactCommand, Result>
{
    public async Task<Result> Handle(UpdateContactCommand request, CancellationToken cancellationToken)
    {
        try
        {
            if (validator.Validate(request) is { IsValid: false } validationResult)
                return Result.Invalid(validationResult.AsErrors());

            var person = await repository.GetByIdAsync(request.PersonId, cancellationToken);
            if (person is null)
            {
                logger.LogWarning("La personne avec l'Id {PersonId} n'a pas été trouvée lors de la tentative de mise à jour du contact avec l'Id {ContactId}", request.PersonId, request.ContactId);
                return Result.NotFound($"La personne avec l'Id {request.PersonId} n'a pas été trouvée lors de la tentative de mise à jour du contact avec l'Id {request.ContactId}");
            }

            var updateContactResult = person.UpdateContact(request.ContactId, request.Email);
            if (updateContactResult.IsNotFound())
                return Result.NotFound([.. updateContactResult.Errors]);
            if (updateContactResult.IsInvalid())
                return Result.Invalid(updateContactResult.ValidationErrors);

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