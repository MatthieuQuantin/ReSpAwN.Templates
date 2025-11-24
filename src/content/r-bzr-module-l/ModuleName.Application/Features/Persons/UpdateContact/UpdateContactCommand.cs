namespace ModuleName.Application.Features.Persons.UpdateContact;

public sealed record UpdateContactCommand(Guid PersonId, Guid ContactId, string Email) : ICommand<Result>;