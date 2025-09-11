namespace ModuleName.Application.Features.Persons.DeletePersonContact;

public sealed record DeletePersonContactCommand(Guid PersonId, Guid Id) : ICommand<Result>;