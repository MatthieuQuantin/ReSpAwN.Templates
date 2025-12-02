using ModuleName.Domain.PersonAggregate;

namespace ModuleName.Application.Features.Persons.CreateContact;

public sealed record ContactResult(ContactId Id, string Email);