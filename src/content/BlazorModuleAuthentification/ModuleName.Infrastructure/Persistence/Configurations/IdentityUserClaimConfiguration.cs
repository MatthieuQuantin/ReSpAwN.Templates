using Microsoft.AspNetCore.Identity;

namespace ModuleName.Infrastructure.Persistence.Configurations;

internal sealed class IdentityUserClaimConfiguration : IEntityTypeConfiguration<IdentityUserClaim<Guid>>
{
    public void Configure(EntityTypeBuilder<IdentityUserClaim<Guid>> b) => b.ToTable("UserClaims");
}