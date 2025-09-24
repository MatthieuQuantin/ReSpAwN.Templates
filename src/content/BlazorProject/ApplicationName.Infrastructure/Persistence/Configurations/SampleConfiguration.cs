using ApplicationName.Domain.SampleAggregate;

namespace ApplicationName.Infrastructure.Persistence.Configurations;

internal sealed class SampleConfiguration : IEntityTypeConfiguration<Sample>
{
    public void Configure(EntityTypeBuilder<Sample> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasConversion(
                v => v.Value,
                v => SampleId.From(v));
    }
}