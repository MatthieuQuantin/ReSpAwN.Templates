using ProjectName.Domain.Commons;
using ProjectName.Domain.PersonAggregate.Events;

namespace ProjectName.Domain.PersonAggregate;

public sealed class Person : EntityBase<PersonId>, IAggregateRoot
{
    public PersonFirstName FirstName { get; private set; }
    public PersonLastName LastName { get; private set; }


    private readonly List<Contact> _contacts = [];
    public IEnumerable<Contact> Contacts => _contacts.AsReadOnly();

#pragma warning disable CS8618 // Un champ non-nullable doit contenir une valeur autre que Null lors de la fermeture du constructeur. Envisagez d’ajouter le modificateur « required » ou de déclarer le champ comme pouvant accepter la valeur Null.
    /// <summary>
    /// Constructeur privé pour EF Core
    /// </summary>
    private Person()
    {
        // Required by EF Core for deserialization
    }
#pragma warning restore CS8618 // Un champ non-nullable doit contenir une valeur autre que Null lors de la fermeture du constructeur. Envisagez d’ajouter le modificateur « required » ou de déclarer le champ comme pouvant accepter la valeur Null.

    /// <summary>
    /// Constructeur privé pour forcer l'utilisation de la méthode Create
    /// </summary>
    /// <param name="firstName"></param>
    /// <param name="lastName"></param>
    private Person(PersonFirstName firstName, PersonLastName lastName)
    {
        Id = PersonId.From(Guid.NewGuid());
        FirstName = firstName;
        LastName = lastName;

        var @event = new PersonCreatedDomainEvent(Id);
        base.RegisterDomainEvent(@event);
    }

    /// <summary>
    /// Permet la création d'une personne
    /// </summary>
    /// <remarks>
    /// La création déclenchera un événement PersonCreatedEvent
    /// </remarks>
    /// <param name="firstName"></param>
    /// <param name="lastName"></param>
    /// <returns></returns>
    public static Result<Person> Create(string firstName, string lastName)
    {
        List<ValidationError> validationErrors = [];

        var personFirstNameResult = PersonFirstName.From(firstName);
        if (personFirstNameResult.IsInvalid())
            validationErrors.AddRange(personFirstNameResult.ValidationErrors);

        var personLastNameResult = PersonLastName.From(lastName);
        if (personLastNameResult.IsInvalid())
            validationErrors.AddRange(personLastNameResult.ValidationErrors);

        if (validationErrors.Count != 0)
            return Result<Person>.Invalid(validationErrors);

        return Result.Created(new Person(personFirstNameResult.Value, personLastNameResult.Value));
    }

    /// <summary>
    /// Permet la mise à jour d'une personne
    /// </summary>
    /// <remarks>
    /// La mise à jour déclenchera un événement PersonUpdatedEvent
    /// </remarks>
    /// <param name="firstName"></param>
    /// <param name="lastName"></param>
    /// <returns></returns>
    public Result Update(string firstName, string lastName)
    {
        List<ValidationError> validationErrors = [];

        var personFirstNameResult = PersonFirstName.From(firstName);
        if (personFirstNameResult.IsInvalid())
            validationErrors.AddRange(personFirstNameResult.ValidationErrors);

        var personLastNameResult = PersonLastName.From(lastName);
        if (personLastNameResult.IsInvalid())
            validationErrors.AddRange(personLastNameResult.ValidationErrors);

        if (validationErrors.Count != 0)
            return Result.Invalid(validationErrors);

        if (FirstName.Equals(personFirstNameResult.Value) && LastName.Equals(personLastNameResult.Value))
            return Result.Success();

        var oldFirstName = FirstName;
        FirstName = personFirstNameResult.Value;

        var oldLastName = LastName;
        LastName = personLastNameResult.Value;

        var @event = new PersonUpdatedDomainEvent(Id);
        base.RegisterDomainEvent(@event);

        return Result.Success();
    }

    /// <summary>
    /// Ajoute un contact à la personne
    /// </summary>
    /// <remarks>
    /// L'ajout d'un contact déclenchera un événement ContactAddedToPersonEvent
    /// </remarks>
    /// <param name="email"></param>
    /// <returns></returns>
    public Result<Contact> AddContact(string email)
    {
        var contactResult = Contact.Create(email);
        if (contactResult.IsInvalid())
            return Result.Invalid(contactResult.ValidationErrors);
        
        var newContact = contactResult.Value;

        var existingContact = _contacts.FirstOrDefault(c => c.Email.Equals(newContact.Email));

        if (existingContact is not null)
            return Result.Success(existingContact, "Ce contact existe déjà pour cette personne.");

        _contacts.Add(newContact);

        var @event = new ContactAddedToPersonDomainEvent(Id, newContact.Id);
        base.RegisterDomainEvent(@event);

        return newContact;
    }

    /// <summary>
    /// Permet la mise à jour de l'email d'un contact
    /// </summary>
    /// <remarks>
    /// La mise à jour déclenchera un événement ContactUpdatedEvent
    /// </remarks>
    /// <param name="contactId"></param>
    /// <param name="newEmail"></param>
    /// <returns></returns>
    public Result UpdateContact(ContactId contactId, string email)
    {
        var contact = _contacts.FirstOrDefault(c => c.Id == contactId);

        if (contact is null)
            return Result.NotFound($"Le contact '{contactId.Value}' n'a pas été trouvée");

        var normalizedEmail = Email.Normalize(email);
        if (_contacts.Any(c => c.Email.Equals(normalizedEmail) && !c.Id.Equals(contactId)))
            return Result.Invalid(new ValidationError(nameof(email), "Cet email est déjà utilisé par un autre contact."));

        contact.Update(email);

        return Result.Success();
    }

    /// <summary>
    /// Supprime un contact de la personne
    /// </summary>
    /// <remarks>
    /// La suppression déclenchera un événement ContactDeletedEvent
    /// </remarks>
    /// <param name="contactId"></param>
    /// <returns></returns>
    public Result DeleteContact(ContactId contactId)
    {
        var contact = _contacts.FirstOrDefault(c => c.Id == contactId);

        if (contact is null)
            return Result.NotFound($"Le contact '{contactId.Value}' n'a pas été trouvé.");

        _contacts.Remove(contact);

        var @event = new ContactDeletedDomainEvent(contactId);
        base.RegisterDomainEvent(@event);

        return Result.Success();
    }
}