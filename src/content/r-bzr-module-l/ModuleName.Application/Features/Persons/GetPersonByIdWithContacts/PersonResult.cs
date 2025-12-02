namespace ModuleName.Application.Features.Persons.GetPersonByIdWithContacts;

public sealed record PersonResult(Guid Id, string FirstName, string LastName, IReadOnlyList<ContactResult> Contacts);