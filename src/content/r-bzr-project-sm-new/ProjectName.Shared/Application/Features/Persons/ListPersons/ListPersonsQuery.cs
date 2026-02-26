using ProjectName.SharedKernel.Application;

namespace ProjectName.Application.Features.Persons.ListPersons;

public sealed record ListPersonsQuery() : IQuery<Result<List<PersonResult>>>;