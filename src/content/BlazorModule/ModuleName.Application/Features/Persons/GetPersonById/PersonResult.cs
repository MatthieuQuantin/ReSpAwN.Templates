namespace ModuleName.Application.Features.Persons.GetPersonById;

public sealed record PersonResult(Guid Id, string FirstName, string LastName, IReadOnlyList<ContactResult> Contacts);