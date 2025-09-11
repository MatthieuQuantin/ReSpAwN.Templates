namespace ModuleName.Application.Features.Persons.CreatePerson;

public sealed record CreatePersonCommand(string FirstName, string LastName) : ICommand<Result<PersonResult>>;