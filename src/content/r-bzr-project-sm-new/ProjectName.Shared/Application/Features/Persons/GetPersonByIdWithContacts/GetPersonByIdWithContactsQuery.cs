using ProjectName.Domain.PersonAggregate;
using ProjectName.SharedKernel.Application;

namespace ProjectName.Application.Features.Persons.GetPersonByIdWithContacts;

public sealed record GetPersonByIdWithContactsQuery(PersonId PersonId) : IQuery<Result<PersonResult>>;