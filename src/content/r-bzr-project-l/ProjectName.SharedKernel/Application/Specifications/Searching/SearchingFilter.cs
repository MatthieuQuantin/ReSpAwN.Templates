namespace ProjectName.SharedKernel.Application.Specifications.Searching;

public enum FilterOp { Equal, NotEqual, Contains, StartsWith, EndsWith, GreaterThan, GreaterThanOrEqual, LessThan, LessThanOrEqual }

// Un filtre simple et générique (string value pour rester simple)
public sealed record SearchingFilter(string Field, FilterOp Op, string Value);
