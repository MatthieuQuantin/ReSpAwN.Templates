using ProjectName.SharedKernel.Application.Persistence;
using ProjectName.Application.Persistence.Repositories;
using ProjectName.Domain.PersonAggregate;

namespace ProjectName.Application.Features.Persons.CreatePerson;

internal sealed class CreatePersonHandler(IUnitOfWork unitOfWork, IProjectNameRepository<Person> repository, IValidator<CreatePersonCommand> validator, ILogger<CreatePersonHandler> logger)
    : ICommandHandler<CreatePersonCommand, Result<PersonResult>>
{
    public async Task<Result<PersonResult>> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
    {
        IAppTransaction? transaction = null;

        try
        {
            if (validator.Validate(request) is { IsValid: false } validationResult)
                return Result.Invalid(validationResult.AsErrors());

            var personCreateResult = Person.Create(request.FirstName, request.LastName);
            if (personCreateResult.IsInvalid())
                return Result.Invalid(personCreateResult.ValidationErrors);

            var person = personCreateResult.Value;

            // Sample of transaction usage
            // This is useful when you need to perform multiple operations that should be atomic.
            transaction = await unitOfWork.BeginTransactionAsync(cancellationToken);

            await repository.AddAsync(person, cancellationToken);

            await transaction.CommitAsync(cancellationToken);

            return new PersonResult(
                person.Id,
                person.FirstName.Value,
                person.LastName.Value);
        }
        catch (Exception exception)
        {
            if (transaction is not null)
                await transaction.RollbackAsync(cancellationToken);

            logger.LogError(exception, "Une erreur est survenue lors de la création de la personne avec le prénom {FirstName} et le nom {LastName}", request.FirstName, request.LastName);
            return Result.Error($"Une erreur est survenue lors de la création de la personne avec le prénom {request.FirstName} et le nom {request.LastName}");
        }
    }
}