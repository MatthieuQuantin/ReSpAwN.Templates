using Microsoft.AspNetCore.Identity;

namespace ModuleName.Infrastructure.Persistence.Configurations;

internal sealed class IdentityUserTokenConfiguration : IEntityTypeConfiguration<IdentityUserToken<Guid>>
{
    public void Configure(EntityTypeBuilder<IdentityUserToken<Guid>> b) => b.ToTable("UserTokens");
}