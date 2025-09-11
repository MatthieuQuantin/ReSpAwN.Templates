namespace ApplicationName.Application.Features.Samples.CreateSample;

public sealed record CreateSampleCommand() : ICommand<Result<SampleResult>>;