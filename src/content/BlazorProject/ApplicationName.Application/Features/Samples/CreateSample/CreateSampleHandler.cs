using ApplicationName.Application.Interfaces.Persistence.Repositories;
using ApplicationName.Domain.SampleAggregate;

namespace ApplicationName.Application.Features.Samples.CreateSample;

internal sealed class CreateSampleHandler(IApplicationNameRepository<Sample> repository, IValidator<CreateSampleCommand> validator, ILogger<CreateSampleHandler> logger)
    : ICommandHandler<CreateSampleCommand, Result<SampleResult>>
{
    private readonly IApplicationNameRepository<Sample> _repository = repository;
    private readonly IValidator<CreateSampleCommand> _validator = validator;
    private readonly ILogger<CreateSampleHandler> _logger = logger;

    public async Task<Result<SampleResult>> Handle(CreateSampleCommand request, CancellationToken cancellationToken)
    {
        try
        {
            if (_validator.Validate(request) is { IsValid: false } validationResult)
                return Result<SampleResult>.Invalid(validationResult.AsErrors());

            var sampleCreateResult = Sample.Create(request.Name);
            if (sampleCreateResult.IsInvalid())
                return Result<SampleResult>.Invalid(sampleCreateResult.ValidationErrors);

            var sample = sampleCreateResult.Value;
            await _repository.AddAsync(sample, cancellationToken);

            return new SampleResult(sample.Id.Value);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Une erreur est survenue lors de la création du sample");
            throw;
        }
    }
}