using ModuleName.Application.Interfaces.Persistence.Repositories;
using ModuleName.Domain.Commons;
using ModuleName.Domain.PersonAggregate;

namespace ModuleName.Application.Features.Persons.CreatePersonContact;

internal sealed class CreatePersonContactHandler(IModuleNameRepository<Person> repository, IValidator<CreatePersonContactCommand> validator, ILogger<CreatePersonContactHandler> logger)
    : ICommandHandler<CreatePersonContactCommand, Result<ContactResult>>
{
    private readonly IModuleNameRepository<Person> _repository = repository;
    private readonly IValidator<CreatePersonContactCommand> _validator = validator;
    private readonly ILogger<CreatePersonContactHandler> _logger = logger;

    public async Task<Result<ContactResult>> Handle(CreatePersonContactCommand request, CancellationToken cancellationToken)
    {
        try
        {
            if (_validator.Validate(request) is { IsValid: false } validationResult)
                return Result<ContactResult>.Invalid(validationResult.AsErrors());

            var person = await _repository.GetByIdAsync(PersonId.From(request.PersonId), cancellationToken);
            if (person is null)
                return Result<ContactResult>.NotFound($"La personne '{request.PersonId}' n'a pas été trouvée");

            var emailCreateResult = Email.Create(request.Email);
            if (emailCreateResult.IsInvalid())
                return Result<ContactResult>.Invalid(emailCreateResult.ValidationErrors);

            var personAddContactResult = person.AddContact(emailCreateResult.Value);
            if (personAddContactResult.IsInvalid())
                return Result<ContactResult>.Invalid(personAddContactResult.ValidationErrors);

            await _repository.UpdateAsync(person, cancellationToken);

            var contact = personAddContactResult.Value;

            return new ContactResult(contact.Id.Value, contact.Email.Value);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Une erreur est survenue lors de l'ajout du contact {Email} pour la personne avec l'Id {PersonId}", request.Email, request.PersonId);
            throw;
        }
    }
}