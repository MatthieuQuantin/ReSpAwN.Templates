using ModuleName.Domain.Commons;
using ModuleName.Domain.PersonAggregate;

namespace ModuleName.Infrastructure.Persistence.Configurations;

internal sealed class ContactConfiguration : IEntityTypeConfiguration<Contact>
{
    public void Configure(EntityTypeBuilder<Contact> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasConversion(
                v => v.Value,
                v => ContactId.From(v));

        builder.Property(x => x.Email)
            .HasConversion(
            v => v.Value,
            v => Email.Create(v).Value)
            .IsRequired();
    }
}