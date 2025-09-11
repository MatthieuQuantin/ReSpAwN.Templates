namespace ModuleName.Application.Features.Persons.CreatePersonContact;

internal sealed class CreatePersonContactCommandValidator : AbstractValidator<CreatePersonContactCommand>
{
    public CreatePersonContactCommandValidator()
    {
        RuleFor(x => x.PersonId).NotEmpty();
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
    }
}