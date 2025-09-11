namespace ModuleName.Application.Features.Persons.GetPersonById;

internal sealed class GetPersonByIdQueryValidator : AbstractValidator<GetPersonByIdQuery>
{
    public GetPersonByIdQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}