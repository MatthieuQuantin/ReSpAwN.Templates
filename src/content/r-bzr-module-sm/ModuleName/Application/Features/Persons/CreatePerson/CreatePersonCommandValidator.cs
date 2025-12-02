using ModuleName.Domain.PersonAggregate;

namespace ModuleName.Application.Features.Persons.CreatePerson;

internal sealed class CreatePersonCommandValidator : AbstractValidator<CreatePersonCommand>
{
    public CreatePersonCommandValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty().MinimumLength(PersonFirstName.MIN_LENGTH).MaximumLength(PersonFirstName.MAX_LENGTH);
        RuleFor(x => x.LastName).NotEmpty().MinimumLength(PersonLastName.MinLength).MaximumLength(PersonLastName.MaxLength);
    }
}