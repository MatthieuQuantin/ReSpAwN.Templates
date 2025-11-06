using ApplicationName.Application.Interfaces.Persistence.Repositories;
using ApplicationName.Domain.SampleAggregate;

namespace ApplicationName.Application.Features.Samples.CreateSample;

internal sealed class CreateSampleHandler(IApplicationNameRepository<Sample> repository, IValidator<CreateSampleCommand> validator, ILogger<CreateSampleHandler> logger)
    : ICommandHandler<CreateSampleCommand, Result<SampleResult>>
{
    public async Task<Result<SampleResult>> Handle(CreateSampleCommand request, CancellationToken cancellationToken)
    {
        try
        {
            if (validator.Validate(request) is { IsValid: false } validationResult)
                return Result.Invalid(validationResult.AsErrors());

            var sampleCreateResult = Sample.Create(request.Name);
            if (sampleCreateResult.IsInvalid())
                return Result.Invalid(sampleCreateResult.ValidationErrors);

            var sample = sampleCreateResult.Value;
            await repository.AddAsync(sample, cancellationToken);

            return new SampleResult(sample.Id.Value);
        }
        catch (Exception exception)
        {
            const string message = "Une erreur est survenue lors de la création du sample";
            logger.LogError(exception, message);
            return Result.Error(message);
        }
    }
}