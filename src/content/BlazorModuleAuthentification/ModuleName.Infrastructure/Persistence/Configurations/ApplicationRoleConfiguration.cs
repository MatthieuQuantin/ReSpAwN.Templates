using ModuleName.Domain.ApplicationRoleAggregate;
using ModuleName.Domain.Commons;

namespace ModuleName.Infrastructure.Persistence.Configurations;

internal sealed class ApplicationRoleConfiguration : IEntityTypeConfiguration<ApplicationRole>
{
    public void Configure(EntityTypeBuilder<ApplicationRole> builder)
    {
        builder.ToTable("Roles");

        // On neutralise l'unicité par défaut sur NormalizedName
        builder.HasIndex(r => r.NormalizedName)
           .HasDatabaseName("RoleNameIndex")
           .IsUnique(false);

        // Unicité "scopée" par environnement
        builder.HasIndex(r => new { r.NormalizedName, r.Environment })
            .HasDatabaseName("UX_Roles_NormalizedName_Environment")
            .IsUnique();

        builder.ToTable(t =>
            t.HasCheckConstraint("CK_Roles_Env_Valid", $"[Environment] IN ({(int)ApplicationEnvironment.BackOffice}, {(int)ApplicationEnvironment.FrontOffice})"));
    }
}