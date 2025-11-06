namespace ModuleName.Application.Features.Persons.GetPersonByIdWithContacts;

public sealed record GetPersonByIdWithContactsQuery(Guid Id) : IQuery<Result<PersonResult>>;