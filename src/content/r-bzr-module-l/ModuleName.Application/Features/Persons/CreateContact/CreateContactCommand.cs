namespace ModuleName.Application.Features.Persons.CreateContact;

public sealed record CreateContactCommand(Guid PersonId, string Email) : ICommand<Result<ContactResult>>;