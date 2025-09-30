using ApplicationName.SharedKernel.Application.Persistence;
using ModuleName.Application.Interfaces.Persistence.Repositories;
using ModuleName.Domain.PersonAggregate;

namespace ModuleName.Application.Features.Persons.CreatePerson;

internal sealed class CreatePersonHandler(IUnitOfWork unitOfWork, IModuleNameRepository<Person> repository, IValidator<CreatePersonCommand> validator, ILogger<CreatePersonHandler> logger)
    : ICommandHandler<CreatePersonCommand, Result<PersonResult>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IModuleNameRepository<Person> _repository = repository;
    private readonly IValidator<CreatePersonCommand> _validator = validator;
    private readonly ILogger<CreatePersonHandler> _logger = logger;

    public async Task<Result<PersonResult>> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
    {
        IAppTransaction? transaction = null;

        try
        {
            if (_validator.Validate(request) is { IsValid: false } validationResult)
                return Result<PersonResult>.Invalid(validationResult.AsErrors());

            var personFirstNameCreateResult = PersonFirstName.From(request.FirstName);
            if (personFirstNameCreateResult.IsInvalid())
                return Result.Invalid(personFirstNameCreateResult.ValidationErrors);

            var personLastNameCreateResult = PersonLastName.From(request.LastName);
            if (personLastNameCreateResult.IsInvalid())
                return Result.Invalid(personLastNameCreateResult.ValidationErrors);

            var personCreateResult = Person.Create(personFirstNameCreateResult.Value, personLastNameCreateResult.Value);
            if (personCreateResult.IsInvalid())
                return Result.Invalid(personCreateResult.ValidationErrors);

            var person = personCreateResult.Value;

            // Sample of transaction usage
            // This is useful when you need to perform multiple operations that should be atomic.
            transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);

            await _repository.AddAsync(person, cancellationToken);

            await transaction.CommitAsync(cancellationToken);

            return new PersonResult(
                person.Id.Value,
                person.FirstName.Value,
                person.LastName.Value);
        }
        catch (Exception exception)
        {
            if (transaction is not null)
                await transaction.RollbackAsync(cancellationToken);

            _logger.LogError(exception, "Une erreur est survenue lors de la création de la personne avec le prénom {FirstName} et le nom {LastName}", request.FirstName, request.LastName);
            throw;
        }
    }
}