using Microsoft.Extensions.Logging;
using ModuleName.Domain.PersonAggregate;

namespace ModuleName.Application.Features.Persons.DeletePersonContact;

internal sealed class DeletePersonContactHandler(IRepository<Person> repository, IValidator<DeletePersonContactCommand> validator, ILogger<DeletePersonContactHandler> logger) : ICommandHandler<DeletePersonContactCommand, Result>
{
    private readonly IRepository<Person> _repository = repository;
    private readonly IValidator<DeletePersonContactCommand> _validator = validator;
    private readonly ILogger<DeletePersonContactHandler> _logger = logger;

    public async Task<Result> Handle(DeletePersonContactCommand request, CancellationToken cancellationToken)
    {
        try
        {
            if (_validator.Validate(request) is { IsValid: false } validationResult)
                return Result.Invalid(validationResult.AsErrors());

            var person = await _repository.GetByIdAsync(PersonId.From(request.PersonId), cancellationToken);
            if (person is null)
                return Result.NotFound($"La personne '{request.PersonId}' n'a pas été trouvée");

            var personDeleteContactResult = person.DeleteContact(ContactId.From(request.Id));
            if (personDeleteContactResult.IsNotFound())
                return Result.NotFound(personDeleteContactResult.Errors.ToArray());

            await _repository.UpdateAsync(person, cancellationToken);
            return Result.Success();
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Une erreur est survenue lors de la suppression du contact avec l'Id {Id} pour la personne avec l'Id {PersonId}", request.Id, request.PersonId);
            throw;
        }
    }
}