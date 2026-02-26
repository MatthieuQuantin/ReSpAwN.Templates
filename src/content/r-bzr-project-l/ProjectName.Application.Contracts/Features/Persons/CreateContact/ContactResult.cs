using ProjectName.Domain.PersonAggregate;

namespace ProjectName.Application.Features.Persons.CreateContact;

public sealed record ContactResult(ContactId Id, string Email);