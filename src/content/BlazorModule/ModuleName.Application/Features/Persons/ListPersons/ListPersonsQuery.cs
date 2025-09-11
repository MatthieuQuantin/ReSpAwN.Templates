namespace ModuleName.Application.Features.Persons.ListPersons;

public sealed record ListPersonsQuery() : IQuery<Result<List<PersonResult>>>;