namespace ModuleName.Application.Features.Persons.DeletePerson;

public sealed record DeletePersonCommand(Guid Id) : ICommand<Result>;