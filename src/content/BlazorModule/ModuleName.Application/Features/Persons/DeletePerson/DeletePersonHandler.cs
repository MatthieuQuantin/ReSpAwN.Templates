using MediatR;
using ModuleName.Application.Interfaces.Persistence.Repositories;
using ModuleName.Domain.Contracts.PersonAggregate.Events;
using ModuleName.Domain.PersonAggregate;

namespace ModuleName.Application.Features.Persons.DeletePerson;

internal sealed class DeletePersonHandler(IModuleNameRepository<Person> repository, IValidator<DeletePersonCommand> validator, IPublisher publisher, ILogger<DeletePersonHandler> logger)
    : ICommandHandler<DeletePersonCommand, Result>
{
    private readonly IModuleNameRepository<Person> _repository = repository;
    private readonly IValidator<DeletePersonCommand> _validator = validator;
    private readonly IPublisher _publisher = publisher;
    private readonly ILogger<DeletePersonHandler> _logger = logger;

    public async Task<Result> Handle(DeletePersonCommand request, CancellationToken cancellationToken)
    {
        try
        {
            if (_validator.Validate(request) is { IsValid: false } validationResult)
                return Result.Invalid(validationResult.AsErrors());

            var person = await _repository.GetByIdAsync(PersonId.From(request.Id), cancellationToken);
            if (person is null)
                return Result.NotFound($"La personne '{request.Id}' n'a pas été trouvée");

            await _repository.DeleteAsync(person, cancellationToken);

            var @event = new PersonDeletedEvent(person.Id.Value);
            await _publisher.Publish(@event, cancellationToken);

            return Result.Success();
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Une erreur est survenue lors de la suppression de la personne avec l'Id {Id}", request.Id);
            throw;
        }
    }
}