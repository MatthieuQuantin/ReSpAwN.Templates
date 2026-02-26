using ProjectName.Domain.PersonAggregate.Contacts;

namespace ProjectName.Application.Features.Persons.CreateContact;

public sealed record ContactResult(ContactId Id, string Email);