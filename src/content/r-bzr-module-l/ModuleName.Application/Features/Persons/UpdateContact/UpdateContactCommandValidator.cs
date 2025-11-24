namespace ModuleName.Application.Features.Persons.UpdateContact;

internal sealed class UpdateContactCommandValidator : AbstractValidator<UpdateContactCommand>
{
    public UpdateContactCommandValidator()
    {
        RuleFor(x => x.PersonId)
            .NotEmpty();

        RuleFor(x => x.ContactId)
            .NotEmpty();

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();
    }
}