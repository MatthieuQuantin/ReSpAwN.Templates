namespace ModuleName.Application.Features.Persons.CreatePersonContact;

public sealed record CreatePersonContactCommand(Guid PersonId, string Email) : ICommand<Result<ContactResult>>;