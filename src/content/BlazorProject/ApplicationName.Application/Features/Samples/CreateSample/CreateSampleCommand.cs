namespace ApplicationName.Application.Features.Samples.CreateSample;

public sealed record CreateSampleCommand(string Name) : ICommand<Result<SampleResult>>;