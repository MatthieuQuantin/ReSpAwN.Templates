using ModuleName.Domain.PersonAggregate;

namespace ModuleName.Infrastructure.Persistence;

internal class ModuleNameDbContext : DbContext
{
    public virtual DbSet<Person> Persons { get; set; }

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
                    });
        }

        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Constants.DBCONTEXT_SCHEMA_NAME);

        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ModuleNameDbContext).Assembly);
    }
}