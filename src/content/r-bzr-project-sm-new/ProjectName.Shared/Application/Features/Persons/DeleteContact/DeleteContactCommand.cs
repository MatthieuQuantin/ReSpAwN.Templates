using ProjectName.Domain.PersonAggregate;
using ProjectName.Domain.PersonAggregate.Contacts;
using ProjectName.SharedKernel.Application;

namespace ProjectName.Application.Features.Persons.DeleteContact;

public sealed record DeleteContactCommand(PersonId PersonId, ContactId ContactId) : ICommand<Result>;