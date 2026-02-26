using ProjectName.Application.Persistence.Repositories;
using ProjectName.Domain.PersonAggregate;
using ProjectName.SharedKernel.Application;

namespace ProjectName.Application.Features.Persons.CreateContact;

internal sealed class CreateContactHandler(IProjectNameRepository<Person> repository, IValidator<CreateContactCommand> validator, ILogger<CreateContactHandler> logger)
    : ICommandHandler<CreateContactCommand, Result<ContactResult>>
{
    public async Task<Result<ContactResult>> Handle(CreateContactCommand request, CancellationToken cancellationToken)
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

            var personAddContactResult = person.AddContact(request.Email);
            if (personAddContactResult.IsInvalid())
                return Result.Invalid(personAddContactResult.ValidationErrors);

            await repository.UpdateAsync(person, cancellationToken);

            var contact = personAddContactResult.Value;

            return new ContactResult(contact.Id, contact.Email.Value);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Une erreur est survenue lors de l'ajout du contact {Email} pour la personne avec l'Id {PersonId}", request.Email, request.PersonId);
            return Result.Error($"Une erreur est survenue lors de l'ajout du contact {request.Email} pour la personne avec l'Id {request.PersonId}");
        }
    }
}