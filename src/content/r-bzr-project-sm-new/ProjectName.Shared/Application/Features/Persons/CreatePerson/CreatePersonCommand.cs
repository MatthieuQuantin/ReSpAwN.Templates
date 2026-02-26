using ProjectName.SharedKernel.Application;

namespace ProjectName.Application.Features.Persons.CreatePerson;

public sealed record CreatePersonCommand(string FirstName, string LastName) : ICommand<Result<PersonResult>>;