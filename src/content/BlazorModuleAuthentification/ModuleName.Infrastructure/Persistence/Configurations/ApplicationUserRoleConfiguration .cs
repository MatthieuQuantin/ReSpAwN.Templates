using ModuleName.Domain.ApplicationUserAggregate;

namespace ModuleName.Infrastructure.Persistence.Configurations;

internal sealed class ApplicationUserRoleConfiguration : IEntityTypeConfiguration<ApplicationUserRole>
{
    public void Configure(EntityTypeBuilder<ApplicationUserRole> builder)
    {
        builder.ToTable("UserRoles");
    }
}