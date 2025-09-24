using ModuleName.Domain.Contracts.PersonAggregate.Events;

namespace ModuleName.Domain.PersonAggregate;

public sealed class Person : EntityBase<PersonId>, IAggregateRoot
{
    public PersonFirstName FirstName { get; private set; }
    public PersonLastName LastName { get; private set; }

    private readonly List<Contact> _contacts = [];
    public IEnumerable<Contact> Contacts => _contacts.AsReadOnly();

#pragma warning disable CS8618 // Un champ non-nullable doit contenir une valeur autre que Null lors de la fermeture du constructeur. Envisagez d’ajouter le modificateur « required » ou de déclarer le champ comme pouvant accepter la valeur Null.
    private Person()
    {
        // Required by EF Core for deserialization
    }
#pragma warning restore CS8618 // Un champ non-nullable doit contenir une valeur autre que Null lors de la fermeture du constructeur. Envisagez d’ajouter le modificateur « required » ou de déclarer le champ comme pouvant accepter la valeur Null.

    private Person(PersonFirstName firstName, PersonLastName lastName)
    {
        Id = PersonId.From(Guid.NewGuid());
        FirstName = firstName;
        LastName = lastName;

        var @event = new PersonCreatedEvent(Id.Value);
        base.RegisterDomainEvent(@event);
    }

    public static Result<Person> Create(PersonFirstName firstName, PersonLastName lastName)
    {

        List<ValidationError> validationErrors = [];

        if (firstName is null)
            validationErrors.Add(new ValidationError(nameof(firstName), "Le prénom de la personne ne peut pas être null."));

        if (lastName is null)
            validationErrors.Add(new ValidationError(nameof(lastName), "Le nom de la personne ne peut pas être null."));

        if (validationErrors.Count != 0)
            return Result<Person>.Invalid(validationErrors);

        return Result.Created(new Person(firstName, lastName));
    }

    public Result Update(PersonFirstName newFirstName, PersonLastName newLastName)
    {
        List<ValidationError> validationErrors = [];

        if (newFirstName is null)
            validationErrors.Add(new ValidationError(nameof(newFirstName), "Le nouveau prénom ne peut pas être null."));

        if (newLastName is null)
            validationErrors.Add(new ValidationError(nameof(newLastName), "Le nouveau nom ne peut pas être null."));

        if (validationErrors.Count != 0)
            return Result.Invalid(validationErrors);

        if (FirstName.Equals(newFirstName) && LastName.Equals(newLastName))
            return Result.Success();

        var oldFirstName = FirstName;
        FirstName = newFirstName;

        var oldLastName = LastName;
        LastName = newLastName;

        var @event = new PersonUpdatedEvent(Id.Value, oldFirstName.Value, newFirstName.Value, oldLastName.Value, newLastName.Value);
        base.RegisterDomainEvent(@event);

        return Result.Success();
    }

    public Result<Contact> AddContact(Email email)
    {
        if (_contacts.Any(c => c.Email.Equals(email)))
            return Result<Contact>.Invalid(new ValidationError(nameof(email), "Cet email est déjà utilisé par un autre contact."));

        var contactCreateResult = Contact.Create(email);
        if (contactCreateResult.IsInvalid())
            return Result<Contact>.Invalid(contactCreateResult.ValidationErrors);

        var contact = contactCreateResult.Value;

        _contacts.Add(contact);

        var @event = new ContactAddedToPersonEvent(Id.Value, contact.Id.Value);
        base.RegisterDomainEvent(@event);

        return contact;
    }

    public Result UpdateContact(ContactId contactId, Email newEmail)
    {
        var contact = _contacts.FirstOrDefault(c => c.Id == contactId);

        if (contact is null)
            return Result.NotFound($"Le contact '{contactId.Value}' n'a pas été trouvée");

        if (newEmail is null)
            return Result.Invalid(new ValidationError(nameof(newEmail), "L'email du contact ne peut pas être null."));

        if (_contacts.Any(c => c.Email.Equals(newEmail) && !c.Id.Equals(contactId)))
            return Result.Invalid(new ValidationError(nameof(newEmail), "Cet email est déjà utilisé par un autre contact."));

        contact.Update(newEmail);

        return Result.Success();
    }

    public Result DeleteContact(ContactId contactId)
    {
        var contact = _contacts.FirstOrDefault(c => c.Id == contactId);

        if (contact is null)
            return Result.NotFound($"Le contact '{contactId.Value}' n'a pas été trouvé.");

        _contacts.Remove(contact);

        var @event = new ContactDeletedEvent(contactId.Value);
        base.RegisterDomainEvent(@event);

        return Result.Success();
    }
}