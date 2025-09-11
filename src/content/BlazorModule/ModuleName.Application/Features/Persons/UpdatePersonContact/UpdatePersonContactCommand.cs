namespace ModuleName.Application.Features.Persons.UpdatePersonContact;

public sealed record UpdatePersonContactCommand(Guid PersonId, Guid Id, string Email) : ICommand<Result>;