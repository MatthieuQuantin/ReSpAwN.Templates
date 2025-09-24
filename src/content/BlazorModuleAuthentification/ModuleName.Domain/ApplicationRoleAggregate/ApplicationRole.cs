using Microsoft.AspNetCore.Identity;

namespace ModuleName.Domain.ApplicationRoleAggregate;

public sealed class ApplicationRole : IdentityRole<Guid>
{
    public ApplicationEnvironment Environment { get; set; }
}