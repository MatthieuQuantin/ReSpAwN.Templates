using ProjectName.Application.Persistence.Repositories;
using ProjectName.Domain.PersonAggregate;
using ProjectName.SharedKernel.Application;

namespace ProjectName.Application.Features.Persons.DeleteContact;

internal sealed class DeleteContactHandler(IProjectNameRepository<Person> repository, IValidator<DeleteContactCommand> validator, ILogger<DeleteContactHandler> logger) : ICommandHandler<DeleteContactCommand, Result>
{
    public async Task<Result> Handle(DeleteContactCommand request, CancellationToken cancellationToken)
    {
        try
        {
            if (validator.Validate(request) is { IsValid: false } validationResult)
                return Result.Invalid(validationResult.AsErrors());

            var person = await repository.GetByIdAsync(request.PersonId, cancellationToken);
            if (person is null)
            {
                logger.LogWarning("La personne '{PersonId}' n'a pas été trouvée", request.PersonId);
                return Result.NotFound($"La personne '{request.PersonId}' n'a pas été trouvée");
            }

            var personDeleteContactResult = person.DeleteContact(request.ContactId);
            if (personDeleteContactResult.IsNotFound())
            {
                logger.LogWarning("Le contact '{ContactId}' n'a pas été trouvé.", request.ContactId);
                return Result.NotFound($"Le contact '{request.ContactId}' n'a pas été trouvé.");
            }

            await repository.UpdateAsync(person, cancellationToken);
            return Result.Success();
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Une erreur est survenue lors de la suppression du contact avec l'Id {Id} pour la personne avec l'Id {PersonId}", request.ContactId, request.PersonId);
            return Result.Error($"Une erreur est survenue lors de la suppression du contact avec l'Id {request.ContactId} pour la personne avec l'Id {request.PersonId}");
        }
    }
}