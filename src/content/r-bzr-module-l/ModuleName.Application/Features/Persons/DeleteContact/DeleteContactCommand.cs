namespace ModuleName.Application.Features.Persons.DeleteContact;

public sealed record DeleteContactCommand(Guid PersonId, Guid Id) : ICommand<Result>;