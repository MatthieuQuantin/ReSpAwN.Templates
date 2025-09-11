namespace ModuleName.Application.Features.Persons.UpdatePerson;

public sealed record UpdatePersonCommand(Guid Id, string FirstName, string LastName) : ICommand<Result>;