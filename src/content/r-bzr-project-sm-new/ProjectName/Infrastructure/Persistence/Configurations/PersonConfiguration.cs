using ProjectName.Domain.PersonAggregate;
using ProjectName.Domain.PersonAggregate.Contacts;

namespace ProjectName.Infrastructure.Persistence.Configurations;

internal sealed class PersonConfiguration : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasConversion(
                v => v.Value,
                v => PersonId.From(v));

        builder.Property(x => x.FirstName)
            .HasConversion(
                v => v.Value,
                v => PersonFirstName.From(v))
            .IsRequired();

        builder.Property(x => x.LastName)
            .HasConversion(
                v => v.Value,
                v => PersonLastName.From(v))
            .IsRequired();

        builder.Navigation(x => x.Contacts).Metadata.SetField("_contacts");
        builder.Navigation(x => x.Contacts).UsePropertyAccessMode(PropertyAccessMode.Field);

        builder.HasMany(typeof(Contact), "_contacts").WithOne().OnDelete(DeleteBehavior.Cascade);
    }
}
