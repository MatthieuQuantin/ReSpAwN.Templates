using Microsoft.AspNetCore.Identity;

namespace ModuleName.Infrastructure.Persistence.Configurations;

internal sealed class IdentityUserLoginConfiguration : IEntityTypeConfiguration<IdentityUserLogin<Guid>>
{
    public void Configure(EntityTypeBuilder<IdentityUserLogin<Guid>> b) => b.ToTable("UserLogins");
}