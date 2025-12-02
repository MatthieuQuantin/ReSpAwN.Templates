using ModuleName.Domain.PersonAggregate;

namespace ModuleName.Application.Features.Persons.UpdatePerson;

internal sealed class UpdatePersonCommandValidator : AbstractValidator<UpdatePersonCommand>
{
    public UpdatePersonCommandValidator()
    {
        RuleFor(x => x.PersonId).NotEmpty();
        RuleFor(x => x.FirstName).NotEmpty().MinimumLength(PersonFirstName.MIN_LENGTH).MaximumLength(PersonFirstName.MAX_LENGTH);
        RuleFor(x => x.LastName).NotEmpty().MinimumLength(PersonLastName.MinLength).MaximumLength(PersonLastName.MaxLength);
    }
}