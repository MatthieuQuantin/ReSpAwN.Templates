namespace ModuleName.Application.Features.Persons.GetPersonById;

public sealed record GetPersonByIdQuery(Guid Id) : IQuery<Result<PersonResult>>;