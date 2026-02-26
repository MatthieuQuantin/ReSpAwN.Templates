using ProjectName.Domain.PersonAggregate;
using ProjectName.Domain.PersonAggregate.Contacts;
using ProjectName.SharedKernel.Application;

namespace ProjectName.Application.Features.Persons.UpdateContact;

public sealed record UpdateContactCommand(PersonId PersonId, ContactId ContactId, string Email) : ICommand<Result>;