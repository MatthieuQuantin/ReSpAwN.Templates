using ModuleName.Application.Persistence.Repositories;
using ModuleName.Domain.PersonAggregate;

namespace ModuleName.Application.Features.Persons.UpdatePerson;

internal sealed class UpdatePersonHandler(IModuleNameRepository<Person> repository, IValidator<UpdatePersonCommand> validator, ILogger<UpdatePersonHandler> logger)
    : ICommandHandler<UpdatePersonCommand, Result>
{
    public async Task<Result> Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
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

            var personUpdateResult = person.Update(request.FirstName, request.LastName);
            if (personUpdateResult.IsInvalid())
                return Result.Invalid(personUpdateResult.ValidationErrors);

            await repository.UpdateAsync(person, cancellationToken);
            return Result.Success();
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Une erreur est survenue lors de la mise à jour de la personne avec l'Id {PersonId}", request.PersonId);
            return Result.Error($"Une erreur est survenue lors de la mise à jour de la personne avec l'Id {request.PersonId}");
        }
    }
}