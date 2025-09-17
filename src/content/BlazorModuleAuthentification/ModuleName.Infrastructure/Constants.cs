namespace ModuleName.Infrastructure;

internal static class Constants
{
    public const string CONNECTION_STRING_NAME = "ModuleNameConnection";// Adjust the connection string name as needed, could be the same as the main project.
    public const string DBCONTEXT_MIGRATIONS_HISTORY_TABLE_NAME = "__ModuleNameMigrationsHistory";// Adjust the migrations history table name, must be different from the main project and other modules.
    public const string DBCONTEXT_SCHEMA_NAME = "ModuleName"; // Adjust the schema name as needed.
}