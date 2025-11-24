using ModuleName.Application.Persistence.Repositories;
using ModuleName.Domain.PersonAggregate;

namespace ModuleName.Application.Features.Persons.CreateContact;

internal sealed class CreateContactHandler(IModuleNameRepository<Person> repository, IValidator<CreateContactCommand> validator, ILogger<CreateContactHandler> logger)
    : ICommandHandler<CreateContactCommand, Result<ContactResult>>
{
    public async Task<Result<ContactResult>> Handle(CreateContactCommand request, CancellationToken cancellationToken)
    {
        try
        {
            if (validator.Validate(request) is { IsValid: false } validationResult)
                return Result<ContactResult>.Invalid(validationResult.AsErrors());

            var person = await repository.GetByIdAsync(PersonId.From(request.PersonId), cancellationToken);
            if (person is null)
            {
                logger.LogWarning("La personne '{PersonId}' n'a pas été trouvée", request.PersonId);
                return Result<ContactResult>.NotFound($"La personne '{request.PersonId}' n'a pas été trouvée");
            }

            var personAddContactResult = person.AddContact(request.Email);
            if (personAddContactResult.IsInvalid())
                return Result<ContactResult>.Invalid(personAddContactResult.ValidationErrors);

            await repository.UpdateAsync(person, cancellationToken);

            var contact = personAddContactResult.Value;

            return new ContactResult(contact.Id.Value, contact.Email.Value);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Une erreur est survenue lors de l'ajout du contact {Email} pour la personne avec l'Id {PersonId}", request.Email, request.PersonId);
            return Result<ContactResult>.Error($"Une erreur est survenue lors de l'ajout du contact {request.Email} pour la personne avec l'Id {request.PersonId}");
        }
    }
}