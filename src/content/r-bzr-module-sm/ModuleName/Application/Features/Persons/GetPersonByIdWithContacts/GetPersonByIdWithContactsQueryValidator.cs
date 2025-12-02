namespace ModuleName.Application.Features.Persons.GetPersonByIdWithContacts;

internal sealed class GetPersonByIdWithContactsQueryValidator : AbstractValidator<GetPersonByIdWithContactsQuery>
{
    public GetPersonByIdWithContactsQueryValidator()
    {
        RuleFor(x => x.PersonId).NotEmpty();
    }
}