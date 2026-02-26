using MediatR;
using ProjectName.Application.Persistence.Repositories;
using ProjectName.Domain.PersonAggregate.Events;
using ProjectName.Domain.PersonAggregate;

namespace ProjectName.Application.Features.Persons.DeletePerson;

internal sealed class DeletePersonHandler(IProjectNameRepository<Person> repository, IValidator<DeletePersonCommand> validator, IPublisher publisher, ILogger<DeletePersonHandler> logger)
    : ICommandHandler<DeletePersonCommand, Result>
{
    public async Task<Result> Handle(DeletePersonCommand request, CancellationToken cancellationToken)
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

            await repository.DeleteAsync(person, cancellationToken);

            var @event = new PersonDeletedDomainEvent(person.Id);
            await publisher.Publish(@event, cancellationToken);

            return Result.Success();
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Une erreur est survenue lors de la suppression de la personne avec l'Id {PersonId}", request.PersonId);
            return Result.Error($"Une erreur est survenue lors de la suppression de la personne avec l'Id {request.PersonId}");
        }
    }
}