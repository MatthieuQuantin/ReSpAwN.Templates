using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using ModuleName.Domain.ApplicationRoleAggregate;
using ModuleName.Domain.ApplicationUserAggregate;
using ModuleName.Domain.Commons;

namespace ModuleName.Infrastructure.Persistence;

internal class ModuleNameDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid, IdentityUserClaim<Guid>, ApplicationUserRole, IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
{
    public ModuleNameDbContext(DbContextOptions<ModuleNameDbContext> options) : base(options)
    { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // If the options have not been configured, use the default SQL Server connection string.
        // This allows for flexibility in how the DbContext is configured, such as in tests or different environments.
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder
                .UseSqlServer(
                    $"Name=ConnectionStrings:{Constants.CONNECTION_STRING_NAME}",
                    sqlServerOptionsBuilder =>
                    {
                        sqlServerOptionsBuilder.MigrationsHistoryTable(Constants.DBCONTEXT_MIGRATIONS_HISTORY_TABLE_NAME, Constants.DBCONTEXT_SCHEMA_NAME);
                    })
                .UseSeeding((context, didMigrate) =>
                {
                    if (!IsAtLatestMigration(context))
                        return;

                    var dbContext = (ModuleNameDbContext)context;

                    var SuperAdminRoleId = dbContext.Roles.FirstOrDefault(r => r.NormalizedName == "SUPERADMIN")?.Id ?? Guid.NewGuid();

                    // Création du rôle SuperAdmin s'il n'existe pas
                    if (!dbContext.Set<ApplicationRole>().Any(r => r.NormalizedName == "SUPERADMIN"))
                    {
                        dbContext.Add(new ApplicationRole
                        {
                            Id = SuperAdminRoleId,
                            Name = "SuperAdmin",
                            NormalizedName = "SUPERADMIN",
                            ConcurrencyStamp = Guid.NewGuid().ToString("N"),
                            Environment = ApplicationEnvironment.BackOffice
                        });

                        dbContext.SaveChanges();
                    }

                    var AdminUserId = dbContext.Users.FirstOrDefault(u => u.NormalizedUserName == "ADMIN")?.Id ?? Guid.NewGuid();

                    // Création de l'utilisateur admin s'il n'existe pas
                    if (!dbContext.Set<ApplicationUser>().Any(u => u.NormalizedUserName == "ADMIN"))
                    {
                        var adminUser = new ApplicationUser
                        {
                            Id = AdminUserId,
                            UserName = "admin@localhost.fr",
                            NormalizedUserName = "ADMIN@LOCALHOST.FR",
                            Email = "admin@localhost.fr",
                            NormalizedEmail = "ADMIN@LOCALHOST.FR",
                            EmailConfirmed = true,
                            SecurityStamp = Guid.NewGuid().ToString("N"),
                            ConcurrencyStamp = Guid.NewGuid().ToString("N")
                        };

                        var hasher = new PasswordHasher<ApplicationUser>();
                        adminUser.PasswordHash = hasher.HashPassword(adminUser, "Admin@123"); // Use a strong password in production

                        dbContext.Add(adminUser);
                        dbContext.SaveChanges();
                    }

                    // Attribution du rôle SuperAdmin à l'utilisateur admin s'il n'est pas déjà attribué
                    if (!dbContext.Set<ApplicationUserRole>().Any(ur => ur.UserId == AdminUserId && ur.RoleId == SuperAdminRoleId))
                    {
                        dbContext.Add(new ApplicationUserRole
                        {
                            UserId = AdminUserId,
                            RoleId = SuperAdminRoleId
                        });

                        dbContext.SaveChanges();
                    }
                });
        }

        base.OnConfiguring(optionsBuilder);
    }

    /// <summary>
    /// Contrôle si la base de données est à jour au regard des dernières migrations.
    /// </summary>
    /// <param name="dbContext"></param>
    /// <returns></returns>
    private static bool IsAtLatestMigration(DbContext dbContext)
    {
        var hasMigrations = dbContext.Database.GetMigrations().Any();
        if (!hasMigrations) return true;

        return !dbContext.Database.GetPendingMigrations().Any();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Constants.DBCONTEXT_SCHEMA_NAME);

        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ModuleNameDbContext).Assembly);
    }
}