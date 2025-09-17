using Microsoft.AspNetCore.Identity;

namespace ModuleName.Infrastructure.Persistence.Configurations;

internal sealed class IdentityRoleClaimConfiguration : IEntityTypeConfiguration<IdentityRoleClaim<Guid>>
{
    public void Configure(EntityTypeBuilder<IdentityRoleClaim<Guid>> b) => b.ToTable("RoleClaims");
}