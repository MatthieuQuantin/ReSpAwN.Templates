namespace ProjectName.Application.Features.Persons.DeleteContact;

internal sealed class DeleteContactCommandValidator : AbstractValidator<DeleteContactCommand>
{
    public DeleteContactCommandValidator()
    {
        RuleFor(x => x.PersonId).NotEmpty();
        RuleFor(x => x.ContactId).NotEmpty();
    }
}